using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Erp.BackOffice.Sale.Models;

namespace Erp.BackOffice.Models
{
    public class PageSetting
    {
        public string PageTitle { get; set; }
        public string ModuleName { get; set; }
        public string ActionName { get; set; }
        public bool DisplaySearchPanel { get; set; }
        public bool IsPopup { get; set; }
        public bool DisplayBackButton { get; set; }
        public bool HideClearButton { get; set; }
        public bool AdvancedSearch { get; set; }
        public string SearchOjectAttr { get; set; }
        public string SearchButtonText { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
    }
}