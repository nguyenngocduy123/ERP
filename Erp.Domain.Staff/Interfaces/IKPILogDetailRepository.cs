using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IKPILogDetailRepository
    {
        /// <summary>
        /// Get all KPILogDetail
        /// </summary>
        /// <returns>KPILogDetail list</returns>
        IQueryable<KPILogDetail> GetAllKPILogDetail();
        IQueryable<vwKPILogDetail> GetAllvwKPILogDetail();

        /// <summary>
        /// Get KPILogDetail information by specific id
        /// </summary>
        /// <param name="Id">Id of KPILogDetail</param>
        /// <returns></returns>
        KPILogDetail GetKPILogDetailById(int Id);

        /// <summary>
        /// Insert KPILogDetail into database
        /// </summary>
        /// <param name="KPILogDetail">Object infomation</param>
        void InsertKPILogDetail(KPILogDetail KPILogDetail);

        /// <summary>
        /// Delete KPILogDetail with specific id
        /// </summary>
        /// <param name="Id">KPILogDetail Id</param>
        void DeleteKPILogDetail(int Id);

        /// <summary>
        /// Delete a KPILogDetail with its Id and Update IsDeleted IF that KPILogDetail has relationship with others
        /// </summary>
        /// <param name="Id">Id of KPILogDetail</param>
        void DeleteKPILogDetailRs(int Id);

        /// <summary>
        /// Update KPILogDetail into database
        /// </summary>
        /// <param name="KPILogDetail">KPILogDetail object</param>
        void UpdateKPILogDetail(KPILogDetail KPILogDetail);
    }
}
