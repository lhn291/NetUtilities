using NetUtilities.Applications.Interfaces;
using NetUtilities.Applications.Services;
using NetUtilities.ViewModels.Components;
using NetUtilities.ViewModels.Formatters;
using NetUtilities.ViewModels.Home;
using NetUtilities.ViewModels.Logs;
using NetUtilities.Views.Components;
using NetUtilities.Views.Formatters;
using NetUtilities.Views.Home;
using NetUtilities.Views.Logs;
using Prism.Ioc;
using NetUtilities.Views.Generators;
using NetUtilities.ViewModels.Generators;

namespace NetUtilities
{
    internal static class ServiceRegister
    {
        public static void UseDialogs(this IContainerRegistry containerRegistry)
        {

        }

        public static void UseNavigaions(this IContainerRegistry containerRegistry)
        {

            // View
            containerRegistry.RegisterForNavigation<JsonFormat, JsonFormatViewModel>();
            containerRegistry.RegisterForNavigation<LogViewerScreen, LogViewerScreenViewModel>();
            containerRegistry.RegisterForNavigation<ClickLogItemPopup, ClickLogItemPopupViewModel>();
            containerRegistry.RegisterForNavigation<TextFormat, TextFormatViewModel>();
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
            
            // Popup
            containerRegistry.RegisterForNavigation<BarcodeGenerator, BarcodeGeneratorViewModel>();

        }

        public static void UseServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IMenuService, MenuService>();
        }
    }
}
