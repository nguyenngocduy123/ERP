using Erp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Interfaces
{
    public interface IModuleRelationshipRepository
    {
        /// <summary>
        /// Get all ModuleRelationship
        /// </summary>
        /// <returns>ModuleRelationship list</returns>
        IQueryable<ModuleRelationship> GetAllModuleRelationship();

        /// <summary>
        /// Get ModuleRelationship information by specific id
        /// </summary>
        /// <param name="Id">Id of ModuleRelationship</param>
        /// <returns></returns>
        ModuleRelationship GetModuleRelationshipById(int Id);

        /// <summary>
        /// Insert ModuleRelationship into database
        /// </summary>
        /// <param name="ModuleRelationship">Object infomation</param>
        void InsertModuleRelationship(ModuleRelationship ModuleRelationship);

        /// <summary>
        /// Delete ModuleRelationship with specific id
        /// </summary>
        /// <param name="Id">ModuleRelationship Id</param>
        void DeleteModuleRelationship(int Id);

        /// <summary>
        /// Delete a ModuleRelationship with its Id and Update IsDeleted IF that ModuleRelationship has relationship with others
        /// </summary>
        /// <param name="Id">Id of ModuleRelationship</param>
        void DeleteModuleRelationshipRs(int Id);

        /// <summary>
        /// Update ModuleRelationship into database
        /// </summary>
        /// <param name="ModuleRelationship">ModuleRelationship object</param>
        void UpdateModuleRelationship(ModuleRelationship ModuleRelationship);
    }
}
