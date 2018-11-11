using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class BonusDisciplineRepository : GenericRepository<ErpStaffDbContext, BonusDiscipline>, IBonusDisciplineRepository
    {
        public BonusDisciplineRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all BonusDiscipline
        /// </summary>
        /// <returns>BonusDiscipline list</returns>
        public IQueryable<BonusDiscipline> GetAllBonusDiscipline()
        {
            return Context.BonusDiscipline.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwBonusDiscipline> GetAllvwBonusDiscipline()
        {
            return Context.vwBonusDiscipline.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get BonusDiscipline information by specific id
        /// </summary>
        /// <param name="BonusDisciplineId">Id of BonusDiscipline</param>
        /// <returns></returns>
        public BonusDiscipline GetBonusDisciplineById(int? Id)
        {
            return Context.BonusDiscipline.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwBonusDiscipline GetvwBonusDisciplineById(int? Id)
        {
            return Context.vwBonusDiscipline.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert BonusDiscipline into database
        /// </summary>
        /// <param name="BonusDiscipline">Object infomation</param>
        public void InsertBonusDiscipline(BonusDiscipline BonusDiscipline)
        {
            Context.BonusDiscipline.Add(BonusDiscipline);
            Context.Entry(BonusDiscipline).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete BonusDiscipline with specific id
        /// </summary>
        /// <param name="Id">BonusDiscipline Id</param>
        public void DeleteBonusDiscipline(int Id)
        {
            BonusDiscipline deletedBonusDiscipline = GetBonusDisciplineById(Id);
            Context.BonusDiscipline.Remove(deletedBonusDiscipline);
            Context.Entry(deletedBonusDiscipline).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a BonusDiscipline with its Id and Update IsDeleted IF that BonusDiscipline has relationship with others
        /// </summary>
        /// <param name="BonusDisciplineId">Id of BonusDiscipline</param>
        public void DeleteBonusDisciplineRs(int Id)
        {
            BonusDiscipline deleteBonusDisciplineRs = GetBonusDisciplineById(Id);
            deleteBonusDisciplineRs.IsDeleted = true;
            UpdateBonusDiscipline(deleteBonusDisciplineRs);
        }

        /// <summary>
        /// Update BonusDiscipline into database
        /// </summary>
        /// <param name="BonusDiscipline">BonusDiscipline object</param>
        public void UpdateBonusDiscipline(BonusDiscipline BonusDiscipline)
        {
            Context.Entry(BonusDiscipline).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
