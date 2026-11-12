using System;
using System.Collections.Generic;

namespace StayFlow.Data.Entities;

public partial class Permission
{
    public long Id { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
