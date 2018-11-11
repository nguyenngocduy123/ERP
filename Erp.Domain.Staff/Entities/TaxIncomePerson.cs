using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class TaxIncomePerson
    {
        public TaxIncomePerson()
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
        public string Code { get; set; }
        public string GeneralTaxationId { get; set; }
        public string GeneralManageId { get; set; }
        public DateTime? StaffStartDate { get; set; }
        public DateTime? StaffEndDate { get; set; }

    }
}
