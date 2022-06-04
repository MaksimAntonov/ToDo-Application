using System;
using System.Data.Entity;
using System.Windows;
using ToDo_Application.Model;
using ToDo_Application.Utils;

namespace ToDo_Application.Forms
{
    public partial class ToDoNewTaskForm : Window
    {
        public ToDoNewTaskForm()
        {
            InitializeComponent();
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            if (!Validator.TextField(todo_title) || !Validator.TextField(todo_task) || !Validator.DateTimeField(todo_deadline))
            {
                MessageBox.Show("All feilds required.");
                return;
            }

            string title = todo_title.Text.Trim();
            string task = todo_task.Text.Trim();
            DateTime deadline = todo_deadline.SelectedDate.Value;

            ApplicationContext db = ApplicationContext.Instance();
            ToDoTask new_task = new ToDoTask(title, task, deadline);

            db.ToDoTasks.Add(new_task);

            if (db.SaveChanges() > 0)
            {
                db.ToDoTasks.Load();
                Close();
            }
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
