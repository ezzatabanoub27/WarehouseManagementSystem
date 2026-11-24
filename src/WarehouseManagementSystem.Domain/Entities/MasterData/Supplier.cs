using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;

namespace WarehouseManagementSystem.Domain.Entities.MasterData
{
    public class Supplier : AuditableEntity
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;


    }
}
