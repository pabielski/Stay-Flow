using Microsoft.AspNetCore.Identity;

namespace StayFlow.Data.Identity;

public class ApplicationRole : IdentityRole<long>
{
    public string? Description { get; set; }
    public bool IsSystem { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}