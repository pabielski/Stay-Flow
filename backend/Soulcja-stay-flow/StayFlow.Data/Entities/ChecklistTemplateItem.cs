using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class ChecklistTemplateItem
{
    public long Id { get; set; }

    public long TemplateId { get; set; }

    public short Position { get; set; }

    public string Description { get; set; } = null!;

    public bool IsRequired { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ChecklistTemplate Template { get; set; } = null!;
}
