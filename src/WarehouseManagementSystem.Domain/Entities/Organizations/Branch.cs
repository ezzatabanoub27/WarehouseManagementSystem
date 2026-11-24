using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;

namespace WarehouseManagementSystem.Domain.Entities.Organizations
{
    public class Branch:AuditableEntity
    {
        public Guid CompanyId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
        public Company Company { get; set; } = null!;

        public ICollection<Warehouse> Warehouses { get; set; }= new List<Warehouse>();
    }
}
