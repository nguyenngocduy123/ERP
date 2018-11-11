using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class LoyaltyPointRepository : GenericRepository<ErpSaleDbContext, LoyaltyPoint>, ILoyaltyPointRepository
    {
        public LoyaltyPointRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all LoyaltyPoint
        /// </summary>
        /// <returns>LoyaltyPoint list</returns>
        public IQueryable<LoyaltyPoint> GetAllLoyaltyPoint()
        {
            return Context.LoyaltyPoint.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get LoyaltyPoint information by specific id
        /// </summary>
        /// <param name="LoyaltyPointId">Id of LoyaltyPoint</param>
        /// <returns></returns>
        public LoyaltyPoint GetLoyaltyPointById(int Id)
        {
            return Context.LoyaltyPoint.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert LoyaltyPoint into database
        /// </summary>
        /// <param name="LoyaltyPoint">Object infomation</param>
        public void InsertLoyaltyPoint(LoyaltyPoint LoyaltyPoint)
        {
            Context.LoyaltyPoint.Add(LoyaltyPoint);
            Context.Entry(LoyaltyPoint).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete LoyaltyPoint with specific id
        /// </summary>
        /// <param name="Id">LoyaltyPoint Id</param>
        public void DeleteLoyaltyPoint(int Id)
        {
            LoyaltyPoint deletedLoyaltyPoint = GetLoyaltyPointById(Id);
            Context.LoyaltyPoint.Remove(deletedLoyaltyPoint);
            Context.Entry(deletedLoyaltyPoint).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a LoyaltyPoint with its Id and Update IsDeleted IF that LoyaltyPoint has relationship with others
        /// </summary>
        /// <param name="LoyaltyPointId">Id of LoyaltyPoint</param>
        public void DeleteLoyaltyPointRs(int Id)
        {
            LoyaltyPoint deleteLoyaltyPointRs = GetLoyaltyPointById(Id);
            deleteLoyaltyPointRs.IsDeleted = true;
            UpdateLoyaltyPoint(deleteLoyaltyPointRs);
        }

        /// <summary>
        /// Update LoyaltyPoint into database
        /// </summary>
        /// <param name="LoyaltyPoint">LoyaltyPoint object</param>
        public void UpdateLoyaltyPoint(LoyaltyPoint LoyaltyPoint)
        {
            Context.Entry(LoyaltyPoint).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
