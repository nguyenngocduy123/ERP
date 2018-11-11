using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Repositories
{
    public class ModuleRelationshipRepository : GenericRepository<ErpDbContext, ModuleRelationship>, IModuleRelationshipRepository
    {
        public ModuleRelationshipRepository(ErpDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ModuleRelationship
        /// </summary>
        /// <returns>ModuleRelationship list</returns>
        public IQueryable<ModuleRelationship> GetAllModuleRelationship()
        {
            return Context.ModuleRelationship.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get ModuleRelationship information by specific id
        /// </summary>
        /// <param name="ModuleRelationshipId">Id of ModuleRelationship</param>
        /// <returns></returns>
        public ModuleRelationship GetModuleRelationshipById(int Id)
        {
            return Context.ModuleRelationship.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ModuleRelationship into database
        /// </summary>
        /// <param name="ModuleRelationship">Object infomation</param>
        public void InsertModuleRelationship(ModuleRelationship ModuleRelationship)
        {
            Context.ModuleRelationship.Add(ModuleRelationship);
            Context.Entry(ModuleRelationship).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ModuleRelationship with specific id
        /// </summary>
        /// <param name="Id">ModuleRelationship Id</param>
        public void DeleteModuleRelationship(int Id)
        {
            ModuleRelationship deletedModuleRelationship = GetModuleRelationshipById(Id);
            Context.ModuleRelationship.Remove(deletedModuleRelationship);
            Context.Entry(deletedModuleRelationship).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ModuleRelationship with its Id and Update IsDeleted IF that ModuleRelationship has relationship with others
        /// </summary>
        /// <param name="ModuleRelationshipId">Id of ModuleRelationship</param>
        public void DeleteModuleRelationshipRs(int Id)
        {
            ModuleRelationship deleteModuleRelationshipRs = GetModuleRelationshipById(Id);
            deleteModuleRelationshipRs.IsDeleted = true;
            UpdateModuleRelationship(deleteModuleRelationshipRs);
        }

        /// <summary>
        /// Update ModuleRelationship into database
        /// </summary>
        /// <param name="ModuleRelationship">ModuleRelationship object</param>
        public void UpdateModuleRelationship(ModuleRelationship ModuleRelationship)
        {
            Context.Entry(ModuleRelationship).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
