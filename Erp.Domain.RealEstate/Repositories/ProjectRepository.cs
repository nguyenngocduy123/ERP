using Erp.Domain.RealEstate.Entities;
using Erp.Domain.RealEstate.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.RealEstate.Repositories
{
    public class ProjectRepository : GenericRepository<ErpRealEstateDbContext, Project>, IProjectRepository
    {
        public ProjectRepository(ErpRealEstateDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Project
        /// </summary>
        /// <returns>Project list</returns>
        public IQueryable<Project> GetAllProject()
        {
            return Context.Project.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Project information by specific id
        /// </summary>
        /// <param name="ProjectId">Id of Project</param>
        /// <returns></returns>
        public Project GetProjectById(int Id)
        {
            return Context.Project.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Project into database
        /// </summary>
        /// <param name="Project">Object infomation</param>
        public void InsertProject(Project Project)
        {
            Context.Project.Add(Project);
            Context.Entry(Project).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Project with specific id
        /// </summary>
        /// <param name="Id">Project Id</param>
        public void DeleteProject(int Id)
        {
            Project deletedProject = GetProjectById(Id);
            Context.Project.Remove(deletedProject);
            Context.Entry(deletedProject).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Project with its Id and Update IsDeleted IF that Project has relationship with others
        /// </summary>
        /// <param name="ProjectId">Id of Project</param>
        public void DeleteProjectRs(int Id)
        {
            Project deleteProjectRs = GetProjectById(Id);
            deleteProjectRs.IsDeleted = true;
            UpdateProject(deleteProjectRs);
        }

        /// <summary>
        /// Update Project into database
        /// </summary>
        /// <param name="Project">Project object</param>
        public void UpdateProject(Project Project)
        {
            Context.Entry(Project).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
