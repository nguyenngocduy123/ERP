using Erp.BackOffice.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Areas.Staff.Models
{
    public class StaffGeneralInfoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "NameStaff", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "CodeStaff", ResourceType = typeof(Wording))]
        public string Code { get; set; }

        [Display(Name = "BranchName", ResourceType = typeof(Wording))]
        public Nullable<int> BranchId { get; set; }
    }
}