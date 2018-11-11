using Erp.Domain.Crm.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Crm.Interfaces
{
    public interface IProcessStepRepository
    {
        /// <summary>
        /// Get all ProcessStep
        /// </summary>
        /// <returns>ProcessStep list</returns>
        IQueryable<ProcessStep> GetAllProcessStep();

        /// <summary>
        /// Get ProcessStep information by specific id
        /// </summary>
        /// <param name="Id">Id of ProcessStep</param>
        /// <returns></returns>
        ProcessStep GetProcessStepById(int Id);

        /// <summary>
        /// Insert ProcessStep into database
        /// </summary>
        /// <param name="ProcessStep">Object infomation</param>
        void InsertProcessStep(ProcessStep ProcessStep);

        /// <summary>
        /// Delete ProcessStep with specific id
        /// </summary>
        /// <param name="Id">ProcessStep Id</param>
        void DeleteProcessStep(int Id);

        /// <summary>
        /// Delete a ProcessStep with its Id and Update IsDeleted IF that ProcessStep has relationship with others
        /// </summary>
        /// <param name="Id">Id of ProcessStep</param>
        void DeleteProcessStepRs(int Id);

        /// <summary>
        /// Update ProcessStep into database
        /// </summary>
        /// <param name="ProcessStep">ProcessStep object</param>
        void UpdateProcessStep(ProcessStep ProcessStep);
    }
}
