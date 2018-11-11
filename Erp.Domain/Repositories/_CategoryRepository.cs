using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Repositories
{
    public class CategoryRepository : GenericRepository<ErpDbContext, Category>, ICategoryRepository
    {
        public CategoryRepository(ErpDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Category
        /// </summary>
        /// <returns>Category list</returns>
        public IQueryable<Category> GetAllCategories()
        {
            return Context.Category.Where(c => c.IsDeleted == null || c.IsDeleted == false).OrderBy(u => u.OrderNo);
        }

        /// <summary>
        /// Get Category information by specific id
        /// </summary>
        /// <param name="categoryId">Id of category</param>
        /// <returns></returns>
        public Category GetCategoryById(int categoryId)
        {
            return Context.Category.SingleOrDefault(c => c.Id == categoryId && (c.IsDeleted == null || c.IsDeleted == false));
        }
        public Category GetCategoryByName(string Name)
        {
            return Context.Category.SingleOrDefault(item => item.Name == Name && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Get Category information by specific code
        /// </summary>
        /// <param name="sCode">Code of category</param>
        /// <returns></returns>
        public IEnumerable<Category> GetCategoryByCode(string sCode)
        {   
            try
            {
                return Context.Category
                    .Where(c => c.Code == sCode && (c.IsDeleted == null || c.IsDeleted == false))
                    .OrderBy(u => u.OrderNo);
            }
            catch { }
            return null;
        }

        public IEnumerable<Category> GetCategoryByParentId(int? parentId)
        {
            try
            {
                return Context.Category
                    .Where(c => c.ParentId == parentId && (c.IsDeleted == null || c.IsDeleted == false))
                    .OrderBy(u => u.OrderNo);
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Insert Category into database
        /// </summary>
        /// <param name="category">Object infomation</param>
        public void InsertCategory(Category category)
        {
            Context.Category.Add(category);
            Context.Entry(category).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete category with specific id
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        public void DeleteCategory(int categoryId)
        {
            Category deletedCategory = GetCategoryById(categoryId);
            Context.Category.Remove(deletedCategory);
            Context.Entry(deletedCategory).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        
        /// <summary>
        /// Delete a Category with its Id and Update IsDeleted IF that category has relationship with others
        /// </summary>
        /// <param name="categoryId">Id of Category</param>
        public  void DeleteCategoryRs(int categoryId)
        {
            Category deleteCategoryRs = GetCategoryById(categoryId);
            deleteCategoryRs.IsDeleted = true;
            UpdateCategory(deleteCategoryRs);
        }

        /// <summary>
        /// Update Category into database
        /// </summary>
        /// <param name="category">Category object</param>
        public void UpdateCategory(Category category)
        {
            Context.Entry(category).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
