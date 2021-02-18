using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksWebitel.Model;

namespace TasksWebitel.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }
       
        public DbSet<Product> Products { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                        .HasMany(x => x.Products)
                        .WithMany(x => x.Orders);
        }
        
    }
}
