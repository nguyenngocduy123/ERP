using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class ServiceComboRepository : GenericRepository<ErpSaleDbContext, ServiceCombo>, IServiceComboRepository
    {
        public ServiceComboRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ServiceCombo
        /// </summary>
        /// <returns>ServiceCombo list</returns>
        public IQueryable<ServiceCombo> GetAllServiceCombo()
        {
            return Context.ServiceCombo.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwServiceCombo> GetAllvwServiceCombo()
        {
            return Context.vwServiceCombo.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get ServiceCombo information by specific id
        /// </summary>
        /// <param name="ServiceComboId">Id of ServiceCombo</param>
        /// <returns></returns>
        public ServiceCombo GetServiceComboById(int Id)
        {
            return Context.ServiceCombo.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwServiceCombo GetvwServiceComboById(int Id)
        {
            return Context.vwServiceCombo.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert ServiceCombo into database
        /// </summary>
        /// <param name="ServiceCombo">Object infomation</param>
        public void InsertServiceCombo(ServiceCombo ServiceCombo)
        {
            Context.ServiceCombo.Add(ServiceCombo);
            Context.Entry(ServiceCombo).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ServiceCombo with specific id
        /// </summary>
        /// <param name="Id">ServiceCombo Id</param>
        public void DeleteServiceCombo(int Id)
        {
            ServiceCombo deletedServiceCombo = GetServiceComboById(Id);
            Context.ServiceCombo.Remove(deletedServiceCombo);
            Context.Entry(deletedServiceCombo).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ServiceCombo with its Id and Update IsDeleted IF that ServiceCombo has relationship with others
        /// </summary>
        /// <param name="ServiceComboId">Id of ServiceCombo</param>
        public void DeleteServiceComboRs(int Id)
        {
            ServiceCombo deleteServiceComboRs = GetServiceComboById(Id);
            deleteServiceComboRs.IsDeleted = true;
            UpdateServiceCombo(deleteServiceComboRs);
        }

        /// <summary>
        /// Update ServiceCombo into database
        /// </summary>
        /// <param name="ServiceCombo">ServiceCombo object</param>
        public void UpdateServiceCombo(ServiceCombo ServiceCombo)
        {
            Context.Entry(ServiceCombo).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
