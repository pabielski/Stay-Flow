using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class RoomType
{
    public long Id { get; set; }

    public long HotelId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public short MaxOccupancy { get; set; }

    public decimal SizeM2 { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
