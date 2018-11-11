using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class TaxIncomePersonRepository : GenericRepository<ErpStaffDbContext, TaxIncomePerson>, ITaxIncomePersonRepository
    {
        public TaxIncomePersonRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all TaxIncomePerson
        /// </summary>
        /// <returns>TaxIncomePerson list</returns>
        public IQueryable<TaxIncomePerson> GetAllTaxIncomePerson()
        {
            return Context.TaxIncomePerson.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get TaxIncomePerson information by specific id
        /// </summary>
        /// <param name="TaxIncomePersonId">Id of TaxIncomePerson</param>
        /// <returns></returns>
        public TaxIncomePerson GetTaxIncomePersonById(int Id)
        {
            return Context.TaxIncomePerson.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert TaxIncomePerson into database
        /// </summary>
        /// <param name="TaxIncomePerson">Object infomation</param>
        public void InsertTaxIncomePerson(TaxIncomePerson TaxIncomePerson)
        {
            Context.TaxIncomePerson.Add(TaxIncomePerson);
            Context.Entry(TaxIncomePerson).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete TaxIncomePerson with specific id
        /// </summary>
        /// <param name="Id">TaxIncomePerson Id</param>
        public void DeleteTaxIncomePerson(int Id)
        {
            TaxIncomePerson deletedTaxIncomePerson = GetTaxIncomePersonById(Id);
            Context.TaxIncomePerson.Remove(deletedTaxIncomePerson);
            Context.Entry(deletedTaxIncomePerson).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a TaxIncomePerson with its Id and Update IsDeleted IF that TaxIncomePerson has relationship with others
        /// </summary>
        /// <param name="TaxIncomePersonId">Id of TaxIncomePerson</param>
        public void DeleteTaxIncomePersonRs(int Id)
        {
            TaxIncomePerson deleteTaxIncomePersonRs = GetTaxIncomePersonById(Id);
            deleteTaxIncomePersonRs.IsDeleted = true;
            UpdateTaxIncomePerson(deleteTaxIncomePersonRs);
        }

        /// <summary>
        /// Update TaxIncomePerson into database
        /// </summary>
        /// <param name="TaxIncomePerson">TaxIncomePerson object</param>
        public void UpdateTaxIncomePerson(TaxIncomePerson TaxIncomePerson)
        {
            Context.Entry(TaxIncomePerson).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
