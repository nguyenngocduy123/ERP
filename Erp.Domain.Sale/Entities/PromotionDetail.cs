using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Domain.Sale.Entities
{
    public class PromotionDetail
    {
        public PromotionDetail()
        {
            
        }

        public int Id { get; set; }

        public Nullable<bool> IsDeleted { get; set; }

        public Nullable<int> PromotionId { get; set; }

        public Nullable<int> ProductId { get; set; }
        public int? QuantityFor { get; set; }

        public double? PercentValue { get; set; }

        public bool? IsAll { get; set; }

        public string CategoryCode { get; set; }

        public string Type { get; set; }

    }
}
