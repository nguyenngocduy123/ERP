using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IDocumentFieldRepository
    {
        /// <summary>
        /// Get all DocumentField
        /// </summary>
        /// <returns>DocumentField list</returns>
        IQueryable<DocumentField> GetAllDocumentField();
        IQueryable<vwDocumentField> GetAllvwDocumentField();
        /// <summary>
        /// Get DocumentField information by specific id
        /// </summary>
        /// <param name="Id">Id of DocumentField</param>
        /// <returns></returns>
        DocumentField GetDocumentFieldById(int Id);
        vwDocumentField GetvwDocumentFieldById(int Id);
        DocumentField GetDocumentFieldByCategory(string Category, int? CategoryId);
        IEnumerable<DocumentField> GetDocumentFieldByName(string name, int DocumentTypeId, string IsSearch, string Category);
        /// <summary>
        /// Insert DocumentField into database
        /// </summary>
        /// <param name="DocumentField">Object infomation</param>
        void InsertDocumentField(DocumentField DocumentField);

        /// <summary>
        /// Delete DocumentField with specific id
        /// </summary>
        /// <param name="Id">DocumentField Id</param>
        void DeleteDocumentField(int Id);

        /// <summary>
        /// Delete a DocumentField with its Id and Update IsDeleted IF that DocumentField has relationship with others
        /// </summary>
        /// <param name="Id">Id of DocumentField</param>
        void DeleteDocumentFieldRs(int Id);

        /// <summary>
        /// Update DocumentField into database
        /// </summary>
        /// <param name="DocumentField">DocumentField object</param>
        void UpdateDocumentField(DocumentField DocumentField);
    }
}
