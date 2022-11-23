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
    public class FluentBookConfig : IEntityTypeConfiguration<Fluent_Book>
    {
        public void Configure(EntityTypeBuilder<Fluent_Book> modelBuilder)
        {
            // Book
            modelBuilder.HasKey(b => b.Book_Id);
            //modelBuilder.Entity<Fluent_Book>.HasKey(b => b.Book_Id); // Useless when using "IEntityTypeConfiguration<Fluent_Book>, becouse we have done a configuration for it"

            modelBuilder.Property(b => b.ISBN).IsRequired().HasMaxLength(15); // Property has MaxLength set to (number)
            modelBuilder.Property(b => b.Title).IsRequired();
            modelBuilder.Property(b => b.Price).IsRequired();

            // 1 to 1 relation between Book and BookDetail
            modelBuilder
                .HasOne(b => b.Fluent_BookDetail)
                .WithOne(b => b.Fluent_Book).HasForeignKey<Fluent_Book>("BookDetail_Id");
            // 1 to N relation between Book and Publisher
            modelBuilder
               .HasOne(b => b.Fluent_Publisher)
               .WithMany(b => b.Fluent_Book).HasForeignKey(b => b.Publisher_Id);
        }
    }
}