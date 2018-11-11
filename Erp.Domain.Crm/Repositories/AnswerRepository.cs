using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Crm.Repositories
{
    public class AnswerRepository : GenericRepository<ErpCrmDbContext, Answer>, IAnswerRepository
    {
        public AnswerRepository(ErpCrmDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Answer
        /// </summary>
        /// <returns>Answer list</returns>
        public IQueryable<Answer> GetAllAnswer()
        {
            return Context.Answer.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get Answer information by specific id
        /// </summary>
        /// <param name="AnswerId">Id of Answer</param>
        /// <returns></returns>
        public Answer GetAnswerById(int Id)
        {
            return Context.Answer.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert Answer into database
        /// </summary>
        /// <param name="Answer">Object infomation</param>
        public void InsertAnswer(Answer Answer)
        {
            Context.Answer.Add(Answer);
            Context.Entry(Answer).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete Answer with specific id
        /// </summary>
        /// <param name="Id">Answer Id</param>
        public void DeleteAnswer(int Id)
        {
            Answer deletedAnswer = GetAnswerById(Id);
            Context.Answer.Remove(deletedAnswer);
            Context.Entry(deletedAnswer).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a Answer with its Id and Update IsDeleted IF that Answer has relationship with others
        /// </summary>
        /// <param name="AnswerId">Id of Answer</param>
        public void DeleteAnswerRs(int Id)
        {
            Answer deleteAnswerRs = GetAnswerById(Id);
            deleteAnswerRs.IsDeleted = true;
            UpdateAnswer(deleteAnswerRs);
        }

        /// <summary>
        /// Update Answer into database
        /// </summary>
        /// <param name="Answer">Answer object</param>
        public void UpdateAnswer(Answer Answer)
        {
            Context.Entry(Answer).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
