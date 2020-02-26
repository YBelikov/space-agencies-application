using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpaceAgenciesDatabaseApp
{
    public partial class SpaceAgenciesDbContext : DbContext
    {
        public SpaceAgenciesDbContext()
        {
        }

        public SpaceAgenciesDbContext(DbContextOptions<SpaceAgenciesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrators> Administrators { get; set; }
        public virtual DbSet<AgenciesPrograms> AgenciesPrograms { get; set; }
        public virtual DbSet<Astronauts> Astronauts { get; set; }
        public virtual DbSet<Countires> Countires { get; set; }
        public virtual DbSet<Crews> Crews { get; set; }
        public virtual DbSet<CrewsAstronauts> CrewsAstronauts { get; set; }
        public virtual DbSet<Missions> Missions { get; set; }
        public virtual DbSet<ProgramsStates> ProgramsStates { get; set; }
        public virtual DbSet<SpaceAgencies> SpaceAgencies { get; set; }
        public virtual DbSet<SpacePrograms> SpacePrograms { get; set; }
        public virtual DbSet<States> States { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-CGTMATU\\SQLEXPRESS;Database=SpaceAgenciesDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrators>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.SpaceAgencyId).HasColumnName("SpaceAgencyID");

                entity.Property(e => e.Surname).IsRequired();

                entity.HasOne(d => d.SpaceAgency)
                    .WithMany(p => p.Administrators)
                    .HasForeignKey(d => d.SpaceAgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Administrators_SpaceAgencies");
            });

            modelBuilder.Entity<AgenciesPrograms>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SpaceAgencyId).HasColumnName("SpaceAgencyID");

                entity.Property(e => e.SpaceProgramId).HasColumnName("SpaceProgramID");

                entity.HasOne(d => d.SpaceAgency)
                    .WithMany(p => p.AgenciesPrograms)
                    .HasForeignKey(d => d.SpaceAgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgenciesPrograms_SpaceAgencies");

                entity.HasOne(d => d.SpaceProgram)
                    .WithMany(p => p.AgenciesPrograms)
                    .HasForeignKey(d => d.SpaceProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgenciesPrograms_SpacePrograms");
            });

            modelBuilder.Entity<Astronauts>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CrewId).HasColumnName("CrewID");

                entity.Property(e => e.Duty)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Surname).IsRequired();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Astronauts)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Astronauts_Countires1");
            });

            modelBuilder.Entity<Countires>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gdp)
                    .HasColumnName("GDP")
                    .HasColumnType("money");
            });

            modelBuilder.Entity<Crews>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MissionId).HasColumnName("MissionID");

                entity.HasOne(d => d.Mission)
                    .WithMany(p => p.Crews)
                    .HasForeignKey(d => d.MissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Crews_Missions");
            });

            modelBuilder.Entity<CrewsAstronauts>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AstronautId).HasColumnName("AstronautID");

                entity.Property(e => e.CrewId).HasColumnName("CrewID");

                entity.HasOne(d => d.Astronaut)
                    .WithMany(p => p.CrewsAstronauts)
                    .HasForeignKey(d => d.AstronautId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CrewsAstronauts_Astronauts");

                entity.HasOne(d => d.Crew)
                    .WithMany(p => p.CrewsAstronauts)
                    .HasForeignKey(d => d.CrewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CrewsAstronauts_Crews");
            });

            modelBuilder.Entity<Missions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Missions)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Missions_SpacePrograms");
            });

            modelBuilder.Entity<ProgramsStates>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.ProgramsStates)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramsStates_SpacePrograms");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.ProgramsStates)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProgramsStates_States");
            });

            modelBuilder.Entity<SpaceAgencies>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateOfEstablishment).HasColumnType("date");

                entity.Property(e => e.HeadquarterCountryId).HasColumnName("HeadquarterCountryID");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.HeadquarterCountry)
                    .WithMany(p => p.SpaceAgencies)
                    .HasForeignKey(d => d.HeadquarterCountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpaceAgencies_Countires");
            });

            modelBuilder.Entity<SpacePrograms>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Target)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
