using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class IssueComment
{
    public long Id { get; set; }

    public long IssueId { get; set; }

    public long? AuthorUserId { get; set; }

    public long? AuthorGuestId { get; set; }

    public string Content { get; set; } = null!;

    public bool IsInternal { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Guest? AuthorGuest { get; set; }

    public virtual User? AuthorUser { get; set; }

    public virtual Issue Issue { get; set; } = null!;
}
