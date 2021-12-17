using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _1._1.DAL.Model;

namespace _1._1.DAL.Configuration
{
    class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("purchase");

            builder.Property(purchase => purchase.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(purchase => purchase.CarId)
                .HasColumnName("card_id");

            builder.Property(purchase => purchase.Sum)
                .HasColumnName("purchase_sum")
                .HasColumnType("decimal(18,0)");

            builder
                .HasOne<PersonalCard>(purchase => purchase.Card)
                .WithMany(card => card.Purchases)
                .HasForeignKey(purchase => purchase.CarId);
        }
    }
}
