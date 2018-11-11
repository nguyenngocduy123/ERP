using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Models
{
    public class BreadcrumbViewModel
    {
        public string Area { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Name { get; set; }

        public int? ParrentId { get; set; }

        public string Url { get; set; }

        public BreadcrumbViewModel Parent { get; set; }
    }
}