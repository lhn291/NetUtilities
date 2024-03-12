using NetUtilities.Domain.Extensions;
using NetUtilities.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows;

namespace NetUtilities.ViewModels.Components
{
    public class ClickLogItemPopupViewModel : BindableBase, IDialogAware
    {
        private string title;
        private string dateLog;
        private string timeZone;
        private string level;
        private string content;
        private string ConvertContent;
        public string Title => string.Empty;
        private string selectedText;

        public string SelectedText
        {
            get { return selectedText; }
            set { SetProperty(ref selectedText, value); }
        }

        private Log myLog;
        public Log MyLog
        {
            get { return myLog; }
            set { SetProperty(ref myLog, value); }
        }

        public string DateLog
        {
            get { return dateLog; }
            set { SetProperty(ref dateLog, value); }
        }

        public string TimeZone
        {
            get { return timeZone; }
            set { SetProperty(ref timeZone, value); }
        }

        public string Level
        {
            get { return level; }
            set { SetProperty(ref level, value); }
        }

        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        public DelegateCommand ClosePopupCommand { get; set; }
        public DelegateCommand FormatJSONCommand { get; set; }
        public DelegateCommand ViewReceiptCommand { get; set; }
        public DelegateCommand ResetCommand { get; set; }

        public ClickLogItemPopupViewModel()
        {
            ClosePopupCommand = new DelegateCommand(CloseDialogExecute);
            FormatJSONCommand = new DelegateCommand(FormatExecute);
            ResetCommand = new DelegateCommand(ResetCommandExecute);
            AddLog();
        }

        private void AddLog()
        {
            if (MyLog != null)
            {
                DateLog = MyLog.DateLog.ToString();
                TimeZone = MyLog.TimeZone;
                Level = MyLog.Level.ToString();
                Content = MyLog.Content;
                ConvertContent = MyLog.Content;
            }
        }

        public void ResetCommandExecute()
        {
            Content = ConvertContent;
            SelectedText = ConvertContent;
        }

        public void FormatExecute()
        {
            if (!string.IsNullOrEmpty(SelectedText))
            {
                if (SelectedText.TryConvertToJSON(out string convertedContent))
                {
                    Content = Content.Replace(SelectedText, convertedContent);
                }
                else
                {
                    MessageBox.Show("Cannot convert selected text to JSON", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ResetCommandExecute();
                string convertedContent;
                if (Content.TryConvertToJSON(out convertedContent))
                {
                    Content = convertedContent;
                }
                else
                {
                    MessageBox.Show("Can not convert to JSON", "Notification", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CloseDialogExecute()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.TryGetValue("MyLog", out Log log))
            {
                MyLog = log;
                AddLog();
            }
            else
            {
                Content = "Handle the case where MyLog parameter is missing or has an invalid value";
            }
        }

        public virtual void RequestCloseExecute(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

    }
}
