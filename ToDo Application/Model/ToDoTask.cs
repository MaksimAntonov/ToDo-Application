using System;
using System.ComponentModel;

namespace ToDo_Application.Model
{
    public class ToDoTask : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _title, _task;
        private DateTime _deadline;

        public event PropertyChangedEventHandler PropertyChanged;

        public ToDoTask() { }

        public ToDoTask(string title, string task, DateTime deadline)
        {
            _title = title;
            _task = task;
            _deadline = deadline;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChange("Title");
            }
        }

        public string Task
        {
            get { return _task; }
            set
            {
                _task = value;
                OnPropertyChange("Task");
            }
        }

        public DateTime Deadline
        {
            get { return _deadline; }
            set
            {
                _deadline = value;
                OnPropertyChange("Deadline");
            }
        }

        protected virtual void OnPropertyChange(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
