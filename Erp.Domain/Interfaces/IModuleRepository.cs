using Erp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Interfaces
{
    public interface IModuleRepository
    {
        /// <summary>
        /// Get all Module
        /// </summary>
        /// <returns>Module list</returns>
        IQueryable<Module> GetAllModule();

        /// <summary>
        /// Get Module information by specific id
        /// </summary>
        /// <param name="Id">Id of Module</param>
        /// <returns></returns>
        Module GetModuleById(int Id);

        /// <summary>
        /// Insert Module into database
        /// </summary>
        /// <param name="Module">Object infomation</param>
        void InsertModule(Module Module);

        /// <summary>
        /// Delete Module with specific id
        /// </summary>
        /// <param name="Id">Module Id</param>
        void DeleteModule(int Id);

        /// <summary>
        /// Delete a Module with its Id and Update IsDeleted IF that Module has relationship with others
        /// </summary>
        /// <param name="Id">Id of Module</param>
        void DeleteModuleRs(int Id);

        /// <summary>
        /// Update Module into database
        /// </summary>
        /// <param name="Module">Module object</param>
        void UpdateModule(Module Module);
    }
}
