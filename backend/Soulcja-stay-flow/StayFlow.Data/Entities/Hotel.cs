using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class Hotel
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public TimeOnly CheckInTime { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ChecklistTemplate> ChecklistTemplates { get; set; } = new List<ChecklistTemplate>();

    public virtual ICollection<ContentPage> ContentPages { get; set; } = new List<ContentPage>();

    public virtual ICollection<DocumentTemplate> DocumentTemplates { get; set; } = new List<DocumentTemplate>();

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual ICollection<RoomType> RoomTypes { get; set; } = new List<RoomType>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<ServiceCategory> ServiceCategories { get; set; } = new List<ServiceCategory>();
}
