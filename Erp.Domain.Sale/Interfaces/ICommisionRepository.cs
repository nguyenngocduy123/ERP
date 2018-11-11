using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ICommisionRepository
    {
        /// <summary>
        /// Get all Commision
        /// </summary>
        /// <returns>Commision list</returns>
        IQueryable<vwCommision> GetAllCommision();

        /// <summary>
        /// Get Commision information by specific id
        /// </summary>
        /// <param name="Id">Id of Commision</param>
        /// <returns></returns>
        Commision GetCommisionById(int Id);

        vwCommision GetvwCommisionById(int Id);

        /// <summary>
        /// Insert Commision into database
        /// </summary>
        /// <param name="Commision">Object infomation</param>
        void InsertCommision(Commision Commision);

        /// <summary>
        /// Delete Commision with specific id
        /// </summary>
        /// <param name="Id">Commision Id</param>
        void DeleteCommision(int Id);

        /// <summary>
        /// Delete a Commision with its Id and Update IsDeleted IF that Commision has relationship with others
        /// </summary>
        /// <param name="Id">Id of Commision</param>
        void DeleteCommisionRs(int Id);

        /// <summary>
        /// Update Commision into database
        /// </summary>
        /// <param name="Commision">Commision object</param>
        void UpdateCommision(Commision Commision);
    }
}
