using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IStaffLuongGiuHopDongRepository
    {
        /// <summary>
        /// Get all StaffLuongGiuHopDong
        /// </summary>
        /// <returns>StaffLuongGiuHopDong list</returns>
        IQueryable<StaffLuongGiuHopDong> GetAllStaffLuongGiuHopDong();

        /// <summary>
        /// Get StaffLuongGiuHopDong information by specific id
        /// </summary>
        /// <param name="Id">Id of StaffLuongGiuHopDong</param>
        /// <returns></returns>
        StaffLuongGiuHopDong GetStaffLuongGiuHopDongById(int Id);

        /// <summary>
        /// Insert StaffLuongGiuHopDong into database
        /// </summary>
        /// <param name="StaffLuongGiuHopDong">Object infomation</param>
        void InsertStaffLuongGiuHopDong(StaffLuongGiuHopDong StaffLuongGiuHopDong);

        /// <summary>
        /// Delete StaffLuongGiuHopDong with specific id
        /// </summary>
        /// <param name="Id">StaffLuongGiuHopDong Id</param>
        void DeleteStaffLuongGiuHopDong(int Id);

        /// <summary>
        /// Delete a StaffLuongGiuHopDong with its Id and Update IsDeleted IF that StaffLuongGiuHopDong has relationship with others
        /// </summary>
        /// <param name="Id">Id of StaffLuongGiuHopDong</param>
        void DeleteStaffLuongGiuHopDongRs(int Id);

        /// <summary>
        /// Update StaffLuongGiuHopDong into database
        /// </summary>
        /// <param name="StaffLuongGiuHopDong">StaffLuongGiuHopDong object</param>
        void UpdateStaffLuongGiuHopDong(StaffLuongGiuHopDong StaffLuongGiuHopDong);
    }
}
