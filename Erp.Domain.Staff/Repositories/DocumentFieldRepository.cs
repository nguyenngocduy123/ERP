using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
namespace Erp.Domain.Staff.Repositories
{
    public class DocumentFieldRepository : GenericRepository<ErpStaffDbContext, DocumentField>, IDocumentFieldRepository
    {
        public DocumentFieldRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all DocumentField
        /// </summary>
        /// <returns>DocumentField list</returns>
        public IQueryable<DocumentField> GetAllDocumentField()
        {
            return Context.DocumentField.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwDocumentField> GetAllvwDocumentField()
        {
            return Context.vwDocumentField.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get DocumentField information by specific id
        /// </summary>
        /// <param name="DocumentFieldId">Id of DocumentField</param>
        /// <returns></returns>
        public DocumentField GetDocumentFieldById(int Id)
        {
            return Context.DocumentField.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwDocumentField GetvwDocumentFieldById(int Id)
        {
            return Context.vwDocumentField.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public DocumentField GetDocumentFieldByCategory(string Category, int? CategoryId)
        {
            return Context.DocumentField.SingleOrDefault(item => item.Category == Category && item.CategoryId==CategoryId && (item.IsDeleted == null || item.IsDeleted == false));
        }


        public IEnumerable<DocumentField> GetDocumentFieldByName(string name, int DocumentTypeId, string IsSearch, string Category)
        {
            return Context.DocumentField.AsEnumerable().Where(item => item.Name == name&&item.DocumentTypeId==DocumentTypeId&&item.IsSearch==IsSearch&&item.Category==Category&&item.CreatedDate.Value.ToString("dd/MM/yyyy")==DateTime.Now.ToString("dd/MM/yyyy") && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert DocumentField into database
        /// </summary>
        /// <param name="DocumentField">Object infomation</param>
        public void InsertDocumentField(DocumentField DocumentField)
        {
            Context.DocumentField.Add(DocumentField);
            Context.Entry(DocumentField).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete DocumentField with specific id
        /// </summary>
        /// <param name="Id">DocumentField Id</param>
        public void DeleteDocumentField(int Id)
        {
            DocumentField deletedDocumentField = GetDocumentFieldById(Id);
            Context.DocumentField.Remove(deletedDocumentField);
            Context.Entry(deletedDocumentField).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a DocumentField with its Id and Update IsDeleted IF that DocumentField has relationship with others
        /// </summary>
        /// <param name="DocumentFieldId">Id of DocumentField</param>
        public void DeleteDocumentFieldRs(int Id)
        {
            DocumentField deleteDocumentFieldRs = GetDocumentFieldById(Id);
            deleteDocumentFieldRs.IsDeleted = true;
            UpdateDocumentField(deleteDocumentFieldRs);
        }

        /// <summary>
        /// Update DocumentField into database
        /// </summary>
        /// <param name="DocumentField">DocumentField object</param>
        public void UpdateDocumentField(DocumentField DocumentField)
        {
            Context.Entry(DocumentField).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
