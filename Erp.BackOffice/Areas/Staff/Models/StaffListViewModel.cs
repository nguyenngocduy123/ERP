using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Staff.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class StaffListViewModel: StaffsViewModel
    {
        public System.Linq.IQueryable<StaffsViewModel> ListStaffsViewModel { get; set; }
    }
}