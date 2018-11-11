using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IHistoryCommissionStaffRepository
    {
        /// <summary>
        /// Get all HistoryCommissionStaff
        /// </summary>
        /// <returns>HistoryCommissionStaff list</returns>
        IQueryable<HistoryCommissionStaff> GetAllHistoryCommissionStaff();
        IQueryable<HistoryCommissionStaff> GetAllHistoryCommissionStaffFull();
        /// <summary>
        /// Get HistoryCommissionStaff information by specific id
        /// </summary>
        /// <param name="Id">Id of HistoryCommissionStaff</param>
        /// <returns></returns>
        HistoryCommissionStaff GetHistoryCommissionStaffById(int Id);
        HistoryCommissionStaff GetHistoryCommissionStaffFullById(int Id);
        /// <summary>
        /// Insert HistoryCommissionStaff into database
        /// </summary>
        /// <param name="HistoryCommissionStaff">Object infomation</param>
        void InsertHistoryCommissionStaff(HistoryCommissionStaff HistoryCommissionStaff);

        /// <summary>
        /// Delete HistoryCommissionStaff with specific id
        /// </summary>
        /// <param name="Id">HistoryCommissionStaff Id</param>
        void DeleteHistoryCommissionStaff(int Id);

        /// <summary>
        /// Delete a HistoryCommissionStaff with its Id and Update IsDeleted IF that HistoryCommissionStaff has relationship with others
        /// </summary>
        /// <param name="Id">Id of HistoryCommissionStaff</param>
        void DeleteHistoryCommissionStaffRs(int Id);

        /// <summary>
        /// Update HistoryCommissionStaff into database
        /// </summary>
        /// <param name="HistoryCommissionStaff">HistoryCommissionStaff object</param>
        void UpdateHistoryCommissionStaff(HistoryCommissionStaff HistoryCommissionStaff);
    }
}
