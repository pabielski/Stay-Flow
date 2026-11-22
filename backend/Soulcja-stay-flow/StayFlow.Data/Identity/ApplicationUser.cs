using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;


namespace StayFlow.Data.Identity
{

    public class ApplicationUser : IdentityUser<long>
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public long? HotelId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
