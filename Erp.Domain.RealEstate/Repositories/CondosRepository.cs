using Erp.Domain.RealEstate.Entities;
using Erp.Domain.RealEstate.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.RealEstate.Repositories
{
    public class CondosRepository : GenericRepository<ErpRealEstateDbContext, Condos>, ICondosRepository
    {
        public CondosRepository(ErpRealEstateDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Condos
        /// </summary>
        /// <returns>Condos list</returns>
        public IQueryable<Condos> GetAllCondos()
        {
            return Context.Condos.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Condos information by specific id
        /// </summary>
        /// <param name="CondosId">Id of Condos</param>
        /// <returns></returns>
        public Condos GetCondosById(int Id)
        {
            return Context.Condos.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public vwCondos GetvwCondosById(int Id)
        {
            return Context.vwCondos.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Condos into database
        /// </summary>
        /// <param name="Condos">Object infomation</param>
        public void InsertCondos(Condos Condos)
        {
            Context.Condos.Add(Condos);
            Context.Entry(Condos).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Condos with specific id
        /// </summary>
        /// <param name="Id">Condos Id</param>
        public void DeleteCondos(int Id)
        {
            Condos deletedCondos = GetCondosById(Id);
            Context.Condos.Remove(deletedCondos);
            Context.Entry(deletedCondos).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Condos with its Id and Update IsDeleted IF that Condos has relationship with others
        /// </summary>
        /// <param name="CondosId">Id of Condos</param>
        public void DeleteCondosRs(int Id)
        {
            Condos deleteCondosRs = GetCondosById(Id);
            deleteCondosRs.IsDeleted = true;
            UpdateCondos(deleteCondosRs);
        }

        /// <summary>
        /// Update Condos into database
        /// </summary>
        /// <param name="Condos">Condos object</param>
        public void UpdateCondos(Condos Condos)
        {
            Context.Entry(Condos).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
