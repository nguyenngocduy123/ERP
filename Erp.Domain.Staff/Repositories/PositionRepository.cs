using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Staff.Repositories
{
    public class PositionRepository : GenericRepository<ErpStaffDbContext, Position>, IPositionRepository
    {
        public PositionRepository(ErpStaffDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Position
        /// </summary>
        /// <returns>Position list</returns>
        public IQueryable<Position> GetAllPosition()
        {
            return Context.Position.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Position information by specific id
        /// </summary>
        /// <param name="PositionId">Id of Position</param>
        /// <returns></returns>
        public Position GetPositionById(int Id)
        {
            return Context.Position.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public Position GetPositionByCode(string code)
        {
            return Context.Position.SingleOrDefault(item => item.Code == code && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert Position into database
        /// </summary>
        /// <param name="Position">Object infomation</param>
        public void InsertPosition(Position Position)
        {
            Context.Position.Add(Position);
            Context.Entry(Position).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Position with specific id
        /// </summary>
        /// <param name="Id">Position Id</param>
        public void DeletePosition(int Id)
        {
            Position deletedPosition = GetPositionById(Id);
            Context.Position.Remove(deletedPosition);
            Context.Entry(deletedPosition).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Position with its Id and Update IsDeleted IF that Position has relationship with others
        /// </summary>
        /// <param name="PositionId">Id of Position</param>
        public void DeletePositionRs(int Id)
        {
            Position deletePositionRs = GetPositionById(Id);
            deletePositionRs.IsDeleted = true;
            UpdatePosition(deletePositionRs);
        }

        /// <summary>
        /// Update Position into database
        /// </summary>
        /// <param name="Position">Position object</param>
        public void UpdatePosition(Position Position)
        {
            Context.Entry(Position).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
