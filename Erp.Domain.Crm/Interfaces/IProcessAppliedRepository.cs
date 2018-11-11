using Erp.Domain.Crm.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Crm.Interfaces
{
    public interface IProcessAppliedRepository
    {
        /// <summary>
        /// Get all ProcessApplied
        /// </summary>
        /// <returns>ProcessApplied list</returns>
        IQueryable<ProcessApplied> GetAllProcessApplied();

        /// <summary>
        /// Get ProcessApplied information by specific id
        /// </summary>
        /// <param name="Id">Id of ProcessApplied</param>
        /// <returns></returns>
        ProcessApplied GetProcessAppliedById(int Id);

        /// <summary>
        /// Insert ProcessApplied into database
        /// </summary>
        /// <param name="ProcessApplied">Object infomation</param>
        void InsertProcessApplied(ProcessApplied ProcessApplied);

        /// <summary>
        /// Delete ProcessApplied with specific id
        /// </summary>
        /// <param name="Id">ProcessApplied Id</param>
        void DeleteProcessApplied(int Id);

        /// <summary>
        /// Delete a ProcessApplied with its Id and Update IsDeleted IF that ProcessApplied has relationship with others
        /// </summary>
        /// <param name="Id">Id of ProcessApplied</param>
        void DeleteProcessAppliedRs(int Id);

        /// <summary>
        /// Update ProcessApplied into database
        /// </summary>
        /// <param name="ProcessApplied">ProcessApplied object</param>
        void UpdateProcessApplied(ProcessApplied ProcessApplied);
    }
}
