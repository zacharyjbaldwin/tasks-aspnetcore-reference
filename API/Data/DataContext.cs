using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(user => user.UserRoles)
                .WithOne(role => role.User)
                .HasForeignKey(role => role.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(user => user.UserRoles)
                .WithOne(role => role.Role)
                .HasForeignKey(role => role.RoleId)
                .IsRequired();

            builder.Entity<AppUser>()
                .Property(role => role.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<AppRole>()
                .Property(role => role.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<AppUser>()
                .HasMany(user => user.TaskItems)
                .WithOne(task => task.AppUser)
                .HasForeignKey(task => task.AppUserId)
                .IsRequired();

            builder.Entity<TaskItem>()
                .Property(task => task.TaskId)
                .ValueGeneratedOnAdd();
        }
    }
}
