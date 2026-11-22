using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class ContentPage
{
    public long Id { get; set; }

    public long HotelId { get; set; }

    public string PageType { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public int Version { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool IsPublished { get; set; }

    public DateTime? PublishedAt { get; set; }

    public long? CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<OnlineCheckIn> OnlineCheckIns { get; set; } = new List<OnlineCheckIn>();
}
