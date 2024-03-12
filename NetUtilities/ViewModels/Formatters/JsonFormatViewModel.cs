using NetUtilities.Domain.Enums;
using NetUtilities.Domain.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace NetUtilities.ViewModels.Formatters
{
    public class JsonFormatViewModel : BindableBase
    {

        private string inputData;
        public string InputData
        {
            get { return inputData; }
            set { SetProperty(ref inputData, value); }
        }

        private string outputData;
        public string OutputData
        {
            get { return outputData; }
            set { SetProperty(ref outputData, value); }
        }

        private string _spaceTab;
        public string SpaceTab
        {
            get { return _spaceTab; }
            set { SetProperty(ref _spaceTab, value); }
        }

        private ObservableCollection<string> _spaceTabs = new ObservableCollection<string>();
        public ObservableCollection<string> SpaceTabs
        {
            get { return _spaceTabs; }
            set { SetProperty(ref _spaceTabs, value); }
        }

        public DelegateCommand FormatJSONCommand { get; set; }
        public DelegateCommand SpaceTabsCommand { get; set; }

        public JsonFormatViewModel()
        {
            FormatJSONCommand = new DelegateCommand(FormatExecute);
            SpaceTabsCommand = new DelegateCommand(SpaceTabsExecute);
            SpaceTabs = new ObservableCollection<string>(Enum.GetValues(typeof(SpaceTabEnum))
               .Cast<SpaceTabEnum>()
               .Select(e => e.GetDisplayName()));
            SpaceTab = "2 Space Tab";
        }

        public void FormatExecute()
        {
            if (!string.IsNullOrEmpty(InputData))
            {
                if (InputData.TryConvertToJSON(out string convertedContent))
                {
                    OutputData = InputData.Replace(InputData, convertedContent);
                }
                else
                {
                    MessageBox.Show("The data is invalid", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("The data is invalid", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SpaceTabsExecute()
        {
            if (SpaceTab != null)
            {
                var enumValue = Enum.GetValues(typeof(SpaceTabEnum))
                    .Cast<SpaceTabEnum>()
                    .FirstOrDefault(e => e.GetDisplayName() == SpaceTab);

                if (InputData != null)
                {
                    if (enumValue != default)
                    {
                        if (enumValue == SpaceTabEnum.ThreeSpaceTab)
                        {
                            if (OutputData.TabJson(out string convertedContent, 4))
                            {
                                OutputData = convertedContent;
                            }
                            else
                            {
                                MessageBox.Show("Cannot convert selected text to JSON", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else if (enumValue == SpaceTabEnum.FourSpaceTab)
                        {
                            if (OutputData.TabJson(out string convertedContent, 6))
                            {
                                OutputData = convertedContent;
                            }
                            else
                            {
                                MessageBox.Show("Cannot convert selected text to JSON", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    else
                    {
                        FormatExecute();
                    }
                }
            }
            else
            {
                MessageBox.Show("OutputData or SpaceTab is null", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}