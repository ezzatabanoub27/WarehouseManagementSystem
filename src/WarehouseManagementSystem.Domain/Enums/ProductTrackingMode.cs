using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain.Enums
{
    public enum ProductTrackingMode
    {
        None = 0,
        Batch = 1,
        Serial = 2,
        BatchAndSerial = 3
    }
}
