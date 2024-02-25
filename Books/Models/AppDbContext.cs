using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Books.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(): base("DefaultConnection")
        {
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}