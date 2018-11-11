using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class DocumentTypeRepository : GenericRepository<ErpStaffDbContext, DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DocumentType
        /// </summary>
        /// <returns>DocumentType list</returns>
        public IQueryable<DocumentType> GetAllDocumentType()
        {
            return Context.DocumentType.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get DocumentType information by specific id
        /// </summary>
        /// <param name="DocumentTypeId">Id of DocumentType</param>
        /// <returns></returns>
        public DocumentType GetDocumentTypeById(int Id)
        {
            return Context.DocumentType.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert DocumentType into database
        /// </summary>
        /// <param name="DocumentType">Object infomation</param>
        public void InsertDocumentType(DocumentType DocumentType)
        {
            Context.DocumentType.Add(DocumentType);
            Context.Entry(DocumentType).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DocumentType with specific id
        /// </summary>
        /// <param name="Id">DocumentType Id</param>
        public void DeleteDocumentType(int Id)
        {
            DocumentType deletedDocumentType = GetDocumentTypeById(Id);
            Context.DocumentType.Remove(deletedDocumentType);
            Context.Entry(deletedDocumentType).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a DocumentType with its Id and Update IsDeleted IF that DocumentType has relationship with others
        /// </summary>
        /// <param name="DocumentTypeId">Id of DocumentType</param>
        public void DeleteDocumentTypeRs(int Id)
        {
            DocumentType deleteDocumentTypeRs = GetDocumentTypeById(Id);
            deleteDocumentTypeRs.IsDeleted = true;
            UpdateDocumentType(deleteDocumentTypeRs);
        }

        /// <summary>
        /// Update DocumentType into database
        /// </summary>
        /// <param name="DocumentType">DocumentType object</param>
        public void UpdateDocumentType(DocumentType DocumentType)
        {
            Context.Entry(DocumentType).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
