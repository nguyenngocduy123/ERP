using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Erp.BackOffice.Areas.Administration.Models
{
    public class ListPagesModel
    {
        public ListPagesModel()
        {
            //PageMenus = new List<PageMenu>();
            //UserPages = new List<UserPage>();
            //UserTypePages = new List<UserTypePage>();
        }

        public int Id { get; set; }

        [Display(Name = "ActionName", ResourceType = typeof(Wording))]
        public string ActionName { get; set; }

        [Display(Name = "ControllerName", ResourceType = typeof(Wording))]
        public string ControllerName { get; set; }
        public string Url { get; set; }

        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "ValueInvalid", ErrorMessage = null)]
        [Display(Name = "OrderNo", ResourceType = typeof(Wording))]
        public int? OrderNo { get; set; }

        public bool? Status { get; set; }

        [Display(Name = "IsVisible", ResourceType = typeof(Wording))]
        public bool? IsVisible { get; set; }

        [Display(Name = "CssClassIcon", ResourceType = typeof(Wording))]
        public string CssClassIcon { get; set; }

        public int ParentId { get; set; }

        public string MenuItemName { get; set; }

        //public virtual ICollection<PageMenu> ParentPageMenus { get; set; }

        //public ICollection<PageMenu> PageMenus { get; set; }

        //public ICollection<UserPage> UserPages { get; set; }

        //public ICollection<UserTypePage> UserTypePages { get; set; }

        [Display(Name = "Language", ResourceType = typeof(Wording))]
        public List<string> ListLanguage { get; set; }

        //public IEnumerable<Language> Language { get; set; }
    }
}