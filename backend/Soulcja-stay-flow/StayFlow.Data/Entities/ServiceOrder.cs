using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class ServiceOrder
{
    public long Id { get; set; }

    public long ServiceId { get; set; }

    public long StayId { get; set; }

    public short Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? ScheduledStart { get; set; }

    public DateTime? ScheduledEnd { get; set; }

    public long? AssignedToUserId { get; set; }

    public string? GuestNotes { get; set; }

    public string? StaffNotes { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User? AssignedToUser { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual Stay Stay { get; set; } = null!;
}
