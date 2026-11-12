using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class DocumentTemplate
{
    public long Id { get; set; }

    public long HotelId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;
}
