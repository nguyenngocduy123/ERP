using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class TaxRateRepository : GenericRepository<ErpStaffDbContext, TaxRate>, ITaxRateRepository
    {
        public TaxRateRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all TaxRate
        /// </summary>
        /// <returns>TaxRate list</returns>
        public IQueryable<TaxRate> GetAllTaxRate()
        {
            return Context.TaxRate.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get TaxRate information by specific id
        /// </summary>
        /// <param name="TaxRateId">Id of TaxRate</param>
        /// <returns></returns>
        public TaxRate GetTaxRateById(int Id)
        {
            return Context.TaxRate.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert TaxRate into database
        /// </summary>
        /// <param name="TaxRate">Object infomation</param>
        public void InsertTaxRate(TaxRate TaxRate)
        {
            Context.TaxRate.Add(TaxRate);
            Context.Entry(TaxRate).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete TaxRate with specific id
        /// </summary>
        /// <param name="Id">TaxRate Id</param>
        public void DeleteTaxRate(int Id)
        {
            TaxRate deletedTaxRate = GetTaxRateById(Id);
            Context.TaxRate.Remove(deletedTaxRate);
            Context.Entry(deletedTaxRate).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a TaxRate with its Id and Update IsDeleted IF that TaxRate has relationship with others
        /// </summary>
        /// <param name="TaxRateId">Id of TaxRate</param>
        public void DeleteTaxRateRs(int Id)
        {
            TaxRate deleteTaxRateRs = GetTaxRateById(Id);
            deleteTaxRateRs.IsDeleted = true;
            UpdateTaxRate(deleteTaxRateRs);
        }

        /// <summary>
        /// Update TaxRate into database
        /// </summary>
        /// <param name="TaxRate">TaxRate object</param>
        public void UpdateTaxRate(TaxRate TaxRate)
        {
            Context.Entry(TaxRate).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
