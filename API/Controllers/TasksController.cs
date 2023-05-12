using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class TasksController : BaseApiController
    {
        private readonly DataContext _context;

        public TasksController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersWithTasks()
        {
            var usersWithTasks = await _context.Users
                .Include(user => user.TaskItems)
                .Select(user => new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    TaskCount = user.TaskItems.Count(),
                    Tasks = user.TaskItems.Select(t => new
                    {
                        t.TaskId,
                        t.DayOfWeek,
                        t.Description,
                        t.Complete
                    })
                })
                .ToListAsync();

            return Ok(usersWithTasks);
        }

        [HttpPost]
        public async Task<ActionResult> AddTask(AddTaskRequestDto addTaskRequestDto)
        {
            var user = await _context.Users
                .Include(user => user.TaskItems)
                .FirstOrDefaultAsync(user => user.Id == User.GetId());

            if (user == null) return BadRequest("No user with that ID.");

            var taskItem = new TaskItem
            {
                DayOfWeek = addTaskRequestDto.DayOfWeek,
                Description = addTaskRequestDto.Description,
                Complete = false
            };

            user.TaskItems.Add(taskItem);

            await _context.SaveChangesAsync();

            return Created("Created new task item.", taskItem.TaskId);
        }
    }
}
