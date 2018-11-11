using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IServiceReminderRepository
    {
        /// <summary>
        /// Get all ServiceReminder
        /// </summary>
        /// <returns>ServiceReminder list</returns>
        IQueryable<ServiceReminder> GetAllServiceReminder();

        /// <summary>
        /// Get ServiceReminder information by specific id
        /// </summary>
        /// <param name="Id">Id of ServiceReminder</param>
        /// <returns></returns>
        ServiceReminder GetServiceReminderById(int Id);

        /// <summary>
        /// Insert ServiceReminder into database
        /// </summary>
        /// <param name="ServiceReminder">Object infomation</param>
        void InsertServiceReminder(ServiceReminder ServiceReminder);

        /// <summary>
        /// Delete ServiceReminder with specific id
        /// </summary>
        /// <param name="Id">ServiceReminder Id</param>
        void DeleteServiceReminder(int Id);

        /// <summary>
        /// Delete a ServiceReminder with its Id and Update IsDeleted IF that ServiceReminder has relationship with others
        /// </summary>
        /// <param name="Id">Id of ServiceReminder</param>
        void DeleteServiceReminderRs(int Id);

        /// <summary>
        /// Update ServiceReminder into database
        /// </summary>
        /// <param name="ServiceReminder">ServiceReminder object</param>
        void UpdateServiceReminder(ServiceReminder ServiceReminder);
    }
}
