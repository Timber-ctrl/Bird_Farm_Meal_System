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
        public virtual DbSet<Area> Areas { get; set; } = null!;
        public virtual DbSet<AssignStaff> AssignStaffs { get; set; } = null!;
        public virtual DbSet<Bird> Birds { get; set; } = null!;
        public virtual DbSet<BirdCategory> BirdCategories { get; set; } = null!;
        public virtual DbSet<Cage> Cages { get; set; } = null!;
        public virtual DbSet<CareMode> CareModes { get; set; } = null!;
        public virtual DbSet<Farm> Farms { get; set; } = null!;
        public virtual DbSet<Food> Foods { get; set; } = null!;
        public virtual DbSet<FoodCategory> FoodCategories { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<MealItem> MealItems { get; set; } = null!;
        public virtual DbSet<MealItemSample> MealItemSamples { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<MenuMeal> MenuMeals { get; set; } = null!;
        public virtual DbSet<MenuMealSample> MenuMealSamples { get; set; } = null!;
        public virtual DbSet<MenuSammple> MenuSammples { get; set; } = null!;
        public virtual DbSet<Plan> Plans { get; set; } = null!;
        public virtual DbSet<PlanCustomMenu> PlanCustomMenus { get; set; } = null!;
        public virtual DbSet<Repeat> Repeats { get; set; } = null!;
        public virtual DbSet<Species> Species { get; set; } = null!;
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

                entity.HasIndex(e => e.Email, "UQ__Admin__A9D10534374A4206")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Password).HasMaxLength(256);
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
                    .HasName("PK__AssignSt__15040300A479EFD8");

                entity.ToTable("AssignStaff");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.AssignStaffs)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AssignSta__Staff__02FC7413");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.AssignStaffs)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AssignSta__TaskI__02084FDA");
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
                    .HasConstraintName("FK__Bird__CareModeId__4BAC3F29");

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

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Cages)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cage__AreaId__45F365D3");

                entity.HasOne(d => d.CareMode)
                    .WithMany(p => p.Cages)
                    .HasForeignKey(d => d.CareModeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cage__CareModeId__440B1D61");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.Cages)
                    .HasForeignKey(d => d.SpeciesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cage__SpeciesId__44FF419A");
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

            modelBuilder.Entity<Farm>(entity =>
            {
                entity.ToTable("Farm");

                entity.HasIndex(e => e.ManagerId, "UQ__Farm__3BA2AAE0015C7E47")
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
                    .HasConstraintName("FK__Food__FoodCatego__5535A963");

                entity.HasOne(d => d.UnitOfMeasurement)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.UnitOfMeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Food__UnitOfMeas__5629CD9C");
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

                entity.HasIndex(e => e.Email, "UQ__Manager__A9D1053487387471")
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

            modelBuilder.Entity<MealItem>(entity =>
            {
                entity.HasKey(e => new { e.MenuMealId, e.FoodId })
                    .HasName("PK__MealItem__A713C8B1B541EFCB");

                entity.ToTable("MealItem");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.MealItems)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealItem__FoodId__6FE99F9F");

                entity.HasOne(d => d.MenuMeal)
                    .WithMany(p => p.MealItems)
                    .HasForeignKey(d => d.MenuMealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealItem__MenuMe__6EF57B66");

                entity.HasOne(d => d.UnitOfMeasurement)
                    .WithMany(p => p.MealItems)
                    .HasForeignKey(d => d.UnitOfMeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealItem__UnitOf__70DDC3D8");
            });

            modelBuilder.Entity<MealItemSample>(entity =>
            {
                entity.HasKey(e => new { e.MenuMealSammpleId, e.FoodId })
                    .HasName("PK__MealItem__DDD6D92C2342CCB9");

                entity.ToTable("MealItemSample");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.MealItemSamples)
                    .HasForeignKey(d => d.FoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealItemS__FoodI__628FA481");

                entity.HasOne(d => d.MenuMealSammple)
                    .WithMany(p => p.MealItemSamples)
                    .HasForeignKey(d => d.MenuMealSammpleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealItemS__MenuM__619B8048");

                entity.HasOne(d => d.UnitOfMeasurement)
                    .WithMany(p => p.MealItemSamples)
                    .HasForeignKey(d => d.UnitOfMeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MealItemS__UnitO__6383C8BA");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.CareMode)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.CareModeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Menu__CareModeId__6754599E");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.SpeciesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Menu__SpeciesId__66603565");
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
                    .HasConstraintName("FK__MenuSammp__CareM__5AEE82B9");

                entity.HasOne(d => d.Species)
                    .WithMany(p => p.MenuSammples)
                    .HasForeignKey(d => d.SpeciesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MenuSammp__Speci__59FA5E80");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plan");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.From).HasColumnType("datetime");

                entity.Property(e => e.To).HasColumnType("datetime");

                entity.HasOne(d => d.Cage)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.CageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Plan__CageId__74AE54BC");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Plan__MenuId__73BA3083");
            });

            modelBuilder.Entity<PlanCustomMenu>(entity =>
            {
                entity.HasKey(e => new { e.PlanId, e.MenuId })
                    .HasName("PK__PlanCust__69C5CF94CBD601DA");

                entity.ToTable("PlanCustomMenu");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ForDay).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.PlanCustomMenus)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlanCusto__MenuI__797309D9");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.PlanCustomMenus)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PlanCusto__PlanI__787EE5A0");
            });

            modelBuilder.Entity<Repeat>(entity =>
            {
                entity.ToTable("Repeat");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type).HasMaxLength(256);

                entity.Property(e => e.Until).HasColumnType("datetime");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.Repeats)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK__Repeat__TaskId__19DFD96B");

                entity.HasOne(d => d.TaskSample)
                    .WithMany(p => p.Repeats)
                    .HasForeignKey(d => d.TaskSampleId)
                    .HasConstraintName("FK__Repeat__TaskSamp__1AD3FDA4");
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
                    .HasConstraintName("FK__Task__CageId__7D439ABD");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Task__ManagerId__7E37BEF6");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskCheck__Asign__07C12930");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskCheckLists)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TaskCheck__TaskI__06CD04F7");
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
                    .HasConstraintName("FK__TaskCheck__TaskC__0C85DE4D");
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
                    .HasConstraintName("FK__TaskCheck__TaskC__0F624AF8");
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
                    .HasConstraintName("FK__TaskSampl__CareM__123EB7A3");
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
                    .HasConstraintName("FK__TaskSampl__TaskS__160F4887");
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

                entity.HasOne(d => d.Assignee)
                    .WithMany(p => p.TicketAssignees)
                    .HasForeignKey(d => d.AssigneeId)
                    .HasConstraintName("FK__Ticket__Assignee__1F98B2C1");

                entity.HasOne(d => d.Cage)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.CageId)
                    .HasConstraintName("FK__Ticket__CageId__208CD6FA");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.TicketCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__CreatorI__1EA48E88");
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

                entity.HasIndex(e => e.Phone, "UQ__Staff__5C7E359E0E5ED543")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Staff__A9D1053446988191")
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
