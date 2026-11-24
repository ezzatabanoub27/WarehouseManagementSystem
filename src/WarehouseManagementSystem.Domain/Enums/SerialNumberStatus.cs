using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain.Enums
{
    public enum SerialNumberStatus
    {
        InStock = 1,
        Reserved = 2,
        Shipped = 3,
        InTransit = 4,
        Damaged = 5,
        Returned = 6
    }
}
