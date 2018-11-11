using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IKPILogRepository
    {
        /// <summary>
        /// Get all KPILog
        /// </summary>
        /// <returns>KPILog list</returns>
        IQueryable<KPILog> GetAllKPILog();

        /// <summary>
        /// Get KPILog information by specific id
        /// </summary>
        /// <param name="Id">Id of KPILog</param>
        /// <returns></returns>
        KPILog GetKPILogById(int Id);

        /// <summary>
        /// Insert KPILog into database
        /// </summary>
        /// <param name="KPILog">Object infomation</param>
        void InsertKPILog(KPILog KPILog);

        /// <summary>
        /// Delete KPILog with specific id
        /// </summary>
        /// <param name="Id">KPILog Id</param>
        void DeleteKPILog(int Id);

        /// <summary>
        /// Delete a KPILog with its Id and Update IsDeleted IF that KPILog has relationship with others
        /// </summary>
        /// <param name="Id">Id of KPILog</param>
        void DeleteKPILogRs(int Id);

        /// <summary>
        /// Update KPILog into database
        /// </summary>
        /// <param name="KPILog">KPILog object</param>
        void UpdateKPILog(KPILog KPILog);
    }
}
