using System;
using System.Data.Entity;
using System.Windows;
using ToDo_Application.Model;
using ToDo_Application.Utils;

namespace ToDo_Application.Forms
{
    public partial class ToDoEditTaskForm : Window
    {
        private readonly ToDoTask _task;

        public ToDoEditTaskForm(ToDoTask task)
        {
            _task = task;

            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            todo_title.Text = _task.Title;
            todo_task.Text = _task.Task;
            todo_deadline.SelectedDate = _task.Deadline;
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            if (!Validator.TextField(todo_title) || !Validator.TextField(todo_task) || !Validator.DateTimeField(todo_deadline))
            {
                MessageBox.Show("All feilds required.");
                return;
            }

            string title = todo_title.Text.Trim();
            if (!_task.Title.Equals(title)) _task.Title = title;

            string task = todo_task.Text.Trim();
            if (!_task.Task.Equals(task)) _task.Task = task;

            DateTime deadline = todo_deadline.SelectedDate.Value;
            if (!_task.Deadline.Equals(deadline)) _task.Deadline = deadline;

            ApplicationContext db = ApplicationContext.Instance();
            db.Entry(_task).State = EntityState.Modified;

            if (db.SaveChanges() > 0)
                Close();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
