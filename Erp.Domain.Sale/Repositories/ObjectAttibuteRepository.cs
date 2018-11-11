using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class ObjectAttributeRepository : GenericRepository<ErpSaleDbContext, ObjectAttribute>, IObjectAttributeRepository
    {
        public ObjectAttributeRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

       
        public IQueryable<ObjectAttribute> GetAllObjectAttribute()
        {
            return Context.ObjectAttribute.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<ObjectAttribute> GetAllObjectAttributeByModuleType(string ModuleType)
        {
            return Context.ObjectAttribute.Where(item => item.ModuleType == ModuleType && item.IsSelected == true && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<ObjectAttribute> GetAllObjectAttributeByModuleCategoryType(string ModuleCategoryType)
        {
            return Context.ObjectAttribute.Where(item => item.ModuleCategoryType == ModuleCategoryType && item.IsSelected == true && (item.IsDeleted == null || item.IsDeleted == false));
        }
       
        public ObjectAttribute GetObjectAttributeById(int Id)
        {
            return Context.ObjectAttribute.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertObjectAttribute(ObjectAttribute ObjectAttribute)
        {
            Context.ObjectAttribute.Add(ObjectAttribute);
            Context.Entry(ObjectAttribute).State = EntityState.Added;
            Context.SaveChanges();
        }

       
        public void DeleteObjectAttribute(int Id)
        {
            ObjectAttribute deletedObjectAttribute = GetObjectAttributeById(Id);
            Context.ObjectAttribute.Remove(deletedObjectAttribute);
            Context.Entry(deletedObjectAttribute).State = EntityState.Deleted;
            Context.SaveChanges();
        }
       
        public void DeleteObjectAttributeRs(int Id)
        {
            ObjectAttribute deleteObjectAttributeRs = GetObjectAttributeById(Id);
            deleteObjectAttributeRs.IsDeleted = true;
            UpdateObjectAttribute(deleteObjectAttributeRs);
        }

        public void UpdateObjectAttribute(ObjectAttribute ObjectAttribute)
        {
            Context.Entry(ObjectAttribute).State = EntityState.Modified;
            Context.SaveChanges();
        }

        // ---- ObjectAttributeValue ------ 

        public IQueryable<ObjectAttributeValue> GetAllObjectAttributeValue()
        {
            return Context.ObjectAttributeValue.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<ObjectAttributeValue> GetAllObjectAttributeValueByAttributeId(int AttrId)
        {
            return Context.ObjectAttributeValue.Where(item => item.AttributeId == AttrId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<ObjectAttributeValue> GetAllObjectAttributeValueByObjectId(int ObjectId)
        {
            return Context.ObjectAttributeValue.Where(item => item.ObjectId == ObjectId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public ObjectAttributeValue GetObjectAttributeValueById(int Id)
        {
            return Context.ObjectAttributeValue.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertObjectAttributeValue(ObjectAttributeValue ObjectAttributeValue)
        {
            Context.ObjectAttributeValue.Add(ObjectAttributeValue);
            Context.Entry(ObjectAttributeValue).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeleteObjectAttributeValue(int Id)
        {
            ObjectAttributeValue deletedObjectAttributeValue = GetObjectAttributeValueById(Id);
            Context.ObjectAttributeValue.Remove(deletedObjectAttributeValue);
            Context.Entry(deletedObjectAttributeValue).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void DeleteObjectAttributeValueRs(int Id)
        {
            ObjectAttributeValue deleteObjectAttributeValueRs = GetObjectAttributeValueById(Id);
            deleteObjectAttributeValueRs.IsDeleted = true;
            UpdateObjectAttributeValue(deleteObjectAttributeValueRs);
        }

        public void UpdateObjectAttributeValue(ObjectAttributeValue ObjectAttributeValue)
        {
            Context.Entry(ObjectAttributeValue).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
