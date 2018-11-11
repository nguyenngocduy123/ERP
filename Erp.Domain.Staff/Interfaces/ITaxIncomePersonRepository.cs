using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ITaxIncomePersonRepository
    {
        /// <summary>
        /// Get all TaxIncomePerson
        /// </summary>
        /// <returns>TaxIncomePerson list</returns>
        IQueryable<TaxIncomePerson> GetAllTaxIncomePerson();

        /// <summary>
        /// Get TaxIncomePerson information by specific id
        /// </summary>
        /// <param name="Id">Id of TaxIncomePerson</param>
        /// <returns></returns>
        TaxIncomePerson GetTaxIncomePersonById(int Id);

        /// <summary>
        /// Insert TaxIncomePerson into database
        /// </summary>
        /// <param name="TaxIncomePerson">Object infomation</param>
        void InsertTaxIncomePerson(TaxIncomePerson TaxIncomePerson);

        /// <summary>
        /// Delete TaxIncomePerson with specific id
        /// </summary>
        /// <param name="Id">TaxIncomePerson Id</param>
        void DeleteTaxIncomePerson(int Id);

        /// <summary>
        /// Delete a TaxIncomePerson with its Id and Update IsDeleted IF that TaxIncomePerson has relationship with others
        /// </summary>
        /// <param name="Id">Id of TaxIncomePerson</param>
        void DeleteTaxIncomePersonRs(int Id);

        /// <summary>
        /// Update TaxIncomePerson into database
        /// </summary>
        /// <param name="TaxIncomePerson">TaxIncomePerson object</param>
        void UpdateTaxIncomePerson(TaxIncomePerson TaxIncomePerson);
    }
}
