using Erp.Domain.Account.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Account.Interfaces
{
    public interface IContactRepository
    {
        /// <summary>
        /// Get all Contact
        /// </summary>
        /// <returns>Contact list</returns>
        IQueryable<Contact> GetAllContact();
        IQueryable<vwContact> GetAllvwContact();
        IQueryable<Contact> GetAllContactByCustomerId(int customerId);
        IQueryable<vwContact> GetAllvwContactByCustomerId(int customerId);
        /// <summary>
        /// Get Contact information by specific id
        /// </summary>
        /// <param name="Id">Id of Contact</param>
        /// <returns></returns>
        Contact GetContactById(int Id);
        vwContact GetvwContactById(int Id);
        vwLogContractbyCondos GetvwLogContractbyId(int Id);
        /// <summary>
        /// Insert Contact into database
        /// </summary>
        /// <param name="Contact">Object infomation</param>
        void InsertContact(Contact Contact);

        /// <summary>
        /// Delete Contact with specific id
        /// </summary>
        /// <param name="Id">Contact Id</param>
        void DeleteContact(int Id);

        /// <summary>
        /// Delete a Contact with its Id and Update IsDeleted IF that Contact has relationship with others
        /// </summary>
        /// <param name="Id">Id of Contact</param>
        void DeleteContactRs(int Id);

        /// <summary>
        /// Update Contact into database
        /// </summary>
        /// <param name="Contact">Contact object</param>
        void UpdateContact(Contact Contact);
    }
}
