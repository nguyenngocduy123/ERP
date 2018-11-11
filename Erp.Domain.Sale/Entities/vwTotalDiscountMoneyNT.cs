using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class vwTotalDiscountMoneyNT
    {
        public vwTotalDiscountMoneyNT()
        {
            
        }

        public int Id { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedUserId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> AssignedUserId { get; set; }

        public Nullable<int> DrugStoreId { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<int> UserManagerId { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> QuantityDay { get; set; }
        public decimal? PercentDecrease { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DecreaseAmount  { get; set; }
        public decimal? RemainingAmount { get; set; }
        public string Status { get; set; }
       // vw
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string DrugStoreName { get; set; }
        public string DrugStoreCode { get; set; }
        public string UserManagerName { get; set; }
        public string UserManagerCode { get; set; }

        public string DistrictId { get; set; }
        public string CityId { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string Address { get; set; }
        public string WardName { get; set; }
    }
}
