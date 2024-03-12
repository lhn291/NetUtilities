using NetUtilities.Views;
using NetUtilities.Views.Home;
using Prism.Ioc;
using System.Windows;

namespace NetUtilities
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.UseDialogs();
            containerRegistry.UseNavigaions();
            containerRegistry.UseServices();
        }

    }
}
