using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IServiceComboRepository
    {
        /// <summary>
        /// Get all ServiceCombo
        /// </summary>
        /// <returns>ServiceCombo list</returns>
        IQueryable<ServiceCombo> GetAllServiceCombo();
        IQueryable<vwServiceCombo> GetAllvwServiceCombo();
        /// <summary>
        /// Get ServiceCombo information by specific id
        /// </summary>
        /// <param name="Id">Id of ServiceCombo</param>
        /// <returns></returns>
        ServiceCombo GetServiceComboById(int Id);
        vwServiceCombo GetvwServiceComboById(int Id);
        /// <summary>
        /// Insert ServiceCombo into database
        /// </summary>
        /// <param name="ServiceCombo">Object infomation</param>
        void InsertServiceCombo(ServiceCombo ServiceCombo);

        /// <summary>
        /// Delete ServiceCombo with specific id
        /// </summary>
        /// <param name="Id">ServiceCombo Id</param>
        void DeleteServiceCombo(int Id);

        /// <summary>
        /// Delete a ServiceCombo with its Id and Update IsDeleted IF that ServiceCombo has relationship with others
        /// </summary>
        /// <param name="Id">Id of ServiceCombo</param>
        void DeleteServiceComboRs(int Id);

        /// <summary>
        /// Update ServiceCombo into database
        /// </summary>
        /// <param name="ServiceCombo">ServiceCombo object</param>
        void UpdateServiceCombo(ServiceCombo ServiceCombo);
    }
}
