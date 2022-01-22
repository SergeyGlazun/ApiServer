using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfAppClient.Models
{
    public static class ActionsApi
    {
       
        public static string GetCardInformation()
        {
            try
            {
                string url = "https://localhost:44304/api/values/all/";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string response;

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {

                    return response = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return "";
        }

        public static string GetCardSearcInformation(string naneProduct)
        {
          
            if (naneProduct != null)
            {
                

                try
                {
                    string url = "https://localhost:44304/api/values/product?nameProduct=" + naneProduct;

                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    string response;

                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {

                        return response = streamReader.ReadToEnd();
                    }
                }
                catch
                {
                    MessageBox.Show("Введите название товара для поиска");

                    return null;
                }
              

               
            }
            else
            {
                MessageBox.Show("Введите название товара для поиска");
            }

            return null;
        }

        public static ProductCart SetProductCart(ProductCart productCart, ImageSource image,string nameProduct)
        {
             HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44304/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image));
            encoder.Save(memStream);

            byte[] imageArray = memStream.ToArray();
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);

            productCart.NameProduct = nameProduct;
            productCart.Img = base64ImageRepresentation;
        

            return productCart;
        }
    }
}
