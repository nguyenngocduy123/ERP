using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities
{
    public class vwTransactionRelationship
    {
        public vwTransactionRelationship()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public string TransactionA { get; set; }
        public string TransactionB { get; set; }
        public string TransactionA_Module { get; set; }
        public string TransactionA_Name { get; set; }
        public string TransactionB_Module { get; set; }
        public string TransactionB_Name { get; set; }
    }
}
