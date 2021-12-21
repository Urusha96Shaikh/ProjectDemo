using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using online_shopping_app.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace online_shopping_app.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductTypes> ProductTypes { get; set; }
        public DbSet<TagNames> TagNames { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
