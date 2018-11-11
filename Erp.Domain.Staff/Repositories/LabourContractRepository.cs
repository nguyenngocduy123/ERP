using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class LabourContractRepository : GenericRepository<ErpStaffDbContext, LabourContract>, ILabourContractRepository
    {
        public LabourContractRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all LabourContract
        /// </summary>
        /// <returns>LabourContract list</returns>
        public IQueryable<LabourContract> GetAllLabourContract()
        {
            return Context.LabourContract.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwLabourContract> GetAllvwLabourContract()
        {
            return Context.vwLabourContract.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get LabourContract information by specific id
        /// </summary>
        /// <param name="LabourContractId">Id of LabourContract</param>
        /// <returns></returns>
        public LabourContract GetLabourContractById(int? Id)
        {
            return Context.LabourContract.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwLabourContract GetvwLabourContractById(int Id)
        {
            return Context.vwLabourContract.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert LabourContract into database
        /// </summary>
        /// <param name="LabourContract">Object infomation</param>
        public void InsertLabourContract(LabourContract LabourContract)
        {
            Context.LabourContract.Add(LabourContract);
            Context.Entry(LabourContract).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete LabourContract with specific id
        /// </summary>
        /// <param name="Id">LabourContract Id</param>
        public void DeleteLabourContract(int Id)
        {
            LabourContract deletedLabourContract = GetLabourContractById(Id);
            Context.LabourContract.Remove(deletedLabourContract);
            Context.Entry(deletedLabourContract).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a LabourContract with its Id and Update IsDeleted IF that LabourContract has relationship with others
        /// </summary>
        /// <param name="LabourContractId">Id of LabourContract</param>
        public void DeleteLabourContractRs(int Id)
        {
            LabourContract deleteLabourContractRs = GetLabourContractById(Id);
            deleteLabourContractRs.IsDeleted = true;
            UpdateLabourContract(deleteLabourContractRs);
        }

        /// <summary>
        /// Update LabourContract into database
        /// </summary>
        /// <param name="LabourContract">LabourContract object</param>
        public void UpdateLabourContract(LabourContract LabourContract)
        {
            Context.Entry(LabourContract).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
