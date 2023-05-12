using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AddTaskRequestDto
    {
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "DayOfWeek is required.")]
        [Range(0, 6, ErrorMessage = "DayOfWeek must be between 0 and 6.")]
        public int DayOfWeek { get; set; }
    }
}
