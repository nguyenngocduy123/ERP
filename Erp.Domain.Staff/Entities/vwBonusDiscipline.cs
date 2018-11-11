using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class vwBonusDiscipline
    {
        public vwBonusDiscipline()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string Category { get; set; }
        public string Formality { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> DayDecision { get; set; }
        public Nullable<System.DateTime> DayEffective { get; set; }
        public string PlaceDecisionsName { get; set; }
        public Nullable<int> PlaceDecisions { get; set; }
        public string Note { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }
        public string CodeName { get; set; }
        public string BranchName { get; set; }
        public string Position { get; set; }
        public string Staff_DepartmentId { get; set; }
        public Nullable<int> Sale_BranchId { get; set; }
        public Nullable<int> BranchDepartmentId { get; set; }

        public string ProfileImage { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string StaffCode { get; set; }
        public string CreatedUserName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public Nullable<int> PlaceDecisions_Branch { get; set; }
        public Nullable<int> Money { get; set; }
    }
}
