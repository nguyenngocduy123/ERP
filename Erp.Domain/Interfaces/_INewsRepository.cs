using Erp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Interfaces
{
    public interface INewsRepository
    {
        /// <summary>
        /// Get all News
        /// </summary>
        /// <returns></returns>
        IQueryable<News> GetAllNews();
        IQueryable<News> GetAllNewsAvailable();
        /// <summary>
        /// Get News with specific Id
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        News GetNewsById(int newsId);

        /// <summary>
        /// Insert News into database
        /// </summary>
        /// <param name="news"></param>
        void InsertNews(News news);

        /// <summary>
        /// Delete News from database with specific newId and Update which user does
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="userId"></param>
        void DeleteNews(int newsId, int userId);

        /// <summary>
        /// Update News into database
        /// </summary>
        /// <param name="news"></param>
        void UpdateNews(News news);

        /// <summary>
        /// Get img Thumbnail Img path in database
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns>string Thumbnail image path</returns>
        string GetThumbnailImgPath(int newsId);

        /// <summary>
        /// Get img path in database
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns>string image path</returns>
        string GetImgPath(int newsId);
    }
}
