using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class Staffs
    {
        public Staffs()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string IdCardNumber { get; set; }
        public Nullable<System.DateTime> IdCardDate { get; set; }
        public string IdCardIssued { get; set; }
        public string Ethnic { get; set; }
        public string Religion { get; set; }
        public string Email { get; set; }
        public string Literacy { get; set; }
        public string Technique { get; set; }
        public string Language { get; set; }
        //public Nullable<int> BranchDepartmentId { get; set; }
        public Nullable<int> PositionId { get; set; }
        public string MaritalStatus { get; set; }
        public string ProvinceId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }
        public string ProfileImage { get; set; }
        public string CountryId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Email2 { get; set; }
        public string Phone2 { get; set; }
        //public Nullable<int> CheckInOut_UserId { get; set; }
        //public Nullable<int> Sale_BranchId { get; set; }
        public Nullable<bool> IsWorking { get; set; }
        public string DrugStore { get; set; }
        public Nullable<int> StaffParentId { get; set; }
        public decimal? CommissionPercent { get; set; }
        public decimal? MinimumRevenue { get; set; }
    }
}
