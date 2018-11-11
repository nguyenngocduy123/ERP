using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities
{
    public class vwInfoPartyA
    {
        public vwInfoPartyA()
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

        public string NamePrefix { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string IdCardNumber { get; set; }
        public string IdCardIssued { get; set; }
        public Nullable<System.DateTime> IdCardDate { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string AccountNumber { get; set; }
        public string Bank { get; set; }
        public string TaxCode { get; set; }
        public string ProvinceId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string WardName { get; set; }
        public string CardIssuedName { get; set; }
        public string CompanyName { get; set; }
    }
}
