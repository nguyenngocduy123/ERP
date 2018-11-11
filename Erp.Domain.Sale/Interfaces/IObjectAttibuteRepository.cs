using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IObjectAttributeRepository
    {

        IQueryable<ObjectAttribute> GetAllObjectAttribute();
        IQueryable<ObjectAttribute> GetAllObjectAttributeByModuleType(string ModuleType);
        IQueryable<ObjectAttribute> GetAllObjectAttributeByModuleCategoryType(string ModuleCategoryType);

        ObjectAttribute GetObjectAttributeById(int Id);

        void InsertObjectAttribute(ObjectAttribute ObjectAttribute);

        void DeleteObjectAttribute(int Id);

        void DeleteObjectAttributeRs(int Id);

        void UpdateObjectAttribute(ObjectAttribute ObjectAttribute);

        // --- ObjectAttributeValue --------------------------------------------------------------
        IQueryable<ObjectAttributeValue> GetAllObjectAttributeValue();
        IQueryable<ObjectAttributeValue> GetAllObjectAttributeValueByAttributeId(int AttrId);
        IQueryable<ObjectAttributeValue> GetAllObjectAttributeValueByObjectId(int ObjectId);

        ObjectAttributeValue GetObjectAttributeValueById(int Id);

        void InsertObjectAttributeValue(ObjectAttributeValue ObjectAttributeValue);

        void DeleteObjectAttributeValue(int Id);

        void DeleteObjectAttributeValueRs(int Id);

        void UpdateObjectAttributeValue(ObjectAttributeValue ObjectAttributeValue);
    }
}
