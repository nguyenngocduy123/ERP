using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class InternalNotificationsRepository : GenericRepository<ErpStaffDbContext, InternalNotifications>, IInternalNotificationsRepository
    {
        public InternalNotificationsRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all InternalNotifications
        /// </summary>
        /// <returns>InternalNotifications list</returns>
        public IQueryable<InternalNotifications> GetAllInternalNotifications()
        {
            return Context.InternalNotifications.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwInternalNotifications> GetAllvwInternalNotifications()
        {
            return Context.vwInternalNotifications.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get InternalNotifications information by specific id
        /// </summary>
        /// <param name="InternalNotificationsId">Id of InternalNotifications</param>
        /// <returns></returns>
        public InternalNotifications GetInternalNotificationsById(int? Id)
        {
            return Context.InternalNotifications.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwInternalNotifications GetvwInternalNotificationsById(int Id)
        {
            return Context.vwInternalNotifications.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert InternalNotifications into database
        /// </summary>
        /// <param name="InternalNotifications">Object infomation</param>
        public void InsertInternalNotifications(InternalNotifications InternalNotifications)
        {
            Context.InternalNotifications.Add(InternalNotifications);
            Context.Entry(InternalNotifications).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete InternalNotifications with specific id
        /// </summary>
        /// <param name="Id">InternalNotifications Id</param>
        public void DeleteInternalNotifications(int Id)
        {
            InternalNotifications deletedInternalNotifications = GetInternalNotificationsById(Id);
            Context.InternalNotifications.Remove(deletedInternalNotifications);
            Context.Entry(deletedInternalNotifications).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a InternalNotifications with its Id and Update IsDeleted IF that InternalNotifications has relationship with others
        /// </summary>
        /// <param name="InternalNotificationsId">Id of InternalNotifications</param>
        public void DeleteInternalNotificationsRs(int Id)
        {
            InternalNotifications deleteInternalNotificationsRs = GetInternalNotificationsById(Id);
            deleteInternalNotificationsRs.IsDeleted = true;
            UpdateInternalNotifications(deleteInternalNotificationsRs);
        }

        /// <summary>
        /// Update InternalNotifications into database
        /// </summary>
        /// <param name="InternalNotifications">InternalNotifications object</param>
        public void UpdateInternalNotifications(InternalNotifications InternalNotifications)
        {
            Context.Entry(InternalNotifications).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
