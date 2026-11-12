using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class ChecklistTemplate
{
    public long Id { get; set; }

    public long HotelId { get; set; }

    public string Name { get; set; } = null!;

    public string TaskType { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ChecklistTemplateItem> ChecklistTemplateItems { get; set; } = new List<ChecklistTemplateItem>();

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<HousekeepingTask> HousekeepingTasks { get; set; } = new List<HousekeepingTask>();
}
