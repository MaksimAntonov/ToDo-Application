using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

using ToDo_Application.Enums;
using ToDo_Application.Views;

namespace ToDo_Application.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        #region Fields
        private readonly IRegionManager _regionManager;
        private string _title = "ToDo Application";
        #endregion

        #region Properties
        public string Title { get => _title; set => SetProperty(ref _title, value); }
        #endregion

        #region Commands
        private DelegateCommand _onLoadedEvent;
        public ICommand OnLoadedEvent => _onLoadedEvent ??= new DelegateCommand(PerformOnLoadedEvent);
        #endregion

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        #region Methods
        private void PerformOnLoadedEvent()
        {
            _regionManager.RequestNavigate(ShellRegions.Content, nameof(ToDoContent));
        }
        #endregion
    }
}
