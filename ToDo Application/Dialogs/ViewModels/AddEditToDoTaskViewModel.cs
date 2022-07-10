using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo_Application.Model;

namespace ToDo_Application.Dialogs.ViewModels
{
    public class AddEditToDoTaskViewModel : BindableBase, IDialogAware
    {
        #region Fields
        private string _title;
        private ToDoTask _toDoTask;
        #endregion

        #region Properties
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        public ToDoTask ToDoTask { get => _toDoTask; set => SetProperty(ref _toDoTask, value); }
        #endregion

        #region Commands
        private DelegateCommand<string> _closeCommand;
        public ICommand CloseCommand => _closeCommand ??= new DelegateCommand<string>(OnDialogClosed);

        public event Action<IDialogResult> RequestClose;
        public void RaiseRequestClose(IDialogResult dialogResult) => RequestClose?.Invoke(dialogResult);
        #endregion

        #region Methods
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed() { }

        public void OnDialogClosed(string isEnded)
        {
            ButtonResult result = ButtonResult.None;

            if (isEnded?.ToLower() == "true")
            {
                if (string.IsNullOrEmpty(ToDoTask.Title) || string.IsNullOrEmpty(ToDoTask.Task))
                    return;

                result = ButtonResult.OK;
            }
            else if (isEnded?.ToLower() == "false")
            {
                result = ButtonResult.Cancel;
            }

            RaiseRequestClose(new DialogResult(result, new DialogParameters() { { "ToDoTask", ToDoTask } }));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>("title");
            ToDoTask = parameters.GetValue<ToDoTask>("toDoTask");
        }
        #endregion
    }
}
