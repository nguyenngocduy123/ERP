using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IStaffAllowanceRepository
    {
        /// <summary>
        /// Get all StaffAllowance
        /// </summary>
        /// <returns>StaffAllowance list</returns>
        IQueryable<StaffAllowance> GetAllStaffAllowance();

        /// <summary>
        /// Get StaffAllowance information by specific id
        /// </summary>
        /// <param name="Id">Id of StaffAllowance</param>
        /// <returns></returns>
        StaffAllowance GetStaffAllowanceById(int Id);

        /// <summary>
        /// Insert StaffAllowance into database
        /// </summary>
        /// <param name="StaffAllowance">Object infomation</param>
        void InsertStaffAllowance(StaffAllowance StaffAllowance);

        /// <summary>
        /// Delete StaffAllowance with specific id
        /// </summary>
        /// <param name="Id">StaffAllowance Id</param>
        void DeleteStaffAllowance(int Id);

        /// <summary>
        /// Delete a StaffAllowance with its Id and Update IsDeleted IF that StaffAllowance has relationship with others
        /// </summary>
        /// <param name="Id">Id of StaffAllowance</param>
        void DeleteStaffAllowanceRs(int Id);

        /// <summary>
        /// Update StaffAllowance into database
        /// </summary>
        /// <param name="StaffAllowance">StaffAllowance object</param>
        void UpdateStaffAllowance(StaffAllowance StaffAllowance);
    }
}
