using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IInternalNotificationsRepository
    {
        /// <summary>
        /// Get all InternalNotifications
        /// </summary>
        /// <returns>InternalNotifications list</returns>
        IQueryable<InternalNotifications> GetAllInternalNotifications();
        IQueryable<vwInternalNotifications> GetAllvwInternalNotifications();
        /// <summary>
        /// Get InternalNotifications information by specific id
        /// </summary>
        /// <param name="Id">Id of InternalNotifications</param>
        /// <returns></returns>
        InternalNotifications GetInternalNotificationsById(int? Id);
        vwInternalNotifications GetvwInternalNotificationsById(int Id);
        /// <summary>
        /// Insert InternalNotifications into database
        /// </summary>
        /// <param name="InternalNotifications">Object infomation</param>
        void InsertInternalNotifications(InternalNotifications InternalNotifications);

        /// <summary>
        /// Delete InternalNotifications with specific id
        /// </summary>
        /// <param name="Id">InternalNotifications Id</param>
        void DeleteInternalNotifications(int Id);

        /// <summary>
        /// Delete a InternalNotifications with its Id and Update IsDeleted IF that InternalNotifications has relationship with others
        /// </summary>
        /// <param name="Id">Id of InternalNotifications</param>
        void DeleteInternalNotificationsRs(int Id);

        /// <summary>
        /// Update InternalNotifications into database
        /// </summary>
        /// <param name="InternalNotifications">InternalNotifications object</param>
        void UpdateInternalNotifications(InternalNotifications InternalNotifications);
    }
}
