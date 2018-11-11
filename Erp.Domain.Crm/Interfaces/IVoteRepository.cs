using Erp.Domain.Crm.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Crm.Interfaces
{
    public interface IVoteRepository
    {
        /// <summary>
        /// Get all Vote
        /// </summary>
        /// <returns>Vote list</returns>
        IQueryable<Vote> GetAllVote();
        IQueryable<vwVote> GetAllvwVote();
        IQueryable<vwVote2> GetAllvwVote2();
        /// <summary>
        /// Get Vote information by specific id
        /// </summary>
        /// <param name="Id">Id of Vote</param>
        /// <returns></returns>
        Vote GetVoteById(int Id);
        vwVote2 GetvwVote2ById(int Id);
        /// <summary>
        /// Insert Vote into database
        /// </summary>
        /// <param name="Vote">Object infomation</param>
        void InsertVote(Vote Vote);

        /// <summary>
        /// Delete Vote with specific id
        /// </summary>
        /// <param name="Id">Vote Id</param>
        void DeleteVote(int Id);

        /// <summary>
        /// Delete a Vote with its Id and Update IsDeleted IF that Vote has relationship with others
        /// </summary>
        /// <param name="Id">Id of Vote</param>
        void DeleteVoteRs(int Id);

        /// <summary>
        /// Update Vote into database
        /// </summary>
        /// <param name="Vote">Vote object</param>
        void UpdateVote(Vote Vote);
    }
}
