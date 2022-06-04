using System.Data.Entity;
using ToDo_Application.Model;

namespace ToDo_Application
{
    class ApplicationContext : DbContext
    {
        private static ApplicationContext _instance;
        public DbSet<ToDoTask> ToDoTasks { get; set; }

        public static ApplicationContext Instance()
        {
            if (_instance == null)
            {
                _instance = new ApplicationContext();
            }

            return _instance;
        }

        private ApplicationContext() : base("DefaultConnection") { }
    }
}
