using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ISalarySettingDetail_StaffRepository
    {
        /// <summary>
        /// Get all SalarySettingDetail_Staff
        /// </summary>
        /// <returns>SalarySettingDetail_Staff list</returns>
        IQueryable<SalarySettingDetail_Staff> GetAllSalarySettingDetail_Staff();

        /// <summary>
        /// Get SalarySettingDetail_Staff information by specific id
        /// </summary>
        /// <param name="Id">Id of SalarySettingDetail_Staff</param>
        /// <returns></returns>
        SalarySettingDetail_Staff GetSalarySettingDetail_StaffById(int Id);
        SalarySettingDetail_Staff GetSalarySettingDetail_StaffBySetting(int SettingId,int staffId,int settingDetailId);
        /// <summary>
        /// Insert SalarySettingDetail_Staff into database
        /// </summary>
        /// <param name="SalarySettingDetail_Staff">Object infomation</param>
        void InsertSalarySettingDetail_Staff(SalarySettingDetail_Staff SalarySettingDetail_Staff);

        /// <summary>
        /// Delete SalarySettingDetail_Staff with specific id
        /// </summary>
        /// <param name="Id">SalarySettingDetail_Staff Id</param>
        void DeleteSalarySettingDetail_Staff(int Id);

        /// <summary>
        /// Delete a SalarySettingDetail_Staff with its Id and Update IsDeleted IF that SalarySettingDetail_Staff has relationship with others
        /// </summary>
        /// <param name="Id">Id of SalarySettingDetail_Staff</param>
        void DeleteSalarySettingDetail_StaffRs(int Id);

        /// <summary>
        /// Update SalarySettingDetail_Staff into database
        /// </summary>
        /// <param name="SalarySettingDetail_Staff">SalarySettingDetail_Staff object</param>
        void UpdateSalarySettingDetail_Staff(SalarySettingDetail_Staff SalarySettingDetail_Staff);
    }
}
