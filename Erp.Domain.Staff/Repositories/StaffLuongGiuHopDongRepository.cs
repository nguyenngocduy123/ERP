using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class StaffLuongGiuHopDongRepository : GenericRepository<ErpStaffDbContext, StaffLuongGiuHopDong>, IStaffLuongGiuHopDongRepository
    {
        public StaffLuongGiuHopDongRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all StaffLuongGiuHopDong
        /// </summary>
        /// <returns>StaffLuongGiuHopDong list</returns>
        public IQueryable<StaffLuongGiuHopDong> GetAllStaffLuongGiuHopDong()
        {
            return Context.StaffLuongGiuHopDong.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get StaffLuongGiuHopDong information by specific id
        /// </summary>
        /// <param name="StaffLuongGiuHopDongId">Id of StaffLuongGiuHopDong</param>
        /// <returns></returns>
        public StaffLuongGiuHopDong GetStaffLuongGiuHopDongById(int Id)
        {
            return Context.StaffLuongGiuHopDong.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert StaffLuongGiuHopDong into database
        /// </summary>
        /// <param name="StaffLuongGiuHopDong">Object infomation</param>
        public void InsertStaffLuongGiuHopDong(StaffLuongGiuHopDong StaffLuongGiuHopDong)
        {
            Context.StaffLuongGiuHopDong.Add(StaffLuongGiuHopDong);
            Context.Entry(StaffLuongGiuHopDong).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete StaffLuongGiuHopDong with specific id
        /// </summary>
        /// <param name="Id">StaffLuongGiuHopDong Id</param>
        public void DeleteStaffLuongGiuHopDong(int Id)
        {
            StaffLuongGiuHopDong deletedStaffLuongGiuHopDong = GetStaffLuongGiuHopDongById(Id);
            Context.StaffLuongGiuHopDong.Remove(deletedStaffLuongGiuHopDong);
            Context.Entry(deletedStaffLuongGiuHopDong).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a StaffLuongGiuHopDong with its Id and Update IsDeleted IF that StaffLuongGiuHopDong has relationship with others
        /// </summary>
        /// <param name="StaffLuongGiuHopDongId">Id of StaffLuongGiuHopDong</param>
        public void DeleteStaffLuongGiuHopDongRs(int Id)
        {
            StaffLuongGiuHopDong deleteStaffLuongGiuHopDongRs = GetStaffLuongGiuHopDongById(Id);
            deleteStaffLuongGiuHopDongRs.IsDeleted = true;
            UpdateStaffLuongGiuHopDong(deleteStaffLuongGiuHopDongRs);
        }

        /// <summary>
        /// Update StaffLuongGiuHopDong into database
        /// </summary>
        /// <param name="StaffLuongGiuHopDong">StaffLuongGiuHopDong object</param>
        public void UpdateStaffLuongGiuHopDong(StaffLuongGiuHopDong StaffLuongGiuHopDong)
        {
            Context.Entry(StaffLuongGiuHopDong).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
