using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class LogDocumentAttributeRepository : GenericRepository<ErpStaffDbContext, LogDocumentAttribute>, ILogDocumentAttributeRepository
    {
        public LogDocumentAttributeRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all LogDocumentAttribute
        /// </summary>
        /// <returns>LogDocumentAttribute list</returns>
        public IQueryable<LogDocumentAttribute> GetAllLogDocumentAttribute()
        {
            return Context.LogDocumentAttribute.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwLogDocumentAttribute> GetAllvwLogDocumentAttribute()
        {
            return Context.vwLogDocumentAttribute.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get LogDocumentAttribute information by specific id
        /// </summary>
        /// <param name="LogDocumentAttributeId">Id of LogDocumentAttribute</param>
        /// <returns></returns>
        public LogDocumentAttribute GetLogDocumentAttributeById(int Id)
        {
            return Context.LogDocumentAttribute.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwLogDocumentAttribute GetvwLogDocumentAttributeById(int Id)
        {
            return Context.vwLogDocumentAttribute.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert LogDocumentAttribute into database
        /// </summary>
        /// <param name="LogDocumentAttribute">Object infomation</param>
        public void InsertLogDocumentAttribute(LogDocumentAttribute LogDocumentAttribute)
        {
            Context.LogDocumentAttribute.Add(LogDocumentAttribute);
            Context.Entry(LogDocumentAttribute).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete LogDocumentAttribute with specific id
        /// </summary>
        /// <param name="Id">LogDocumentAttribute Id</param>
        public void DeleteLogDocumentAttribute(int Id)
        {
            LogDocumentAttribute deletedLogDocumentAttribute = GetLogDocumentAttributeById(Id);
            Context.LogDocumentAttribute.Remove(deletedLogDocumentAttribute);
            Context.Entry(deletedLogDocumentAttribute).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a LogDocumentAttribute with its Id and Update IsDeleted IF that LogDocumentAttribute has relationship with others
        /// </summary>
        /// <param name="LogDocumentAttributeId">Id of LogDocumentAttribute</param>
        public void DeleteLogDocumentAttributeRs(int Id)
        {
            LogDocumentAttribute deleteLogDocumentAttributeRs = GetLogDocumentAttributeById(Id);
            deleteLogDocumentAttributeRs.IsDeleted = true;
            UpdateLogDocumentAttribute(deleteLogDocumentAttributeRs);
        }

        /// <summary>
        /// Update LogDocumentAttribute into database
        /// </summary>
        /// <param name="LogDocumentAttribute">LogDocumentAttribute object</param>
        public void UpdateLogDocumentAttribute(LogDocumentAttribute LogDocumentAttribute)
        {
            Context.Entry(LogDocumentAttribute).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
