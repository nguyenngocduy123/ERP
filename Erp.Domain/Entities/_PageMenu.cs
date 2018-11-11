using System;
using System.Collections.Generic;

namespace Erp.Domain.Entities
{
    public partial class PageMenu
    {
        public int Id { get; set; }
        public string LanguageId { get; set; }
        public Nullable<int> PageId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public string CssClassIcon { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<bool> IsVisible { get; set; }
        public Nullable<bool> IsDashboard { get; set; }
        public string DashboardView { get; set; }
    }
}
