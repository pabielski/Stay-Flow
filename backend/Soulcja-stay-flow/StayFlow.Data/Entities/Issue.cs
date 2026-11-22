using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class Issue
{
    public long Id { get; set; }

    public long HotelId { get; set; }

    public long? RoomId { get; set; }

    public long CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Priority { get; set; } = null!;

    public string Status { get; set; } = null!;

    public long? ReportedByUserId { get; set; }

    public long? ReportedByGuestId { get; set; }

    public long? HousekeepingTaskId { get; set; }

    public long? AssignedToUserId { get; set; }

    public string? ResolutionNotes { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public DateTime? ClosedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual IssueCategory Category { get; set; } = null!;

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual HousekeepingTask? HousekeepingTask { get; set; }

    public virtual ICollection<IssueComment> IssueComments { get; set; } = new List<IssueComment>();

    public virtual Guest? ReportedByGuest { get; set; }

    public virtual Room? Room { get; set; }
}
