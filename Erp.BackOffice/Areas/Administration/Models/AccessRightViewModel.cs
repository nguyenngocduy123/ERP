using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Areas.Administration.Models
{
    public class AccessRightViewModel
    {
        public List<UserType_AccessRightPageViewModel> UserType_AccessRightPageViewModel { get; set; }
        public List<vwPage> ListPages { get; set; }
        public List<UserType> UserTypes { get; set; }
    }

    public class UserType_AccessRightPageViewModel
    {
        public int UserTypeId { get; set; }
        public List<int> ListAccessRightPage { get; set; }
    }

    public class AccessRightCreateModel
    {
        public string[] UserTypePage { get; set; }
        public string[] UserPage { get; set; }
        public List<string> UserIds { get; set; }
    }
}