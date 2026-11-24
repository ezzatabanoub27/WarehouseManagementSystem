using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;

namespace WarehouseManagementSystem.Domain.Entities.MasterData
{
    public class Unit : AuditableEntity
    {

        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public ICollection<Product> Products { get; set; }= new List<Product>();
    }
}
