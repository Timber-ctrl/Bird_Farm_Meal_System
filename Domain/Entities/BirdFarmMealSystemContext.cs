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

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; } = null!;
        public virtual DbSet<Area> Areas { get; set; } = null!;
        public virtual DbSet<AssignStaff> AssignStaffs { get; set; } = null!;
        public virtual DbSet<Bird> Birds { get; set; } = null!;
        public virtual DbSet<BirdCategory> BirdCategories { get; set; } = null!;
        public virtual DbSet<Cage> Cages { get; set; } = null!;
        public virtual DbSet<CareMode> CareModes { get; set; } = null!;
        public virtual DbSet<Counter> Counters { get; set; } = null!;
        public virtual DbSet<DeviceToken> DeviceTokens { get; set; } = null!;
        public virtual DbSet<Farm> Farms { get; set; } = null!;
        public virtual DbSet<Food> Foods { get; set; } = null!;
        public virtual DbSet<FoodCategory> FoodCategories { get; set; } = null!;
        public virtual DbSet<FoodReport> FoodReports { get; set; } = null!;
        public virtual DbSet<Hash> Hashes { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<JobParameter> JobParameters { get; set; } = null!;
        public virtual DbSet<JobQueue> JobQueues { get; set; } = null!;
        public virtual DbSet<List> Lists { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<MealItem> MealItems { get; set; } = null!;
        public virtual DbSet<MealItemSample> MealItemSamples { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<MenuMeal> MenuMeals { get; set; } = null!;
        public virtual DbSet<MenuMealSample> MenuMealSamples { get; set; } = null!;
        public virtual DbSet<MenuSammple> MenuSammples { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Plan> Plans { get; set; } = null!;
        public virtual DbSet<PlanDetail> PlanDetails { get; set; } = null!;
        public virtual DbSet<Repeat> Repeats { get; set; } = null!;
        public virtual DbSet<Schema> Schemas { get; set; } = null!;
        public virtual DbSet<Server> Servers { get; set; } = null!;
        public virtual DbSet<Set> Sets { get; set; } = null!;
        public virtual DbSet<Species> Species { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<TaskCheckList> TaskCheckLists { get; set; } = null!;
        public virtual DbSet<TaskCheckListReport> TaskCheckListReports { get; set; } = null!;
        public virtual DbSet<TaskCheckListReportItem> TaskCheckListReportItems { get; set; } = null!;
        public virtual DbSet<TaskSample> TaskSamples { get; set; } = null!;
        public virtual DbSet<TaskSampleCheckList> TaskSampleCheckLists { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<UnitOfMeasurement> UnitOfMeasurements { get; set; } = null!;
        public virtual DbSet<Staff> Staff { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.HasIndex(e => e.Email, "UQ__Admin__A9D10534B75486F2")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Password).HasMaxLength(256);
            });

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_HangFire_CounterAggregated");

                entity.ToTable("AggregatedCounter", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_AggregatedCounter_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.Farm)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.FarmId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Area__FarmId__30F848ED");
            });

            modelBuilder.Entity<AssignStaff>(entity =>
            {
                entity.HasKey(e => new { e.TaskId, e.StaffId })
                    .HasName("PK__AssignSt__15040300119A0571");

                entity.ToTable("AssignStaff");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.AssignStaffs)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AssignSta__Staff__7E37BEF6");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.AssignStaffs)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AssignSta__TaskI__7D439ABD");
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
                    .HasConstraintName("FK__Bird__CageId__49C3F6B7");

                entity.HasOne(d => d.CareMode)
                    .WithMany(p => p.Birds)
                    .HasForeignKey(d => d.CareModeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bird__CareModeId__4CA06362");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Birds)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bird__CategoryId__4BAC3F29");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Birds)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_Bird_Menu");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.Birds)
                    .HasForeignKey(d => d.SpeciesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bird__SpeciesId__4AB81AF0");
            });

            modelBuilder.Entity<BirdCategory>(entity =>
            {
                entity.ToTable("BirdCategory");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);
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

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Cages)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cage__AreaId__45F365D3");
            });

            modelBuilder.Entity<CareMode>(entity =>
            {
                entity.ToTable("CareMode");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_Counter");

                entity.ToTable("Counter", "HangFire");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<DeviceToken>(entity =>
            {
                entity.ToTable("DeviceToken");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.DeviceTokens)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK__DeviceTok__Admin__6F7F8B4B");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.DeviceTokens)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__DeviceTok__Manag__7073AF84");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.DeviceTokens)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__DeviceTok__Staff__6E8B6712");
            });

            modelBuilder.Entity<Farm>(entity =>
            {
                entity.ToTable("Farm");

                entity.HasIndex(e => e.ManagerId, "UQ__Farm__3BA2AAE0C68ED653")
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
                    .HasConstraintName("FK__Farm__ManagerId__2D27B809");
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
                    .HasConstraintName("FK__Food__FoodCatego__5629CD9C");

                entity.HasOne(d => d.UnitOfMeasurement)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.UnitOfMeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Food__UnitOfMeas__571DF1D5");
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

            modelBuilder.Entity<FoodReport>(entity =>
            {
                entity.ToTable("FoodReport");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodReports)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FoodRepor__FoodI__2CBDA3B5");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.FoodReports)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FoodRepor__Staff__2BC97F7C");
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_HangFire_Hash");

                entity.ToTable("Hash", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName")
                    .HasFilter("([StateName] IS NOT NULL)");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_HangFire_JobParameter");

                entity.ToTable("JobParameter", "HangFire");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameters)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.HasKey(e => new { e.Queue, e.Id })
                    .HasName("PK_HangFire_JobQueue");

                entity.ToTable("JobQueue", "HangFire");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_List");

                entity.ToTable("List", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("Manager");

                entity.HasIndex(e => e.Email, "UQ__Manager__A9D10534C8235CA1")
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

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ManagerNavigation)
                    .HasPrincipalKey<Farm>(p => p.ManagerId)
                    .HasForeignKey<Manager>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Manager_Farm");
            });

            modelBuilder.Entity<MealItem>(entity =>
            {
                entity.HasKey(e => new { e.MenuMealId, e.FoodId })
                    .HasName("PK__MealItem__A713C8B1523ED0BF");

                entity.ToTable("MealItem");

                entity.HasIndex(e => e.Id, "IX_MealItemId")
                    .IsUnique();

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.MealItems)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealItem__FoodId__6FE99F9F");

                entity.HasOne(d => d.MenuMeal)
                    .WithMany(p => p.MealItems)
                    .HasForeignKey(d => d.MenuMealId)
                    .HasConstraintName("FK__MealItem__MenuMe__6EF57B66");
            });

            modelBuilder.Entity<MealItemSample>(entity =>
            {
                entity.HasKey(e => new { e.MenuMealSampleId, e.FoodId })
                    .HasName("PK__MealItem__DDD6D92C3FE05996");

                entity.ToTable("MealItemSample");

                entity.HasIndex(e => e.Id, "IX_MealItemSample")
                    .IsUnique();

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.MealItemSamples)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealItemS__FoodI__6383C8BA");

                entity.HasOne(d => d.MenuMealSample)
                    .WithMany(p => p.MealItemSamples)
                    .HasForeignKey(d => d.MenuMealSampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealItemS__MenuM__628FA481");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<MenuMeal>(entity =>
            {
                entity.ToTable("MenuMeal");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuMeals)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MenuMeal__MenuId__6B24EA82");
            });

            modelBuilder.Entity<MenuMealSample>(entity =>
            {
                entity.ToTable("MenuMealSample");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.MenuSample)
                    .WithMany(p => p.MenuMealSamples)
                    .HasForeignKey(d => d.MenuSampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuMealSample_MenuSammple");
            });

            modelBuilder.Entity<MenuSammple>(entity =>
            {
                entity.ToTable("MenuSammple");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.CareMode)
                    .WithMany(p => p.MenuSammples)
                    .HasForeignKey(d => d.CareModeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MenuSammp__CareM__5BE2A6F2");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.MenuSammples)
                    .HasForeignKey(d => d.SpeciesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MenuSammp__Speci__5AEE82B9");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Link).HasMaxLength(256);

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.Property(e => e.Type).HasMaxLength(256);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK__Notificat__Admin__68D28DBC");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__Notificat__Manag__69C6B1F5");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__Notificat__Staff__67DE6983");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plan");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.From).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(256);

                entity.Property(e => e.To).HasColumnType("datetime");

                entity.HasOne(d => d.Cage)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.CageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Plan__CageId__74AE54BC");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK__Plan__MenuId__73BA3083");
            });

            modelBuilder.Entity<PlanDetail>(entity =>
            {
                entity.ToTable("PlanDetail");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.PlanDetails)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlanDetai__PlanI__753864A1");
            });

            modelBuilder.Entity<Repeat>(entity =>
            {
                entity.ToTable("Repeat");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type).HasMaxLength(256);

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Repeats)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK__Repeat__TaskId__151B244E");

                entity.HasOne(d => d.TaskSample)
                    .WithMany(p => p.Repeats)
                    .HasForeignKey(d => d.TaskSampleId)
                    .HasConstraintName("FK__Repeat__TaskSamp__160F4887");
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.HasIndex(e => e.LastHeartbeat, "IX_HangFire_Server_LastHeartbeat");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value })
                    .HasName("PK_HangFire_Set");

                entity.ToTable("Set", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Set_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => new { e.Key, e.Score }, "IX_HangFire_Set_Score");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Species>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.BirdCategory)
                    .WithMany(p => p.Species)
                    .HasForeignKey(d => d.BirdCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Species__BirdCat__403A8C7D");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Id })
                    .HasName("PK_HangFire_State");

                entity.ToTable("State", "HangFire");

                entity.HasIndex(e => e.CreatedAt, "IX_HangFire_State_CreatedAt");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.StartAt).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(256);

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Task__ManagerId__797309D9");
            });

            modelBuilder.Entity<TaskCheckList>(entity =>
            {
                entity.ToTable("TaskCheckList");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Asignee)
                    .WithMany(p => p.TaskCheckLists)
                    .HasForeignKey(d => d.AsigneeId)
                    .HasConstraintName("FK__TaskCheck__Asign__02FC7413");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskCheckLists)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskCheck__TaskI__02084FDA");
            });

            modelBuilder.Entity<TaskCheckListReport>(entity =>
            {
                entity.ToTable("TaskCheckListReport");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FinishAt).HasColumnType("datetime");

                entity.HasOne(d => d.TaskCheckList)
                    .WithMany(p => p.TaskCheckListReports)
                    .HasForeignKey(d => d.TaskCheckListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskCheck__TaskC__07C12930");
            });

            modelBuilder.Entity<TaskCheckListReportItem>(entity =>
            {
                entity.ToTable("TaskCheckListReportItem");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Issue).HasMaxLength(256);

                entity.HasOne(d => d.TaskCheckListReport)
                    .WithMany(p => p.TaskCheckListReportItems)
                    .HasForeignKey(d => d.TaskCheckListReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskCheck__TaskC__0A9D95DB");
            });

            modelBuilder.Entity<TaskSample>(entity =>
            {
                entity.ToTable("TaskSample");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.CareMode)
                    .WithMany(p => p.TaskSamples)
                    .HasForeignKey(d => d.CareModeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskSampl__CareM__0D7A0286");
            });

            modelBuilder.Entity<TaskSampleCheckList>(entity =>
            {
                entity.ToTable("TaskSampleCheckList");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.TaskSample)
                    .WithMany(p => p.TaskSampleCheckLists)
                    .HasForeignKey(d => d.TaskSampleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskSampl__TaskS__114A936A");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Priority).HasMaxLength(256);

                entity.Property(e => e.Status).HasMaxLength(256);

                entity.Property(e => e.TicketCategory).HasMaxLength(256);

                entity.Property(e => e.Title)
                    .HasMaxLength(256)
                    .IsFixedLength();

                entity.HasOne(d => d.Assignee)
                    .WithMany(p => p.TicketAssignees)
                    .HasForeignKey(d => d.AssigneeId)
                    .HasConstraintName("FK__Ticket__Assignee__1AD3FDA4");

                entity.HasOne(d => d.Cage)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.CageId)
                    .HasConstraintName("FK__Ticket__CageId__1BC821DD");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.TicketCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__CreatorI__19DFD96B");
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

                entity.HasIndex(e => e.Phone, "UQ__Staff__5C7E359E0791B1E7")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Staff__A9D105349A73699C")
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
                    .HasConstraintName("FK__Staff__FarmId__36B12243");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
