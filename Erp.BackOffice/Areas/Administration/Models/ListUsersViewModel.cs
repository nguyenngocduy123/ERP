using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Erp.Domain.Entities;

namespace Erp.BackOffice.Administration.Models
{
    public class ListUsersViewModel
    {
        public IEnumerable<vwUsers> Users { get; set; }
    }
}