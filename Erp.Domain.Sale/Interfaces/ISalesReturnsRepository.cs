using Erp.Domain.Sale.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Erp.Domain.Sale.Interfaces
{
    public interface ISalesReturnsRepository
    {
        /// <summary>
        /// Get all SalesReturns
        /// </summary>
        /// <returns>SalesReturns list</returns>
        IQueryable<SalesReturns> GetAllSalesReturns();
        IQueryable<vwSalesReturns> GetAllvwSalesReturns();
        /// <summary>
        /// Get SalesReturns information by specific id
        /// </summary>
        /// <param name="Id">Id of SalesReturns</param>
        /// <returns></returns>
        SalesReturns GetSalesReturnsById(int Id);
        vwSalesReturns GetvwSalesReturnsById(int Id);
        vwSalesReturns GetvwSalesReturnsByTransactionCode(string code);
        /// <summary>
        /// Insert SalesReturns into database
        /// </summary>
        /// <param name="SalesReturns">Object infomation</param>
        int InsertSalesReturns(SalesReturns SalesReturns, List<SalesReturnsDetail> orderDetails);
        void InsertSalesReturn(SalesReturns SalesReturns);
        /// <summary>
        /// Delete SalesReturns with specific id
        /// </summary>
        /// <param name="Id">SalesReturns Id</param>
        void DeleteSalesReturns(int Id);

        /// <summary>
        /// Delete a SalesReturns with its Id and Update IsDeleted IF that SalesReturns has relationship with others
        /// </summary>
        /// <param name="Id">Id of SalesReturns</param>
        void DeleteSalesReturnsRs(int Id);

        /// <summary>
        /// Update SalesReturns into database
        /// </summary>
        /// <param name="SalesReturns">SalesReturns object</param>
        void UpdateSalesReturns(SalesReturns SalesReturns);
       
        // Order detail
        IQueryable<vwSalesReturnsDetail> GetAllReturnsDetailsByReturnId(int orderId);
        IQueryable<vwSalesReturnsDetail> GetAllvwReturnsDetails();
        SalesReturnsDetail GetSalesReturnsDetailById(int Id);


        void InsertSalesReturnsDetail(SalesReturnsDetail SalesReturnsDetail);


        void DeleteSalesReturnsDetail(int Id);
        void DeleteSalesReturnsDetail(IEnumerable<SalesReturnsDetail> list);

        void DeleteSalesReturnsDetailRs(int Id);


        void UpdateSalesReturnsDetail(SalesReturnsDetail SalesReturnsDetail);
    }
}
