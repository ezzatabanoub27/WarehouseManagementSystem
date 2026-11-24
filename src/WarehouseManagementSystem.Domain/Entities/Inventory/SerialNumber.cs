using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Entities.MasterData;
using WarehouseManagementSystem.Domain.Entities.Organizations;
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Domain.Entities.Inventory
{
    public class SerialNumber :AuditableEntity
    {
        public Guid ProductId { get; set; }

        public Guid? LotId { get; set; }

        public string SerialNumberValue { get; set; } = string.Empty;

        public Guid? CurrentLocationId { get; set; }

        public SerialNumberStatus Status { get; set; }= SerialNumberStatus.InStock;

        public DateTime? ReceivedAtUtc { get; set; }

        public Product Product { get; set; } = null!;
        public Lot? Lot { get; set; }
        public Location? CurrentLocation { get; set; }
    }
}
