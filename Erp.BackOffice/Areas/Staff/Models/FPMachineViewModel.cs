using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class FPMachineViewModel
    {
        public int Id { get; set; }

        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "CreatedUser", ResourceType = typeof(Wording))]
        public int? CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(Wording))]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "ModifiedUser", ResourceType = typeof(Wording))]
        public int? ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }

        [Display(Name = "ModifiedDate", ResourceType = typeof(Wording))]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "AssignedUserId", ResourceType = typeof(Wording))]
        public int? AssignedUserId { get; set; }
        public string AssignedUserName { get; set; }

        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [StringLength(100, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "StringError", ErrorMessage = null)]
        [Display(Name = "Name", ResourceType = typeof(Wording))]       
        public string Ten_may { get; set; }

        [Display(Name = "Ten_may_tinh", ResourceType = typeof(Wording))]
        public string Ten_may_tinh { get; set; }

        [Display(Name = "Loai_ket_noi", ResourceType = typeof(Wording))]
        public string Loai_ket_noi { get; set; }
        public Nullable<int> Ma_loai_ket_noi { get; set; }
        public string ID_Ket_noi_COM { get; set; }
        public string ID_Ket_noi_IP { get; set; }
        public Nullable<int> Cong_COM { get; set; }
        [Display(Name = "Dia_chi_IP", ResourceType = typeof(Wording))]
        public string Dia_chi_IP { get; set; }
        [Display(Name = "Toc_do_truyen", ResourceType = typeof(Wording))]
        public string Toc_do_truyen { get; set; }
        [Display(Name = "Port", ResourceType = typeof(Wording))]
        public Nullable<int> Port { get; set; }
        public Nullable<int> Loaimay { get; set; }
        public Nullable<int> Passwd { get; set; }
        [Display(Name = "Url", ResourceType = typeof(Wording))]
        public string url { get; set; }
        [Display(Name = "useurl", ResourceType = typeof(Wording))]
        public bool? useurl { get; set; }
        public int? AutoID { get; set; }
        public string GetDataSchedule { get; set; }
        public string TeamviewerID { get; set; }
        public string TeamviewerPassword { get; set; }
        [Display(Name = "Note", ResourceType = typeof(Wording))]
        public string Note { get; set; }
        public int count_van_tay { get; set; }
        public int total_van_tay { get; set; }
    }
}