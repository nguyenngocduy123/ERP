using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class KPILogDetail_ItemRepository : GenericRepository<ErpStaffDbContext, KPILogDetail_Item>, IKPILogDetail_ItemRepository
    {
        public KPILogDetail_ItemRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all KPILogDetail_Item
        /// </summary>
        /// <returns>KPILogDetail_Item list</returns>
        public IQueryable<KPILogDetail_Item> GetAllKPILogDetail_Item()
        {
            return Context.KPILogDetail_Item.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get KPILogDetail_Item information by specific id
        /// </summary>
        /// <param name="KPILogDetail_ItemId">Id of KPILogDetail_Item</param>
        /// <returns></returns>
        public KPILogDetail_Item GetKPILogDetail_ItemById(int Id)
        {
            return Context.KPILogDetail_Item.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert KPILogDetail_Item into database
        /// </summary>
        /// <param name="KPILogDetail_Item">Object infomation</param>
        public void InsertKPILogDetail_Item(KPILogDetail_Item KPILogDetail_Item)
        {
            Context.KPILogDetail_Item.Add(KPILogDetail_Item);
            Context.Entry(KPILogDetail_Item).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete KPILogDetail_Item with specific id
        /// </summary>
        /// <param name="Id">KPILogDetail_Item Id</param>
        public void DeleteKPILogDetail_Item(int Id)
        {
            KPILogDetail_Item deletedKPILogDetail_Item = GetKPILogDetail_ItemById(Id);
            Context.KPILogDetail_Item.Remove(deletedKPILogDetail_Item);
            Context.Entry(deletedKPILogDetail_Item).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a KPILogDetail_Item with its Id and Update IsDeleted IF that KPILogDetail_Item has relationship with others
        /// </summary>
        /// <param name="KPILogDetail_ItemId">Id of KPILogDetail_Item</param>
        public void DeleteKPILogDetail_ItemRs(int Id)
        {
            KPILogDetail_Item deleteKPILogDetail_ItemRs = GetKPILogDetail_ItemById(Id);
            deleteKPILogDetail_ItemRs.IsDeleted = true;
            UpdateKPILogDetail_Item(deleteKPILogDetail_ItemRs);
        }

        /// <summary>
        /// Update KPILogDetail_Item into database
        /// </summary>
        /// <param name="KPILogDetail_Item">KPILogDetail_Item object</param>
        public void UpdateKPILogDetail_Item(KPILogDetail_Item KPILogDetail_Item)
        {
            Context.Entry(KPILogDetail_Item).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
