using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ISalaryTableDetail_StaffRepository
    {
        /// <summary>
        /// Get all SalaryTableDetail_Staff
        /// </summary>
        /// <returns>SalaryTableDetail_Staff list</returns>
        IQueryable<SalaryTableDetail_Staff> GetAllSalaryTableDetail_Staff();

        /// <summary>
        /// Get SalaryTableDetail_Staff information by specific id
        /// </summary>
        /// <param name="Id">Id of SalaryTableDetail_Staff</param>
        /// <returns></returns>
        SalaryTableDetail_Staff GetSalaryTableDetail_StaffById(int Id);
        SalaryTableDetail_Staff GetSalaryTableDetail_StaffBySalaryTableId(int Id);

        /// <summary>
        /// Insert SalaryTableDetail_Staff into database
        /// </summary>
        /// <param name="SalaryTableDetail_Staff">Object infomation</param>
        void InsertSalaryTableDetail_Staff(SalaryTableDetail_Staff SalaryTableDetail_Staff);

        /// <summary>
        /// Delete SalaryTableDetail_Staff with specific id
        /// </summary>
        /// <param name="Id">SalaryTableDetail_Staff Id</param>
        void DeleteSalaryTableDetail_Staff(int Id);
        void DeleteSalaryTableDetail_StaffBySalaryTableId(int SalaryTableId);

        /// <summary>
        /// Delete a SalaryTableDetail_Staff with its Id and Update IsDeleted IF that SalaryTableDetail_Staff has relationship with others
        /// </summary>
        /// <param name="Id">Id of SalaryTableDetail_Staff</param>
        void DeleteSalaryTableDetail_StaffRs(int Id);

        /// <summary>
        /// Update SalaryTableDetail_Staff into database
        /// </summary>
        /// <param name="SalaryTableDetail_Staff">SalaryTableDetail_Staff object</param>
        void UpdateSalaryTableDetail_Staff(SalaryTableDetail_Staff SalaryTableDetail_Staff);
    }
}
