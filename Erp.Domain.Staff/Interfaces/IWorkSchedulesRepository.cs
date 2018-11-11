using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IWorkSchedulesRepository
    {
        /// <summary>
        /// Get all WorkSchedules
        /// </summary>
        /// <returns>WorkSchedules list</returns>
        IQueryable<WorkSchedules> GetAllWorkSchedules();
        IQueryable<vwWorkSchedules> GetvwAllWorkSchedules();
        /// <summary>
        /// Get WorkSchedules information by specific id
        /// </summary>
        /// <param name="Id">Id of WorkSchedules</param>
        /// <returns></returns>
        WorkSchedules GetWorkSchedulesById(int Id);
        vwWorkSchedules GetvwWorkSchedulesById(int Id);
        /// <summary>
        /// Insert WorkSchedules into database
        /// </summary>
        /// <param name="WorkSchedules">Object infomation</param>
        void InsertWorkSchedules(WorkSchedules WorkSchedules);

        /// <summary>
        /// Delete WorkSchedules with specific id
        /// </summary>
        /// <param name="Id">WorkSchedules Id</param>
        void DeleteWorkSchedules(int Id);

        /// <summary>
        /// Delete a WorkSchedules with its Id and Update IsDeleted IF that WorkSchedules has relationship with others
        /// </summary>
        /// <param name="Id">Id of WorkSchedules</param>
        void DeleteWorkSchedulesRs(int Id);

        /// <summary>
        /// Update WorkSchedules into database
        /// </summary>
        /// <param name="WorkSchedules">WorkSchedules object</param>
        void UpdateWorkSchedules(WorkSchedules WorkSchedules);
        WorkSchedules GetByStaffAndShifts(string Day, int StaffId, int ShiftsId);
        void Delete(string Day, int StaffId, int ShiftsId);
    }
}
