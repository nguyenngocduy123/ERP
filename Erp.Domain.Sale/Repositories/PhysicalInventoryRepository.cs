using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class PhysicalInventoryRepository : GenericRepository<ErpSaleDbContext, PhysicalInventory>, IPhysicalInventoryRepository
    {
        public PhysicalInventoryRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all PhysicalInventory
        /// </summary>
        /// <returns>PhysicalInventory list</returns>
        public IQueryable<PhysicalInventory> GetAllPhysicalInventory()
        {
            return Context.PhysicalInventory.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwPhysicalInventory> GetAllvwPhysicalInventory()
        {
            return Context.vwPhysicalInventory;
        }

        /// <summary>
        /// Get PhysicalInventory information by specific id
        /// </summary>
        /// <param name="PhysicalInventoryId">Id of PhysicalInventory</param>
        /// <returns></returns>
        public PhysicalInventory GetPhysicalInventoryById(int Id)
        {
            return Context.PhysicalInventory.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwPhysicalInventory GetvwPhysicalInventoryById(int Id)
        {
            return Context.vwPhysicalInventory.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert PhysicalInventory into database
        /// </summary>
        /// <param name="PhysicalInventory">Object infomation</param>
        public void InsertPhysicalInventory(PhysicalInventory PhysicalInventory, List<PhysicalInventoryDetail> DetailList)
        {
            Context.PhysicalInventory.Add(PhysicalInventory);
            Context.Entry(PhysicalInventory).State = EntityState.Added;
            Context.SaveChanges();

            for(int i=0; i<DetailList.Count;i++)
            {
                DetailList[i].PhysicalInventoryId = PhysicalInventory.Id;
                InsertPhysicalInventoryDetail(DetailList[i]);
            }
        }

        /// <summary>
        /// Delete PhysicalInventory with specific id
        /// </summary>
        /// <param name="Id">PhysicalInventory Id</param>
        public void DeletePhysicalInventory(int Id)
        {
            PhysicalInventory deletedPhysicalInventory = GetPhysicalInventoryById(Id);
            Context.PhysicalInventory.Remove(deletedPhysicalInventory);
            Context.Entry(deletedPhysicalInventory).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a PhysicalInventory with its Id and Update IsDeleted IF that PhysicalInventory has relationship with others
        /// </summary>
        /// <param name="PhysicalInventoryId">Id of PhysicalInventory</param>
        public void DeletePhysicalInventoryRs(int Id)
        {
            PhysicalInventory deletePhysicalInventoryRs = GetPhysicalInventoryById(Id);
            deletePhysicalInventoryRs.IsDeleted = true;
            UpdatePhysicalInventory(deletePhysicalInventoryRs);
        }

        /// <summary>
        /// Update PhysicalInventory into database
        /// </summary>
        /// <param name="PhysicalInventory">PhysicalInventory object</param>
        public void UpdatePhysicalInventory(PhysicalInventory PhysicalInventory)
        {
            Context.Entry(PhysicalInventory).State = EntityState.Modified;
            Context.SaveChanges();
        }

        // --------------------------------

        public IQueryable<PhysicalInventoryDetail> GetAllPhysicalInventoryDetail(int PhysicalInventoryId)
        {
            return Context.PhysicalInventoryDetail.Where(item => item.PhysicalInventoryId == PhysicalInventoryId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwPhysicalInventoryDetail> GetAllvwPhysicalInventoryDetail(int PhysicalInventoryId)
        {
            return Context.vwPhysicalInventoryDetail.Where(item => item.PhysicalInventoryId == PhysicalInventoryId && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public PhysicalInventoryDetail GetPhysicalInventoryDetailById(int Id)
        {
            return Context.PhysicalInventoryDetail.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void InsertPhysicalInventoryDetail(PhysicalInventoryDetail PhysicalInventoryDetail)
        {
            Context.PhysicalInventoryDetail.Add(PhysicalInventoryDetail);
            Context.Entry(PhysicalInventoryDetail).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void DeletePhysicalInventoryDetail(int Id)
        {
            PhysicalInventoryDetail deletedPhysicalInventoryDetail = GetPhysicalInventoryDetailById(Id);
            Context.PhysicalInventoryDetail.Remove(deletedPhysicalInventoryDetail);
            Context.Entry(deletedPhysicalInventoryDetail).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public void DeletePhysicalInventoryDetailRs(int Id)
        {
            PhysicalInventoryDetail deletePhysicalInventoryDetailRs = GetPhysicalInventoryDetailById(Id);
            deletePhysicalInventoryDetailRs.IsDeleted = true;
            UpdatePhysicalInventoryDetail(deletePhysicalInventoryDetailRs);
        }

        public void UpdatePhysicalInventoryDetail(PhysicalInventoryDetail PhysicalInventoryDetail)
        {
            Context.Entry(PhysicalInventoryDetail).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
