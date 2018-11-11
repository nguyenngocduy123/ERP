using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Crm.Repositories
{
    public class VoteRepository : GenericRepository<ErpCrmDbContext, Vote>, IVoteRepository
    {
        public VoteRepository(ErpCrmDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Vote
        /// </summary>
        /// <returns>Vote list</returns>
        public IQueryable<Vote> GetAllVote()
        {
            return Context.Vote;
        }

        public IQueryable<vwVote> GetAllvwVote()
        {
            return Context.vwVote;
        }
        public IQueryable<vwVote2> GetAllvwVote2()
        {
            return Context.vwVote2.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get Vote information by specific id
        /// </summary>
        /// <param name="VoteId">Id of Vote</param>
        /// <returns></returns>
        public Vote GetVoteById(int Id)
        {
            return Context.Vote.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public vwVote2 GetvwVote2ById(int Id)
        {
            return Context.vwVote2.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert Vote into database
        /// </summary>
        /// <param name="Vote">Object infomation</param>
        public void InsertVote(Vote Vote)
        {
            Context.Vote.Add(Vote);
            Context.Entry(Vote).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Vote with specific id
        /// </summary>
        /// <param name="Id">Vote Id</param>
        public void DeleteVote(int Id)
        {
            Vote deletedVote = GetVoteById(Id);
            Context.Vote.Remove(deletedVote);
            Context.Entry(deletedVote).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Vote with its Id and Update IsDeleted IF that Vote has relationship with others
        /// </summary>
        /// <param name="VoteId">Id of Vote</param>
        public void DeleteVoteRs(int Id)
        {
            Vote deleteVoteRs = GetVoteById(Id);
            deleteVoteRs.IsDeleted = true;
            UpdateVote(deleteVoteRs);
        }

        /// <summary>
        /// Update Vote into database
        /// </summary>
        /// <param name="Vote">Vote object</param>
        public void UpdateVote(Vote Vote)
        {
            Context.Entry(Vote).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
