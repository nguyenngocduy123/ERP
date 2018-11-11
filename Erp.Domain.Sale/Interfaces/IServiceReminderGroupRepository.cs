using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IServiceReminderGroupRepository
    {
        /// <summary>
        /// Get all ServiceReminderGroup
        /// </summary>
        /// <returns>ServiceReminderGroup list</returns>
        IQueryable<ServiceReminderGroup> GetAllServiceReminderGroup();
        IQueryable<vwServiceReminderGroup> GetAllvwServiceReminderGroup();
        /// <summary>
        /// Get ServiceReminderGroup information by specific id
        /// </summary>
        /// <param name="Id">Id of ServiceReminderGroup</param>
        /// <returns></returns>
        ServiceReminderGroup GetServiceReminderGroupById(int Id);
        vwServiceReminderGroup GetvwServiceReminderGroupById(int Id);
        /// <summary>
        /// Insert ServiceReminderGroup into database
        /// </summary>
        /// <param name="ServiceReminderGroup">Object infomation</param>
        void InsertServiceReminderGroup(ServiceReminderGroup ServiceReminderGroup);

        /// <summary>
        /// Delete ServiceReminderGroup with specific id
        /// </summary>
        /// <param name="Id">ServiceReminderGroup Id</param>
        void DeleteServiceReminderGroup(int Id);

        /// <summary>
        /// Delete a ServiceReminderGroup with its Id and Update IsDeleted IF that ServiceReminderGroup has relationship with others
        /// </summary>
        /// <param name="Id">Id of ServiceReminderGroup</param>
        void DeleteServiceReminderGroupRs(int Id);

        /// <summary>
        /// Update ServiceReminderGroup into database
        /// </summary>
        /// <param name="ServiceReminderGroup">ServiceReminderGroup object</param>
        void UpdateServiceReminderGroup(ServiceReminderGroup ServiceReminderGroup);

        void DeleteServiceReminderGroupList(IEnumerable<ServiceReminderGroup> list);
    }
}
