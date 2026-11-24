using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;

namespace WarehouseManagementSystem.Domain.Entities.Organizations
{
    public class Company:AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? TaxNumber { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}
