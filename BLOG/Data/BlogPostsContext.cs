using BLOG.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOG.Data
{
    public class BlogPostsContext
        : DbContext
    {
        public BlogPostsContext(DbContextOptions<BlogPostsContext> options)
            :base(options)
        {
          
        }

        public DbSet<BlogPost> BlogPost { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
