using Erp.Domain.RealEstate.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.RealEstate.Interfaces
{
    public interface ICondosRepository
    {
        /// <summary>
        /// Get all Condos
        /// </summary>
        /// <returns>Condos list</returns>
        IQueryable<Condos> GetAllCondos();

        /// <summary>
        /// Get Condos information by specific id
        /// </summary>
        /// <param name="Id">Id of Condos</param>
        /// <returns></returns>
        Condos GetCondosById(int Id);
        vwCondos GetvwCondosById(int Id);

        /// <summary>
        /// Insert Condos into database
        /// </summary>
        /// <param name="Condos">Object infomation</param>
        void InsertCondos(Condos Condos);

        /// <summary>
        /// Delete Condos with specific id
        /// </summary>
        /// <param name="Id">Condos Id</param>
        void DeleteCondos(int Id);

        /// <summary>
        /// Delete a Condos with its Id and Update IsDeleted IF that Condos has relationship with others
        /// </summary>
        /// <param name="Id">Id of Condos</param>
        void DeleteCondosRs(int Id);

        /// <summary>
        /// Update Condos into database
        /// </summary>
        /// <param name="Condos">Condos object</param>
        void UpdateCondos(Condos Condos);
    }
}
