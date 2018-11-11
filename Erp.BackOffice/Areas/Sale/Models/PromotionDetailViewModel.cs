using Erp.BackOffice.App_GlobalResources;
using Erp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Erp.BackOffice.Sale.Models
{
    public class PromotionDetailViewModel
    {
        public int Id { get; set; }

        [Display(Name = "IsDeleted")]
        public bool? IsDeleted { get; set; }

        [Display(Name = "Promotion", ResourceType = typeof(Wording))]
        public int? PromotionId { get; set; }

        [Display(Name = "Product", ResourceType = typeof(Wording))]
        public int? ProductId { get; set; }

        [Display(Name = "PercentValue", ResourceType = typeof(Wording))]
        public double? PercentValue { get; set; }

        [Display(Name = "QuantityForPromotion", ResourceType = typeof(Wording))]
        public int? QuantityFor { get; set; }

        [Display(Name = "CategoryCode", ResourceType = typeof(Wording))]
        public string CategoryCode { get; set; }

        [Display(Name = "IsAll", ResourceType = typeof(Wording))]
        public bool? IsAll { get; set; }

        [Display(Name = "Type", ResourceType = typeof(Wording))]
        public string Type { get; set; }
    }
}