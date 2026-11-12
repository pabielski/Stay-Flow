using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class HousekeepingTask
{
    public long Id { get; set; }

    public long RoomId { get; set; }

    public long? StayId { get; set; }

    public long ChecklistTemplateId { get; set; }

    public string TaskType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateOnly ScheduledDate { get; set; }

    public long? AssignedToUserId { get; set; }

    public long? AssignedByUserId { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public long? InspectedByUserId { get; set; }

    public DateTime? InspectedAt { get; set; }

    public string? InspectionNotes { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User? AssignedByUser { get; set; }

    public virtual User? AssignedToUser { get; set; }

    public virtual ChecklistTemplate ChecklistTemplate { get; set; } = null!;

    public virtual ICollection<HousekeepingTaskChecklistItem> HousekeepingTaskChecklistItems { get; set; } = new List<HousekeepingTaskChecklistItem>();

    public virtual User? InspectedByUser { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual Room Room { get; set; } = null!;

    public virtual Stay? Stay { get; set; }
}
