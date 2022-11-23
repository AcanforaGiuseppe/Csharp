using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.FluentConfig;
using Models.Models;

namespace DataAccess.Data
{
    // Commands on Package Manager Console
    // Adding a migration to the database for reporting all the work done till now
    // add-migration 'migrationName'
    // Updates the database with the last migration
    // update-database
    // Updates the database with the "migrationName"
    // update-database 'migrationName'

    public class ApplicationDbContext : DbContext
    {
        // Setting up a constructor for giving him options
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        // Giving the database all the access to our Models
        public DbSet<BookDetailsFromView> BookDetailsFromViews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        // Fluent Models config
        public DbSet<Fluent_BookDetail> Fluent_BookDetails { get; set; }
        public DbSet<Fluent_Book> Fluent_Books { get; set; }
        public DbSet<Fluent_Author> Fluent_Authors { get; set; }
        public DbSet<Fluent_Publisher> fluent_Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting up category table name and column name
            modelBuilder.Entity<Category>().ToTable("tbl_category");
            modelBuilder.Entity<Category>().Property(c => c.Name).HasColumnName("CategoryName");

            // Composite key (N to N relationships)
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.Author_Id, ba.Book_Id });

            // Configurations w/ fluent API
            modelBuilder.ApplyConfiguration(new FluentBookConfig());
            modelBuilder.ApplyConfiguration(new FluentBookDetailsConfig());
            modelBuilder.ApplyConfiguration(new FluentBookAuthorConfig());
            modelBuilder.ApplyConfiguration(new FluentPublisherConfig());
            modelBuilder.ApplyConfiguration(new FluentAuthorConfig());

            // HasNoKey omits the primary key
            // The ToView method is applied to a type to specify the name of the database view or table that the type should map to
            modelBuilder.Entity<BookDetailsFromView>().HasNoKey().ToView("GetOnlyBookDetails");

        }
    }
}
