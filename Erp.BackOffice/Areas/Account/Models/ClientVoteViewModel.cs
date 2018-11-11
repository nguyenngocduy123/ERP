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
    public class ClientVoteViewModel
    {
        public CustomerViewModel Customer { get; set; }
        public ProductInvoiceViewModel ProductInvoice { get; set; }
        public UsingServiceLogDetailViewModel UsingServiceLogDetail { get; set; }
        public List<QuestionViewModel> ListQuestion { get; set; }
    }
}