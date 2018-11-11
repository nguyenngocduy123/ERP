using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class PhysicalInventoryDetail
    {
        public PhysicalInventoryDetail()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public int PhysicalInventoryId { get; set; }
        public int ProductId { get; set; }
        public int QuantityInInventory { get; set; }
        public int QuantityRemaining { get; set; }
        public int QuantityDiff { get; set; }
        public string Note { get; set; }
        public string ReferenceVoucher { get; set; }
        public string LoCode { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
    }
}
