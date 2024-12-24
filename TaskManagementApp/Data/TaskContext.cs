using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Models;

namespace TaskManagementApp.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        // DbSet represents the collection of tasks in the database.
        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
