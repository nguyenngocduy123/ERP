using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Crm.Repositories
{
    public class QuestionRepository : GenericRepository<ErpCrmDbContext, Question>, IQuestionRepository
    {
        public QuestionRepository(ErpCrmDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Question
        /// </summary>
        /// <returns>Question list</returns>
        public IQueryable<Question> GetAllQuestion()
        {
            return Context.Question.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Question information by specific id
        /// </summary>
        /// <param name="QuestionId">Id of Question</param>
        /// <returns></returns>
        public Question GetQuestionById(int Id)
        {
            return Context.Question.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Question into database
        /// </summary>
        /// <param name="Question">Object infomation</param>
        public void InsertQuestion(Question Question)
        {
            Context.Question.Add(Question);
            Context.Entry(Question).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Question with specific id
        /// </summary>
        /// <param name="Id">Question Id</param>
        public void DeleteQuestion(int Id)
        {
            Question deletedQuestion = GetQuestionById(Id);
            Context.Question.Remove(deletedQuestion);
            Context.Entry(deletedQuestion).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Question with its Id and Update IsDeleted IF that Question has relationship with others
        /// </summary>
        /// <param name="QuestionId">Id of Question</param>
        public void DeleteQuestionRs(int Id)
        {
            Question deleteQuestionRs = GetQuestionById(Id);
            deleteQuestionRs.IsDeleted = true;
            UpdateQuestion(deleteQuestionRs);
        }

        /// <summary>
        /// Update Question into database
        /// </summary>
        /// <param name="Question">Question object</param>
        public void UpdateQuestion(Question Question)
        {
            Context.Entry(Question).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
