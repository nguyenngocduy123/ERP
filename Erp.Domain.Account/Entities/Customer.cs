using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Account.Entities
{
    public class Customer
    {
        public Customer()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string Code { get; set; }
        public string CompanyName { get; set; }
        public int? BranchId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public string WardId { get; set; }
        public string DistrictId { get; set; }
        public string CityId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string CardCode { get; set; }
        public int? Point { get; set; }
        public string SearchFullName { get; set; }
        public string Image { get; set; }
        public string IdCardNumber { get; set; }
        public Nullable<System.DateTime> IdCardDate { get; set; }
        public string IdCardIssued { get; set; }
        public string CustomerType { get; set; }
        public string PositionCode { get; set; }
        public int? UserId { get; set; }
        public int? ManagerStaffId { get; set; }
    }
}
