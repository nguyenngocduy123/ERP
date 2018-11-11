using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities
{
    public class PaymentDetail
    {
        public PaymentDetail()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public string Name { get; set; }

        public Nullable<int> PaymentId { get; set; }
        public string MaChungTuGoc { get; set; }
        public string LoaiChungTuGoc { get; set; }
        public decimal? Amount { get; set; }
        public string GroupName { get; set; }
    }
}
