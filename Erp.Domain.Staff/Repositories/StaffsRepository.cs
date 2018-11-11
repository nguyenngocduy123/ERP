using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class StaffsRepository : GenericRepository<ErpStaffDbContext, Staffs>, IStaffsRepository
    {
        public StaffsRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Staffs
        /// </summary>
        /// <returns>Staffs list</returns>
        public IQueryable<Staffs> GetAllStaffs()
        {
            return Context.Staffs.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwStaffs> GetvwAllStaffs()
        {
            return Context.vwStaffs.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get Staffs information by specific id
        /// </summary>
        /// <param name="StaffsId">Id of Staffs</param>
        /// <returns></returns>
        public Staffs GetStaffsById(int Id)
        {
            return Context.Staffs.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwStaffs> GetvwStaffsByListDrugStore(string DrugStore)
        {
            return Context.vwStaffs.Where(item => ("," + DrugStore + ",").Contains("," + item.DrugStore + ",") == true && (item.IsDeleted == null || item.IsDeleted == false));
        }
        //public IQueryable<vwStaffs> GetvwStaffsByBranchIdAndBranchDepartmentId(int BranchId, int BranchDepartmentId)
        //{
        //    return Context.vwStaffs.Where(item => item.Sale_BranchId == BranchId && item.BranchDepartmentId == BranchDepartmentId && (item.IsDeleted == null || item.IsDeleted == false));
        //}
        //public Staffs GetStaffsByBranchId(int? BranchId, string Position)
        //{
        //    return Context.Staffs.Where(item => item.BranchId == BranchId&&item.Position==Position && (item.IsDeleted == null || item.IsDeleted == false)).FirstOrDefault();
        //}
        public vwStaffs GetvwStaffsById(int Id)
        {
            return Context.vwStaffs.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        //public vwStaffs GetvwStaffsByUserEnrollNumber(int UserEnrollNumber)
        //{
        //    return Context.vwStaffs.SingleOrDefault(item => item.CheckInOut_UserId == UserEnrollNumber && (item.IsDeleted == null || item.IsDeleted == false));
        //}
        public vwStaffs GetvwStaffsByUser(int UserId)
        {
            return Context.vwStaffs.SingleOrDefault(item => item.UserId == UserId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        //public vwStaffs GetvwStaffsByBranchId(int? BranchId, string Position)
        //{
        //    return Context.vwStaffs.Where(item => item.Sale_BranchId == BranchId && item.Position == Position && (item.IsDeleted == null || item.IsDeleted == false)).FirstOrDefault();
        //}
        /// <summary>
        /// Insert Staffs into database
        /// </summary>
        /// <param name="Staffs">Object infomation</param>
        public void InsertStaffs(Staffs Staffs)
        {
            Context.Staffs.Add(Staffs);
            Context.Entry(Staffs).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Staffs with specific id
        /// </summary>
        /// <param name="Id">Staffs Id</param>
        public void DeleteStaffs(int Id)
        {
            Staffs deletedStaffs = GetStaffsById(Id);
            Context.Staffs.Remove(deletedStaffs);
            Context.Entry(deletedStaffs).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Staffs with its Id and Update IsDeleted IF that Staffs has relationship with others
        /// </summary>
        /// <param name="StaffsId">Id of Staffs</param>
        public void DeleteStaffsRs(int Id)
        {
            Staffs deleteStaffsRs = GetStaffsById(Id);
            deleteStaffsRs.IsDeleted = true;
            UpdateStaffs(deleteStaffsRs);
        }

        /// <summary>
        /// Update Staffs into database
        /// </summary>
        /// <param name="Staffs">Staffs object</param>
        public void UpdateStaffs(Staffs Staffs)
        {
            Context.Entry(Staffs).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
