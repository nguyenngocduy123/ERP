using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ISalarySettingDetailRepository
    {
        /// <summary>
        /// Get all SalarySettingDetail
        /// </summary>
        /// <returns>SalarySettingDetail list</returns>
        IQueryable<SalarySettingDetail> GetAllSalarySettingDetail();

        /// <summary>
        /// Get SalarySettingDetail information by specific id
        /// </summary>
        /// <param name="Id">Id of SalarySettingDetail</param>
        /// <returns></returns>
        SalarySettingDetail GetSalarySettingDetailById(int Id);

        /// <summary>
        /// Insert SalarySettingDetail into database
        /// </summary>
        /// <param name="SalarySettingDetail">Object infomation</param>
        void InsertSalarySettingDetail(SalarySettingDetail SalarySettingDetail);

        /// <summary>
        /// Delete SalarySettingDetail with specific id
        /// </summary>
        /// <param name="Id">SalarySettingDetail Id</param>
        void DeleteSalarySettingDetail(int Id);

        /// <summary>
        /// Delete a SalarySettingDetail with its Id and Update IsDeleted IF that SalarySettingDetail has relationship with others
        /// </summary>
        /// <param name="Id">Id of SalarySettingDetail</param>
        void DeleteSalarySettingDetailRs(int Id);

        /// <summary>
        /// Update SalarySettingDetail into database
        /// </summary>
        /// <param name="SalarySettingDetail">SalarySettingDetail object</param>
        void UpdateSalarySettingDetail(SalarySettingDetail SalarySettingDetail);
    }
}
