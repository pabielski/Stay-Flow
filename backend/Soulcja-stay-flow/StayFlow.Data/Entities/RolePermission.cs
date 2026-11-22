using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class RolePermission
{
    public long Id { get; set; }

    public long RoleId { get; set; }

    public long PermissionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Permission Permission { get; set; } = null!;
}
