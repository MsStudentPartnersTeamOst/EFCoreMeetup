using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            ResetAndPrepare();

            RunTest(
                ef6Test: () =>
                {
                    using (var db = new EF6.AdventureWorksDbContext())
                    {
                        var customers = db.Customers.ToList();
                    }
                },
                
                efCoreTest: () =>
                {
                   
                }
            );

            Console.ReadLine();
        }

        private static void ResetAndPrepare()
        {
            
        }


        private static void RunTest(Action ef6Test, Action efCoreTest)
        {
            var stopwatch = new Stopwatch();
            for (int iteration = 0; iteration < 5; iteration++)
            {
                Console.WriteLine($"Iteration {iteration}");

                stopwatch.Restart();
                ef6Test();
                stopwatch.Stop();
                var ef6 = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"    - EF6:    {ef6}\r\n");

                stopwatch.Restart();
                efCoreTest();
                stopwatch.Stop();
                var efCore = stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"    - EFCore:    {efCore}\r\n");

                var improvement = (ef6 - efCore)/(double) ef6;
                Console.WriteLine($"    - Improvement:    {improvement}\r\n");
            }
        }
    }
}
