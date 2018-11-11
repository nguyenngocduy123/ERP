using Erp.Domain.RealEstate.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.RealEstate.Interfaces
{
    public interface IBlockRepository
    {
        /// <summary>
        /// Get all Block
        /// </summary>
        /// <returns>Block list</returns>
        IQueryable<Block> GetAllBlock();

        /// <summary>
        /// Get Block information by specific id
        /// </summary>
        /// <param name="Id">Id of Block</param>
        /// <returns></returns>
        Block GetBlockById(int Id);

        /// <summary>
        /// Insert Block into database
        /// </summary>
        /// <param name="Block">Object infomation</param>
        void InsertBlock(Block Block);

        /// <summary>
        /// Delete Block with specific id
        /// </summary>
        /// <param name="Id">Block Id</param>
        void DeleteBlock(int Id);

        /// <summary>
        /// Delete a Block with its Id and Update IsDeleted IF that Block has relationship with others
        /// </summary>
        /// <param name="Id">Id of Block</param>
        void DeleteBlockRs(int Id);

        /// <summary>
        /// Update Block into database
        /// </summary>
        /// <param name="Block">Block object</param>
        void UpdateBlock(Block Block);
    }
}
