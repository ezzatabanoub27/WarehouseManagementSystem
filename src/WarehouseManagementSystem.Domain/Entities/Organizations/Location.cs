using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;
using WarehouseManagementSystem.Domain.Enums;

namespace WarehouseManagementSystem.Domain.Entities.Organizations
{
    public class Location : AuditableEntity
    {
        public Guid WarehouseId { get; set; }

        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public LocationType LocationType { get; set; }
        public bool IsActive { get; set; } = true;

        public Warehouse Warehouse { get; set; } = null!;
    }
}
