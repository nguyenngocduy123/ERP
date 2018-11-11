using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwReportCustomer
    {
        public int Id { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        public string Code { get; set; }

        public string Note { get; set; }

        public string CompanyName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public bool? Gender { get; set; }

        public string Address { get; set; }

        public string DistrictId { get; set; }

        public string WardId { get; set; }

        public string CityId { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public int? Point { get; set; }

        public int? BranchId { get; set; }

        public int? QtyContact { get; set; }

        public int? QtyInvoice { get; set; }

        public int? QtySaleOrder { get; set; }
    }
}
