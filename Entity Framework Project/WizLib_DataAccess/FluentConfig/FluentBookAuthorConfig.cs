using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace DataAccess.FluentConfig
{
    public class FluentBookAuthorConfig : IEntityTypeConfiguration<Fluent_BookAuthor>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookAuthor> modelBuilder)
        {
            // Book Author
            // N to N relation
            modelBuilder.HasKey(ba => new { ba.Author_Id, ba.Book_Id });
            // Sets the field that has relation 1 - N (Fluent_Book with Fluent_BookAuthors), with ForeignKey Book_Id
            modelBuilder
              .HasOne(b => b.Fluent_Book)
              .WithMany(b => b.Fluent_BookAuthors).HasForeignKey(b => b.Book_Id);
            modelBuilder
              .HasOne(b => b.Fluent_Author)
              .WithMany(b => b.Fluent_BookAuthors).HasForeignKey(b => b.Author_Id);
        }
    }
}