using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class KPIItemRepository : GenericRepository<ErpStaffDbContext, KPIItem>, IKPIItemRepository
    {
        public KPIItemRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all KPIItem
        /// </summary>
        /// <returns>KPIItem list</returns>
        public IQueryable<KPIItem> GetAllKPIItem()
        {
            return Context.KPIItem.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get KPIItem information by specific id
        /// </summary>
        /// <param name="KPIItemId">Id of KPIItem</param>
        /// <returns></returns>
        public KPIItem GetKPIItemById(int Id)
        {
            return Context.KPIItem.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert KPIItem into database
        /// </summary>
        /// <param name="KPIItem">Object infomation</param>
        public void InsertKPIItem(KPIItem KPIItem)
        {
            Context.KPIItem.Add(KPIItem);
            Context.Entry(KPIItem).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete KPIItem with specific id
        /// </summary>
        /// <param name="Id">KPIItem Id</param>
        public void DeleteKPIItem(int Id)
        {
            KPIItem deletedKPIItem = GetKPIItemById(Id);
            Context.KPIItem.Remove(deletedKPIItem);
            Context.Entry(deletedKPIItem).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a KPIItem with its Id and Update IsDeleted IF that KPIItem has relationship with others
        /// </summary>
        /// <param name="KPIItemId">Id of KPIItem</param>
        public void DeleteKPIItemRs(int Id)
        {
            KPIItem deleteKPIItemRs = GetKPIItemById(Id);
            deleteKPIItemRs.IsDeleted = true;
            UpdateKPIItem(deleteKPIItemRs);
        }

        /// <summary>
        /// Update KPIItem into database
        /// </summary>
        /// <param name="KPIItem">KPIItem object</param>
        public void UpdateKPIItem(KPIItem KPIItem)
        {
            Context.Entry(KPIItem).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
