using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.Domain.Common
{
    public abstract  class AuditableEntity:BaseEntity
    {
        public DateTime CreatedAtUtc { get; private set; }

        public string? CreatedBy { get; private set; }

        public DateTime? LastModifiedAtUtc { get; private set; }

        public string? LastModifiedBy { get; private set; }

        protected AuditableEntity()
        {
            CreatedAtUtc = DateTime.UtcNow;
        }

        public void SetCreationAudit(string? createdBy)
        {
            CreatedBy = createdBy;
        }

        public void SetModificationAudit(string? modifiedBy)
        {
            LastModifiedAtUtc = DateTime.UtcNow;
            LastModifiedBy = modifiedBy;
        }


    }
}
