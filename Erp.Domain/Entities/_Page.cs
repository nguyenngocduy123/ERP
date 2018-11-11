using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Entities
{
    public partial class Page
    {
        public Page()
        {
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string AreaName { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Url { get; set; }
        public Nullable<int> OrderNo { get; set; }
        public Nullable<bool> Status { get; set; }
        public string CssClassIcon { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsVisible { get; set; }
        public Nullable<bool> IsView { get; set; }
        public Nullable<bool> IsAdd { get; set; }
        public Nullable<bool> IsEdit { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsImport { get; set; }
        public Nullable<bool> IsExport { get; set; }
        public Nullable<bool> IsPrint { get; set; }
    }
}
