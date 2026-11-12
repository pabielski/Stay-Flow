using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class User
{
    public long Id { get; set; }

    public long? HotelId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ContentPage> ContentPages { get; set; } = new List<ContentPage>();

    public virtual Hotel? Hotel { get; set; }

    public virtual ICollection<HousekeepingTask> HousekeepingTaskAssignedByUsers { get; set; } = new List<HousekeepingTask>();

    public virtual ICollection<HousekeepingTask> HousekeepingTaskAssignedToUsers { get; set; } = new List<HousekeepingTask>();

    public virtual ICollection<HousekeepingTask> HousekeepingTaskInspectedByUsers { get; set; } = new List<HousekeepingTask>();

    public virtual ICollection<Issue> IssueAssignedToUsers { get; set; } = new List<Issue>();

    public virtual ICollection<IssueComment> IssueComments { get; set; } = new List<IssueComment>();

    public virtual ICollection<Issue> IssueReportedByUsers { get; set; } = new List<Issue>();

    public virtual ICollection<OnlineCheckIn> OnlineCheckIns { get; set; } = new List<OnlineCheckIn>();

    public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
