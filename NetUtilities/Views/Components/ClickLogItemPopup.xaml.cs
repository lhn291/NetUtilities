using NetUtilities.ViewModels.Components;
using System.Windows.Controls;
using System.Windows.Input;

namespace NetUtilities.Views.Components
{
    /// <summary>
    /// Interaction logic for ClickLogItemPopup.xaml
    /// </summary>
    public partial class ClickLogItemPopup : UserControl
    {
        public ClickLogItemPopup()
        {
            InitializeComponent();
        }

        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (DataContext is ClickLogItemPopupViewModel viewModel)
            {
                if (sender is TextBox textBox)
                {
                    viewModel.SelectedText = textBox.SelectedText;
                }
            }
        }

    }
}
