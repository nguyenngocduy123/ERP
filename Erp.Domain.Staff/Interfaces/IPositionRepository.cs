using Erp.Domain.Staff.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Staff.Interfaces
{
    public interface IPositionRepository
    {
        /// <summary>
        /// Get all Position
        /// </summary>
        /// <returns>Position list</returns>
        IQueryable<Position> GetAllPosition();

        /// <summary>
        /// Get Position information by specific id
        /// </summary>
        /// <param name="Id">Id of Position</param>
        /// <returns></returns>
        Position GetPositionById(int Id);
        Position GetPositionByCode(string code);
        /// <summary>
        /// Insert Position into database
        /// </summary>
        /// <param name="Position">Object infomation</param>
        void InsertPosition(Position Position);

        /// <summary>
        /// Delete Position with specific id
        /// </summary>
        /// <param name="Id">Position Id</param>
        void DeletePosition(int Id);

        /// <summary>
        /// Delete a Position with its Id and Update IsDeleted IF that Position has relationship with others
        /// </summary>
        /// <param name="Id">Id of Position</param>
        void DeletePositionRs(int Id);

        /// <summary>
        /// Update Position into database
        /// </summary>
        /// <param name="Position">Position object</param>
        void UpdatePosition(Position Position);
    }
}
