using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ITimekeepingSynthesisRepository
    {
        /// <summary>
        /// Get all TimekeepingSynthesis
        /// </summary>
        /// <returns>TimekeepingSynthesis list</returns>
        IQueryable<TimekeepingSynthesis> GetAllTimekeepingSynthesis();
        IQueryable<vwTimekeepingSynthesis> GetAllvwTimekeepingSynthesis();
        /// <summary>
        /// Get TimekeepingSynthesis information by specific id
        /// </summary>
        /// <param name="Id">Id of TimekeepingSynthesis</param>
        /// <returns></returns>
        TimekeepingSynthesis GetTimekeepingSynthesisById(int Id);
        TimekeepingSynthesis GetTimekeepingSynthesisByStaff(int StaffId, int Month, int Year);
        /// <summary>
        /// Insert TimekeepingSynthesis into database
        /// </summary>
        /// <param name="TimekeepingSynthesis">Object infomation</param>
        void InsertTimekeepingSynthesis(TimekeepingSynthesis TimekeepingSynthesis);

        /// <summary>
        /// Delete TimekeepingSynthesis with specific id
        /// </summary>
        /// <param name="Id">TimekeepingSynthesis Id</param>
        void DeleteTimekeepingSynthesis(int Id);

        /// <summary>
        /// Delete a TimekeepingSynthesis with its Id and Update IsDeleted IF that TimekeepingSynthesis has relationship with others
        /// </summary>
        /// <param name="Id">Id of TimekeepingSynthesis</param>
        void DeleteTimekeepingSynthesisRs(int Id);

        /// <summary>
        /// Update TimekeepingSynthesis into database
        /// </summary>
        /// <param name="TimekeepingSynthesis">TimekeepingSynthesis object</param>
        void UpdateTimekeepingSynthesis(TimekeepingSynthesis TimekeepingSynthesis);
    }
}
