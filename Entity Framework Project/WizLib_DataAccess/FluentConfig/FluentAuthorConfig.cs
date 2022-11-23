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

    public class FluentAuthorConfig : IEntityTypeConfiguration<Fluent_Author>
    {
        public void Configure(EntityTypeBuilder<Fluent_Author> modelBuilder)
        {
            // Author
            modelBuilder.HasKey(b => b.Author_Id);
            modelBuilder.Property(b => b.FirstName).IsRequired();
            modelBuilder.Property(b => b.LastName).IsRequired();
            // Ignore is used for ignoring the property on the database -> we can use that on code, not viewable on database
            modelBuilder.Ignore(b => b.FullName);
        }
    }
}