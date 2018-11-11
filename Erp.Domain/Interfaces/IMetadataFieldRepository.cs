using Erp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Interfaces
{
    public interface IMetadataFieldRepository
    {
        /// <summary>
        /// Get all MetadataField
        /// </summary>
        /// <returns>MetadataField list</returns>
        IQueryable<vwMetadataField> GetAllMetadataField();

        /// <summary>
        /// Get MetadataField information by specific id
        /// </summary>
        /// <param name="Id">Id of MetadataField</param>
        /// <returns></returns>
        MetadataField GetMetadataFieldById(int Id);

        /// <summary>
        /// Insert MetadataField into database
        /// </summary>
        /// <param name="MetadataField">Object infomation</param>
        void InsertMetadataField(MetadataField MetadataField);

        /// <summary>
        /// Delete MetadataField with specific id
        /// </summary>
        /// <param name="Id">MetadataField Id</param>
        void DeleteMetadataField(int Id);

        /// <summary>
        /// Delete a MetadataField with its Id and Update IsDeleted IF that MetadataField has relationship with others
        /// </summary>
        /// <param name="Id">Id of MetadataField</param>
        void DeleteMetadataFieldRs(int Id);

        /// <summary>
        /// Update MetadataField into database
        /// </summary>
        /// <param name="MetadataField">MetadataField object</param>
        void UpdateMetadataField(MetadataField MetadataField);
    }
}
