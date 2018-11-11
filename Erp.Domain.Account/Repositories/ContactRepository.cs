using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Account.Repositories
{
    public class ContactRepository : GenericRepository<ErpAccountDbContext, Contact>, IContactRepository
    {
        public ContactRepository(ErpAccountDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Contact
        /// </summary>
        /// <returns>Contact list</returns>
        public IQueryable<Contact> GetAllContact()
        {
            return Context.Contact.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwContact> GetAllvwContact()
        {
            return Context.vwContact.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<Contact> GetAllContactByCustomerId(int customerId)
        {
            return Context.Contact.Where(item => item.CustomerId == customerId && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwContact> GetAllvwContactByCustomerId(int customerId)
        {
            return Context.vwContact.Where(item => item.CustomerId == customerId && (item.IsDeleted == null || item.IsDeleted == false));
        }
       
        /// <summary>
        /// Get Contact information by specific id
        /// </summary>
        /// <param name="ContactId">Id of Contact</param>
        /// <returns></returns>
        public Contact GetContactById(int Id)
        {
            return Context.Contact.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwContact GetvwContactById(int Id)
        {
            return Context.vwContact.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwLogContractbyCondos GetvwLogContractbyId(int Id)
        {
            return Context.vwLogContractbyCondos.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert Contact into database
        /// </summary>
        /// <param name="Contact">Object infomation</param>
        public void InsertContact(Contact Contact)
        {
            Context.Contact.Add(Contact);
            Context.Entry(Contact).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Contact with specific id
        /// </summary>
        /// <param name="Id">Contact Id</param>
        public void DeleteContact(int Id)
        {
            Contact deletedContact = GetContactById(Id);
            Context.Contact.Remove(deletedContact);
            Context.Entry(deletedContact).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Contact with its Id and Update IsDeleted IF that Contact has relationship with others
        /// </summary>
        /// <param name="ContactId">Id of Contact</param>
        public void DeleteContactRs(int Id)
        {
            Contact deleteContactRs = GetContactById(Id);
            deleteContactRs.IsDeleted = true;
            UpdateContact(deleteContactRs);
        }

        /// <summary>
        /// Update Contact into database
        /// </summary>
        /// <param name="Contact">Contact object</param>
        public void UpdateContact(Contact Contact)
        {
            Context.Entry(Contact).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
