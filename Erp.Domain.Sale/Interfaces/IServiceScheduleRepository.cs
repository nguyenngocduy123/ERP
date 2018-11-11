using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IServiceScheduleRepository
    {
        /// <summary>
        /// Get all ServiceSchedule
        /// </summary>
        /// <returns>ServiceSchedule list</returns>
        IQueryable<ServiceSchedule> GetAllServiceSchedule();
        IQueryable<vwServiceSchedule> GetAllvwServiceSchedule();
        /// <summary>
        /// Get ServiceSchedule information by specific id
        /// </summary>
        /// <param name="Id">Id of ServiceSchedule</param>
        /// <returns></returns>
        ServiceSchedule GetServiceScheduleById(int Id);
        vwServiceSchedule GetvwServiceScheduleById(int Id);
        /// <summary>
        /// Insert ServiceSchedule into database
        /// </summary>
        /// <param name="ServiceSchedule">Object infomation</param>
        void InsertServiceSchedule(ServiceSchedule ServiceSchedule);

        /// <summary>
        /// Delete ServiceSchedule with specific id
        /// </summary>
        /// <param name="Id">ServiceSchedule Id</param>
        void DeleteServiceSchedule(int Id);

        /// <summary>
        /// Delete a ServiceSchedule with its Id and Update IsDeleted IF that ServiceSchedule has relationship with others
        /// </summary>
        /// <param name="Id">Id of ServiceSchedule</param>
        void DeleteServiceScheduleRs(int Id);

        /// <summary>
        /// Update ServiceSchedule into database
        /// </summary>
        /// <param name="ServiceSchedule">ServiceSchedule object</param>
        void UpdateServiceSchedule(ServiceSchedule ServiceSchedule);
    }
}
