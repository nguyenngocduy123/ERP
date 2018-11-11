using Erp.BackOffice.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Areas.Administration.Models
{
    public class PageMenuViewModel
    {
        public int Id { get; set; }
        public string LanguageId { get; set; }

        [Display(Name = "PageId", ResourceType = typeof(Wording))]
        public int? PageId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Error))]
        [Display(Name = "Name", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "Url", ResourceType = typeof(Wording))]
        public string Url { get; set; }

        [Display(Name = "OrderNo", ResourceType = typeof(Wording))]
        public Nullable<int> OrderNo { get; set; }

        [Display(Name = "CssClassIcon", ResourceType = typeof(Wording))]
        public string CssClassIcon { get; set; }

        [Display(Name = "ParentMenuName", ResourceType = typeof(Wording))]
        public Nullable<int> ParentId { get; set; }

        [Display(Name = "IsVisible", ResourceType = typeof(Wording))]
        public Nullable<bool> IsVisible { get; set; }

        [Display(Name = "AreaName", ResourceType = typeof(Wording))]
        public string AreaName { get; set; }

        [Display(Name = "PageUrl", ResourceType = typeof(Wording))]
        public string PageUrl { get; set; }
        public Nullable<bool> IsDashboard { get; set; }
        public string DashboardView { get; set; }

        public int? CountNotifications { get; set; }
    }
}