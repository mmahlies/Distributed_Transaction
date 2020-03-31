using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTran.Models
{
  public  class DBContext1 : DbContext
    {
        public DbContextOptionsBuilder dbContextOptionsBuilder;
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DBContext1(DbContextOptions<DBContext1> options) : base(options) { }
                                        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
         //   options.UseSqlServer("Data Source=.;Initial Catalog=db1;Integrated Security=True; ");
          //  dbContextOptionsBuilder = options;
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public List<Post> Posts { get; } = new List<Post>();
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
