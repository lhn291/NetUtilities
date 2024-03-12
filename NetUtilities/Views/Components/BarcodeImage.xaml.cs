using Prism.Commands;
using System;
using System.Windows;
using System.Windows.Controls;
using ZXing;

namespace NetUtilities.Views.Components
{
    public partial class BarcodeImage : UserControl
    {
        public event EventHandler<string> ImageDropped;
        public static readonly DependencyProperty QRCodeTypeProperty = DependencyProperty.Register(
            "QRCodeType",
            typeof(BarcodeFormat),
            typeof(BarcodeImage),
            new FrameworkPropertyMetadata(BarcodeFormat.CODE_128, FrameworkPropertyMetadataOptions.AffectsRender, OnQRCodeTypeChanged));

        public BarcodeFormat QRCodeType
        {
            get { return (BarcodeFormat)GetValue(QRCodeTypeProperty); }
            set { SetValue(QRCodeTypeProperty, value); }
        }

        public static readonly DependencyProperty DropImageCommandProperty = DependencyProperty.Register(
            "DropImageCommand",
            typeof(DelegateCommand<DragEventArgs>),
            typeof(BarcodeImage),
            new PropertyMetadata(null));

        public DelegateCommand<DragEventArgs> DropImageCommand
        {
            get { return (DelegateCommand<DragEventArgs>)GetValue(DropImageCommandProperty); }
            set { SetValue(DropImageCommandProperty, value); }
        }


        public BarcodeImage()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.Drop += OnImageDropped;
        }

        public static readonly DependencyProperty BarcodeImagePathProperty = DependencyProperty.Register(
            "BarcodeImagePath",
            typeof(string),
            typeof(BarcodeImage),
            new FrameworkPropertyMetadata(string.Empty));

        public string BarcodeImagePath
        {
            get { return (string)GetValue(BarcodeImagePathProperty); }
            set { SetValue(BarcodeImagePathProperty, value); }
        }

        private static void OnQRCodeTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BarcodeImage barcodeImage)
            {
                barcodeImage.barcodeType.Text = barcodeImage.QRCodeType.ToString();
            }
        }

        private void OnImageDropped(object sender, DragEventArgs e)
        {
            if (DropImageCommand != null && DropImageCommand.CanExecute(e))
            {
                DropImageCommand.Execute(e);
            }
        }


    }
}
