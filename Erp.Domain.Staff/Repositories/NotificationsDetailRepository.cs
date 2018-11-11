using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class NotificationsDetailRepository : GenericRepository<ErpStaffDbContext, NotificationsDetail>, INotificationsDetailRepository
    {
        public NotificationsDetailRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all InternalNotifications
        /// </summary>
        /// <returns>InternalNotifications list</returns>
        public IQueryable<NotificationsDetail> GetAllNotificationsDetail()
        {
            return Context.NotificationsDetail.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<NotificationsDetail> GetAllNotificationsDetailbyId(int? Id)
        {
            return Context.NotificationsDetail.Where(item =>item.NotificationsId==Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwNotificationsDetail> GetAllvwNotificationsDetailbyId(int? Id)
        {
            return Context.vwNotificationsDetail.Where(item => item.NotificationsId == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get InternalNotifications information by specific id
        /// </summary>
        /// <param name="InternalNotificationsId">Id of InternalNotifications</param>
        /// <returns></returns>
        public NotificationsDetail GetNotificationsDetailById(int? Id)
        {
            return Context.NotificationsDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
      
        /// <summary>
        /// Insert InternalNotifications into database
        /// </summary>
        /// <param name="InternalNotifications">Object infomation</param>
        public void InsertNotificationsDetail(NotificationsDetail NotificationsDetail)
        {
            Context.NotificationsDetail.Add(NotificationsDetail);
            Context.Entry(NotificationsDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete InternalNotifications with specific id
        /// </summary>
        /// <param name="Id">InternalNotifications Id</param>
        public void DeleteNotificationsDetail(int Id)
        {
            NotificationsDetail deletedNotificationsDetail = GetNotificationsDetailById(Id);
            Context.NotificationsDetail.Remove(deletedNotificationsDetail);
            Context.Entry(deletedNotificationsDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a InternalNotifications with its Id and Update IsDeleted IF that InternalNotifications has relationship with others
        /// </summary>
        /// <param name="InternalNotificationsId">Id of InternalNotifications</param>
        public void DeleteNotificationsDetailRs(int Id)
        {
            NotificationsDetail deleteNotificationsDetailRs = GetNotificationsDetailById(Id);
            deleteNotificationsDetailRs.IsDeleted = true;
            UpdateNotificationsDetail(deleteNotificationsDetailRs);
        }

        /// <summary>
        /// Update InternalNotifications into database
        /// </summary>
        /// <param name="InternalNotifications">InternalNotifications object</param>
        public void UpdateNotificationsDetail(NotificationsDetail NotificationsDetail)
        {
            Context.Entry(NotificationsDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
