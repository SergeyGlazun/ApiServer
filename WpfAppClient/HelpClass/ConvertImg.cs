using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;

namespace WpfAppClient.HelpClass
{
    static class  ConvertImg
    {
        public static Bitmap GetImg(string sproductCart)
        {
            byte[] array = Convert.FromBase64String(sproductCart);
            MemoryStream ms = new MemoryStream(array, 0,
            array.Length);
            ms.Write(array, 0, array.Length);

            return (Bitmap)Image.FromStream(ms, true);
        }

        public static BitmapSource ToWpfBitmap(Bitmap bitmap)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Bmp);
            stream.Position = 0;
            BitmapImage result = new BitmapImage();
            result.BeginInit();
            result.CacheOption = BitmapCacheOption.OnLoad;
            result.StreamSource = stream;
            result.EndInit();
            result.Freeze();
            return result;
        }
    }
}
