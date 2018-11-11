using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IRegisterForOvertimeRepository
    {
        /// <summary>
        /// Get all RegisterForOvertime
        /// </summary>
        /// <returns>RegisterForOvertime list</returns>
        IQueryable<RegisterForOvertime> GetAllRegisterForOvertime();
        IQueryable<vwRegisterForOvertime> GetAllvwRegisterForOvertime();
        /// <summary>
        /// Get RegisterForOvertime information by specific id
        /// </summary>
        /// <param name="Id">Id of RegisterForOvertime</param>
        /// <returns></returns>
        RegisterForOvertime GetRegisterForOvertimeById(int Id);
        vwRegisterForOvertime GetvwRegisterForOvertimeById(int Id);
        /// <summary>
        /// Insert RegisterForOvertime into database
        /// </summary>
        /// <param name="RegisterForOvertime">Object infomation</param>
        void InsertRegisterForOvertime(RegisterForOvertime RegisterForOvertime);

        /// <summary>
        /// Delete RegisterForOvertime with specific id
        /// </summary>
        /// <param name="Id">RegisterForOvertime Id</param>
        void DeleteRegisterForOvertime(int Id);

        /// <summary>
        /// Delete a RegisterForOvertime with its Id and Update IsDeleted IF that RegisterForOvertime has relationship with others
        /// </summary>
        /// <param name="Id">Id of RegisterForOvertime</param>
        void DeleteRegisterForOvertimeRs(int Id);

        /// <summary>
        /// Update RegisterForOvertime into database
        /// </summary>
        /// <param name="RegisterForOvertime">RegisterForOvertime object</param>
        void UpdateRegisterForOvertime(RegisterForOvertime RegisterForOvertime);
    }
}
