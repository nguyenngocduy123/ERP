using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Sale.Models
{
    public class CommisionCustomerDetailViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<CommisionCustomerViewModel> DetailList { get; set; }
    }
}