using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using WpfAppClient.HelpClass;
using WpfAppClient.Models;

namespace WpfAppClient
{
    /// <summary>
    /// Логика взаимодействия для ApdateCard.xaml
    /// </summary>
    public partial class ApdateCard : Window
    {
        int i;
        List<ProductCart> productCarts;
        static HttpClient client = new HttpClient();
        public ApdateCard(int i, List<ProductCart> productCarts)
        {
           
            InitializeComponent();

            this.i = i;

            this.productCarts = productCarts;

            currentImage.Source = ConvertImg.ToWpfBitmap(ConvertImg.GetImg(productCarts[i].Img));

            inputNameProduct.Text = productCarts[i].NameProduct;
        }

        private void MenuItem_Click_OpenApdate(object sender, RoutedEventArgs e)
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

        private async void  MenuItem_Click_SaveApdate(object sender, RoutedEventArgs e)
        {
            
            await UpdateProductAsync(ActionsApi.SetProductCart(productCarts[i], currentImage.Source, inputNameProduct.Text));

            this.Close();
        }

        static async Task<ProductCart> UpdateProductAsync(ProductCart product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"https://localhost:44304/api/Values/edit/{product.Id}", product);
            response.EnsureSuccessStatusCode();
           
            product = await response.Content.ReadAsAsync<ProductCart>();
            return product;
        }
    }
}
