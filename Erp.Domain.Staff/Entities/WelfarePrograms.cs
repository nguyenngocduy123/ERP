using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Staff.Entities
{
    public class WelfarePrograms
    {
        public WelfarePrograms()
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
        public Nullable<System.DateTime> ProvideStartDate { get; set; }
        public Nullable<System.DateTime> ProvideEndDate { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> TotalEstimatedCost { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string Purpose { get; set; }
        public string Formality { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public Nullable<System.DateTime> RegistrationStartDate { get; set; }
        public Nullable<System.DateTime> RegistrationEndDate { get; set; }
        public Nullable<System.DateTime> ImplementationStartDate { get; set; }
        public Nullable<System.DateTime> ImplementationEndDate { get; set; }
        public Nullable<int> MoneyStaff { get; set; }
        public Nullable<int> MoneyCompany { get; set; }
        public Nullable<int> TotalStaffCompany { get; set; }
        public Nullable<int> TotalMoneyStaff { get; set; }
        public Nullable<int> TotalMoneyCompany { get; set; }
        public Nullable<int> TotalStaffCompanyAll { get; set; }
        public Nullable<int> TotalActualCosts { get; set; }
        public string ApplicationObject { get; set; }

    }
}
