using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace _02_Performance.EF6
{
    class AdventureWorksDbContext : DbContext
    {
        public AdventureWorksDbContext() : base("AdventureWorksDb")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("[Sales].[Customer]");
        }
    }
}
