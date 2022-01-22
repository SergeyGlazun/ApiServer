using Microsoft.Win32;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAppClient.HelpClass;
using WpfAppClient.Models;

namespace WpfAppClient
{
    /// <summary>
    /// Логика взаимодействия для AddCard.xaml
    /// </summary>
    public partial class AddCard : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
       
        public AddCard()
        {
            InitializeComponent();


            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            textblockTime.Text = DateTime.Now.ToLongTimeString();
        }
        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                try
                {                   
                    currentImage.Source = new BitmapImage(new Uri(openFile.FileName));
                }
                catch
                {
                    MessageBox.Show("что то пошло не так");
                }
            }
        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            try
            {
               
                ProductCart productCart = new ProductCart();

                productCart = ActionsApi.SetProductCart(productCart, currentImage.Source, inputNameProduct.Text);

                if (inputNameProduct.Text.Length > 2 && productCart.Img != null)
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:44304/api/Values/addTask");
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json1 = new JavaScriptSerializer().Serialize(new
                        {
                            NameProduct = inputNameProduct.Text,
                            Img = productCart.Img
                        }); ;

                        streamWriter.Write(json1);
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
   

                    this.Close();
                }
                else
                {
                    MessageBox.Show("заполните данные");
                }
            }
            catch (Exception i)
            {
                MessageBox.Show(i.ToString());
            }

        }
    }
}
