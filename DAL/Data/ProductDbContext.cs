using BAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> products { get; set; }
        public DbSet<Coupon> coupons { get; set; }
        public DbSet<CartDetails> cartDetails { get; set; }
        public DbSet<CartHeader> cartHeader { get; set; }
        
    }
}
