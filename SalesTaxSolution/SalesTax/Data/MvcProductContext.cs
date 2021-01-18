using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesTax.Models;


namespace SalesTax.Data
{
    public class MvcProductContext : DbContext
    {
        public MvcProductContext (DbContextOptions<MvcProductContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
    }
}
