using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class Room
{
    public long Id { get; set; }

    public long HotelId { get; set; }

    public long RoomTypeId { get; set; }

    public string Number { get; set; } = null!;

    public short Floor { get; set; }

    public string CleaningStatus { get; set; } = null!;

    public string TechnicalStatus { get; set; } = null!;

    public string? Notes { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<HousekeepingTask> HousekeepingTasks { get; set; } = new List<HousekeepingTask>();

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual RoomType RoomType { get; set; } = null!;

    public virtual ICollection<Stay> Stays { get; set; } = new List<Stay>();
}
