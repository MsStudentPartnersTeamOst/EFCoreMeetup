using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace _04_BatchInsert.EF6
{
    internal class AdventureWorksDbContext : DbContext
    {
        public AdventureWorksDbContext() : base("AdventureWorksDb")
        {
            Database.SetInitializer<AdventureWorksDbContext>(null);
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }   
}
