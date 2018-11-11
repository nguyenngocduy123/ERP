using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IBankRepository
    {
        /// <summary>
        /// Get all Bank
        /// </summary>
        /// <returns>Bank list</returns>
        IQueryable<Bank> GetAllBank();

        /// <summary>
        /// Get Bank information by specific id
        /// </summary>
        /// <param name="Id">Id of Bank</param>
        /// <returns></returns>
        Bank GetBankById(int Id);

        /// <summary>
        /// Insert Bank into database
        /// </summary>
        /// <param name="Bank">Object infomation</param>
        void InsertBank(Bank Bank);

        /// <summary>
        /// Delete Bank with specific id
        /// </summary>
        /// <param name="Id">Bank Id</param>
        void DeleteBank(int Id);

        /// <summary>
        /// Delete a Bank with its Id and Update IsDeleted IF that Bank has relationship with others
        /// </summary>
        /// <param name="Id">Id of Bank</param>
        void DeleteBankRs(int Id);

        /// <summary>
        /// Update Bank into database
        /// </summary>
        /// <param name="Bank">Bank object</param>
        void UpdateBank(Bank Bank);
    }
}
