using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("TaskItems")]
    public class TaskItem
    {
        [Key]
        public Guid TaskId { get; set; }
        public int DayOfWeek { get; set; }
        public string Description { get; set; }
        public bool Complete { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
