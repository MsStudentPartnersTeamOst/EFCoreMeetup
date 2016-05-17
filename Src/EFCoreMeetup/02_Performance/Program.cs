using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using _02_Performance.EF6;

namespace _02_Performance
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ResetAndPrepare();

            RunTest(
                ef6Test: () =>
                {
                    using (var db = new AdventureWorksDbContext())
                    {
                        var customers = db.Customers.ToList();
                    }
                },
                
                efCoreTest: () =>
                {
                    using (var db = new EFCore.AdventureWorksDbContext())
                    {
                        var customers = db.Customers.ToList();
                    }
                }
            );

            Console.WriteLine("\r\nDone.");
            Console.ReadLine();
        }

        private static void ResetAndPrepare()
        {
            Console.Write("Start warmup...");
            using (var db = new AdventureWorksDbContext())
            {
                var customers = db.Customers.FirstOrDefault();
            }

            using (var db = new EFCore.AdventureWorksDbContext())
            {
                var customers = db.Customers.FirstOrDefault();
            }
            Console.WriteLine(" done!");
            Console.WriteLine();
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
