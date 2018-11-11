using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwInternalNotifications
    {
        public vwInternalNotifications()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public string PlaceOfNotice  { get; set; }
        public string PlaceOfReceipt  { get; set; }
        public string Titles  { get; set; }
        public string Type { get; set; }
        public string Content  { get; set; }


        //public string DepartmentNotice { get; set; }
        //public string BranchNotice { get; set; }
        //public string DepartmentReceipt { get; set; }
        //public string BranchReceipt { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ModifiedUserName { get; set; }
        public string ProfileImage { get; set; }
        public Nullable<bool> Disable { get; set; }
        public Nullable<bool> Seen { get; set; }
        public string ActionName { get; set; }
        public string ModuleName { get; set; }
    }
}
