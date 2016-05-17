using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using _04_BatchInsert.EF6;

namespace _04_BatchInsert
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ResetAndPrepare();

            RunTest(
                ef6Test: () =>
                {
                    using (var db = new EF6.AdventureWorksDbContext())
                    {
                        for (int i = 0; i < 1000; i++)
                        {
                            var category = new EF6.ProductCategory { Name = $"ProductCategory_EF6_{DateTime.Now.Ticks}-{i}", ModifiedDate = DateTime.Now, rowguid = Guid.NewGuid()};
                            db.ProductCategories.Add(category);
                        }
                        db.SaveChanges();
                    }
                },
                
                efCoreTest: () =>
                {
                    using (var db = new EFCore.AdventureWorksDbContext())
                    {
                        for (int i = 1000; i < 2000; i++)
                        {
                            var category = new EFCore.ProductCategory { Name = $"ProductCategory_EFCore_{DateTime.Now.Ticks}-{i}", ModifiedDate = DateTime.Now, rowguid = Guid.NewGuid() };
                            db.ProductCategories.Add(category);
                        }
                        db.SaveChanges();
                    }
                }
            );

            Console.WriteLine("\r\nDone.");
            Console.ReadLine();
        }

        private static void ResetAndPrepare()
        {
            Console.Write("Start warmup...");
            using (var db = new EF6.AdventureWorksDbContext())
            {
                var customers = db.Customers.FirstOrDefault();
                DeleteConstraintsAndRecords(db);
            }

            using (var db = new EFCore.AdventureWorksDbContext())
            {
                var customers = db.Customers.FirstOrDefault();
            }
            Console.WriteLine(" done!");
            Console.WriteLine();
        }

        private static void DeleteConstraintsAndRecords(AdventureWorksDbContext db)
        {
            try
            {
                db.Database.ExecuteSqlCommand("ALTER TABLE [Production].[ProductSubcategory] DROP CONSTRAINT \"FK_ProductSubcategory_ProductCategory_ProductCategoryID\";");
            }
            catch (Exception)
            { }

            try
            {
                db.Database.ExecuteSqlCommand("DELETE FROM [Production].[ProductCategory];");
            }
            catch (Exception)
            { }
        }


        private static void RunTest(Action ef6Test, Action efCoreTest)
        {
            var stopwatch = new Stopwatch();
            for (int iteration = 0; iteration < 3; iteration++)
            {
                Console.WriteLine($"\r\nIteration {iteration}");

                stopwatch.Restart();
                ef6Test();
                stopwatch.Stop();
                var ef6 = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"    - EF6: {ef6} ms");

                stopwatch.Restart();
                efCoreTest();
                stopwatch.Stop();
                var efCore = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"    - EFCore: {efCore} ms");

                var improvement = (ef6 - efCore)/(double) ef6;
                Console.WriteLine($"    - Improvement:    {improvement.AsPercentageString()} %");
            }
        }

        public static string AsPercentageString(this double val)
        {
            var result = Math.Truncate(val*10000)/100;
            return result.ToString(CultureInfo.InvariantCulture);
        }
    }
}
