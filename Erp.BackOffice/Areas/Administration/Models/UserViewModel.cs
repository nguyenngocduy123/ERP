using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Erp.BackOffice.Areas.Administration.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int? LoginFailedCount { get; set; }
        public string UserTypeName { get; set; }
        public UserStatus Status { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ListHouse { get; set; }
    }
}