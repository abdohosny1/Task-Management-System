using MyTask_Management_System.Core.Helper;
using System.ComponentModel.DataAnnotations;

namespace MyTask_Management_System.Core.Model
{
    public class TaskModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters.")]
        public string Title { get; set; }

        public string Description { get; set; }

        // Apply the custom validation here for TaskStatus
        [Required(ErrorMessage = "Status is required.")]
        [ValidTaskStatus(ErrorMessage = "Status must be a valid TaskStatus value (ToDo, InProgress, Done).")]
        public string Status { get; set; } // Enum for status

        [Required(ErrorMessage = "DueDate is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime DueDate { get; set; }
    }

}
