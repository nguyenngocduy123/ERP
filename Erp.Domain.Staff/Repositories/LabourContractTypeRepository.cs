using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class LabourContractTypeRepository : GenericRepository<ErpStaffDbContext, LabourContractType>, ILabourContractTypeRepository
    {
        public LabourContractTypeRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all LabourContractType
        /// </summary>
        /// <returns>LabourContractType list</returns>
        public IQueryable<LabourContractType> GetAllLabourContractType()
        {
            return Context.LabourContractType.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get LabourContractType information by specific id
        /// </summary>
        /// <param name="LabourContractTypeId">Id of LabourContractType</param>
        /// <returns></returns>
        public LabourContractType GetLabourContractTypeById(int Id)
        {
            return Context.LabourContractType.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert LabourContractType into database
        /// </summary>
        /// <param name="LabourContractType">Object infomation</param>
        public void InsertLabourContractType(LabourContractType LabourContractType)
        {
            Context.LabourContractType.Add(LabourContractType);
            Context.Entry(LabourContractType).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete LabourContractType with specific id
        /// </summary>
        /// <param name="Id">LabourContractType Id</param>
        public void DeleteLabourContractType(int Id)
        {
            LabourContractType deletedLabourContractType = GetLabourContractTypeById(Id);
            Context.LabourContractType.Remove(deletedLabourContractType);
            Context.Entry(deletedLabourContractType).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a LabourContractType with its Id and Update IsDeleted IF that LabourContractType has relationship with others
        /// </summary>
        /// <param name="LabourContractTypeId">Id of LabourContractType</param>
        public void DeleteLabourContractTypeRs(int Id)
        {
            LabourContractType deleteLabourContractTypeRs = GetLabourContractTypeById(Id);
            deleteLabourContractTypeRs.IsDeleted = true;
            UpdateLabourContractType(deleteLabourContractTypeRs);
        }

        /// <summary>
        /// Update LabourContractType into database
        /// </summary>
        /// <param name="LabourContractType">LabourContractType object</param>
        public void UpdateLabourContractType(LabourContractType LabourContractType)
        {
            Context.Entry(LabourContractType).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
