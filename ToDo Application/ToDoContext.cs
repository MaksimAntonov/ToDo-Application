using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
