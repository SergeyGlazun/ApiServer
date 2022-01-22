using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationAPI.Model.DB
{
    public class DbInitializer
    {
        public static async Task InitializeCard(CardsDB card)
        {
            if (card.Cards.Count() == 0)
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(@"img/bosch.jpg");
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                card.Cards.Add(
                      new ProductCart
                      {
                          NameProduct = "пылесос",
                          Img = base64ImageRepresentation

                      });
                imageArray = System.IO.File.ReadAllBytes(@"img/Chair.png");
                base64ImageRepresentation = Convert.ToBase64String(imageArray);
                card.Cards.Add(
                     new ProductCart
                     {
                         NameProduct = "стул",
                         Img = base64ImageRepresentation

                     });
            }
          
            card.SaveChanges();
        }
    }
}
