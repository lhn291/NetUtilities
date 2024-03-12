using Humanizer;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace NetUtilities.ViewModels.Formatters
{
    public class TextFormatViewModel : BindableBase
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

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        private string _replaceText;
        public string ReplaceText
        {
            get { return _replaceText; }
            set { SetProperty(ref _replaceText, value); }
        }

        private LetterCasing selectedTextStatus;
        public LetterCasing SelectedTextStatus
        {
            get { return selectedTextStatus; }
            set { SetProperty(ref selectedTextStatus, value); }
        }

        private Dictionary<string, LetterCasing> _textStatusMappings = new Dictionary<string, LetterCasing>
        {
            { "LowerCase", LetterCasing.LowerCase },
            { "UpperCase", LetterCasing.AllCaps },
            { "SentenceCase", LetterCasing.Sentence },
            { "TitleCase", LetterCasing.Title }
        };

        public Dictionary<string, LetterCasing> TextStatusMappings
        {
            get { return _textStatusMappings; }
            set { SetProperty(ref _textStatusMappings, value); }
        }

        public DelegateCommand HumanizeCommand { get; set; }
        public DelegateCommand DehumanizeCommand { get; set; }
        public DelegateCommand CamelizeCommand { get; set; }
        public DelegateCommand PascalizeCommand { get; set; }
        public DelegateCommand UnderscoreCommand { get; set; }
        public DelegateCommand ReplaceCommand { get; set; }

        public TextFormatViewModel()
        {
            HumanizeCommand = new DelegateCommand(HumanizeExecute);
            DehumanizeCommand = new DelegateCommand(DehumanizeExecute);
            CamelizeCommand = new DelegateCommand(CamelizeExecute);
            PascalizeCommand = new DelegateCommand(PascalizeExecute);
            UnderscoreCommand = new DelegateCommand(UnderscoreExecute);
            ReplaceCommand = new DelegateCommand(ReplaceExecute);
            SelectedTextStatus = LetterCasing.LowerCase;
        }

        public void HumanizeExecute()
        {
            if (!string.IsNullOrEmpty(InputData))
            {
                OutputData = InputData.Humanize(SelectedTextStatus);
            }
            else
            {
                MessageBox.Show("Input data is empty or invalid text status selected.", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DehumanizeExecute()
        {
            if (!string.IsNullOrEmpty(InputData))
            {
                OutputData = InputData.Humanize(LetterCasing.AllCaps);
            }
            else
            {
                MessageBox.Show("TInput data is empty.", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void UnderscoreExecute()
        {
            if (!string.IsNullOrEmpty(InputData))
            {
                OutputData = InputData.Underscore();
            }
            else
            {
                MessageBox.Show("Input data is empty.", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void PascalizeExecute()
        {
            if (!string.IsNullOrEmpty(InputData))
            {
                OutputData = InputData.Pascalize();
            }
            else
            {
                MessageBox.Show("Input data is empty.", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void CamelizeExecute()
        {
            if (!string.IsNullOrEmpty(InputData))
            {
                OutputData = InputData.Camelize();
            }
            else
            {
                MessageBox.Show("Input data is empty.", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ReplaceExecute()
        {
            if (!string.IsNullOrEmpty(InputData) && !string.IsNullOrEmpty(SearchText) && !string.IsNullOrEmpty(ReplaceText))
            {
                try
                {
                    string result = InputData;
                    result = Regex.Replace(result, SearchText, ReplaceText);
                    OutputData = result;
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Regular Expression:", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Input data, search, and replace fields cannot be empty.", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
