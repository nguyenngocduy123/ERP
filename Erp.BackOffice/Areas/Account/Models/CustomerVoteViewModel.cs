using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Erp.BackOffice.Sale.Models;
using Erp.BackOffice.Crm.Models;
namespace Erp.BackOffice.Account.Models
{
    public class CustomerVoteViewModel
    {
        public CustomerViewModel Customer { get; set; }
    }
}