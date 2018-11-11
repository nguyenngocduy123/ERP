using Erp.Domain.Crm.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Crm.Interfaces
{
    public interface IProcessActionRepository
    {
        /// <summary>
        /// Get all ProcessAction
        /// </summary>
        /// <returns>ProcessAction list</returns>
        IQueryable<ProcessAction> GetAllProcessAction();

        /// <summary>
        /// Get ProcessAction information by specific id
        /// </summary>
        /// <param name="Id">Id of ProcessAction</param>
        /// <returns></returns>
        ProcessAction GetProcessActionById(int Id);

        /// <summary>
        /// Insert ProcessAction into database
        /// </summary>
        /// <param name="ProcessAction">Object infomation</param>
        void InsertProcessAction(ProcessAction ProcessAction);

        /// <summary>
        /// Delete ProcessAction with specific id
        /// </summary>
        /// <param name="Id">ProcessAction Id</param>
        void DeleteProcessAction(int Id);

        /// <summary>
        /// Delete a ProcessAction with its Id and Update IsDeleted IF that ProcessAction has relationship with others
        /// </summary>
        /// <param name="Id">Id of ProcessAction</param>
        void DeleteProcessActionRs(int Id);

        /// <summary>
        /// Update ProcessAction into database
        /// </summary>
        /// <param name="ProcessAction">ProcessAction object</param>
        void UpdateProcessAction(ProcessAction ProcessAction);
    }
}
