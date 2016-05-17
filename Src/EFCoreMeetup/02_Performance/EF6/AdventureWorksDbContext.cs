using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace _02_Performance.EF6
{
    internal class AdventureWorksDbContext : DbContext
    {
        public AdventureWorksDbContext() : base("AdventureWorksDb")
        {
            Database.SetInitializer<AdventureWorksDbContext>(null);
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }   
}
