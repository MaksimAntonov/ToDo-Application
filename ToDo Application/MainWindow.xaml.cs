using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using ToDo_Application.Forms;
using ToDo_Application.Model;

namespace ToDo_Application
{
    public partial class MainWindow : Window
    {
        private readonly ApplicationContext _db;
        private SortDescription _sortDescription;

        public MainWindow()
        {
            InitializeComponent();

            _db = ApplicationContext.Instance();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            sortTasks.SelectionChanged += OnSortTasksSelectionChanged;

            LoadToDoTasks();
        }

        private void LoadToDoTasks()
        {
            _db.ToDoTasks.Load();
            BindingList<ToDoTask> tasks = _db.ToDoTasks.Local.ToBindingList();
            tasks.ListChanged += OnTasksListChanged;

            todoData.ItemsSource = tasks;

            NoToDoDataMessageVisibilityUpdate();
        }

        private void OnTasksListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged)
            {
                SortTasksList();
            }

            NoToDoDataMessageVisibilityUpdate();
        }

        private void OnSortTasksSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int tagValue = 0;

            if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem comboBoxItem)
            {
                tagValue = Convert.ToInt32(comboBoxItem.Tag);
            }

            switch (tagValue)
            {
                case 1:
                    SetSortTasks("Deadline", ListSortDirection.Ascending);
                    break;
                case 2:
                    SetSortTasks("Deadline", ListSortDirection.Descending);
                    break;
                case 3:
                    SetSortTasks("Title", ListSortDirection.Ascending);
                    break;
                case 4:
                    SetSortTasks("Title", ListSortDirection.Descending);
                    break;
                default:
                    break;
            }
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            ToDoNewTaskForm taskForm = new ToDoNewTaskForm();
            taskForm.ShowDialog();
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            if (e.Source is Button button && button.DataContext is ToDoTask task)
            {
                ToDoEditTaskForm form = new ToDoEditTaskForm(task);
                form.Show();
            }
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            if (e.Source is Button button && button.DataContext is ToDoTask task)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Confirm deleting task",
                                                                    "Delete task?",
                                                                    MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _db.ToDoTasks.Remove(task);
                    _db.SaveChanges();
                }
            }
        }

        private void SetSortTasks(string sortField, ListSortDirection sortDirection)
        {
            _sortDescription = new SortDescription(sortField, sortDirection);
            SortTasksList();
        }

        private void SortTasksList()
        {
            if (_sortDescription == null || _sortDescription.PropertyName == null) return;

            todoData.Items.SortDescriptions.Clear();
            todoData.Items.SortDescriptions.Add(_sortDescription);
        }

        private void NoToDoDataMessageVisibilityUpdate()
        {
            if (todoData.Items.Count > 0)
                noToDoDataMessage.Visibility = Visibility.Hidden;
            else
                noToDoDataMessage.Visibility = Visibility.Visible;
        }
    }
}
