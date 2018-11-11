using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ICommisionCustomerRepository
    {
        /// <summary>
        /// Get all CommisionCustomer
        /// </summary>
        /// <returns>CommisionCustomer list</returns>
        IQueryable<vwCommisionCustomer> GetAllCommisionCustomer();

        /// <summary>
        /// Get CommisionCustomer information by specific id
        /// </summary>
        /// <param name="Id">Id of CommisionCustomer</param>
        /// <returns></returns>
        CommisionCustomer GetCommisionCustomerById(int Id);

        vwCommisionCustomer GetvwCommisionCustomerById(int Id);

        /// <summary>
        /// Insert CommisionCustomer into database
        /// </summary>
        /// <param name="CommisionCustomer">Object infomation</param>
        void InsertCommisionCustomer(CommisionCustomer CommisionCustomer);

        /// <summary>
        /// Delete CommisionCustomer with specific id
        /// </summary>
        /// <param name="Id">CommisionCustomer Id</param>
        void DeleteCommisionCustomer(int Id);

        /// <summary>
        /// Delete a CommisionCustomer with its Id and Update IsDeleted IF that Commision has relationship with others
        /// </summary>
        /// <param name="Id">Id of CommisionCustomer</param>
        void DeleteCommisionCustomerRs(int Id);

        /// <summary>
        /// Update Commision into database
        /// </summary>
        /// <param name="CommisionCustomer">CommisionCustomer object</param>
        void UpdateCommisionCustomer(CommisionCustomer CommisionCustomer);
    }
}
