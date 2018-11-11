using Erp.Domain.Crm.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Crm.Interfaces
{
    public interface IProcessStageRepository
    {
        /// <summary>
        /// Get all ProcessStage
        /// </summary>
        /// <returns>ProcessStage list</returns>
        IQueryable<ProcessStage> GetAllProcessStage();

        /// <summary>
        /// Get ProcessStage information by specific id
        /// </summary>
        /// <param name="Id">Id of ProcessStage</param>
        /// <returns></returns>
        ProcessStage GetProcessStageById(int Id);

        /// <summary>
        /// Insert ProcessStage into database
        /// </summary>
        /// <param name="ProcessStage">Object infomation</param>
        void InsertProcessStage(ProcessStage ProcessStage);

        /// <summary>
        /// Delete ProcessStage with specific id
        /// </summary>
        /// <param name="Id">ProcessStage Id</param>
        void DeleteProcessStage(int Id);

        /// <summary>
        /// Delete a ProcessStage with its Id and Update IsDeleted IF that ProcessStage has relationship with others
        /// </summary>
        /// <param name="Id">Id of ProcessStage</param>
        void DeleteProcessStageRs(int Id);

        /// <summary>
        /// Update ProcessStage into database
        /// </summary>
        /// <param name="ProcessStage">ProcessStage object</param>
        void UpdateProcessStage(ProcessStage ProcessStage);
    }
}
