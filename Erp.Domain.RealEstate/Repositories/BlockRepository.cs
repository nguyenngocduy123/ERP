using Erp.Domain.RealEstate.Entities;
using Erp.Domain.RealEstate.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.RealEstate.Repositories
{
    public class BlockRepository : GenericRepository<ErpRealEstateDbContext, Block>, IBlockRepository
    {
        public BlockRepository(ErpRealEstateDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Block
        /// </summary>
        /// <returns>Block list</returns>
        public IQueryable<Block> GetAllBlock()
        {
            return Context.Block.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Block information by specific id
        /// </summary>
        /// <param name="BlockId">Id of Block</param>
        /// <returns></returns>
        public Block GetBlockById(int Id)
        {
            return Context.Block.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Block into database
        /// </summary>
        /// <param name="Block">Object infomation</param>
        public void InsertBlock(Block Block)
        {
            Context.Block.Add(Block);
            Context.Entry(Block).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Block with specific id
        /// </summary>
        /// <param name="Id">Block Id</param>
        public void DeleteBlock(int Id)
        {
            Block deletedBlock = GetBlockById(Id);
            Context.Block.Remove(deletedBlock);
            Context.Entry(deletedBlock).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Block with its Id and Update IsDeleted IF that Block has relationship with others
        /// </summary>
        /// <param name="BlockId">Id of Block</param>
        public void DeleteBlockRs(int Id)
        {
            Block deleteBlockRs = GetBlockById(Id);
            deleteBlockRs.IsDeleted = true;
            UpdateBlock(deleteBlockRs);
        }

        /// <summary>
        /// Update Block into database
        /// </summary>
        /// <param name="Block">Block object</param>
        public void UpdateBlock(Block Block)
        {
            Context.Entry(Block).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
