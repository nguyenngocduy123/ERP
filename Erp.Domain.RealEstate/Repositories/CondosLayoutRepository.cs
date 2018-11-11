using Erp.Domain.RealEstate.Entities;
using Erp.Domain.RealEstate.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.RealEstate.Repositories
{
    public class CondosLayoutRepository : GenericRepository<ErpRealEstateDbContext, CondosLayout>, ICondosLayoutRepository
    {
        public CondosLayoutRepository(ErpRealEstateDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all CondosLayout
        /// </summary>
        /// <returns>CondosLayout list</returns>
        public IQueryable<CondosLayout> GetAllCondosLayout()
        {
            return Context.CondosLayout.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get CondosLayout information by specific id
        /// </summary>
        /// <param name="CondosLayoutId">Id of CondosLayout</param>
        /// <returns></returns>
        public CondosLayout GetCondosLayoutById(int Id)
        {
            return Context.CondosLayout.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert CondosLayout into database
        /// </summary>
        /// <param name="CondosLayout">Object infomation</param>
        public void InsertCondosLayout(CondosLayout CondosLayout)
        {
            Context.CondosLayout.Add(CondosLayout);
            Context.Entry(CondosLayout).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete CondosLayout with specific id
        /// </summary>
        /// <param name="Id">CondosLayout Id</param>
        public void DeleteCondosLayout(int Id)
        {
            CondosLayout deletedCondosLayout = GetCondosLayoutById(Id);
            Context.CondosLayout.Remove(deletedCondosLayout);
            Context.Entry(deletedCondosLayout).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a CondosLayout with its Id and Update IsDeleted IF that CondosLayout has relationship with others
        /// </summary>
        /// <param name="CondosLayoutId">Id of CondosLayout</param>
        public void DeleteCondosLayoutRs(int Id)
        {
            CondosLayout deleteCondosLayoutRs = GetCondosLayoutById(Id);
            deleteCondosLayoutRs.IsDeleted = true;
            UpdateCondosLayout(deleteCondosLayoutRs);
        }

        /// <summary>
        /// Update CondosLayout into database
        /// </summary>
        /// <param name="CondosLayout">CondosLayout object</param>
        public void UpdateCondosLayout(CondosLayout CondosLayout)
        {
            Context.Entry(CondosLayout).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
