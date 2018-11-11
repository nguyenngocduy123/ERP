using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class ListTimekeepingViewModel
    {

        public Nullable<int> StaffId { get; set; }

        public DateTime? HoursIn { get; set; }

        public DateTime? HoursOut { get; set; }

     
    }
}