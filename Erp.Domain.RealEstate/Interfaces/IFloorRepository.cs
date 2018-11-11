using Erp.Domain.RealEstate.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.RealEstate.Interfaces
{
    public interface IFloorRepository
    {
        /// <summary>
        /// Get all Floor
        /// </summary>
        /// <returns>Floor list</returns>
        IQueryable<Floor> GetAllFloor();

        /// <summary>
        /// Get Floor information by specific id
        /// </summary>
        /// <param name="Id">Id of Floor</param>
        /// <returns></returns>
        Floor GetFloorById(int Id);

        /// <summary>
        /// Insert Floor into database
        /// </summary>
        /// <param name="Floor">Object infomation</param>
        void InsertFloor(Floor Floor);

        /// <summary>
        /// Delete Floor with specific id
        /// </summary>
        /// <param name="Id">Floor Id</param>
        void DeleteFloor(int Id);

        /// <summary>
        /// Delete a Floor with its Id and Update IsDeleted IF that Floor has relationship with others
        /// </summary>
        /// <param name="Id">Id of Floor</param>
        void DeleteFloorRs(int Id);

        /// <summary>
        /// Update Floor into database
        /// </summary>
        /// <param name="Floor">Floor object</param>
        void UpdateFloor(Floor Floor);
    }
}
