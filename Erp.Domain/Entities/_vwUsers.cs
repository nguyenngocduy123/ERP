using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities
{
    public class vwUsers
    {
        public vwUsers()
        {

        }

        public int Id { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string UserName { get; set; }
        public UserStatus Status { get; set; }
        public string UserCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Sex { get; set; }
        public int UserTypeId { get; set; }
        public Nullable<int> LoginFailedCount { get; set; }
        public Nullable<int> LoginFailedCount2 { get; set; }
        public Nullable<int> LoginFailedCount3 { get; set; }
        public Nullable<System.DateTime> LastChangedPassword { get; set; }
        public string UserTypeName { get; set; }
        //public Nullable<int> BranchId { get; set; }
        //public string BranchName { get; set; }
        //public string BranchCode { get; set; }
        public string ProfileImage { get; set; }
        public string WarehouseId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string ParentName { get; set; }
        public string DrugStore { get; set; }
        public string UserTypeCode { get; set; }
        public string DrugStoreCode { get; set; }
        public string DrugStoreName { get; set; }
        public Nullable<System.DateTime> CooperationDay { get; set; }
    }
}
