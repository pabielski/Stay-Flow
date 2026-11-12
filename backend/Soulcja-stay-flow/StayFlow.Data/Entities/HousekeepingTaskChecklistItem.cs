using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class HousekeepingTaskChecklistItem
{
    public long Id { get; set; }

    public long TaskId { get; set; }

    public short Position { get; set; }

    public string Description { get; set; } = null!;

    public bool IsRequired { get; set; }

    public bool IsDone { get; set; }

    public DateTime? DoneAt { get; set; }

    public virtual HousekeepingTask Task { get; set; } = null!;
}
