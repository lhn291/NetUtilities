using FastEnumUtility;

namespace NetUtilities.Domain.Enums
{
    public enum HomeMenuType
    {
        //Converter
        [Label("Converters", 0)]
        [Label("pack://siteoforigin:,,,/Resources/Images/convert_icon.png", 1)]
        [Label("", 2)]
        Converter = 1,
        [Label("PHP Array <> Json", 0)]
        [Label("pack://siteoforigin:,,,/Resources/Images/icons_point.png", 1)]
        [Label("Converters", 2)]
        Converter_PHP2Json = 2,
        [Label("Number", 0)]
        [Label("pack://siteoforigin:,,,/Resources/Images/icons_point.png", 1)]
        [Label("Converters", 2)]
        Converter_Number = 3,
        [Label("Color", 0)]
        [Label("pack://siteoforigin:,,,/Resources/Images/icons_point.png", 1)]
        [Label("Converters", 2)]
        Converter_Color = 4,

        //Formatter
        [Label("Formatters", 0)]
        [Label("pack://siteoforigin:,,,/Resources/Images/format_icon.png", 1)]
        [Label("", 2)]
        Formatter = 10,
        [Label("Json", 0)]
        [Label("pack://siteoforigin:,,,/Resources/Images/icons_point.png", 1)]
        [Label("Formatters", 2)]
        Formatter_Json = 11,
        [Label("Text", 0)]
        [Label("pack://siteoforigin:,,,/Resources/Images/icons_point.png", 1)]
        [Label("Formatters", 2)]
        Formatter_Text = 12,

        //Generator
        [Label("Generators", 0)]
        [Label("pack://siteoforigin:,,,/Resources/Images/generator_icon.png", 1)]
        [Label("", 2)]
        Generator = 20,
        [Label("Fake Data", 0)]
        [Label("pack://siteoforigin:,,,/Resources/Images/icons_point.png", 1)]
        [Label("Generators", 2)]
        Generator_FakeData = 21,
        [Label("Barcode", 0)]
        [Label("pack://siteoforigin:,,,/Resources/Images/icons_point.png", 1)]
        [Label("Generators", 2)]
        Generator_Barcode = 22,

        //Logs
        [Label("Logs", 0)]
        [Label("pack://siteoforigin:,,,/Resources/Images/log_icon.png", 1)]
        [Label("", 2)]
        Log = 30,
        [Label("Viewer", 0)]
        [Label("pack://siteoforigin:,,,/Resources/Images/icons_point.png", 1)]
        [Label("Logs", 2)]
        Log_Viewer = 31,
    }
}
