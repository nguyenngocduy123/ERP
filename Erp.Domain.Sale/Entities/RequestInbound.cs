using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class RequestInbound
    {
        public RequestInbound()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public string Code { get; set; }
        public decimal? TotalAmount { get; set; }
        public Nullable<int> WarehouseSourceId { get; set; }
        public Nullable<int> WarehouseDestinationId { get; set; }
        public string Status { get; set; }
        public string BarCode { get; set; }
        public string CancelReason { get; set; }
        public string Note { get; set; }
        public Nullable<int> InboundId { get; set; }
        public Nullable<int> OutboundId { get; set; }
        public string ShipName { get; set; }
        public string ShipPhone { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<bool> Error { get; set; }
        public Nullable<int> ErrorQuantity { get; set; }
    }
}
