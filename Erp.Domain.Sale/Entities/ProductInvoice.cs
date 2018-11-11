using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class ProductInvoice
    {
        public ProductInvoice()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string Code { get; set; }
        public decimal TotalAmount { get; set; }
        public double TaxFee { get; set; }
        public double Discount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string ShipName { get; set; }
        public string ShipPhone { get; set; }
        public string ShipAddress { get; set; }
        public string ShipWardId { get; set; }
        public string ShipDistrictId { get; set; }
        public string ShipCityId { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public bool IsArchive { get; set; }
        public int BranchId { get; set; }
        public string PaymentMethod { get; set; }
        public string CancelReason { get; set; }
        public int? SalerId { get; set; }
        //public int? StaffId { get; set; }
        public string CodeInvoiceRed { get; set; }
        public bool IsReturn { get; set; }
        public Nullable<System.DateTime> NextPaymentDate { get; set; }
        public int? StaffCreateId { get; set; }

        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }
        public string Type { get; set; }
        public decimal FixedDiscount { get; set; }
        public decimal IrregularDiscount { get; set; }

        public Nullable<int> DrugStoreUserId { get; set; }
        public Nullable<int> AccountancyUserId { get; set; }
        public Nullable<System.DateTime> DrugStoreDate { get; set; }
        public Nullable<System.DateTime> AccountancyDate { get; set; }
    }
}
