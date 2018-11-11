using Erp.BackOffice.App_GlobalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Administration.Models
{
    public class ColumnFieldViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DataType { get; set; }

        public string Length { get; set; }
    }
}