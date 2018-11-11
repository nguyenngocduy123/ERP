using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface IProductInvoiceRepository
    {
        /// <summary>
        /// Get all ProductInvoice
        /// </summary>
        /// <returns>ProductInvoice list</returns>
        IQueryable<ProductInvoice> GetAllProductInvoice();
        IQueryable<vwProductInvoice> GetAllvwProductInvoice();
        IQueryable<vwProductInvoice> GetAllvwProductInvoiceFull();

        /// <summary>
        /// Get ProductInvoice information by specific id
        /// </summary>
        /// <param name="Id">Id of ProductInvoice</param>
        /// <returns></returns>
        ProductInvoice GetProductInvoiceById(int Id);
        vwProductInvoice GetvwProductInvoiceById(int Id);
        vwProductInvoice GetvwProductInvoiceFullById(int Id);
        vwProductInvoice GetvwProductInvoiceByCode(string code);

        /// <summary>
        /// Insert ProductInvoice into database
        /// </summary>
        /// <param name="ProductInvoice">Object infomation</param>
        int InsertProductInvoice(ProductInvoice ProductInvoice, List<ProductInvoiceDetail> orderDetails);

        /// <summary>
        /// Delete ProductInvoice with specific id
        /// </summary>
        /// <param name="Id">ProductInvoice Id</param>
        void DeleteProductInvoice(int Id);

        /// <summary>
        /// Delete a ProductInvoice with its Id and Update IsDeleted IF that ProductInvoice has relationship with others
        /// </summary>
        /// <param name="Id">Id of ProductInvoice</param>
        void DeleteProductInvoiceRs(int Id);

        void UpdateProductInvoice(ProductInvoice ProductInvoice);


        // Order detail
        IQueryable<vwProductInvoiceDetail> GetAllvwInvoiceDetails();
        IQueryable<ProductInvoiceDetail> GetAllInvoiceDetailsByInvoiceId(int InvoiceId);
        IQueryable<vwProductInvoiceDetail> GetAllvwInvoiceDetailsByInvoiceId(int InvoiceId);
        ProductInvoiceDetail GetProductInvoiceDetailById(int Id);
        vwProductInvoiceDetail GetvwProductInvoiceDetailById(int Id);
    
        void InsertProductInvoiceDetail(ProductInvoiceDetail ProductInvoiceDetail);

     
        void DeleteProductInvoiceDetail(int Id);
        void DeleteProductInvoiceDetail(List<ProductInvoiceDetail> list);
        
        void DeleteProductInvoiceDetailRs(int Id);

        
        void UpdateProductInvoiceDetail(ProductInvoiceDetail ProductInvoiceDetail);
    }
}
