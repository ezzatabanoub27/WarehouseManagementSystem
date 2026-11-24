using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseManagementSystem.Domain.Common;

namespace WarehouseManagementSystem.Domain.Entities.MasterData
{
    public class Category :AuditableEntity
    {

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }

        public ICollection<Category> SubCategories { get; set; }= new List<Category>();

        public ICollection<Product> Products { get; set; }= new List<Product>();

        public bool IsActive { get; set; } = true;
    }
}
