using ArandaSoftTest.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArandaSoftTest.INFRASTRUCTURE.Data.Configurations
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // mapear entidades de productConfiguration
            //builder.ToTable("TablaEspaniol");
            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.CategoryId).HasColumnName("CategoryID");
            //.HasColumnName("idCampoEspaniol");

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Image)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Category)
                .WithMany(p => p.Product)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_Category");
        }
    }
}
