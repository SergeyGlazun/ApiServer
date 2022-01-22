using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPI.Model;

namespace WebApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        CardsDB db;

        public ValuesController(CardsDB dB)
        {
            this.db = dB;
        }
      
        [HttpGet("all")]
        public IEnumerable<ProductCart> GeTasks()
        {
            return db.Cards;
        }

        
        [HttpGet("product")]
        public async Task<IActionResult> GetProduct(string nameProduct)
        {
            if (string.IsNullOrEmpty(nameProduct))
            {
                return BadRequest();
            }

            try
            {
                IQueryable<ProductCart> cardProductQuery = from item in db.Cards
                                                           where item.NameProduct == nameProduct
                                                           select new ProductCart
                                                           {
                                                               Id = item.Id,
                                                               NameProduct = item.NameProduct,
                                                               Img = item.Img
                                                           };

                var cardProduct = await cardProductQuery.FirstOrDefaultAsync();

                if (cardProduct == null)
                {
                    return NotFound();
                }

                return Ok(cardProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("addTask")]
        public async Task<ActionResult> PostProduct([FromBody] ProductCart value)
        {
            var entity = db.Cards.Add(value);

            await db.SaveChangesAsync();

            return Created($"api/values/product?nameProduct={entity.Entity.NameProduct}", entity.Entity);
        }

        [HttpPut("edit/{id}")]
        public async Task<ActionResult> ApdateCardProduct(int id,[FromBody] ProductCart productCartDto)
        {

            if (id == productCartDto.Id)
            {
                var productCart = new ProductCart
                {
                    Id = productCartDto.Id,
                    Img = productCartDto.Img,
                    NameProduct = productCartDto.NameProduct
                };

                db.Entry(productCart).State = EntityState.Modified;

                await db.SaveChangesAsync();
            }

            return Ok();

        }
    }

}
