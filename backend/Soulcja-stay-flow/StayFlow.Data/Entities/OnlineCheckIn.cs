using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class OnlineCheckIn
{
    public long Id { get; set; }

    public long StayId { get; set; }

    public DateTime CompletedAt { get; set; }

    public string Method { get; set; } = null!;

    public long? CompletedByUserId { get; set; }

    public long TermsPageId { get; set; }

    public TimeOnly? EstimatedArrivalTime { get; set; }

    public string? GuestNotes { get; set; }

    public string? StaffNotes { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Stay Stay { get; set; } = null!;

    public virtual ContentPage TermsPage { get; set; } = null!;
}
