using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface ITechniqueRepository
    {
        /// <summary>
        /// Get all Technique
        /// </summary>
        /// <returns>Technique list</returns>
        IQueryable<Technique> GetAllTechnique();

        /// <summary>
        /// Get Technique information by specific id
        /// </summary>
        /// <param name="Id">Id of Technique</param>
        /// <returns></returns>
        Technique GetTechniqueById(int Id);

        /// <summary>
        /// Insert Technique into database
        /// </summary>
        /// <param name="Technique">Object infomation</param>
        void InsertTechnique(Technique Technique);

        /// <summary>
        /// Delete Technique with specific id
        /// </summary>
        /// <param name="Id">Technique Id</param>
        void DeleteTechnique(int Id);

        /// <summary>
        /// Delete a Technique with its Id and Update IsDeleted IF that Technique has relationship with others
        /// </summary>
        /// <param name="Id">Id of Technique</param>
        void DeleteTechniqueRs(int Id);

        /// <summary>
        /// Update Technique into database
        /// </summary>
        /// <param name="Technique">Technique object</param>
        void UpdateTechnique(Technique Technique);
    }
}
