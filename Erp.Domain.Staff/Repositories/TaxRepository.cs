using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class TaxRepository : GenericRepository<ErpStaffDbContext, Tax>, ITaxRepository
    {
        public TaxRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Tax
        /// </summary>
        /// <returns>Tax list</returns>
        public IQueryable<Tax> GetAllTax()
        {
            return Context.Tax.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwTax> GetvwAllTax()
        {
            return Context.vwTax.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get Tax information by specific id
        /// </summary>
        /// <param name="TaxId">Id of Tax</param>
        /// <returns></returns>
        public Tax GetTaxById(int Id)
        {
            return Context.Tax.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Tax into database
        /// </summary>
        /// <param name="Tax">Object infomation</param>
        public void InsertTax(Tax Tax)
        {
            Context.Tax.Add(Tax);
            Context.Entry(Tax).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Tax with specific id
        /// </summary>
        /// <param name="Id">Tax Id</param>
        public void DeleteTax(int Id)
        {
            Tax deletedTax = GetTaxById(Id);
            Context.Tax.Remove(deletedTax);
            Context.Entry(deletedTax).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Tax with its Id and Update IsDeleted IF that Tax has relationship with others
        /// </summary>
        /// <param name="TaxId">Id of Tax</param>
        public void DeleteTaxRs(int Id)
        {
            Tax deleteTaxRs = GetTaxById(Id);
            deleteTaxRs.IsDeleted = true;
            UpdateTax(deleteTaxRs);
        }

        /// <summary>
        /// Update Tax into database
        /// </summary>
        /// <param name="Tax">Tax object</param>
        public void UpdateTax(Tax Tax)
        {
            Context.Entry(Tax).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
