using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Staff.Models
{
    public class StaffsTaxModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public string Birthday { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Phone { get; set; }
        public string IdCardNumber { get; set; }
      
        public Nullable<int> BranchId { get; set; }
        public string PositionName { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string WardName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string BranchName { get;  set; }
        public string ContryName { get;  set; }
        public string Email { get;  set; }
    }
}