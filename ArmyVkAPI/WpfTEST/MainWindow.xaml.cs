using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTEST
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var myimg = ByteToImage(await DownloadImageFromWebsiteAsync("https://sun9-63.userapi.com/c5231/u64641839/a_b11f50a2.jpg?ava=1"));

            if (myimg != null)
                img.Source = myimg;


        }

        private async Task<byte[]> DownloadImageFromWebsiteAsync(string url)
        {
            try
            {
                return await Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    WebClient w = new WebClient();
                    byte[] imageData = w.DownloadData(url);

                    WebRequest req = WebRequest.Create(url);
                    WebResponse response = req.GetResponse();
                    Stream stream = response.GetResponseStream();                    

                    return imageData;
                });

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не найдено изображение", "error");
                return null;
            }
        }

        // Метод, который конвертирует байты в изображение ImageSource
        public static ImageSource ByteToImage(byte[] imageData)
        {
            try
            {
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(imageData);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();

                ImageSource imgSrc = biImg as ImageSource;

                return imgSrc;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
