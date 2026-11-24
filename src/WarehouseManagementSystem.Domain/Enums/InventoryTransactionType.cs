using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain.Enums
{
    public enum InventoryTransactionType
    {
        GoodsReceipt = 1,
        Shipment = 2,
        StockTransfer = 3,
        Adjustment = 4,
        Return = 5
    }
}
