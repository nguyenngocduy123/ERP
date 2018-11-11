using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IWorkingProcessRepository
    {
        /// <summary>
        /// Get all WorkingProcess
        /// </summary>
        /// <returns>WorkingProcess list</returns>
        IQueryable<WorkingProcess> GetAllWorkingProcess();

        /// <summary>
        /// Get WorkingProcess information by specific id
        /// </summary>
        /// <param name="Id">Id of WorkingProcess</param>
        /// <returns></returns>
        WorkingProcess GetWorkingProcessById(int Id);

        /// <summary>
        /// Insert WorkingProcess into database
        /// </summary>
        /// <param name="WorkingProcess">Object infomation</param>
        void InsertWorkingProcess(WorkingProcess WorkingProcess);

        /// <summary>
        /// Delete WorkingProcess with specific id
        /// </summary>
        /// <param name="Id">WorkingProcess Id</param>
        void DeleteWorkingProcess(int Id);

        /// <summary>
        /// Delete a WorkingProcess with its Id and Update IsDeleted IF that WorkingProcess has relationship with others
        /// </summary>
        /// <param name="Id">Id of WorkingProcess</param>
        void DeleteWorkingProcessRs(int Id);

        /// <summary>
        /// Update WorkingProcess into database
        /// </summary>
        /// <param name="WorkingProcess">WorkingProcess object</param>
        void UpdateWorkingProcess(WorkingProcess WorkingProcess);
    }
}
