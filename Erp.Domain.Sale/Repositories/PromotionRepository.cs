using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class PromotionRepository : GenericRepository<ErpSaleDbContext, Promotion>, IPromotionRepository
    {
        public PromotionRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        
        public IQueryable<Promotion> GetAllPromotion()
        {
            return Context.Promotion.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        
        public Promotion GetPromotionById(int Id)
        {
            return Context.Promotion.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }


        public void InsertPromotion(Promotion Promotion, List<PromotionDetail> listDetail)
        {
            Context.Promotion.Add(Promotion);
            Context.Entry(Promotion).State = EntityState.Added;
            Context.SaveChanges();


            for(int i=0; i < listDetail.Count; i++)
            {
                listDetail[i].PromotionId = Promotion.Id;
                InsertPromotionDetail(listDetail[i]);
            }
        }

        
        public void DeletePromotion(int Id)
        {
            Promotion deletedPromotion = GetPromotionById(Id);
            Context.Promotion.Remove(deletedPromotion);
            Context.Entry(deletedPromotion).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        
        public void DeletePromotionRs(int Id)
        {
            Promotion deletePromotionRs = GetPromotionById(Id);
            deletePromotionRs.IsDeleted = true;
            UpdatePromotion(deletePromotionRs);
        }

        
        public void UpdatePromotion(Promotion Promotion)
        {
            Context.Entry(Promotion).State = EntityState.Modified;
            Context.SaveChanges();
        }


        //detail
        public IQueryable<PromotionDetail> GetAllPromotionDetailBy(int PromotionId)
        {
            return Context.PromotionDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false) && item.PromotionId == PromotionId);
        }

        public PromotionDetail GetPromotionDetailById(int Id)
        {
            return Context.PromotionDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false) && item.Id == Id).FirstOrDefault();
        }

        public void InsertPromotionDetail(PromotionDetail PromotionDetail)
        {
            Context.PromotionDetail.Add(PromotionDetail);
            Context.Entry(PromotionDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeletePromotionDetail(int Id)
        {
            PromotionDetail deletedPromotion = GetPromotionDetailById(Id);
            Context.PromotionDetail.Remove(deletedPromotion);
            Context.Entry(deletedPromotion).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void DeletePromotionDetailRs(int Id)
        {
            Promotion deletePromotionRs = GetPromotionById(Id);
            deletePromotionRs.IsDeleted = true;
            UpdatePromotion(deletePromotionRs);
        }

        public void UpdatePromotionDetail(PromotionDetail PromotionDetail)
        {
            Context.Entry(PromotionDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
