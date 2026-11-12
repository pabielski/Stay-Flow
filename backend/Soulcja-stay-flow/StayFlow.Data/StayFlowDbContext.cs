using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StayFlow.Data.Entities;

namespace StayFlow.Data;

public partial class StayFlowDbContext : DbContext
{
    public StayFlowDbContext(DbContextOptions<StayFlowDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChecklistTemplate> ChecklistTemplates { get; set; }

    public virtual DbSet<ChecklistTemplateItem> ChecklistTemplateItems { get; set; }

    public virtual DbSet<ContentPage> ContentPages { get; set; }

    public virtual DbSet<DocumentTemplate> DocumentTemplates { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<HousekeepingTask> HousekeepingTasks { get; set; }

    public virtual DbSet<HousekeepingTaskChecklistItem> HousekeepingTaskChecklistItems { get; set; }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<IssueCategory> IssueCategories { get; set; }

    public virtual DbSet<IssueComment> IssueComments { get; set; }

    public virtual DbSet<OnlineCheckIn> OnlineCheckIns { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceCategory> ServiceCategories { get; set; }

    public virtual DbSet<ServiceOrder> ServiceOrders { get; set; }

    public virtual DbSet<Stay> Stays { get; set; }

    public virtual DbSet<StayGuest> StayGuests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChecklistTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("checklist_templates_pkey");

            entity.ToTable("checklist_templates");

            entity.HasIndex(e => new { e.HotelId, e.Name }, "uq_checklist_templates_hotel_name").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.TaskType)
                .HasMaxLength(20)
                .HasColumnName("task_type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Hotel).WithMany(p => p.ChecklistTemplates)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_checklist_templates_hotels");
        });

        modelBuilder.Entity<ChecklistTemplateItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("checklist_template_items_pkey");

            entity.ToTable("checklist_template_items");

            entity.HasIndex(e => e.TemplateId, "ix_checklist_template_items_template_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.IsRequired)
                .HasDefaultValue(true)
                .HasColumnName("is_required");
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.TemplateId).HasColumnName("template_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Template).WithMany(p => p.ChecklistTemplateItems)
                .HasForeignKey(d => d.TemplateId)
                .HasConstraintName("fk_checklist_template_items_templates");
        });

        modelBuilder.Entity<ContentPage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("content_pages_pkey");

            entity.ToTable("content_pages");

            entity.HasIndex(e => e.CreatedByUserId, "ix_content_pages_created_by_user_id");

            entity.HasIndex(e => new { e.HotelId, e.Slug, e.Version }, "uq_content_pages_hotel_slug_version").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedByUserId).HasColumnName("created_by_user_id");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.IsPublished).HasColumnName("is_published");
            entity.Property(e => e.PageType)
                .HasMaxLength(20)
                .HasColumnName("page_type");
            entity.Property(e => e.PublishedAt).HasColumnName("published_at");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .HasColumnName("slug");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.Version)
                .HasDefaultValue(1)
                .HasColumnName("version");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.ContentPages)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_content_pages_users");

            entity.HasOne(d => d.Hotel).WithMany(p => p.ContentPages)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_content_pages_hotels");
        });

        modelBuilder.Entity<DocumentTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("document_templates_pkey");

            entity.ToTable("document_templates");

            entity.HasIndex(e => new { e.HotelId, e.Code }, "uq_document_templates_hotel_code").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Hotel).WithMany(p => p.DocumentTemplates)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_document_templates_hotels");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("guests_pkey");

            entity.ToTable("guests");

            entity.HasIndex(e => e.Email, "uq_guests_email").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hotels_pkey");

            entity.ToTable("hotels");

            entity.HasIndex(e => new { e.Name, e.City }, "uq_hotels_name_city").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .HasColumnName("address");
            entity.Property(e => e.CheckInTime)
                .HasDefaultValueSql("'15:00:00'::time without time zone")
                .HasColumnName("check_in_time");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<HousekeepingTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("housekeeping_tasks_pkey");

            entity.ToTable("housekeeping_tasks");

            entity.HasIndex(e => e.AssignedByUserId, "ix_housekeeping_tasks_assigned_by_user_id");

            entity.HasIndex(e => new { e.AssignedToUserId, e.Status }, "ix_housekeeping_tasks_assigned_to_status");

            entity.HasIndex(e => e.ChecklistTemplateId, "ix_housekeeping_tasks_checklist_template_id");

            entity.HasIndex(e => e.InspectedByUserId, "ix_housekeeping_tasks_inspected_by_user_id");

            entity.HasIndex(e => e.RoomId, "ix_housekeeping_tasks_room_id");

            entity.HasIndex(e => e.StayId, "ix_housekeeping_tasks_stay_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AssignedByUserId).HasColumnName("assigned_by_user_id");
            entity.Property(e => e.AssignedToUserId).HasColumnName("assigned_to_user_id");
            entity.Property(e => e.ChecklistTemplateId).HasColumnName("checklist_template_id");
            entity.Property(e => e.CompletedAt).HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.InspectedAt).HasColumnName("inspected_at");
            entity.Property(e => e.InspectedByUserId).HasColumnName("inspected_by_user_id");
            entity.Property(e => e.InspectionNotes).HasColumnName("inspection_notes");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.ScheduledDate).HasColumnName("scheduled_date");
            entity.Property(e => e.StartedAt).HasColumnName("started_at");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.StayId).HasColumnName("stay_id");
            entity.Property(e => e.TaskType)
                .HasMaxLength(20)
                .HasColumnName("task_type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.AssignedByUser).WithMany(p => p.HousekeepingTaskAssignedByUsers)
                .HasForeignKey(d => d.AssignedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_housekeeping_tasks_assigned_by");

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.HousekeepingTaskAssignedToUsers)
                .HasForeignKey(d => d.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_housekeeping_tasks_assigned_to");

            entity.HasOne(d => d.ChecklistTemplate).WithMany(p => p.HousekeepingTasks)
                .HasForeignKey(d => d.ChecklistTemplateId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_housekeeping_tasks_templates");

            entity.HasOne(d => d.InspectedByUser).WithMany(p => p.HousekeepingTaskInspectedByUsers)
                .HasForeignKey(d => d.InspectedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_housekeeping_tasks_inspected_by");

            entity.HasOne(d => d.Room).WithMany(p => p.HousekeepingTasks)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_housekeeping_tasks_rooms");

            entity.HasOne(d => d.Stay).WithMany(p => p.HousekeepingTasks)
                .HasForeignKey(d => d.StayId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_housekeeping_tasks_stays");
        });

        modelBuilder.Entity<HousekeepingTaskChecklistItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("housekeeping_task_checklist_items_pkey");

            entity.ToTable("housekeeping_task_checklist_items");

            entity.HasIndex(e => e.TaskId, "ix_housekeeping_task_checklist_items_task_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.DoneAt).HasColumnName("done_at");
            entity.Property(e => e.IsDone).HasColumnName("is_done");
            entity.Property(e => e.IsRequired).HasColumnName("is_required");
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.Task).WithMany(p => p.HousekeepingTaskChecklistItems)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("fk_housekeeping_task_checklist_items_tasks");
        });

        modelBuilder.Entity<Issue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("issues_pkey");

            entity.ToTable("issues");

            entity.HasIndex(e => e.AssignedToUserId, "ix_issues_assigned_to_user_id");

            entity.HasIndex(e => e.CategoryId, "ix_issues_category_id");

            entity.HasIndex(e => new { e.HotelId, e.Status }, "ix_issues_hotel_status");

            entity.HasIndex(e => e.HousekeepingTaskId, "ix_issues_housekeeping_task_id");

            entity.HasIndex(e => e.ReportedByGuestId, "ix_issues_reported_by_guest_id");

            entity.HasIndex(e => e.ReportedByUserId, "ix_issues_reported_by_user_id");

            entity.HasIndex(e => e.RoomId, "ix_issues_room_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AssignedToUserId).HasColumnName("assigned_to_user_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ClosedAt).HasColumnName("closed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.HousekeepingTaskId).HasColumnName("housekeeping_task_id");
            entity.Property(e => e.Priority)
                .HasMaxLength(20)
                .HasDefaultValueSql("'medium'::character varying")
                .HasColumnName("priority");
            entity.Property(e => e.ReportedByGuestId).HasColumnName("reported_by_guest_id");
            entity.Property(e => e.ReportedByUserId).HasColumnName("reported_by_user_id");
            entity.Property(e => e.ResolutionNotes).HasColumnName("resolution_notes");
            entity.Property(e => e.ResolvedAt).HasColumnName("resolved_at");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'open'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.IssueAssignedToUsers)
                .HasForeignKey(d => d.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_issues_assigned_to");

            entity.HasOne(d => d.Category).WithMany(p => p.Issues)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_issues_categories");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Issues)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_issues_hotels");

            entity.HasOne(d => d.HousekeepingTask).WithMany(p => p.Issues)
                .HasForeignKey(d => d.HousekeepingTaskId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_issues_housekeeping_tasks");

            entity.HasOne(d => d.ReportedByGuest).WithMany(p => p.Issues)
                .HasForeignKey(d => d.ReportedByGuestId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_issues_reported_by_guest");

            entity.HasOne(d => d.ReportedByUser).WithMany(p => p.IssueReportedByUsers)
                .HasForeignKey(d => d.ReportedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_issues_reported_by_user");

            entity.HasOne(d => d.Room).WithMany(p => p.Issues)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_issues_rooms");
        });

        modelBuilder.Entity<IssueCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("issue_categories_pkey");

            entity.ToTable("issue_categories");

            entity.HasIndex(e => e.Name, "uq_issue_categories_name").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<IssueComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("issue_comments_pkey");

            entity.ToTable("issue_comments");

            entity.HasIndex(e => e.AuthorGuestId, "ix_issue_comments_author_guest_id");

            entity.HasIndex(e => e.AuthorUserId, "ix_issue_comments_author_user_id");

            entity.HasIndex(e => e.IssueId, "ix_issue_comments_issue_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AuthorGuestId).HasColumnName("author_guest_id");
            entity.Property(e => e.AuthorUserId).HasColumnName("author_user_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IsInternal).HasColumnName("is_internal");
            entity.Property(e => e.IssueId).HasColumnName("issue_id");

            entity.HasOne(d => d.AuthorGuest).WithMany(p => p.IssueComments)
                .HasForeignKey(d => d.AuthorGuestId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_issue_comments_guests");

            entity.HasOne(d => d.AuthorUser).WithMany(p => p.IssueComments)
                .HasForeignKey(d => d.AuthorUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_issue_comments_users");

            entity.HasOne(d => d.Issue).WithMany(p => p.IssueComments)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("fk_issue_comments_issues");
        });

        modelBuilder.Entity<OnlineCheckIn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("online_check_ins_pkey");

            entity.ToTable("online_check_ins");

            entity.HasIndex(e => e.CompletedByUserId, "ix_online_check_ins_completed_by_user_id");

            entity.HasIndex(e => e.TermsPageId, "ix_online_check_ins_terms_page_id");

            entity.HasIndex(e => e.StayId, "uq_online_check_ins_stay").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CompletedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("completed_at");
            entity.Property(e => e.CompletedByUserId).HasColumnName("completed_by_user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.EstimatedArrivalTime).HasColumnName("estimated_arrival_time");
            entity.Property(e => e.GuestNotes).HasColumnName("guest_notes");
            entity.Property(e => e.Method)
                .HasMaxLength(20)
                .HasDefaultValueSql("'online'::character varying")
                .HasColumnName("method");
            entity.Property(e => e.StaffNotes).HasColumnName("staff_notes");
            entity.Property(e => e.StayId).HasColumnName("stay_id");
            entity.Property(e => e.TermsPageId).HasColumnName("terms_page_id");

            entity.HasOne(d => d.CompletedByUser).WithMany(p => p.OnlineCheckIns)
                .HasForeignKey(d => d.CompletedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_online_check_ins_users");

            entity.HasOne(d => d.Stay).WithOne(p => p.OnlineCheckIn)
                .HasForeignKey<OnlineCheckIn>(d => d.StayId)
                .HasConstraintName("fk_online_check_ins_stays");

            entity.HasOne(d => d.TermsPage).WithMany(p => p.OnlineCheckIns)
                .HasForeignKey(d => d.TermsPageId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_online_check_ins_content_pages");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permissions_pkey");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.Code, "uq_permissions_code").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "uq_roles_name").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsSystem).HasColumnName("is_system");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_permissions_pkey");

            entity.ToTable("role_permissions");

            entity.HasIndex(e => e.PermissionId, "ix_role_permissions_permission_id");

            entity.HasIndex(e => new { e.RoleId, e.PermissionId }, "uq_role_permissions_role_permission").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_role_permissions_permissions");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_role_permissions_roles");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rooms_pkey");

            entity.ToTable("rooms");

            entity.HasIndex(e => e.RoomTypeId, "ix_rooms_room_type_id");

            entity.HasIndex(e => new { e.HotelId, e.Number }, "uq_rooms_hotel_number").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CleaningStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'dirty'::character varying")
                .HasColumnName("cleaning_status");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Floor).HasColumnName("floor");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Number)
                .HasMaxLength(10)
                .HasColumnName("number");
            entity.Property(e => e.RoomTypeId).HasColumnName("room_type_id");
            entity.Property(e => e.TechnicalStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'operational'::character varying")
                .HasColumnName("technical_status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_rooms_hotels");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_rooms_room_types");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("room_types_pkey");

            entity.ToTable("room_types");

            entity.HasIndex(e => new { e.HotelId, e.Name }, "uq_room_types_hotel_name").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.MaxOccupancy).HasColumnName("max_occupancy");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.SizeM2)
                .HasPrecision(6, 2)
                .HasColumnName("size_m2");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Hotel).WithMany(p => p.RoomTypes)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_room_types_hotels");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("services_pkey");

            entity.ToTable("services");

            entity.HasIndex(e => new { e.CategoryId, e.Name }, "uq_services_category_name").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AvailableFrom).HasColumnName("available_from");
            entity.Property(e => e.AvailableTo).HasColumnName("available_to");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.RequiresBooking).HasColumnName("requires_booking");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Category).WithMany(p => p.Services)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_services_categories");
        });

        modelBuilder.Entity<ServiceCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_categories_pkey");

            entity.ToTable("service_categories");

            entity.HasIndex(e => new { e.HotelId, e.Name }, "uq_service_categories_hotel_name").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Hotel).WithMany(p => p.ServiceCategories)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_service_categories_hotels");
        });

        modelBuilder.Entity<ServiceOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_orders_pkey");

            entity.ToTable("service_orders");

            entity.HasIndex(e => e.AssignedToUserId, "ix_service_orders_assigned_to_user_id");

            entity.HasIndex(e => new { e.ServiceId, e.ScheduledStart }, "ix_service_orders_service_schedule");

            entity.HasIndex(e => e.StayId, "ix_service_orders_stay_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AssignedToUserId).HasColumnName("assigned_to_user_id");
            entity.Property(e => e.CancelledAt).HasColumnName("cancelled_at");
            entity.Property(e => e.CompletedAt).HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.GuestNotes).HasColumnName("guest_notes");
            entity.Property(e => e.Quantity)
                .HasDefaultValue((short)1)
                .HasColumnName("quantity");
            entity.Property(e => e.ScheduledEnd).HasColumnName("scheduled_end");
            entity.Property(e => e.ScheduledStart).HasColumnName("scheduled_start");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.StaffNotes).HasColumnName("staff_notes");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pending'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.StayId).HasColumnName("stay_id");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.AssignedToUser).WithMany(p => p.ServiceOrders)
                .HasForeignKey(d => d.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_service_orders_users");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceOrders)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_service_orders_services");

            entity.HasOne(d => d.Stay).WithMany(p => p.ServiceOrders)
                .HasForeignKey(d => d.StayId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_service_orders_stays");
        });

        modelBuilder.Entity<Stay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("stays_pkey");

            entity.ToTable("stays");

            entity.HasIndex(e => e.RoomId, "ix_stays_room_id");

            entity.HasIndex(e => e.CheckInToken, "uq_stays_check_in_token").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CheckInDate).HasColumnName("check_in_date");
            entity.Property(e => e.CheckInToken)
                .HasMaxLength(100)
                .HasColumnName("check_in_token");
            entity.Property(e => e.CheckOutDate).HasColumnName("check_out_date");
            entity.Property(e => e.CheckedOutAt).HasColumnName("checked_out_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'unpaid'::character varying")
                .HasColumnName("payment_status");
            entity.Property(e => e.RoomAccessCode)
                .HasMaxLength(20)
                .HasColumnName("room_access_code");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'expected'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Room).WithMany(p => p.Stays)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_stays_rooms");
        });

        modelBuilder.Entity<StayGuest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("stay_guests_pkey");

            entity.ToTable("stay_guests");

            entity.HasIndex(e => e.GuestId, "ix_stay_guests_guest_id");

            entity.HasIndex(e => new { e.StayId, e.IsPrimary }, "uq_stay_guests_one_primary")
                .IsUnique()
                .HasFilter("is_primary");

            entity.HasIndex(e => new { e.StayId, e.GuestId }, "uq_stay_guests_stay_guest").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.IsPrimary).HasColumnName("is_primary");
            entity.Property(e => e.StayId).HasColumnName("stay_id");

            entity.HasOne(d => d.Guest).WithMany(p => p.StayGuests)
                .HasForeignKey(d => d.GuestId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_stay_guests_guests");

            entity.HasOne(d => d.Stay).WithMany(p => p.StayGuests)
                .HasForeignKey(d => d.StayId)
                .HasConstraintName("fk_stay_guests_stays");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.HotelId, "ix_users_hotel_id");

            entity.HasIndex(e => e.Email, "uq_users_email").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastLoginAt).HasColumnName("last_login_at");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Users)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_users_hotels");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_roles_pkey");

            entity.ToTable("user_roles");

            entity.HasIndex(e => e.RoleId, "ix_user_roles_role_id");

            entity.HasIndex(e => new { e.UserId, e.RoleId }, "uq_user_roles_user_role").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_user_roles_roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_roles_users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
