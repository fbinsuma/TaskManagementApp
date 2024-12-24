using System;

namespace TaskManagementApp.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }          // Unique identifier for the task
        public string Title { get; set; }    // Title of the task
        public string Description { get; set; }  // Description of the task
        public DateTime DueDate { get; set; }  // Due date of the task
        public bool IsCompleted { get; set; }  // Whether the task is completed or not
    }
}
