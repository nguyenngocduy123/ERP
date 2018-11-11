using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IPhysicalInventoryRepository
    {
        /// <summary>
        /// Get all PhysicalInventory
        /// </summary>
        /// <returns>PhysicalInventory list</returns>
        IQueryable<PhysicalInventory> GetAllPhysicalInventory();
        IQueryable<vwPhysicalInventory> GetAllvwPhysicalInventory();

        /// <summary>
        /// Get PhysicalInventory information by specific id
        /// </summary>
        /// <param name="Id">Id of PhysicalInventory</param>
        /// <returns></returns>
        PhysicalInventory GetPhysicalInventoryById(int Id);
        vwPhysicalInventory GetvwPhysicalInventoryById(int Id);
        /// <summary>
        /// Insert PhysicalInventory into database
        /// </summary>
        /// <param name="PhysicalInventory">Object infomation</param>
        void InsertPhysicalInventory(PhysicalInventory PhysicalInventory, List<PhysicalInventoryDetail> DetailList);

        /// <summary>
        /// Delete PhysicalInventory with specific id
        /// </summary>
        /// <param name="Id">PhysicalInventory Id</param>
        void DeletePhysicalInventory(int Id);

        /// <summary>
        /// Delete a PhysicalInventory with its Id and Update IsDeleted IF that PhysicalInventory has relationship with others
        /// </summary>
        /// <param name="Id">Id of PhysicalInventory</param>
        void DeletePhysicalInventoryRs(int Id);

        /// <summary>
        /// Update PhysicalInventory into database
        /// </summary>
        /// <param name="PhysicalInventory">PhysicalInventory object</param>
        void UpdatePhysicalInventory(PhysicalInventory PhysicalInventory);


        // ------------------------- 

        IQueryable<PhysicalInventoryDetail> GetAllPhysicalInventoryDetail(int PhysicalInventoryId);
        IQueryable<vwPhysicalInventoryDetail> GetAllvwPhysicalInventoryDetail(int PhysicalInventoryId);

        PhysicalInventoryDetail GetPhysicalInventoryDetailById(int Id);

        void InsertPhysicalInventoryDetail(PhysicalInventoryDetail PhysicalInventoryDetail);

        void DeletePhysicalInventoryDetail(int Id);

        void DeletePhysicalInventoryDetailRs(int Id);

        void UpdatePhysicalInventoryDetail(PhysicalInventoryDetail PhysicalInventoryDetail);
    }
}
