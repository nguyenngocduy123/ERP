using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwTaxIncomePersonDetail
    {
        public vwTaxIncomePersonDetail()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }
        public int? StaffId { get; set; }

        public Nullable<int> TaxIncomePersonId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string PositionName { get; set; }

        public string BranchName { get; set; }

        public int? Sale_BranchId { get; set; }

        public bool? Gender { get; set; }

        public string GenderName { get; set; }

        public string Email { get; set; }

        public string IdCardNumber { get; set; }

        public string CountryId { get; set; }

        public string ProvinceName { get; set; }

        public string DistrictName { get; set; }

        public string WardName { get; set; }

    }
}
