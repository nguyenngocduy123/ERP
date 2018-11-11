using Erp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Get all Category
        /// </summary>
        /// <returns>Category list</returns>
        IQueryable<Category> GetAllCategories();

        /// <summary>
        /// Get Category information by specific id
        /// </summary>
        /// <param name="categoryId">Id of category</param>
        /// <returns></returns>
        Category GetCategoryById(int categoryId);
        Category GetCategoryByName(string Name);
        /// <summary>
        /// Get Categorys by code
        /// </summary>
        /// <returns>Category list</returns>
        IEnumerable<Category> GetCategoryByCode(string sCode);
        IEnumerable<Category> GetCategoryByParentId(int? parentId);
        /// <summary>
        /// Insert Category into database
        /// </summary>
        /// <param name="category">Object infomation</param>
        void InsertCategory(Category category);

        /// <summary>
        /// Delete category with specific id
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        void DeleteCategory(int categoryId);

        /// <summary>
        /// Delete a Category with its Id and Update IsDeleted IF that category has relationship with others
        /// </summary>
        /// <param name="categoryId">Id of Category</param>
        void DeleteCategoryRs(int categoryId);

        /// <summary>
        /// Update Category into database
        /// </summary>
        /// <param name="category">Category object</param>
        void UpdateCategory(Category category);
    }
}
