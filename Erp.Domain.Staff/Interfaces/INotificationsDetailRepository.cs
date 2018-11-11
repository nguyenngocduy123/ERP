using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface INotificationsDetailRepository
    {
        /// <summary>
        /// Get all InternalNotifications
        /// </summary>
        /// <returns>InternalNotifications list</returns>
        IQueryable<NotificationsDetail> GetAllNotificationsDetail();
        IQueryable<NotificationsDetail> GetAllNotificationsDetailbyId(int? Id);

        IQueryable<vwNotificationsDetail> GetAllvwNotificationsDetailbyId(int? Id);

        /// <summary>
        /// Get InternalNotifications information by specific id
        /// </summary>
        /// <param name="Id">Id of InternalNotifications</param>
        /// <returns></returns>
        NotificationsDetail GetNotificationsDetailById(int? Id);
        /// <summary>
        /// Insert InternalNotifications into database
        /// </summary>
        /// <param name="InternalNotifications">Object infomation</param>
        void InsertNotificationsDetail(NotificationsDetail NotificationsDetail);

        /// <summary>
        /// Delete InternalNotifications with specific id
        /// </summary>
        /// <param name="Id">InternalNotifications Id</param>
        void DeleteNotificationsDetail(int Id);

        /// <summary>
        /// Delete a InternalNotifications with its Id and Update IsDeleted IF that InternalNotifications has relationship with others
        /// </summary>
        /// <param name="Id">Id of InternalNotifications</param>
        void DeleteNotificationsDetailRs(int Id);

        /// <summary>
        /// Update InternalNotifications into database
        /// </summary>
        /// <param name="InternalNotifications">InternalNotifications object</param>
        void UpdateNotificationsDetail(NotificationsDetail NotificationsDetail);
    }
}
