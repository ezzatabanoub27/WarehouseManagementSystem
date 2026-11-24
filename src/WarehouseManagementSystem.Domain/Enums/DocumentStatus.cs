using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain.Enums
{
    public enum DocumentStatus
    {
        Draft = 1,
        PendingApproval = 2,
        Approved = 3,
        Posted = 4,
        PartiallyCompleted = 5,
        Completed = 6,
        Cancelled = 7
    }
}
