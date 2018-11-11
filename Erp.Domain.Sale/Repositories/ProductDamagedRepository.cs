using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class ProductDamagedRepository : GenericRepository<ErpSaleDbContext, ProductDamaged>, IProductDamagedRepository
    {
        public ProductDamagedRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all ProductDamaged
        /// </summary>
        /// <returns>ProductDamaged list</returns>
        public IQueryable<ProductDamaged> GetAllProductDamaged()
        {
            return Context.ProductDamaged.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Get ProductDamaged information by specific id
        /// </summary>
        /// <param name="ProductDamagedId">Id of ProductDamaged</param>
        /// <returns></returns>
        public ProductDamaged GetProductDamagedById(int Id)
        {
            return Context.ProductDamaged.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        /// <summary>
        /// Insert ProductDamaged into database
        /// </summary>
        /// <param name="ProductDamaged">Object infomation</param>
        public void InsertProductDamaged(ProductDamaged ProductDamaged)
        {
            Context.ProductDamaged.Add(ProductDamaged);
            Context.Entry(ProductDamaged).State = EntityState.Added;
            Context.SaveChanges();
        }

        /// <summary>
        /// Delete ProductDamaged with specific id
        /// </summary>
        /// <param name="Id">ProductDamaged Id</param>
        public void DeleteProductDamaged(int Id)
        {
            ProductDamaged deletedProductDamaged = GetProductDamagedById(Id);
            Context.ProductDamaged.Remove(deletedProductDamaged);
            Context.Entry(deletedProductDamaged).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        
        /// <summary>
        /// Delete a ProductDamaged with its Id and Update IsDeleted IF that ProductDamaged has relationship with others
        /// </summary>
        /// <param name="ProductDamagedId">Id of ProductDamaged</param>
        public void DeleteProductDamagedRs(int Id)
        {
            ProductDamaged deleteProductDamagedRs = GetProductDamagedById(Id);
            deleteProductDamagedRs.IsDeleted = true;
            UpdateProductDamaged(deleteProductDamagedRs);
        }

        /// <summary>
        /// Update ProductDamaged into database
        /// </summary>
        /// <param name="ProductDamaged">ProductDamaged object</param>
        public void UpdateProductDamaged(ProductDamaged ProductDamaged)
        {
            Context.Entry(ProductDamaged).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
