using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Repositories
{
    public class ModuleRepository : GenericRepository<ErpDbContext, Module>, IModuleRepository
    {
        public ModuleRepository(ErpDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Module
        /// </summary>
        /// <returns>Module list</returns>
        public IQueryable<Module> GetAllModule()
        {
            return Context.Module.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Module information by specific id
        /// </summary>
        /// <param name="ModuleId">Id of Module</param>
        /// <returns></returns>
        public Module GetModuleById(int Id)
        {
            return Context.Module.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Module into database
        /// </summary>
        /// <param name="Module">Object infomation</param>
        public void InsertModule(Module Module)
        {
            Context.Module.Add(Module);
            Context.Entry(Module).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Module with specific id
        /// </summary>
        /// <param name="Id">Module Id</param>
        public void DeleteModule(int Id)
        {
            Module deletedModule = GetModuleById(Id);
            Context.Module.Remove(deletedModule);
            Context.Entry(deletedModule).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Module with its Id and Update IsDeleted IF that Module has relationship with others
        /// </summary>
        /// <param name="ModuleId">Id of Module</param>
        public void DeleteModuleRs(int Id)
        {
            Module deleteModuleRs = GetModuleById(Id);
            deleteModuleRs.IsDeleted = true;
            UpdateModule(deleteModuleRs);
        }

        /// <summary>
        /// Update Module into database
        /// </summary>
        /// <param name="Module">Module object</param>
        public void UpdateModule(Module Module)
        {
            Context.Entry(Module).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
