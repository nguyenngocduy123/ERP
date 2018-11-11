using Erp.Domain.Crm.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Crm.Interfaces
{
    public interface IProcessRepository
    {
        /// <summary>
        /// Get all Process
        /// </summary>
        /// <returns>Process list</returns>
        IQueryable<Process> GetAllProcess();

        /// <summary>
        /// Get Process information by specific id
        /// </summary>
        /// <param name="Id">Id of Process</param>
        /// <returns></returns>
        Process GetProcessById(int Id);

        /// <summary>
        /// Insert Process into database
        /// </summary>
        /// <param name="Process">Object infomation</param>
        void InsertProcess(Process Process);

        /// <summary>
        /// Delete Process with specific id
        /// </summary>
        /// <param name="Id">Process Id</param>
        void DeleteProcess(int Id);

        /// <summary>
        /// Delete a Process with its Id and Update IsDeleted IF that Process has relationship with others
        /// </summary>
        /// <param name="Id">Id of Process</param>
        void DeleteProcessRs(int Id);

        /// <summary>
        /// Update Process into database
        /// </summary>
        /// <param name="Process">Process object</param>
        void UpdateProcess(Process Process);
    }
}
