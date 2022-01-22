using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfAppClient.HelpClass;
using WpfAppClient.Models;

namespace WpfAppClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int i=1;
     
        List<ProductCart> sproductCart;
        AddCard AddCard;
        ApdateCard apdateCard;
        string json;
        public MainWindow()
        {
            InitializeComponent();
           
          
            json = ActionsApi.GetCardInformation();

            sproductCart = new List<ProductCart>();
            sproductCart = JsonConvert.DeserializeObject<List<ProductCart>>(json);

            if(sproductCart != null)
            {
                nameProduct.Content = sproductCart[0].NameProduct;
                picHolder.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[0].Img));

                picHolder1.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[1].Img));
                nameProduct1.Content = sproductCart[0 + 1].NameProduct;
            }
            else
            {
               
                nameProduct.Content = "Нет подключения к серверу";
                picHolder.Source = new BitmapImage(new Uri(@"img/imgPng.png", UriKind.RelativeOrAbsolute));

                picHolder1.Source = new BitmapImage(new Uri(@"img/imgPng.png", UriKind.RelativeOrAbsolute));
                nameProduct1.Content = "Нет подключения к серверу";

                btnBack.IsEnabled = false;
                btnNext.IsEnabled = false;

                startSerch.IsEnabled = false;
                btnSort.IsEnabled = false;

                btnAdd.IsEnabled = false;
                btnApdate.IsEnabled = false;

            }

        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            i--;
            btnNext.IsEnabled = true;
            if (i <=0)
            {
                i = 0;
            }
            nameProduct.Content = sproductCart[i].NameProduct;
            picHolder.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[i].Img));

            nameProduct1.Content = sproductCart[i+1].NameProduct;
            picHolder1.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[i+1].Img));
        }

        private void GoNext(object sender, RoutedEventArgs e)
        {

            i++;

            if (i >= sproductCart.Count)
            {

                i = sproductCart.Count - 1;

            }

            if(i+1!= sproductCart.Count)
            {
                nameProduct.Content = sproductCart[i].NameProduct;
                picHolder.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[i].Img));

                nameProduct1.Content = sproductCart[i + 1].NameProduct;
                picHolder1.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[i + 1].Img));
            }
            else
            {
                nameProduct.Content = sproductCart[i-1].NameProduct;
                picHolder.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[i-1].Img));

                nameProduct1.Content = sproductCart[i].NameProduct;
                picHolder1.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[i].Img));
                --i;
            }
                
       
        }

        private void SerhProduct(object sender, RoutedEventArgs e)
        {
            

            try
            {
                string json = ActionsApi.GetCardSearcInformation(inputName.Text);

                List<ProductCart> sproductCart = new List<ProductCart>();

                sproductCart = JsonConvert.DeserializeObject<List<ProductCart>>(json);

                if (sproductCart.Count != 0)
                {

                    nameProduct.Content = sproductCart[0].NameProduct;
                    picHolder.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[0].Img));

                   
                }
                else
                {
                    MessageBox.Show("Данного товара не найдена");

                }
            }
            catch
            {
                MessageBox.Show("Данного товара не найдена");
            }
           
           
        }

        private void addCard(object sender, RoutedEventArgs e)
        {

            AddCard = new AddCard();

            AddCard.Show();
           
        }

        private void update(object sender, RoutedEventArgs e)
        {
            json = ActionsApi.GetCardInformation();

            sproductCart = new List<ProductCart>();
            sproductCart = JsonConvert.DeserializeObject<List<ProductCart>>(json);

            if (sproductCart != null)
            {
                nameProduct.Content = sproductCart[0].NameProduct;
                picHolder.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[0].Img));

                picHolder1.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[1].Img));
                nameProduct1.Content = sproductCart[0 + 1].NameProduct;

                btnBack.IsEnabled = true;
                btnNext.IsEnabled = true;

                startSerch.IsEnabled = true;
                btnSort.IsEnabled = true;

                btnAdd.IsEnabled = true;
                btnApdate.IsEnabled = true;


                MessageBox.Show("Данные обновленны");
            }
            else
            {

                nameProduct.Content = "Нет подключения к серверу";
                picHolder.Source = new BitmapImage(new Uri(@"img/imgPng.png", UriKind.RelativeOrAbsolute));

                picHolder1.Source = new BitmapImage(new Uri(@"img/imgPng.png", UriKind.RelativeOrAbsolute));
                nameProduct1.Content = "Нет подключения к серверу";

                MessageBox.Show("сервер не доступен");
            }

        }

        private void updateCard(object sender, RoutedEventArgs e)
        {
            apdateCard = new ApdateCard(i, sproductCart);
            apdateCard.Show();
        }

        private void sort(object sender, RoutedEventArgs e)
        {
          
            sproductCart = sproductCart.OrderBy(obj => obj.NameProduct).ToList();
            int i = 0;

            nameProduct.Content = sproductCart[0].NameProduct;
            picHolder.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[0].Img));

            nameProduct1.Content = sproductCart[1].NameProduct;
            picHolder1.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(sproductCart[1].Img));
        }
    }
}
