using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class StaffFamilyRepository : GenericRepository<ErpStaffDbContext, StaffFamily>, IStaffFamilyRepository
    {
        public StaffFamilyRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all StaffFamily
        /// </summary>
        /// <returns>StaffFamily list</returns>
        public IQueryable<StaffFamily> GetAllStaffFamily()
        {
            return Context.StaffFamily.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<StaffFamily> GetAllStaffFamilyByStaffId(int staffId)
        {
            return Context.StaffFamily.Where(item => item.StaffId == staffId && (item.IsDeleted == null || item.IsDeleted == false));
        }


        /// <summary>
        /// Get StaffFamily information by specific id
        /// </summary>
        /// <param name="StaffFamilyId">Id of StaffFamily</param>
        /// <returns></returns>
        public StaffFamily GetStaffFamilyById(int Id)
        {
            return Context.StaffFamily.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert StaffFamily into database
        /// </summary>
        /// <param name="StaffFamily">Object infomation</param>
        public void InsertStaffFamily(StaffFamily StaffFamily)
        {
            Context.StaffFamily.Add(StaffFamily);
            Context.Entry(StaffFamily).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete StaffFamily with specific id
        /// </summary>
        /// <param name="Id">StaffFamily Id</param>
        public void DeleteStaffFamily(int Id)
        {
            StaffFamily deletedStaffFamily = GetStaffFamilyById(Id);
            Context.StaffFamily.Remove(deletedStaffFamily);
            Context.Entry(deletedStaffFamily).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a StaffFamily with its Id and Update IsDeleted IF that StaffFamily has relationship with others
        /// </summary>
        /// <param name="StaffFamilyId">Id of StaffFamily</param>
        public void DeleteStaffFamilyRs(int Id)
        {
            StaffFamily deleteStaffFamilyRs = GetStaffFamilyById(Id);
            deleteStaffFamilyRs.IsDeleted = true;
            UpdateStaffFamily(deleteStaffFamilyRs);
        }

        /// <summary>
        /// Update StaffFamily into database
        /// </summary>
        /// <param name="StaffFamily">StaffFamily object</param>
        public void UpdateStaffFamily(StaffFamily StaffFamily)
        {
            Context.Entry(StaffFamily).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
