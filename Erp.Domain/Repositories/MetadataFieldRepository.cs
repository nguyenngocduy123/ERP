using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Repositories
{
    public class MetadataFieldRepository : GenericRepository<ErpDbContext, MetadataField>, IMetadataFieldRepository
    {
        public MetadataFieldRepository(ErpDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all MetadataField
        /// </summary>
        /// <returns>MetadataField list</returns>
        public IQueryable<vwMetadataField> GetAllMetadataField()
        {
            return Context.vwMetadataField.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get MetadataField information by specific id
        /// </summary>
        /// <param name="MetadataFieldId">Id of MetadataField</param>
        /// <returns></returns>
        public MetadataField GetMetadataFieldById(int Id)
        {
            return Context.MetadataField.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert MetadataField into database
        /// </summary>
        /// <param name="MetadataField">Object infomation</param>
        public void InsertMetadataField(MetadataField MetadataField)
        {
            Context.MetadataField.Add(MetadataField);
            Context.Entry(MetadataField).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete MetadataField with specific id
        /// </summary>
        /// <param name="Id">MetadataField Id</param>
        public void DeleteMetadataField(int Id)
        {
            MetadataField deletedMetadataField = GetMetadataFieldById(Id);
            Context.MetadataField.Remove(deletedMetadataField);
            Context.Entry(deletedMetadataField).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a MetadataField with its Id and Update IsDeleted IF that MetadataField has relationship with others
        /// </summary>
        /// <param name="MetadataFieldId">Id of MetadataField</param>
        public void DeleteMetadataFieldRs(int Id)
        {
            MetadataField deleteMetadataFieldRs = GetMetadataFieldById(Id);
            deleteMetadataFieldRs.IsDeleted = true;
            UpdateMetadataField(deleteMetadataFieldRs);
        }

        /// <summary>
        /// Update MetadataField into database
        /// </summary>
        /// <param name="MetadataField">MetadataField object</param>
        public void UpdateMetadataField(MetadataField MetadataField)
        {
            Context.Entry(MetadataField).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
