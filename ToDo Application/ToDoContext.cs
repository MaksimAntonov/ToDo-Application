using System.Data.Entity;

using ToDo_Application.Model;

namespace ToDo_Application
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDoTask> ToDoTasks { get; set; }

        public ToDoContext() : base("DefaultConnection")
        {
        }
    }
}
