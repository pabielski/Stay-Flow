using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class IssueCategory
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
