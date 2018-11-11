using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IDocumentTypeRepository
    {
        /// <summary>
        /// Get all DocumentType
        /// </summary>
        /// <returns>DocumentType list</returns>
        IQueryable<DocumentType> GetAllDocumentType();

        /// <summary>
        /// Get DocumentType information by specific id
        /// </summary>
        /// <param name="Id">Id of DocumentType</param>
        /// <returns></returns>
        DocumentType GetDocumentTypeById(int Id);

        /// <summary>
        /// Insert DocumentType into database
        /// </summary>
        /// <param name="DocumentType">Object infomation</param>
        void InsertDocumentType(DocumentType DocumentType);

        /// <summary>
        /// Delete DocumentType with specific id
        /// </summary>
        /// <param name="Id">DocumentType Id</param>
        void DeleteDocumentType(int Id);

        /// <summary>
        /// Delete a DocumentType with its Id and Update IsDeleted IF that DocumentType has relationship with others
        /// </summary>
        /// <param name="Id">Id of DocumentType</param>
        void DeleteDocumentTypeRs(int Id);

        /// <summary>
        /// Update DocumentType into database
        /// </summary>
        /// <param name="DocumentType">DocumentType object</param>
        void UpdateDocumentType(DocumentType DocumentType);
    }
}
