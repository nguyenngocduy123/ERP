using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class StaffAllowanceRepository : GenericRepository<ErpStaffDbContext, StaffAllowance>, IStaffAllowanceRepository
    {
        public StaffAllowanceRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all StaffAllowance
        /// </summary>
        /// <returns>StaffAllowance list</returns>
        public IQueryable<StaffAllowance> GetAllStaffAllowance()
        {
            return Context.StaffAllowance.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get StaffAllowance information by specific id
        /// </summary>
        /// <param name="StaffAllowanceId">Id of StaffAllowance</param>
        /// <returns></returns>
        public StaffAllowance GetStaffAllowanceById(int Id)
        {
            return Context.StaffAllowance.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert StaffAllowance into database
        /// </summary>
        /// <param name="StaffAllowance">Object infomation</param>
        public void InsertStaffAllowance(StaffAllowance StaffAllowance)
        {
            Context.StaffAllowance.Add(StaffAllowance);
            Context.Entry(StaffAllowance).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete StaffAllowance with specific id
        /// </summary>
        /// <param name="Id">StaffAllowance Id</param>
        public void DeleteStaffAllowance(int Id)
        {
            StaffAllowance deletedStaffAllowance = GetStaffAllowanceById(Id);
            Context.StaffAllowance.Remove(deletedStaffAllowance);
            Context.Entry(deletedStaffAllowance).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a StaffAllowance with its Id and Update IsDeleted IF that StaffAllowance has relationship with others
        /// </summary>
        /// <param name="StaffAllowanceId">Id of StaffAllowance</param>
        public void DeleteStaffAllowanceRs(int Id)
        {
            StaffAllowance deleteStaffAllowanceRs = GetStaffAllowanceById(Id);
            deleteStaffAllowanceRs.IsDeleted = true;
            UpdateStaffAllowance(deleteStaffAllowanceRs);
        }

        /// <summary>
        /// Update StaffAllowance into database
        /// </summary>
        /// <param name="StaffAllowance">StaffAllowance object</param>
        public void UpdateStaffAllowance(StaffAllowance StaffAllowance)
        {
            Context.Entry(StaffAllowance).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
