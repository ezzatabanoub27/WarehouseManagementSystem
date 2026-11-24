using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain.Enums
{
    public  enum StockAdjustmentReason
    {
        PhysicalCount = 1,
        Damaged = 2,
        Expired = 3,
        Lost = 4,
        Found = 5,
        OpeningBalance = 6,
        Other = 7
    }
}
