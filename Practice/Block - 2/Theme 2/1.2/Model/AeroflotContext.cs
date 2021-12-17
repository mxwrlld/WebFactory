using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace _1._2.Model
{
    public partial class AeroflotContext : DbContext
    {
        public AeroflotContext()
        {
        }

        public AeroflotContext(DbContextOptions<AeroflotContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<PassInTrip> PassInTrips { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-089LE8I;Database=Aeroflot;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.IdComp)
                    .HasName("PK_id");

                entity.ToTable("Company");

                entity.Property(e => e.IdComp)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_comp");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<PassInTrip>(entity =>
            {
                entity.HasKey(e => new { e.TripNo, e.Date, e.IdPsg })
                    .HasName("PK_pit");

                entity.ToTable("Pass_in_trip");

                entity.Property(e => e.TripNo).HasColumnName("trip_no");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.IdPsg).HasColumnName("ID_psg");

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("place")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdPsgNavigation)
                    .WithMany(p => p.PassInTrips)
                    .HasForeignKey(d => d.IdPsg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_psg");

                entity.HasOne(d => d.TripNoNavigation)
                    .WithMany(p => p.PassInTrips)
                    .HasForeignKey(d => d.TripNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_trip");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(e => e.IdPsg)
                    .HasName("PK_ID_psg");

                entity.ToTable("Passenger");

                entity.Property(e => e.IdPsg).HasColumnName("ID_psg");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.TripNo)
                    .HasName("PK_trip_no");

                entity.ToTable("Trip");

                entity.Property(e => e.TripNo).HasColumnName("trip_no");

                entity.Property(e => e.IdComp).HasColumnName("ID_comp");

                entity.Property(e => e.Plane)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("plane")
                    .IsFixedLength(true);

                entity.Property(e => e.TimeIn).HasColumnName("time_in");

                entity.Property(e => e.TimeOut).HasColumnName("time_out");

                entity.Property(e => e.TownFrom)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("town_from")
                    .IsFixedLength(true);

                entity.Property(e => e.TownTo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("town_to")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdCompNavigation)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.IdComp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID_comp");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
