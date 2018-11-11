using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class BankRepository : GenericRepository<ErpStaffDbContext, Bank>, IBankRepository
    {
        public BankRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Bank
        /// </summary>
        /// <returns>Bank list</returns>
        public IQueryable<Bank> GetAllBank()
        {
            return Context.Bank.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Bank information by specific id
        /// </summary>
        /// <param name="BankId">Id of Bank</param>
        /// <returns></returns>
        public Bank GetBankById(int Id)
        {
            return Context.Bank.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Bank into database
        /// </summary>
        /// <param name="Bank">Object infomation</param>
        public void InsertBank(Bank Bank)
        {
            Context.Bank.Add(Bank);
            Context.Entry(Bank).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Bank with specific id
        /// </summary>
        /// <param name="Id">Bank Id</param>
        public void DeleteBank(int Id)
        {
            Bank deletedBank = GetBankById(Id);
            Context.Bank.Remove(deletedBank);
            Context.Entry(deletedBank).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Bank with its Id and Update IsDeleted IF that Bank has relationship with others
        /// </summary>
        /// <param name="BankId">Id of Bank</param>
        public void DeleteBankRs(int Id)
        {
            Bank deleteBankRs = GetBankById(Id);
            deleteBankRs.IsDeleted = true;
            UpdateBank(deleteBankRs);
        }

        /// <summary>
        /// Update Bank into database
        /// </summary>
        /// <param name="Bank">Bank object</param>
        public void UpdateBank(Bank Bank)
        {
            Context.Entry(Bank).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
