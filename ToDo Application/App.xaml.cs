using System;
using System.Windows;
using Prism.Ioc;
using ToDo_Application.Views;
using ToDo_Application.Dialogs.Views;

namespace ToDo_Application
{
   public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ToDoContext>();

            containerRegistry.RegisterForNavigation<ToDoContent>();

            containerRegistry.RegisterDialog<AddEditToDoTask>();
        }
    }
}
