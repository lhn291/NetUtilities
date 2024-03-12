using System.Windows;

namespace NetUtilities.AttachedProperties
{
    public class TextBoxHighlighter
    {
        public static readonly DependencyProperty IsHighlightTextBoxProperty = DependencyProperty.RegisterAttached("IsHighlightTextBox",
                                                                                                            typeof(bool),
                                                                                                            typeof(TextBoxHighlighter),
                                                                                                            new PropertyMetadata(false, null, null));

        public static bool GetIsHighlightTextBox(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsHighlightTextBoxProperty);
        }
        public static void SetIsHighlightTextBox(DependencyObject obj, bool value)
        {
            obj.SetValue(IsHighlightTextBoxProperty, value);
        }
    }
}
