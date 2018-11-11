using Erp.Domain.Crm.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Crm.Interfaces
{
    public interface IAnswerRepository
    {
        /// <summary>
        /// Get all Answer
        /// </summary>
        /// <returns>Answer list</returns>
        IQueryable<Answer> GetAllAnswer();

        /// <summary>
        /// Get Answer information by specific id
        /// </summary>
        /// <param name="Id">Id of Answer</param>
        /// <returns></returns>
        Answer GetAnswerById(int Id);

        /// <summary>
        /// Insert Answer into database
        /// </summary>
        /// <param name="Answer">Object infomation</param>
        void InsertAnswer(Answer Answer);

        /// <summary>
        /// Delete Answer with specific id
        /// </summary>
        /// <param name="Id">Answer Id</param>
        void DeleteAnswer(int Id);

        /// <summary>
        /// Delete a Answer with its Id and Update IsDeleted IF that Answer has relationship with others
        /// </summary>
        /// <param name="Id">Id of Answer</param>
        void DeleteAnswerRs(int Id);

        /// <summary>
        /// Update Answer into database
        /// </summary>
        /// <param name="Answer">Answer object</param>
        void UpdateAnswer(Answer Answer);
    }
}
