using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class KPILogRepository : GenericRepository<ErpStaffDbContext, KPILog>, IKPILogRepository
    {
        public KPILogRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all KPILog
        /// </summary>
        /// <returns>KPILog list</returns>
        public IQueryable<KPILog> GetAllKPILog()
        {
            return Context.KPILog.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get KPILog information by specific id
        /// </summary>
        /// <param name="KPILogId">Id of KPILog</param>
        /// <returns></returns>
        public KPILog GetKPILogById(int Id)
        {
            return Context.KPILog.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert KPILog into database
        /// </summary>
        /// <param name="KPILog">Object infomation</param>
        public void InsertKPILog(KPILog KPILog)
        {
            Context.KPILog.Add(KPILog);
            Context.Entry(KPILog).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete KPILog with specific id
        /// </summary>
        /// <param name="Id">KPILog Id</param>
        public void DeleteKPILog(int Id)
        {
            KPILog deletedKPILog = GetKPILogById(Id);
            Context.KPILog.Remove(deletedKPILog);
            Context.Entry(deletedKPILog).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a KPILog with its Id and Update IsDeleted IF that KPILog has relationship with others
        /// </summary>
        /// <param name="KPILogId">Id of KPILog</param>
        public void DeleteKPILogRs(int Id)
        {
            KPILog deleteKPILogRs = GetKPILogById(Id);
            deleteKPILogRs.IsDeleted = true;
            UpdateKPILog(deleteKPILogRs);
        }

        /// <summary>
        /// Update KPILog into database
        /// </summary>
        /// <param name="KPILog">KPILog object</param>
        public void UpdateKPILog(KPILog KPILog)
        {
            Context.Entry(KPILog).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
