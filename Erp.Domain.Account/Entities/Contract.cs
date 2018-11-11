using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities
{
    public class Contract
    {
        public Contract()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public string Type { get; set; }
        public string Code { get; set; }
        public string Place { get; set; }
        public Nullable<System.DateTime> EffectiveDate { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string TemplateFile { get; set; }
        public Nullable<int> InfoPartyAId { get; set; }
        public string IdTypeContract { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string TransactionCode { get; set; }
        public string Status { get; set; }
    }
}
