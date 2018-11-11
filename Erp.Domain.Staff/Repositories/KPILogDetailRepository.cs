using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class KPILogDetailRepository : GenericRepository<ErpStaffDbContext, KPILogDetail>, IKPILogDetailRepository
    {
        public KPILogDetailRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all KPILogDetail
        /// </summary>
        /// <returns>KPILogDetail list</returns>
        public IQueryable<KPILogDetail> GetAllKPILogDetail()
        {
            return Context.KPILogDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwKPILogDetail> GetAllvwKPILogDetail()
        {
            return Context.vwKPILogDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get KPILogDetail information by specific id
        /// </summary>
        /// <param name="KPILogDetailId">Id of KPILogDetail</param>
        /// <returns></returns>
        public KPILogDetail GetKPILogDetailById(int Id)
        {
            return Context.KPILogDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert KPILogDetail into database
        /// </summary>
        /// <param name="KPILogDetail">Object infomation</param>
        public void InsertKPILogDetail(KPILogDetail KPILogDetail)
        {
            Context.KPILogDetail.Add(KPILogDetail);
            Context.Entry(KPILogDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete KPILogDetail with specific id
        /// </summary>
        /// <param name="Id">KPILogDetail Id</param>
        public void DeleteKPILogDetail(int Id)
        {
            KPILogDetail deletedKPILogDetail = GetKPILogDetailById(Id);
            Context.KPILogDetail.Remove(deletedKPILogDetail);
            Context.Entry(deletedKPILogDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a KPILogDetail with its Id and Update IsDeleted IF that KPILogDetail has relationship with others
        /// </summary>
        /// <param name="KPILogDetailId">Id of KPILogDetail</param>
        public void DeleteKPILogDetailRs(int Id)
        {
            KPILogDetail deleteKPILogDetailRs = GetKPILogDetailById(Id);
            deleteKPILogDetailRs.IsDeleted = true;
            UpdateKPILogDetail(deleteKPILogDetailRs);
        }

        /// <summary>
        /// Update KPILogDetail into database
        /// </summary>
        /// <param name="KPILogDetail">KPILogDetail object</param>
        public void UpdateKPILogDetail(KPILogDetail KPILogDetail)
        {
            Context.Entry(KPILogDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
