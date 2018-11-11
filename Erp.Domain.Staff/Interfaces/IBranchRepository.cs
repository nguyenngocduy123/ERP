using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IBranchRepository
    {
        /// <summary>
        /// Get all Branch
        /// </summary>
        /// <returns>Branch list</returns>
        IQueryable<Branch> GetAllBranch();
        IQueryable<vwBranch> GetAllvwBranch();
        /// <summary>
        /// Get Branch information by specific id
        /// </summary>
        /// <param name="Id">Id of Branch</param>
        /// <returns></returns>
        Branch GetBranchById(int Id);
        vwBranch GetvwBranchById(int Id);
        vwBranch GetvwBranchByCode(string Code);
        /// <summary>
        /// Insert Branch into database
        /// </summary>
        /// <param name="Branch">Object infomation</param>
        void InsertBranch(Branch Branch);

        /// <summary>
        /// Delete Branch with specific id
        /// </summary>
        /// <param name="Id">Branch Id</param>
        void DeleteBranch(int Id);

        /// <summary>
        /// Delete a Branch with its Id and Update IsDeleted IF that Branch has relationship with others
        /// </summary>
        /// <param name="Id">Id of Branch</param>
        void DeleteBranchRs(int Id);

        /// <summary>
        /// Update Branch into database
        /// </summary>
        /// <param name="Branch">Branch object</param>
        void UpdateBranch(Branch Branch);
    }
}
