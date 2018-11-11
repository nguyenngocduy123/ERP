using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IWelfareProgramsRepository
    {
        /// <summary>
        /// Get all WelfarePrograms
        /// </summary>
        /// <returns>WelfarePrograms list</returns>
        IQueryable<WelfarePrograms> GetAllWelfarePrograms();

        /// <summary>
        /// Get WelfarePrograms information by specific id
        /// </summary>
        /// <param name="Id">Id of WelfarePrograms</param>
        /// <returns></returns>
        WelfarePrograms GetWelfareProgramsById(int Id);

        /// <summary>
        /// Insert WelfarePrograms into database
        /// </summary>
        /// <param name="WelfarePrograms">Object infomation</param>
        void InsertWelfarePrograms(WelfarePrograms WelfarePrograms);

        /// <summary>
        /// Delete WelfarePrograms with specific id
        /// </summary>
        /// <param name="Id">WelfarePrograms Id</param>
        void DeleteWelfarePrograms(int Id);

        /// <summary>
        /// Delete a WelfarePrograms with its Id and Update IsDeleted IF that WelfarePrograms has relationship with others
        /// </summary>
        /// <param name="Id">Id of WelfarePrograms</param>
        void DeleteWelfareProgramsRs(int Id);

        /// <summary>
        /// Update WelfarePrograms into database
        /// </summary>
        /// <param name="WelfarePrograms">WelfarePrograms object</param>
        void UpdateWelfarePrograms(WelfarePrograms WelfarePrograms);
    }
}
