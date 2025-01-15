using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stockManagement.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockManagement.Infrastructure
{
    public class StockDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(this._configuration.GetConnectionString("DefaultConnection"));
            
        }
        public StockDbContext(IConfiguration config, DbContextOptions<StockDbContext> options):base(options) {
            _configuration = config;
            
        }
        public DbSet<Product> Products { get; set; }
        
    }
}
