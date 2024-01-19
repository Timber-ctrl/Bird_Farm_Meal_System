using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Entities
{
    public partial class BirdFarmMealSystemContext : DbContext
    {
        public BirdFarmMealSystemContext()
        {
        }

        public BirdFarmMealSystemContext(DbContextOptions<BirdFarmMealSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssignStaff> AssignStaffs { get; set; } = null!;
        public virtual DbSet<Bird> Birds { get; set; } = null!;
        public virtual DbSet<Cage> Cages { get; set; } = null!;
        public virtual DbSet<DayOfWeek> DayOfWeeks { get; set; } = null!;
        public virtual DbSet<Farm> Farms { get; set; } = null!;
        public virtual DbSet<Food> Foods { get; set; } = null!;
        public virtual DbSet<FoodCategory> FoodCategories { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<MenuItem> MenuItems { get; set; } = null!;
        public virtual DbSet<Plan> Plans { get; set; } = null!;
        public virtual DbSet<PlanMenu> PlanMenus { get; set; } = null!;
        public virtual DbSet<Repeat> Repeats { get; set; } = null!;
        public virtual DbSet<Species> Species { get; set; } = null!;
        public virtual DbSet<Stage> Stages { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<TaskItem> TaskItems { get; set; } = null!;
        public virtual DbSet<TaskSample> TaskSamples { get; set; } = null!;
        public virtual DbSet<TaskSampleItem> TaskSampleItems { get; set; } = null!;
        public virtual DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; } = null!;
        public virtual DbSet<Staff> Staff { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssignStaff>(entity =>
            {
                entity.HasKey(e => new { e.TaskId, e.StaffId })
                    .HasName("PK__AssignSt__150403007F163025");

                entity.ToTable("AssignStaff");

                entity.HasIndex(e => e.TaskId, "UQ__AssignSt__7C6949B0EC8D533F")
                    .IsUnique();

                entity.HasIndex(e => e.StaffId, "UQ__AssignSt__96D4AB16E9B6CBCA")
                    .IsUnique();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Staff)
                    .WithOne(p => p.AssignStaff)
                    .HasForeignKey<AssignStaff>(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AssignSta__Staff__6C190EBB");

                entity.HasOne(d => d.Task)
                    .WithOne(p => p.AssignStaff)
                    .HasForeignKey<AssignStaff>(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AssignSta__TaskI__6B24EA82");
            });

            modelBuilder.Entity<Bird>(entity =>
            {
                entity.ToTable("Bird");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(256);

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DayOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.Cage)
                    .WithMany(p => p.Birds)
                    .HasForeignKey(d => d.CageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bird__CageId__3E52440B");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.Birds)
                    .HasForeignKey(d => d.SpeciesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bird__SpeciesId__3F466844");

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.Birds)
                    .HasForeignKey(d => d.StageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bird__StageId__403A8C7D");
            });

            modelBuilder.Entity<Cage>(entity =>
            {
                entity.ToTable("Cage");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).HasMaxLength(256);

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Material).HasMaxLength(256);

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Cages)
                    .HasForeignKey(d => d.FarmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cage__FarmId__3A81B327");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.Cages)
                    .HasForeignKey(d => d.SpeciesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cage__SpeciesId__398D8EEE");

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.Cages)
                    .HasForeignKey(d => d.StageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cage__StageId__38996AB5");
            });

            modelBuilder.Entity<DayOfWeek>(entity =>
            {
                entity.ToTable("DayOfWeek");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Farm>(entity =>
            {
                entity.ToTable("Farm");

                entity.HasIndex(e => e.ManagerId, "UQ__Farm__3BA2AAE0431D4FD1")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Phone).HasMaxLength(256);

                entity.HasOne(d => d.Manager)
                    .WithOne(p => p.Farm)
                    .HasForeignKey<Farm>(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Farm__ManagerId__29572725");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Food");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Status).HasMaxLength(256);

                entity.HasOne(d => d.FoodCategory)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.FoodCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Food__FoodCatego__49C3F6B7");

                entity.HasOne(d => d.UnitOfMeasurement)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.UnitOfMeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Food__UnitOfMeas__4AB81AF0");
            });

            modelBuilder.Entity<FoodCategory>(entity =>
            {
                entity.ToTable("FoodCategory");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("Manager");

                entity.HasIndex(e => e.Email, "UQ__Manager__A9D10534ECEBB531")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Password).HasMaxLength(256);

                entity.Property(e => e.Phone).HasMaxLength(256);

                entity.Property(e => e.Status).HasMaxLength(256);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.SpeciesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Menu__SpeciesId__4E88ABD4");

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.StageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Menu__StageId__4F7CD00D");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.FoodId })
                    .HasName("PK__MenuItem__61C8090E788748BE");

                entity.ToTable("MenuItem");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MenuItem__FoodId__5441852A");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MenuItem__MenuId__534D60F1");

                entity.HasOne(d => d.UnitOfMeasurement)
                    .WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.UnitOfMeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MenuItem__UnitOf__5535A963");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plan");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Bird)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.BirdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Plan__BirdId__59063A47");
            });

            modelBuilder.Entity<PlanMenu>(entity =>
            {
                entity.HasKey(e => new { e.PlanId, e.MenuId })
                    .HasName("PK__PlanMenu__69C5CF94DCDD7E6E");

                entity.ToTable("PlanMenu");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.DayOfWeekNavigation)
                    .WithMany(p => p.PlanMenus)
                    .HasForeignKey(d => d.DayOfWeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlanMenu__DayOfW__619B8048");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.PlanMenus)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlanMenu__MenuId__5FB337D6");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.PlanMenus)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlanMenu__PlanId__5EBF139D");
            });

            modelBuilder.Entity<Repeat>(entity =>
            {
                entity.ToTable("Repeat");

                entity.HasIndex(e => e.TaskId, "UQ__Repeat__7C6949B0DFED84F7")
                    .IsUnique();

                entity.HasIndex(e => e.TaskSampleId, "UQ__Repeat__B7D56F23C8F5D2CD")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type).HasMaxLength(256);

                entity.Property(e => e.Until).HasColumnType("datetime");

                entity.HasOne(d => d.Task)
                    .WithOne(p => p.Repeat)
                    .HasForeignKey<Repeat>(d => d.TaskId)
                    .HasConstraintName("FK__Repeat__TaskId__7C4F7684");

                entity.HasOne(d => d.TaskSample)
                    .WithOne(p => p.Repeat)
                    .HasForeignKey<Repeat>(d => d.TaskSampleId)
                    .HasConstraintName("FK__Repeat__TaskSamp__7D439ABD");
            });

            modelBuilder.Entity<Species>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.ToTable("Stage");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.HasOne(d => d.Cage)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.CageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Task__CageId__6477ECF3");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Task__ManagerId__656C112C");
            });

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("TaskItem");

                entity.HasIndex(e => e.StaffId, "UQ__TaskItem__96D4AB16774472BD")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Staff)
                    .WithOne(p => p.TaskItem)
                    .HasForeignKey<TaskItem>(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskItem__StaffI__70DDC3D8");
            });

            modelBuilder.Entity<TaskSample>(entity =>
            {
                entity.ToTable("TaskSample");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.TaskSamples)
                    .HasForeignKey(d => d.StageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskSampl__Stage__74AE54BC");
            });

            modelBuilder.Entity<TaskSampleItem>(entity =>
            {
                entity.ToTable("TaskSampleItem");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.TaskSample)
                    .WithMany(p => p.TaskSampleItems)
                    .HasForeignKey(d => d.TaskSampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskSampl__TaskS__778AC167");
            });

            modelBuilder.Entity<UnitOfMeasurement>(entity =>
            {
                entity.ToTable("UnitOfMeasurement");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.HasIndex(e => e.Phone, "UQ__Staff__5C7E359E732D6870")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Staff__A9D10534148E3DF8")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Password).HasMaxLength(256);

                entity.Property(e => e.Phone).HasMaxLength(256);

                entity.Property(e => e.Status).HasMaxLength(256);

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Staff)
                    .HasForeignKey(d => d.FarmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Staff__FarmId__2F10007B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
