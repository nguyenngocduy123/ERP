using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class TechniqueRepository : GenericRepository<ErpStaffDbContext, Technique>, ITechniqueRepository
    {
        public TechniqueRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Technique
        /// </summary>
        /// <returns>Technique list</returns>
        public IQueryable<Technique> GetAllTechnique()
        {
            return Context.Technique.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Technique information by specific id
        /// </summary>
        /// <param name="TechniqueId">Id of Technique</param>
        /// <returns></returns>
        public Technique GetTechniqueById(int Id)
        {
            return Context.Technique.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Technique into database
        /// </summary>
        /// <param name="Technique">Object infomation</param>
        public void InsertTechnique(Technique Technique)
        {
            Context.Technique.Add(Technique);
            Context.Entry(Technique).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Technique with specific id
        /// </summary>
        /// <param name="Id">Technique Id</param>
        public void DeleteTechnique(int Id)
        {
            Technique deletedTechnique = GetTechniqueById(Id);
            Context.Technique.Remove(deletedTechnique);
            Context.Entry(deletedTechnique).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Technique with its Id and Update IsDeleted IF that Technique has relationship with others
        /// </summary>
        /// <param name="TechniqueId">Id of Technique</param>
        public void DeleteTechniqueRs(int Id)
        {
            Technique deleteTechniqueRs = GetTechniqueById(Id);
            deleteTechniqueRs.IsDeleted = true;
            UpdateTechnique(deleteTechniqueRs);
        }

        /// <summary>
        /// Update Technique into database
        /// </summary>
        /// <param name="Technique">Technique object</param>
        public void UpdateTechnique(Technique Technique)
        {
            Context.Entry(Technique).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
