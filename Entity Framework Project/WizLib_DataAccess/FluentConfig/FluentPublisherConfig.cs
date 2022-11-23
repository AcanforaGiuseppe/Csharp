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
    public class FluentPublisherConfig : IEntityTypeConfiguration<Fluent_Publisher>
    {
        public void Configure(EntityTypeBuilder<Fluent_Publisher> modelBuilder)
        {
            // Publisher
            modelBuilder.HasKey(b => b.Publisher_Id);               // Set the Entity with a Primary Key
            modelBuilder.Property(b => b.Name).IsRequired();        // Set the property "Name" and that it's Required
            modelBuilder.Property(b => b.Location).IsRequired();    // Set the property "Location" and that it's Required
        }
    }
}