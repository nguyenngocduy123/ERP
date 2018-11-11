using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IBranchDepartmentRepository
    {
        /// <summary>
        /// Get all BranchDepartment
        /// </summary>
        /// <returns>BranchDepartment list</returns>
        IQueryable<BranchDepartment> GetAllBranchDepartment();
        IQueryable<vwBranchDepartment> GetAllvwBranchDepartment();
        /// <summary>
        /// Get BranchDepartment information by specific id
        /// </summary>
        /// <param name="Id">Id of BranchDepartment</param>
        /// <returns></returns>
        BranchDepartment GetBranchDepartmentById(int Id);
        vwBranchDepartment GetvwBranchDepartmentById(int Id);
        /// <summary>
        /// Insert BranchDepartment into database
        /// </summary>
        /// <param name="BranchDepartment">Object infomation</param>
        void InsertBranchDepartment(BranchDepartment BranchDepartment);

        /// <summary>
        /// Delete BranchDepartment with specific id
        /// </summary>
        /// <param name="Id">BranchDepartment Id</param>
        void DeleteBranchDepartment(int Id);

        /// <summary>
        /// Delete a BranchDepartment with its Id and Update IsDeleted IF that BranchDepartment has relationship with others
        /// </summary>
        /// <param name="Id">Id of BranchDepartment</param>
        void DeleteBranchDepartmentRs(int Id);

        /// <summary>
        /// Update BranchDepartment into database
        /// </summary>
        /// <param name="BranchDepartment">BranchDepartment object</param>
        void UpdateBranchDepartment(BranchDepartment BranchDepartment);
    }
}
