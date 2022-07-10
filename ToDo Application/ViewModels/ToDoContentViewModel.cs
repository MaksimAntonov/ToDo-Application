using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo_Application.Model;

namespace ToDo_Application.ViewModels
{
    public class ToDoContentViewModel : BindableBase
    {
        #region Fields
        private readonly ToDoContext _context;
        private readonly IDialogService _dialogService;
        private List<ToDoTask> _toDoTasks = new List<ToDoTask>();
        private int _sortTypeIndex = 0;
        #endregion

        #region Properties
        public List<ToDoTask> ToDoTasks { get => _toDoTasks; set => SetProperty(ref _toDoTasks, value); }
        public int SortTypeIndex 
        { 
            get => _sortTypeIndex;
            set 
            { 
                SetProperty(ref _sortTypeIndex, value);
                LoadTasks().Await();
            } 
        }
        #endregion

        #region Commands
        private DelegateCommand _loadTaskCommand;
        public ICommand LoadTaskCommand => _loadTaskCommand ??= new DelegateCommand(async () => await LoadTasks());

        private DelegateCommand _addTaskCommand;
        public ICommand AddTaskCommand => _addTaskCommand ??= new DelegateCommand(CreateTask);

        private DelegateCommand<ToDoTask> _editTaskCommand;
        public ICommand EditTaskCommand => _editTaskCommand ??= new DelegateCommand<ToDoTask>(UpdateTask);

        private DelegateCommand<ToDoTask> _removeTaskCommand;
        public ICommand RemoveTaskCommand => _removeTaskCommand ??= new DelegateCommand<ToDoTask>(async (ToDoTask toDoTask) => await RemoveTask(toDoTask));

        private DelegateCommand<int> _sortTasksCommand;
        public ICommand SortTasksCommand => _sortTasksCommand ??= new DelegateCommand<int>(SortTasks);
        #endregion

        public ToDoContentViewModel(ToDoContext toDoContext, IDialogService dialogService)
        {
            _context = toDoContext;
            _dialogService = dialogService;
        }

        #region Methods
        private async Task LoadTasks()
        {
            IOrderedQueryable<ToDoTask> toDoTasks;

            switch (SortTypeIndex)
            {
                case 1:
                    toDoTasks = _context.ToDoTasks.OrderBy(t => t.Deadline);
                    break;
                case 2:
                    toDoTasks = _context.ToDoTasks.OrderByDescending(t => t.Deadline);
                    break;
                case 3:
                    toDoTasks = _context.ToDoTasks.OrderBy(t => t.Title);
                    break;
                case 4:
                    toDoTasks = _context.ToDoTasks.OrderByDescending(t => t.Title);
                    break;
                default:
                    toDoTasks = _context.ToDoTasks;
                    break;
            }

            ToDoTasks = await toDoTasks.ToListAsync();
        }

        private void CreateTask()
        {
            OpenDialog("Create ToDo Task", new ToDoTask(), async r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    ToDoTask toDoTask = r.Parameters.GetValue<ToDoTask>("ToDoTask");
                    if (!ToDoTasks.Any(t => t.Id == toDoTask.Id))
                    {
                        _context.ToDoTasks.Add(toDoTask);
                        await _context.SaveChangesAsync();
                        await LoadTasks();
                    }
                }
            });
        }

        private void UpdateTask(ToDoTask toDoTask)
        {
            OpenDialog("Edit ToDo Task", toDoTask, async r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    ToDoTask toDoTask = r.Parameters.GetValue<ToDoTask>("ToDoTask");
                    if (ToDoTasks.Any(t => t.Id == toDoTask.Id))
                    {
                        await _context.SaveChangesAsync();
                    }
                }
            });
        }

        private async Task RemoveTask(ToDoTask toDoTask)
        {
            if (ToDoTasks.Any(t => t.Id == toDoTask.Id))
            {
                _context.ToDoTasks.Remove(toDoTask);
                await _context.SaveChangesAsync();
                await LoadTasks();
            }
        }

        private void OpenDialog(string title, ToDoTask toDoTask, Action<IDialogResult> callback)
        {
            DialogParameters parameters = new DialogParameters
            {
                { "title", title },
                { "toDoTask", toDoTask }
            };
            _dialogService.ShowDialog("AddEditToDoTask", parameters, callback);
        }

        private void SortTasks(int sortIndex)
        {
            
        }
        #endregion
    }
}
