using AmrockStudy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmrockStudy.Data
{
    public class ECommerceContext:DbContext
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {

        }
        public DbSet<GeneralProduct> GeneralProduct { get; set; }
        public DbSet<GlassProduct> GlassProduct { get; set; }

        public DbSet<Users> User { get; set; }

        public DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<GeneralProduct>().ToTable("GeneralProduct");
            modelBuilder.Entity<GlassProduct>().ToTable("GlassProduct");
            modelBuilder.Entity<Orders>().ToTable("Orders");
            //discrible the relationship between products and users
            modelBuilder.Entity<Orders>().HasKey(o => new { o.id, o.UserID });

        }

    }
}
