using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _1._1.DAL.Model;

namespace _1._1.DAL.Configuration
{
    class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("user_profile");

            builder.Property(user => user.Id)
                .HasColumnName("user_id")
                .IsRequired();
            
            builder.Property(user => user.Email)
                .HasMaxLength(50)
                .HasColumnType("varchar")
                .HasColumnName("email")
                .IsRequired();

            builder.Property(user => user.FirstName)
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .HasColumnName("first_name");

            builder.Property(user => user.LastName)
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .HasColumnName("last_name");

            builder.Property(user => user.Birthdate)
                .HasColumnType("date")
                .HasColumnName("birthdate");

            builder
                .HasOne<PersonalCard>(user => user.PersonalCard)
                .WithOne(card => card.User)
                .HasForeignKey<PersonalCard>(card => card.Id);
        }
    }
}
