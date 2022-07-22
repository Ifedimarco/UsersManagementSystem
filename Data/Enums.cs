using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgolePayUsersManagementSystem.Data
{
    public enum Roles
    {
        User = 0,
        Aggregator = 1,
        Agent = 2,
        Admin = 3,
    }

    public enum UserStatus
    {
        Active = 0,
        Deactivated = 1,
    }

   public enum UpgradeRequestStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2,
    }

}
