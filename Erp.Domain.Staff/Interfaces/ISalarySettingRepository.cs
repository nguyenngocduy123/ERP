using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ISalarySettingRepository
    {
        /// <summary>
        /// Get all SalarySetting
        /// </summary>
        /// <returns>SalarySetting list</returns>
        IQueryable<SalarySetting> GetAllSalarySetting();

        /// <summary>
        /// Get SalarySetting information by specific id
        /// </summary>
        /// <param name="Id">Id of SalarySetting</param>
        /// <returns></returns>
        SalarySetting GetSalarySettingById(int Id);
        //SalarySetting GetSalarySettingByDepartment(int? Department);
        /// <summary>
        /// Insert SalarySetting into database
        /// </summary>
        /// <param name="SalarySetting">Object infomation</param>
        void InsertSalarySetting(SalarySetting SalarySetting);

        /// <summary>
        /// Delete SalarySetting with specific id
        /// </summary>
        /// <param name="Id">SalarySetting Id</param>
        void DeleteSalarySetting(int Id);

        /// <summary>
        /// Delete a SalarySetting with its Id and Update IsDeleted IF that SalarySetting has relationship with others
        /// </summary>
        /// <param name="Id">Id of SalarySetting</param>
        void DeleteSalarySettingRs(int Id);

        /// <summary>
        /// Update SalarySetting into database
        /// </summary>
        /// <param name="SalarySetting">SalarySetting object</param>
        void UpdateSalarySetting(SalarySetting SalarySetting);
    }
}
