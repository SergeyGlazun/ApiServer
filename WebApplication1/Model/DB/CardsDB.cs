using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationAPI.Model
{
    public class CardsDB:DbContext
    {
        public CardsDB(DbContextOptions<CardsDB> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ProductCart> Cards { get; set; }
    }
}
