using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _01_EFCore101
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                db.Database.EnsureCreated();

                db.Blogs.Add(new Blog { Name = $"My {DateTime.Now.Millisecond}. awesome berlin cloud computing blog", Url = $"http://myblog.com/{DateTime.Now.Millisecond}" });
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                Console.WriteLine("All blogs in database:");

                var blogs = db.Blogs.OrderBy(x => x.Url);
                foreach (var blog in blogs)
                {
                    Console.WriteLine($" - {blog.Name} {blog.Url}");
                }

                Console.ReadLine();
            }
        }

        public class BloggingContext : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Post> Posts { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=01_EFCore101;Trusted_Connection=True;");
            }
        }

        public class Blog
        {
            public int BlogId { get; set; }
            public string Url { get; set; }

            public string Name { get; set; }

            public List<Post> Posts { get; set; }
        }

        public class Post
        {
            public int PostId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }

            public int BlogId { get; set; }
            public Blog Blog { get; set; }
        }
    }
}
