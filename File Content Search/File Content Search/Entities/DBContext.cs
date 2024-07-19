using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Entities
{
    public class MyContext : DbContext
    {
        public DbSet<LibraryItemLine> LibraryItemLines { get; set; }
        public DbSet<LibraryItem> LibraryItems { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Setting> Settings { get; set; }

        private static bool _created = false;

        public MyContext()
        {
            if(!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=LibraryItemsDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryItem>().ToTable("LibraryItems");
            modelBuilder.Entity<LibraryItemLine>().ToTable("LibraryItemLines");
            modelBuilder.Entity<Library>().ToTable("Libraries");
            modelBuilder.Entity<Setting>().ToTable("Settings");
        }
    }
}
