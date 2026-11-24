using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain.Enums
{
    public enum StockReservationStatus 
    {
        Active = 1,
        PartiallyFulfilled = 2,
        Fulfilled = 3,
        Released = 4,
        Cancelled = 5
    }
}
