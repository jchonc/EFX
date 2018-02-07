using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFX.Data.Business
{
    public partial class BusinessContext : DbContext
    {
        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=radicalogic.core.business;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Incident>(entity =>
            {
                entity.ToTable("incidents", "inc");

                entity.HasIndex(e => new { e.TenantId, e.FileId })
                    .HasName("inc_file_id_unique")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnName("created_on");

                entity.Property(e => e.DetailsJson).HasColumnName("details_json");

                entity.Property(e => e.EventDescription).HasColumnName("event_description");

                entity.Property(e => e.EventSeverity)
                    .HasColumnName("event_severity")
                    .HasMaxLength(255);

                entity.Property(e => e.FileId)
                    .HasColumnName("file_id")
                    .HasMaxLength(255);

                entity.Property(e => e.GeneralIncidentType)
                    .HasColumnName("general_incident_type")
                    .HasMaxLength(255);

                entity.Property(e => e.LastModifiedBy)
                    .HasColumnName("last_modified_by")
                    .HasMaxLength(255);

                entity.Property(e => e.LastModifiedOn).HasColumnName("last_modified_on");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.SpecificIncidentType)
                    .HasColumnName("specific_incident_type")
                    .HasMaxLength(255);

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(50);

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            });

            modelBuilder.Entity<Patients>(entity =>
            {
                entity.ToTable("patients", "common");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CountryCode)
                    .HasColumnName("country_code")
                    .HasMaxLength(50);

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(255);

                entity.Property(e => e.HomePhone)
                    .HasColumnName("home_phone")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(255);

                entity.Property(e => e.MiddleInitial)
                    .HasColumnName("middle_initial")
                    .HasMaxLength(255);

                entity.Property(e => e.Race)
                    .HasColumnName("race")
                    .HasMaxLength(50);

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasMaxLength(50);

                entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            });
        }
    }
}
