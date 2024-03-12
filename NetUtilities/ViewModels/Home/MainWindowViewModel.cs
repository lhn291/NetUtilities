using NetUtilities.Applications.Interfaces;
using NetUtilities.Domain.Entities;
using NetUtilities.Domain.Enums;
using NetUtilities.Domain.Extensions;
using NetUtilities.ViewModels.Formatters;
using NetUtilities.Views.Formatters;
using NetUtilities.Views.Generators;
using NetUtilities.Views.Logs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace NetUtilities.ViewModels.Home
{
    public class MainWindowViewModel : BindableBase
    {
        private IRegionManager regionManager;
        private IMenuService _menuService;

        private ObservableCollection<MenuItem> _menuItems = new ObservableCollection<MenuItem>();
        public ObservableCollection<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }

        public DelegateCommand<string> MenuClickItemCommand { get; private set; }

        private Dictionary<HomeMenuType, string> MenuTypeToViewMap = new Dictionary<HomeMenuType, string>
        {
            { HomeMenuType.Converter_PHP2Json, null },
            { HomeMenuType.Converter_Number, null },
            { HomeMenuType.Converter_Color, null },
            { HomeMenuType.Formatter_Json, nameof(JsonFormat) },
            { HomeMenuType.Formatter_Text, nameof(TextFormat) },
            { HomeMenuType.Generator_FakeData, null },
            { HomeMenuType.Generator_Barcode, nameof(BarcodeGenerator) },
            { HomeMenuType.Log_Viewer, nameof(LogViewerScreen) }
        };

        public MainWindowViewModel(IRegionManager regionManager, IMenuService menuService)
        {
            this.regionManager = regionManager;
            _menuService = menuService;
            MenuItems = new ObservableCollection<MenuItem>(_menuService.GetHomeMenuItems());
            MenuClickItemCommand = new DelegateCommand<string>(MenuClickItemExecute);
        }

        private void MenuClickItemExecute(string menuItemName)
        {
            try
            {
                HomeMenuType selectedMenuType = Enum.GetValues<HomeMenuType>()
                    .FirstOrDefault(menuType => menuType.GetValueMenuEnum().Equals(menuItemName));

                if (selectedMenuType != 0)
                {
                    if (MenuTypeToViewMap.TryGetValue(selectedMenuType, out string viewName) && !string.IsNullOrEmpty(viewName))
                    {
                        regionManager.RequestNavigate(viewName);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
