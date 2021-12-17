using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _1._1.DAL.Model;

namespace _1._1.DAL.Configuration
{
    class PersonalCardConfiguration : IEntityTypeConfiguration<PersonalCard>
    {
        public void Configure(EntityTypeBuilder<PersonalCard> builder)
        {
            builder.ToTable("personal_card");

            builder.HasKey("Id");

            builder.Property(card => card.Discount)
                .HasColumnName("discount");
        }
    }
}
