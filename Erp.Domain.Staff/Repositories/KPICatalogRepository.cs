using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class KPICatalogRepository : GenericRepository<ErpStaffDbContext, KPICatalog>, IKPICatalogRepository
    {
        public KPICatalogRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all KPICatalog
        /// </summary>
        /// <returns>KPICatalog list</returns>
        public IQueryable<KPICatalog> GetAllKPICatalog()
        {
            return Context.KPICatalog.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get KPICatalog information by specific id
        /// </summary>
        /// <param name="KPICatalogId">Id of KPICatalog</param>
        /// <returns></returns>
        public KPICatalog GetKPICatalogById(int Id)
        {
            return Context.KPICatalog.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert KPICatalog into database
        /// </summary>
        /// <param name="KPICatalog">Object infomation</param>
        public void InsertKPICatalog(KPICatalog KPICatalog)
        {
            Context.KPICatalog.Add(KPICatalog);
            Context.Entry(KPICatalog).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete KPICatalog with specific id
        /// </summary>
        /// <param name="Id">KPICatalog Id</param>
        public void DeleteKPICatalog(int Id)
        {
            KPICatalog deletedKPICatalog = GetKPICatalogById(Id);
            Context.KPICatalog.Remove(deletedKPICatalog);
            Context.Entry(deletedKPICatalog).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a KPICatalog with its Id and Update IsDeleted IF that KPICatalog has relationship with others
        /// </summary>
        /// <param name="KPICatalogId">Id of KPICatalog</param>
        public void DeleteKPICatalogRs(int Id)
        {
            KPICatalog deleteKPICatalogRs = GetKPICatalogById(Id);
            deleteKPICatalogRs.IsDeleted = true;
            UpdateKPICatalog(deleteKPICatalogRs);
        }

        /// <summary>
        /// Update KPICatalog into database
        /// </summary>
        /// <param name="KPICatalog">KPICatalog object</param>
        public void UpdateKPICatalog(KPICatalog KPICatalog)
        {
            Context.Entry(KPICatalog).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
