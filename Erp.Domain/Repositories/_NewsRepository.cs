using System.Globalization;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Repositories
{
    public class NewsRepository : GenericRepository<ErpDbContext, News>, INewsRepository
    {
        private readonly ICategoryRepository _categoryRepository;
        public NewsRepository(ErpDbContext context)
            : base(context)
        {
            _categoryRepository = new CategoryRepository(context);
        }

        /// <summary>
        /// Get all News
        /// </summary>
        /// <returns></returns>
        public IQueryable<News> GetAllNews()
        {
            //var list = _categoryRepository.GetAllCategories().Select(x => x.Id).ToList();
            //return Context.News.Where(n => (n.IsDeleted == null || !n.IsDeleted.Value) && (n.Category.IsDeleted == null || n.Category.IsDeleted == false)).OrderByDescending(u => u.ModifiedDate);
            return Context.News.Where(n => (n.IsDeleted == null || !n.IsDeleted.Value)).OrderByDescending(u => u.ModifiedDate);
        }
        public IQueryable<News> GetAllNewsAvailable()
        {
            var list = _categoryRepository.GetAllCategories().Select(x => x.Id).ToList();
            //return Context.News.Where(n => (n.IsDeleted == null || !n.IsDeleted.Value) && (n.Category.IsDeleted == null || n.Category.IsDeleted == false));
            return Context.News.Where(n => (n.IsDeleted == null || !n.IsDeleted.Value) && (list.Contains(n.CategoryId.Value)));
        }
        /// <summary>
        /// Get News with specific Id
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public News GetNewsById(int newsId)
        {
            return Context.News.SingleOrDefault(n => n.Id == newsId && (n.IsDeleted == null || !n.IsDeleted.Value) && (n.Category.IsDeleted == null || n.Category.IsDeleted == false));
        }

        /// <summary>
        /// Insert News into database
        /// </summary>
        /// <param name="news"></param>
        public void InsertNews(News news)
        {
            Context.News.Add(news);
            Context.Entry(news).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete News from database with specific newId and Update which user does
        /// </summary>
        /// <param name="newsId">Id of news</param>
        /// <param name="userId">Id of user</param>
        public void DeleteNews(int newsId, int userId)
        {
            News deletedNews = GetNewsById(newsId);
            deletedNews.IsDeleted = true;
            deletedNews.ModifiedDate = DateTime.Now;
            deletedNews.UpdateUser = userId;
            Context.Entry(deletedNews).State = EntityState.Modified;
            Context.SaveChanges();
        }

        /// <summary>
        /// Update News into database
        /// </summary>
        /// <param name="news"></param>
        public void UpdateNews(News news)
        {
            Context.Entry(news).State = EntityState.Modified;
            Context.SaveChanges();
        }

        /// <summary>
        /// Get img Thumbnail Img path in database
        /// </summary>
        /// <param name="newsId">Id of new</param>
        /// <returns>string image path</returns>
        public string GetThumbnailImgPath(int newsId)
        {
            var strPath = Context.News.Where(n => n.Id == newsId && (n.IsDeleted == null || !n.IsDeleted.Value)).Select(n => n.ThumbnailPath).FirstOrDefault();
            if (strPath != null) return strPath.ToString(CultureInfo.InvariantCulture);
            return null;
        }

        /// <summary>
        /// Get img path in database
        /// </summary>
        /// <param name="newsId">Id of new</param>
        /// <returns>string image path</returns>
        public string GetImgPath(int newsId)
        {
            var strPath = Context.News.Where(n => n.Id == newsId && (n.IsDeleted == null || !n.IsDeleted.Value)).Select(n => n.ImagePath).FirstOrDefault();
            if (strPath != null) return strPath.ToString(CultureInfo.InvariantCulture);
            return null;
        }
    }
}
