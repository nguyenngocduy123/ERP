using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IPromotionRepository
    {
        IQueryable<Promotion> GetAllPromotion();

        Promotion GetPromotionById(int Id);

        void InsertPromotion(Promotion Promotion, List<PromotionDetail> listDetail);

        void DeletePromotion(int Id);

        void DeletePromotionRs(int Id);

        void UpdatePromotion(Promotion Promotion);

        // detail

        IQueryable<PromotionDetail> GetAllPromotionDetailBy(int PromotionId);

        PromotionDetail GetPromotionDetailById(int Id);

        void InsertPromotionDetail(PromotionDetail PromotionDetail);

        void DeletePromotionDetail(int Id);

        void DeletePromotionDetailRs(int Id);

        void UpdatePromotionDetail(PromotionDetail PromotionDetail);
    }
}
