using Erp.Domain.RealEstate.Entities;
using Erp.Domain.RealEstate.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.RealEstate.Repositories
{
    public class FloorRepository : GenericRepository<ErpRealEstateDbContext, Floor>, IFloorRepository
    {
        public FloorRepository(ErpRealEstateDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Floor
        /// </summary>
        /// <returns>Floor list</returns>
        public IQueryable<Floor> GetAllFloor()
        {
            return Context.Floor.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Floor information by specific id
        /// </summary>
        /// <param name="FloorId">Id of Floor</param>
        /// <returns></returns>
        public Floor GetFloorById(int Id)
        {
            return Context.Floor.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Floor into database
        /// </summary>
        /// <param name="Floor">Object infomation</param>
        public void InsertFloor(Floor Floor)
        {
            Context.Floor.Add(Floor);
            Context.Entry(Floor).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Floor with specific id
        /// </summary>
        /// <param name="Id">Floor Id</param>
        public void DeleteFloor(int Id)
        {
            Floor deletedFloor = GetFloorById(Id);
            Context.Floor.Remove(deletedFloor);
            Context.Entry(deletedFloor).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Floor with its Id and Update IsDeleted IF that Floor has relationship with others
        /// </summary>
        /// <param name="FloorId">Id of Floor</param>
        public void DeleteFloorRs(int Id)
        {
            Floor deleteFloorRs = GetFloorById(Id);
            deleteFloorRs.IsDeleted = true;
            UpdateFloor(deleteFloorRs);
        }

        /// <summary>
        /// Update Floor into database
        /// </summary>
        /// <param name="Floor">Floor object</param>
        public void UpdateFloor(Floor Floor)
        {
            Context.Entry(Floor).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
