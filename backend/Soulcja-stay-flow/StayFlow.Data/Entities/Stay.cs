using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class Stay
{
    public long Id { get; set; }

    public long RoomId { get; set; }

    public DateOnly CheckInDate { get; set; }

    public DateOnly CheckOutDate { get; set; }

    public string Status { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public string CheckInToken { get; set; } = null!;

    public string? RoomAccessCode { get; set; }

    public DateTime? CheckedOutAt { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<HousekeepingTask> HousekeepingTasks { get; set; } = new List<HousekeepingTask>();

    public virtual OnlineCheckIn? OnlineCheckIn { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();

    public virtual ICollection<StayGuest> StayGuests { get; set; } = new List<StayGuest>();
}
