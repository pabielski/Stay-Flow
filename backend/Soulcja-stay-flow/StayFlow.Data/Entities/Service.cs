using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class Service
{
    public long Id { get; set; }

    public long CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool RequiresBooking { get; set; }

    public bool AvailableAtCheckIn { get; set; }

    public short? DurationMinutes { get; set; }

    public short? Capacity { get; set; }

    public TimeOnly? AvailableFrom { get; set; }

    public TimeOnly? AvailableTo { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ServiceCategory Category { get; set; } = null!;

    public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
}
