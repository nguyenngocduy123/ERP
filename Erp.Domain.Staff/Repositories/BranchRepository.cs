using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class BranchRepository : GenericRepository<ErpStaffDbContext, Branch>, IBranchRepository
    {
        public BranchRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Branch
        /// </summary>
        /// <returns>Branch list</returns>
        public IQueryable<Branch> GetAllBranch()
        {
            return Context.Branch.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwBranch> GetAllvwBranch()
        {
            return Context.vwBranch.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get Branch information by specific id
        /// </summary>
        /// <param name="BranchId">Id of Branch</param>
        /// <returns></returns>
        public Branch GetBranchById(int Id)
        {
            return Context.Branch.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwBranch GetvwBranchById(int Id)
        {
            return Context.vwBranch.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwBranch GetvwBranchByCode(string Code)
        {
            return Context.vwBranch.SingleOrDefault(item => item.Code == Code && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert Branch into database
        /// </summary>
        /// <param name="Branch">Object infomation</param>
        public void InsertBranch(Branch Branch)
        {
            Context.Branch.Add(Branch);
            Context.Entry(Branch).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Branch with specific id
        /// </summary>
        /// <param name="Id">Branch Id</param>
        public void DeleteBranch(int Id)
        {
            Branch deletedBranch = GetBranchById(Id);
            Context.Branch.Remove(deletedBranch);
            Context.Entry(deletedBranch).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Branch with its Id and Update IsDeleted IF that Branch has relationship with others
        /// </summary>
        /// <param name="BranchId">Id of Branch</param>
        public void DeleteBranchRs(int Id)
        {
            Branch deleteBranchRs = GetBranchById(Id);
            deleteBranchRs.IsDeleted = true;
            UpdateBranch(deleteBranchRs);
        }

        /// <summary>
        /// Update Branch into database
        /// </summary>
        /// <param name="Branch">Branch object</param>
        public void UpdateBranch(Branch Branch)
        {
            Context.Entry(Branch).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
