using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Administration.Models
{
    public class EditViewField
    {
        public string LabelName { get; set; }
        public string FieldName { get; set; }
        public string EditControl { get; set; }
        public Nullable<bool> IsRequired { get; set; }
        public Nullable<int> OrderNo { get; set; }
    }
}