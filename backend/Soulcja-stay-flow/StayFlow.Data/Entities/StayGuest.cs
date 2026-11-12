using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class StayGuest
{
    public long Id { get; set; }

    public long StayId { get; set; }

    public long GuestId { get; set; }

    public bool IsPrimary { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Guest Guest { get; set; } = null!;

    public virtual Stay Stay { get; set; } = null!;
}
