using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IProductDamagedRepository
    {
        /// <summary>
        /// Get all ProductDamaged
        /// </summary>
        /// <returns>ProductDamaged list</returns>
        IQueryable<ProductDamaged> GetAllProductDamaged();

        /// <summary>
        /// Get ProductDamaged information by specific id
        /// </summary>
        /// <param name="Id">Id of ProductDamaged</param>
        /// <returns></returns>
        ProductDamaged GetProductDamagedById(int Id);

        /// <summary>
        /// Insert ProductDamaged into database
        /// </summary>
        /// <param name="ProductDamaged">Object infomation</param>
        void InsertProductDamaged(ProductDamaged ProductDamaged);

        /// <summary>
        /// Delete ProductDamaged with specific id
        /// </summary>
        /// <param name="Id">ProductDamaged Id</param>
        void DeleteProductDamaged(int Id);

        /// <summary>
        /// Delete a ProductDamaged with its Id and Update IsDeleted IF that ProductDamaged has relationship with others
        /// </summary>
        /// <param name="Id">Id of ProductDamaged</param>
        void DeleteProductDamagedRs(int Id);

        /// <summary>
        /// Update ProductDamaged into database
        /// </summary>
        /// <param name="ProductDamaged">ProductDamaged object</param>
        void UpdateProductDamaged(ProductDamaged ProductDamaged);
    }
}
