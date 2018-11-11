using Erp.Domain.Crm.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Crm.Interfaces
{
    public interface IQuestionRepository
    {
        /// <summary>
        /// Get all Question
        /// </summary>
        /// <returns>Question list</returns>
        IQueryable<Question> GetAllQuestion();

        /// <summary>
        /// Get Question information by specific id
        /// </summary>
        /// <param name="Id">Id of Question</param>
        /// <returns></returns>
        Question GetQuestionById(int Id);

        /// <summary>
        /// Insert Question into database
        /// </summary>
        /// <param name="Question">Object infomation</param>
        void InsertQuestion(Question Question);

        /// <summary>
        /// Delete Question with specific id
        /// </summary>
        /// <param name="Id">Question Id</param>
        void DeleteQuestion(int Id);

        /// <summary>
        /// Delete a Question with its Id and Update IsDeleted IF that Question has relationship with others
        /// </summary>
        /// <param name="Id">Id of Question</param>
        void DeleteQuestionRs(int Id);

        /// <summary>
        /// Update Question into database
        /// </summary>
        /// <param name="Question">Question object</param>
        void UpdateQuestion(Question Question);
    }
}
