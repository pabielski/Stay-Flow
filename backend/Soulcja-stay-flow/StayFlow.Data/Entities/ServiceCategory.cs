using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class ServiceCategory
{
    public long Id { get; set; }

    public long HotelId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
