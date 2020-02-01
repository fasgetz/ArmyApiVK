using Awesomium.Core;
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

            //pan1.Items.Add(tabitem);

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //var myimg = ByteToImage(await DownloadImageFromWebsiteAsync("https://sun9-63.userapi.com/c5231/u64641839/a_b11f50a2.jpg?ava=1"));

            //if (myimg != null)
            //{
            //    img.Source = myimg;
            //}

            //button.IsEnabled = false;
            //webView = WebCore.CreateWebView(1920, 1080);
            //webView.Source = new Uri("http://www.google.com");
            //webView.LoadingFrameComplete += WebView_LoadingFrameComplete;
            ////webView.LoadingFrameComplete += (s, e) =>
            ////{

            ////};

            //WebCore.Run();
            //webView.Dispose();

            //button.IsEnabled = true;


            Task t = new Task(() =>
            {
                WebCore.Initialize(new WebConfig(), true);
                webView = WebCore.CreateWebView(1920, 1080, WebViewType.Window);
                webView.LoadingFrameComplete += WebView_LoadingFrameComplete;
                webView.Source = new Uri("https://my.mail.ru/mail/summer.68/video/_myvideo/37.html");
                WebCore.Run();
            });
            t.Start();


        }

        WebView webView;
        private void WebView_LoadingFrameComplete(object sender, FrameEventArgs e)
        {
            if (!e.IsMainFrame)
                return;
            BitmapSurface surface = (BitmapSurface)webView.Surface;
            surface.SaveToPNG("result.png", true);

            WebCore.Shutdown();

            MessageBox.Show("end");
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

        private void Canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            MessageBox.Show("test");
        }
    }
}
