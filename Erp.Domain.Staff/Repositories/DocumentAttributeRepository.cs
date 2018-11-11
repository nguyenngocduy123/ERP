using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class DocumentAttributeRepository : GenericRepository<ErpStaffDbContext, DocumentAttribute>, IDocumentAttributeRepository
    {
        public DocumentAttributeRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DocumentAttribute
        /// </summary>
        /// <returns>DocumentAttribute list</returns>
        public IQueryable<DocumentAttribute> GetAllDocumentAttribute()
        {
            return Context.DocumentAttribute.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get DocumentAttribute information by specific id
        /// </summary>
        /// <param name="DocumentAttributeId">Id of DocumentAttribute</param>
        /// <returns></returns>
        public DocumentAttribute GetDocumentAttributeById(int Id)
        {
            return Context.DocumentAttribute.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert DocumentAttribute into database
        /// </summary>
        /// <param name="DocumentAttribute">Object infomation</param>
        public void InsertDocumentAttribute(DocumentAttribute DocumentAttribute)
        {
            Context.DocumentAttribute.Add(DocumentAttribute);
            Context.Entry(DocumentAttribute).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DocumentAttribute with specific id
        /// </summary>
        /// <param name="Id">DocumentAttribute Id</param>
        public void DeleteDocumentAttribute(int Id)
        {
            DocumentAttribute deletedDocumentAttribute = GetDocumentAttributeById(Id);
            Context.DocumentAttribute.Remove(deletedDocumentAttribute);
            Context.Entry(deletedDocumentAttribute).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a DocumentAttribute with its Id and Update IsDeleted IF that DocumentAttribute has relationship with others
        /// </summary>
        /// <param name="DocumentAttributeId">Id of DocumentAttribute</param>
        public void DeleteDocumentAttributeRs(int Id)
        {
            DocumentAttribute deleteDocumentAttributeRs = GetDocumentAttributeById(Id);
            deleteDocumentAttributeRs.IsDeleted = true;
            UpdateDocumentAttribute(deleteDocumentAttributeRs);
        }

        /// <summary>
        /// Update DocumentAttribute into database
        /// </summary>
        /// <param name="DocumentAttribute">DocumentAttribute object</param>
        public void UpdateDocumentAttribute(DocumentAttribute DocumentAttribute)
        {
            Context.Entry(DocumentAttribute).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
