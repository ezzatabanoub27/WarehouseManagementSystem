using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;

namespace WarehouseManagementSystem.Domain.Entities.Organizations
{
    public class Warehouse:AuditableEntity
    {
        public Guid BranchId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;

        public Branch Branch { get; set; } = null!;

        public ICollection<Location> Locations { get; set; }= new List<Location>();
    }
}
