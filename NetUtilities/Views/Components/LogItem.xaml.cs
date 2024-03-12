using NetUtilities.Domain.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetUtilities.Views.Components
{
    /// <summary>
    /// Interaction logic for LogItem.xaml
    /// </summary>
    public partial class LogItem : UserControl
    {
        private string ExtentContent = string.Empty;
        private bool isExtent = false;
        public LogItem()
        {
            InitializeComponent();
            Loaded += TextBlockClick_Loaded;
        }

        private void MessageTextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!isExtent)
            {
                MessageTextBlock.Text = ExtentContent;
                isExtent = true;
                buttonImageBrush.ImageSource = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Images/show_icon.png"));
            }
            else
            {
                ExtentExecute();
                isExtent = false;
                buttonImageBrush.ImageSource = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Images/un_show_icon.png"));
            }

        }

        private void TextBlockClick_Loaded(object sender, RoutedEventArgs e)
        {
            if (ExtentContent == string.Empty)
            {
                ExtentContent = MessageTextBlock.Text;
                ExtentExecute();
            }
            else if (MessageTextBlock.Text.Equals(ExtentContent))
            {
                MessageTextBlock.Text = ExtentContent;
            }
            else
            {
                MessageTextBlock.Text = ExtentContent;
                ExtentExecute();
            }
        }

        public void ExtentExecute()
        {
            if (!string.IsNullOrWhiteSpace(ExtentContent))
            {
                var content = ExtentContent.ExtendString(out bool isExtended);

                if (isExtended)
                {
                    MessageTextBlock.Text = content;
                }
                else
                {
                    MessageTextBlock.Text = content;
                    buttonImageBrush.ImageSource = null;
                    bdButton.Visibility = Visibility.Hidden;
                }
            }
        }


    }
}
