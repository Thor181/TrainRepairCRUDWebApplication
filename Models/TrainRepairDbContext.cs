using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrainRepairCRUDWebApplication.Models
{
    public partial class TrainRepairDbContext : DbContext
    {
        public TrainRepairDbContext()
        {
        }

        public TrainRepairDbContext(DbContextOptions<TrainRepairDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brigade> Brigades { get; set; } = null!;
        public virtual DbSet<BrigadeName> BrigadeNames { get; set; } = null!;
        public virtual DbSet<Directorate> Directorates { get; set; } = null!;
        public virtual DbSet<Railway> Railways { get; set; } = null!;
        public virtual DbSet<RailwayCarriage> RailwayCarriages { get; set; } = null!;
        public virtual DbSet<Repair> Repairs { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Salary> Salaries { get; set; } = null!;
        public virtual DbSet<TypeCarriage> TypeCarriages { get; set; } = null!;
        public virtual DbSet<TypeRepair> TypeRepairs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Worker> Workers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Service.Config.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brigade>(entity =>
            {

                entity.ToTable("Brigade");

                entity.Property(e => e.Id).HasColumnName("id")
                                          .UseIdentityAlwaysColumn()
                                          .HasIdentityOptions(1, 1, 1);

                entity.Property(e => e.IdBrigade).HasColumnName("idBrigade");

                entity.Property(e => e.IdWorker).HasColumnName("idWorker");

                entity.HasOne(d => d.IdBrigadeNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdBrigade)
                    .HasConstraintName("Brigade_BrigadeNames_fkey");

                entity.HasOne(d => d.IdWorkerNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdWorker)
                    .HasConstraintName("Brigade_Worker_fkey");
            });

            modelBuilder.Entity<BrigadeName>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.NameBrigade).HasColumnName("nameBrigade");
            });

            modelBuilder.Entity<Directorate>(entity =>
            {
                entity.ToTable("Directorate");

                entity.HasIndex(e => e.Id, "unique_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.Directorate1).HasColumnName("directorate");
            });

            modelBuilder.Entity<Railway>(entity =>
            {
                entity.ToTable("Railway");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.AddressExternal).HasColumnName("addressExternal");

                entity.Property(e => e.BankExternal).HasColumnName("bankExternal");

                entity.Property(e => e.External).HasColumnName("external");

                entity.Property(e => e.InnExternal).HasColumnName("innExternal");

                entity.Property(e => e.RailwayName).HasColumnName("railwayName");
            });

            modelBuilder.Entity<RailwayCarriage>(entity =>
            {
                entity.ToTable("RailwayCarriage");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.IdDirectorate).HasColumnName("idDirectorate");

                entity.Property(e => e.IdRailway).HasColumnName("idRailway");

                entity.Property(e => e.IdRepair).HasColumnName("idRepair");

                entity.Property(e => e.IdTypeCarriage).HasColumnName("idTypeCarriage");

                entity.HasOne(d => d.IdDirectorateNavigation)
                    .WithMany(p => p.RailwayCarriages)
                    .HasForeignKey(d => d.IdDirectorate)
                    .HasConstraintName("RailwayCarriage_Directorate_fkey");

                entity.HasOne(d => d.IdRailwayNavigation)
                    .WithMany(p => p.RailwayCarriages)
                    .HasForeignKey(d => d.IdRailway)
                    .HasConstraintName("RailwayCarriage_Railway_fkey");

                entity.HasOne(d => d.IdRepairNavigation)
                    .WithMany(p => p.RailwayCarriages)
                    .HasForeignKey(d => d.IdRepair)
                    .HasConstraintName("RailwayCarriage_Repair_fkey");

                entity.HasOne(d => d.IdTypeCarriageNavigation)
                    .WithMany(p => p.RailwayCarriages)
                    .HasForeignKey(d => d.IdTypeCarriage)
                    .HasConstraintName("RailwayCarriage_TypeCarriage_fkey");
            });

            modelBuilder.Entity<Repair>(entity =>
            {
                entity.ToTable("Repair");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Bonus).HasColumnName("bonus");

                entity.Property(e => e.BonusPercent).HasColumnName("bonusPercent");

                entity.Property(e => e.DateStart).HasColumnName("dateStart");

                entity.Property(e => e.DateStop).HasColumnName("dateStop");

                entity.Property(e => e.IdBrigade).HasColumnName("idBrigade");

                entity.Property(e => e.IdTypeRepair).HasColumnName("idTypeRepair");

                entity.Property(e => e.Money)
                    .HasColumnType("money")
                    .HasColumnName("money");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.HasOne(d => d.IdBrigadeNavigation)
                    .WithMany(p => p.Repairs)
                    .HasForeignKey(d => d.IdBrigade)
                    .HasConstraintName("Repair_BrigadeNames_fkey");

                entity.HasOne(d => d.IdTypeRepairNavigation)
                    .WithMany(p => p.Repairs)
                    .HasForeignKey(d => d.IdTypeRepair)
                    .HasConstraintName("Repair_TypeRepair_fkey");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Role1).HasColumnName("role");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.ToTable("Salary");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.Allowance).HasColumnName("allowance");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.IdWorker).HasColumnName("idWorker");

                entity.HasOne(d => d.IdWorkerNavigation)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.IdWorker)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Salary_Worker_fkey");
            });

            modelBuilder.Entity<TypeCarriage>(entity =>
            {
                entity.ToTable("TypeCarriage");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.TypeCarriage1).HasColumnName("typeCarriage");
            });

            modelBuilder.Entity<TypeRepair>(entity =>
            {
                entity.ToTable("TypeRepair");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.TypeRepair1).HasColumnName("typeRepair");
            });


            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.Login).HasColumnName("login");

                entity.Property(e => e.Password).HasColumnName("password");


                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("Users_roles_fkey");
            });

            modelBuilder.Entity<Worker>(entity =>
            {
                entity.ToTable("Worker");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, 0L);

                entity.Property(e => e.BankKart).HasColumnName("bankKart");

                entity.Property(e => e.BaseWorker).HasColumnName("baseWorker");

                entity.Property(e => e.IsForeman).HasColumnName("isForeman");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Patronymic).HasColumnName("patronymic");

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.Property(e => e.SpecialWorker).HasColumnName("specialWorker");

                entity.Property(e => e.Surname).HasColumnName("surname");

                entity.Property(e => e.YearWorker).HasColumnName("yearWorker");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
