using System;
using Microsoft.EntityFrameworkCore;
using MiniMVC.Models;

namespace MiniMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    
    }
}

