using Erp.BackOffice.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Administration.Models
{
    public class ColumnViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Label { get; set; }

        public string Type { get; set; }
        public bool IsSearch { get; set; }
        public bool IsTable { get; set; }
    }
}