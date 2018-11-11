using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class SaleReportRepository : GenericRepository<ErpSaleDbContext, Product>, ISaleReportRepository
    {
        public SaleReportRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        public IQueryable<vwReportCustomer> GetAllvwReportCustomer()
        {
            return Context.vwReportCustomer;
        }

        public IQueryable<vwReportProduct> GetAllvwReportProduct()
        {
            return Context.vwReportProduct;
        }

        public IEnumerable<vwReportProduct> GetAllspReportProductOnSale(int branchId)
        {
            object[] sqlParams = 
            {
                new SqlParameter("@branchId", branchId) // int = 0
            };

            var list = Context.Database.SqlQuery<vwReportProduct>("Sale_spReportProductOnSale @branchId", sqlParams);

            return list;
        }

        public IEnumerable<spReportProductInbound> GetAllspReportProductInbound(int branchId, DateTime startDate, DateTime endDate, decimal minTotalAmount, decimal maxTotalAmount, int warehouseDestinationId)
        {
            object[] sqlParams = 
            {
                new SqlParameter("@branchId", branchId), // int = 0
                new SqlParameter("@startDate", startDate), // datetime
                new SqlParameter("@endDate", endDate), //datetime
                new SqlParameter("@minTotalAmount", minTotalAmount), // decimal
                new SqlParameter("@maxTotalAmount", maxTotalAmount), // decimal
                new SqlParameter("@warehouseDestinationId", warehouseDestinationId) // int
            };

            var list = Context.Database.SqlQuery<spReportProductInbound>("Sale_spReportInboundProduct @branchId, @startDate, @endDate, @minTotalAmount, @maxTotalAmount, @warehouseDestinationId", sqlParams);

            return list;
        }

        public IEnumerable<spReportProductInbound> GetAllspReportProductOutbound(int branchId, DateTime startDate, DateTime endDate, decimal minTotalAmount, decimal maxTotalAmount, int warehouseDestinationId)
        {
            object[] sqlParams = 
            {
                new SqlParameter("@branchId", branchId), // int = 0
                new SqlParameter("@startDate", startDate), // datetime
                new SqlParameter("@endDate", endDate), //datetime
                new SqlParameter("@minTotalAmount", minTotalAmount), // decimal
                new SqlParameter("@maxTotalAmount", maxTotalAmount), // decimal
                new SqlParameter("@warehouseDestinationId", warehouseDestinationId) // int
            };

            var list = Context.Database.SqlQuery<spReportProductInbound>("Sale_spReportOutboundProduct @branchId, @startDate, @endDate, @minTotalAmount, @maxTotalAmount, @warehouseDestinationId", sqlParams);

            return list;
        }
    }
}
