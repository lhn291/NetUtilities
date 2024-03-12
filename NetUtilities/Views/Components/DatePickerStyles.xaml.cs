using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NetUtilities.Views.Components
{
    /// <summary>
    /// Interaction logic for DatePickerStyles.xaml
    /// </summary>
    public partial class DatePickerStyles : UserControl, INotifyPropertyChanged
    {
        private int selectedHour;
        private int selectedMinute;
        private int selectedSecond;

        public List<int> HourItems { get; set; }
        public List<int> MinuteItems { get; set; }
        public List<int> SecondItems { get; set; }


        public int SelectedHour
        {
            get { return selectedHour; }
            set
            {
                if (selectedHour != value)
                {
                    selectedHour = value;
                    OnPropertyChanged("SelectedHour");
                }
            }
        }

        public int SelectedMinute
        {
            get { return selectedMinute; }
            set
            {
                if (selectedMinute != value)
                {
                    selectedMinute = value;
                    OnPropertyChanged("SelectedMinute");
                }
            }
        }

        public int SelectedSecond
        {
            get { return selectedSecond; }
            set
            {
                if (selectedSecond != value)
                {
                    selectedSecond = value;
                    OnPropertyChanged("SelectedSecond");
                }
            }
        }

        public static readonly DependencyProperty LogDateChangedCommandProperty =
    DependencyProperty.Register(
        nameof(LogDateChangedCommand),
        typeof(DelegateCommand),
        typeof(DatePickerStyles),
        new PropertyMetadata(null));

        public DelegateCommand LogDateChangedCommand
        {
            get { return (DelegateCommand)GetValue(LogDateChangedCommandProperty); }
            set { SetValue(LogDateChangedCommandProperty, value); }
        }

        public static readonly DependencyProperty DateSelectedProperty =
    DependencyProperty.Register(
        "DateSelected",
        typeof(DateTime?),
        typeof(DatePickerStyles),
        new FrameworkPropertyMetadata(default(DateTime?), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DateTime? DateSelected
        {
            get { return (DateTime?)GetValue(DateSelectedProperty); }
            set { SetValue(DateSelectedProperty, value); }
        }

        public DatePickerStyles()
        {
            InitializeComponent();
            HourItems = Enumerable.Range(0, 24).ToList();
            MinuteItems = Enumerable.Range(0, 60).ToList();
            SecondItems = Enumerable.Range(0, 60).ToList();
            ResetTime();
            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "SelectedHour" || e.PropertyName == "SelectedMinute" || e.PropertyName == "SelectedSecond")
                {
                    TimeChange();
                }
            };
        }

        private void custom_date_picker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (custom_date_picker.SelectedDate.HasValue)
            {
                DateTime selectedDate = custom_date_picker.SelectedDate.Value;
                selectedDate = selectedDate.Date.AddHours(SelectedHour).AddMinutes(SelectedMinute).AddSeconds(SelectedSecond);
                custom_date_picker.SelectedDate = selectedDate;
                DateSelected = selectedDate;
            }
            else
            {
                DateSelected = default(DateTime?);
            }
        }

        private void clean_button_Click(object sender, RoutedEventArgs e)
        {
            custom_date_picker.SelectedDate = null;
            DateSelected = null;
            e.Source = custom_date_picker;
            ResetTime();
        }

        private void ResetTime()
        {
            SelectedHour = 0;
            SelectedMinute = 0;
            SelectedSecond = 0;
        }

        private void TimeChange()
        {
            if (custom_date_picker.SelectedDate.HasValue)
            {
                DateTime selectedDate = custom_date_picker.SelectedDate.Value;
                selectedDate = selectedDate.Date.AddHours(SelectedHour).AddMinutes(SelectedMinute).AddSeconds(SelectedSecond);
                custom_date_picker.SelectedDate = selectedDate;
            }
        }

    }
}
