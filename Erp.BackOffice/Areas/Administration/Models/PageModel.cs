using Erp.BackOffice.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Areas.Administration.Models
{
    public class PageModel
    {
        //[Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "AreaName", ResourceType = typeof(Wording))]
        public string AreaName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "ActionName", ResourceType = typeof(Wording))]
        public string ActionName { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "ControllerName", ResourceType = typeof(Wording))]
        public string ControllerName { get; set; }

        [Display(Name = "Url", ResourceType = typeof(Wording))]
        public string Url { get; set; }

        [Display(Name = "OrderNo", ResourceType = typeof(Wording))]
        public Nullable<int> OrderNo { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Wording))]
        public bool Status { get; set; }

        [Display(Name = "CssClassIcon", ResourceType = typeof(Wording))]
        public string CssClassIcon { get; set; }

        [Display(Name = "ParentMenuName", ResourceType = typeof(Wording))]
        public Nullable<int> ParentId { get; set; }

        [Display(Name = "IsDeleted", ResourceType = typeof(Wording))]
        public bool IsDeleted { get; set; }

        [Display(Name = "IsVisible", ResourceType = typeof(Wording))]
        public bool IsVisible { get; set; }

        [Display(Name = "View", ResourceType = typeof(Wording))]
        public bool IsView { get; set; }

        [Display(Name = "Add", ResourceType = typeof(Wording))]
        public bool IsAdd { get; set; }

        [Display(Name = "Edit", ResourceType = typeof(Wording))]
        public bool IsEdit { get; set; }

        [Display(Name = "Delete", ResourceType = typeof(Wording))]
        public bool IsDelete { get; set; }

        [Display(Name = "Import", ResourceType = typeof(Wording))]
        public bool IsImport { get; set; }

        [Display(Name = "Export", ResourceType = typeof(Wording))]
        public bool IsExport { get; set; }

        [Display(Name = "Print", ResourceType = typeof(Wording))]
        public bool IsPrint { get; set; }

        ////[Required]
        //public int Id { get; set; }

        ////[Display(Name = "")]
        //public string ActionName { get; set; }

        ////[Display(Name = "")]
        //public string ControllerName { get; set; }

        ////[Display(Name = "")]
        //public string Url { get; set; }

        ////[Display(Name = "")]
        //public Nullable<int> OrderNo { get; set; }

        ////[Display(Name = "")]
        //public bool Status { get; set; }

        ////[Display(Name = "")]
        //public string CssClassIcon { get; set; }
    }
}