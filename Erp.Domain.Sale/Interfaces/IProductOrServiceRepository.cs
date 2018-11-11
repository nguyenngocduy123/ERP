using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IProductOrServiceRepository
    {
        /// <summary>
        /// Get all Product
        /// </summary>
        /// <returns>Product list</returns>
        /// Lấy hàng hóa và dịch vụ chung luôn là product
        IQueryable<Product> GetAllProduct();
        /// <summary>
        /// lấy view của hàng hóa và dịch vụ
        /// </summary>
        /// <returns></returns>
        IQueryable<vwProductAndService> GetAllvwProductAndService();
        /// <summary>
        /// Lấy hàng hóa không là product
        /// </summary>
        /// <returns></returns>
        IQueryable<vwProduct> GetAllvwProduct();
        /// <summary>
        /// lấy dịch vụ không là service
        /// </summary>
        /// <returns></returns>
        IQueryable<vwService> GetAllvwService();

        IQueryable<Product> GetAllProductByType(string type);

        IQueryable<vwProduct> GetAllvwProductByType(string type);
        /// <summary>
        /// Get Product information by specific id
        /// </summary>
        /// <param name="Id">Id of Product</param>
        /// <returns></returns>
        Product GetProductById(int Id);
        vwProduct GetvwProductById(int Id);
        vwService GetvwServiceById(int Id);
        /// <summary>
        /// Insert Product into database
        /// </summary>
        /// <param name="Product">Object infomation</param>
        void InsertProduct(Product Product);
        void InsertService(Product Service);
        /// <summary>
        /// Delete Product with specific id
        /// </summary>
        /// <param name="Id">Product Id</param>
        void DeleteProduct(int Id);
        void DeleteService(int Id);
        /// <summary>
        /// Delete a Product with its Id and Update IsDeleted IF that Product has relationship with others
        /// </summary>
        /// <param name="Id">Id of Product</param>
        void DeleteProductRs(int Id);
        void DeleteServiceRs(int Id);
        /// <summary>
        /// Update Product into database
        /// </summary>
        /// <param name="Product">Product object</param>
        void UpdateProduct(Product Product);
        void UpdateService(Product Service);
        ServiceCombo GetServiceComboById(int Id);
        void InsertServiceCombo(vwService Service, List<ServiceCombo> orderDetails);
        void DeleteServiceCombo(IEnumerable<ServiceCombo> list);
        void InsertServiceCombo(ServiceCombo ServiceCombo);
    }
}
