using Microsoft.EntityFrameworkCore;

namespace _04_BatchInsert.EFCore
{
    internal class AdventureWorksDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AdventureWorksDb"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
            
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            
        //    modelBuilder.Entity<Customer>().ToTable("Sales.Customer");
        //}
    }
}