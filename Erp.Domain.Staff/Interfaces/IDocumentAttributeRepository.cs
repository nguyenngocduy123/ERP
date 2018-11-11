using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IDocumentAttributeRepository
    {
        /// <summary>
        /// Get all DocumentAttribute
        /// </summary>
        /// <returns>DocumentAttribute list</returns>
        IQueryable<DocumentAttribute> GetAllDocumentAttribute();

        /// <summary>
        /// Get DocumentAttribute information by specific id
        /// </summary>
        /// <param name="Id">Id of DocumentAttribute</param>
        /// <returns></returns>
        DocumentAttribute GetDocumentAttributeById(int Id);

        /// <summary>
        /// Insert DocumentAttribute into database
        /// </summary>
        /// <param name="DocumentAttribute">Object infomation</param>
        void InsertDocumentAttribute(DocumentAttribute DocumentAttribute);

        /// <summary>
        /// Delete DocumentAttribute with specific id
        /// </summary>
        /// <param name="Id">DocumentAttribute Id</param>
        void DeleteDocumentAttribute(int Id);

        /// <summary>
        /// Delete a DocumentAttribute with its Id and Update IsDeleted IF that DocumentAttribute has relationship with others
        /// </summary>
        /// <param name="Id">Id of DocumentAttribute</param>
        void DeleteDocumentAttributeRs(int Id);

        /// <summary>
        /// Update DocumentAttribute into database
        /// </summary>
        /// <param name="DocumentAttribute">DocumentAttribute object</param>
        void UpdateDocumentAttribute(DocumentAttribute DocumentAttribute);
    }
}
