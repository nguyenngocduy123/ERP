using Erp.Domain.RealEstate.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.RealEstate.Interfaces
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Get all Project
        /// </summary>
        /// <returns>Project list</returns>
        IQueryable<Project> GetAllProject();

        /// <summary>
        /// Get Project information by specific id
        /// </summary>
        /// <param name="Id">Id of Project</param>
        /// <returns></returns>
        Project GetProjectById(int Id);

        /// <summary>
        /// Insert Project into database
        /// </summary>
        /// <param name="Project">Object infomation</param>
        void InsertProject(Project Project);

        /// <summary>
        /// Delete Project with specific id
        /// </summary>
        /// <param name="Id">Project Id</param>
        void DeleteProject(int Id);

        /// <summary>
        /// Delete a Project with its Id and Update IsDeleted IF that Project has relationship with others
        /// </summary>
        /// <param name="Id">Id of Project</param>
        void DeleteProjectRs(int Id);

        /// <summary>
        /// Update Project into database
        /// </summary>
        /// <param name="Project">Project object</param>
        void UpdateProject(Project Project);
    }
}
