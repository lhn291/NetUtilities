using Microsoft.Win32;
using NetUtilities.Domain.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;

namespace NetUtilities.ViewModels.Generators
{
    public class BarcodeGeneratorViewModel : BindableBase
    {
        private string _barcodeText = "Barcode";
        public string BarcodeText
        {
            get { return _barcodeText; }
            set
            {
                if (_barcodeText != value)
                {
                    _barcodeText = value;
                    UpdateBarcodeImageExecute();
                }
            }
        }

        private bool _isErrorMessage = false;
        public bool IsErrorMessage
        {
            get { return _isErrorMessage; }
            set
            { SetProperty(ref _isErrorMessage, value); }
        }

        private string _messageError = null;
        public string MessageError
        {
            get { return _messageError; }
            set
            { SetProperty(ref _messageError, value); }
        }

        private string _barcodeImagePath;
        public string BarcodeImagePath
        {
            get { return _barcodeImagePath; }
            set
            { SetProperty(ref _barcodeImagePath, value); }
        }

        private BarcodeFormat _qrCodeType = BarcodeFormat.CODE_128;
        public BarcodeFormat QRCodeType
        {
            get { return _qrCodeType; }
            set
            { SetProperty(ref _qrCodeType, value); }
        }

        private Dictionary<string, BarcodeFormat> _qrCodeTypes = new Dictionary<string, BarcodeFormat>
        {
            { "QRCode", BarcodeFormat.QR_CODE },
            { "Code 128", BarcodeFormat.CODE_128 },
            { "Code 39", BarcodeFormat.CODE_39 },
            { "EAN", BarcodeFormat.EAN_13 }
        };

        public Dictionary<string, BarcodeFormat> QRCodeTypes
        {
            get { return _qrCodeTypes; }
            set { SetProperty(ref _qrCodeTypes, value); }
        }

        public DelegateCommand ScanFromFileCommand { get; set; }
        public DelegateCommand CopyImageToClipboardCommand { get; set; }
        public DelegateCommand UpdateBarcodeImageCommand { get; set; }
        public DelegateCommand<DragEventArgs> DropImageCommand { get; private set; }

        public BarcodeGeneratorViewModel()
        {
            ScanFromFileCommand = new DelegateCommand(ScanFromFileExecute);
            CopyImageToClipboardCommand = new DelegateCommand(CopyImageToClipboardExecute);
            UpdateBarcodeImageCommand = new DelegateCommand(UpdateBarcodeImageExecute);
            DropImageCommand = new DelegateCommand<DragEventArgs>(OnImageDropped);
            UpdateBarcodeImageExecute();
        }

        private void ScanFromFileExecute()
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Title = "Select an image file",
                    Filter = "PNG and JPG Images|*.png;*.jpg;*.gif;",
                    Multiselect = false
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    string selectedFilePath = openFileDialog.FileName;

                    if (!selectedFilePath.IsFileImageValid())
                    {
                        MessageBox.Show("Invalid image format. Please select a PNG or JPG image.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    using (Bitmap bitmap = new Bitmap(selectedFilePath))
                    {
                        var barcodeReader = new BarcodeReader
                        {
                            AutoRotate = true,
                            Options = new DecodingOptions { TryHarder = true },
                        };
                        var result = barcodeReader.Decode(bitmap);

                        if (result != null)
                        {
                            BarcodeImagePath = selectedFilePath;
                            QRCodeType = result.BarcodeFormat;
                            BarcodeText = result.Text;
                        }
                        else
                        {
                            MessageBox.Show("Can't recognize barcode on image.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageError = ex.Message;
            }
        }

        private void CopyImageToClipboardExecute()
        {
            try
            {
                if (!string.IsNullOrEmpty(BarcodeImagePath))
                {
                    byte[] imageBytes = File.ReadAllBytes(BarcodeImagePath);
                    BitmapSource bitmapSource = LoadImage(imageBytes);

                    if (bitmapSource != null)
                    {
                        Clipboard.SetImage(bitmapSource);
                    }

                    MessageBox.Show("Barcode is coppy", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageError = ex.Message;
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnImageDropped(DragEventArgs e)
        {
            try 
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                    if (files != null && files.Length > 0)
                    {
                        string selectedFilePath = files[0];
                        if (!selectedFilePath.IsFileImageValid())
                        {
                            MessageBox.Show("Invalid image format. Please select a PNG, GIF or JPG image.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        using (Bitmap bitmap = new Bitmap(selectedFilePath))
                        {
                            var barcodeReader = new BarcodeReader
                            {
                                AutoRotate = true,
                                Options = new DecodingOptions { TryHarder = true },
                            };
                            var result = barcodeReader.Decode(bitmap);

                            if (result != null)
                            {
                                BarcodeImagePath = selectedFilePath;
                                QRCodeType = result.BarcodeFormat;
                                BarcodeText = result.Text;
                            }
                            else
                            {
                                MessageBox.Show("Can't recognize barcode on image.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageError = ex.Message;
            } 
        }

        private BitmapSource LoadImage(byte[] imageBytes)
        {
            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                return image;
            }
        }

        private void UpdateBarcodeImageExecute()
        {
            try
            {
                if (string.IsNullOrEmpty(BarcodeText))
                {
                    BarcodeImagePath = "pack://siteoforigin:,,,/Resources/Images/null_image.png";
                    IsErrorMessage = false;
                    MessageError = null;
                    return;
                }
                IsErrorMessage = false;
                MessageError = null;
                BarcodeFormat barcodeFormat = QRCodeType;
                BarcodeWriter barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = barcodeFormat;
                barcodeWriter.Options = new QrCodeEncodingOptions
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = 700,
                    Height = 300,
                };
                Bitmap barcodeBitmap = barcodeWriter.Write(BarcodeText);
                using (MemoryStream memory = new MemoryStream())
                {
                    barcodeBitmap.Save(memory, ImageFormat.Png);
                    memory.Position = 0;
                    byte[] imageData = memory.ToArray();
                    string tempFileName = Path.GetTempFileName();
                    File.WriteAllBytes(tempFileName, imageData);
                    BarcodeImagePath = tempFileName; 
                    barcodeBitmap.Dispose();
                }
            }
            catch (Exception ex)
            {
                IsErrorMessage = true;
                MessageError = ex.Message;
            }
        }

    }
}
