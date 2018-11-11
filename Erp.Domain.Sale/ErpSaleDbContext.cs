using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Entities.Mapping;

namespace Erp.Domain.Sale
{
    public class ErpSaleDbContext : DbContext, IDbContext
    {
        static ErpSaleDbContext()
        {
            Database.SetInitializer<ErpSaleDbContext>(null);
        }

        public ErpSaleDbContext()
            : base("Name=ErpDbContext")
        {
            // this.Configuration.LazyLoadingEnabled = false;
            // this.Configuration.ProxyCreationEnabled = false;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        //Erp

        public DbSet<Product> Product { get; set; }
        public DbSet<vwProduct> vwProduct { get; set; }
      
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<vwSupplier> vwSupplier { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<vwPurchaseOrder> vwPurchaseOrder { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public DbSet<vwPurchaseOrderDetail> vwPurchaseOrderDetail { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<vwInventory> vwInventory { get; set; }
        public DbSet<InventoryByMonth> InventoryByMonth { get; set; }
        public DbSet<ProductOutbound> ProductOutbound { get; set; }
        public DbSet<vwProductOutbound> vwProductOutbound { get; set; }
        public DbSet<ProductInbound> ProductInbound { get; set; }
        public DbSet<vwProductInbound> vwProductInbound { get; set; }
        public DbSet<ProductInboundDetail> ProductInboundDetail { get; set; }
        public DbSet<ProductOutboundDetail> ProductOutboundDetail { get; set; }
        public DbSet<vwProductInboundDetail> vwProductInboundDetail { get; set; }
        public DbSet<vwProductOutboundDetail> vwProductOutboundDetail { get; set; }
        public DbSet<PhysicalInventory> PhysicalInventory { get; set; }
        public DbSet<vwPhysicalInventory> vwPhysicalInventory { get; set; }
        public DbSet<PhysicalInventoryDetail> PhysicalInventoryDetail { get; set; }
        public DbSet<vwPhysicalInventoryDetail> vwPhysicalInventoryDetail { get; set; }
        public DbSet<ObjectAttribute> ObjectAttribute { get; set; }
        public DbSet<ObjectAttributeValue> ObjectAttributeValue { get; set; }

        public DbSet<Commision> Commision { get; set; }
        public DbSet<ProductInvoice> ProductInvoice { get; set; }
        public DbSet<vwProductInvoice> vwProductInvoice { get; set; }
        public DbSet<ProductInvoiceDetail> ProductInvoiceDetail { get; set; }
        public DbSet<vwProductInvoiceDetail> vwProductInvoiceDetail { get; set; }
        public DbSet<vwCommision> vwCommision { get; set; }
        public DbSet<vwReportCustomer> vwReportCustomer { get; set; }
        public DbSet<vwReportProduct> vwReportProduct { get; set; }
        public DbSet<WarehouseLocationItem> WarehouseLocationItem { get; set; }
        public DbSet<vwWarehouseLocationItem> vwWarehouseLocationItem { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<PromotionDetail> PromotionDetail { get; set; }
        public DbSet<SalesReturns> SalesReturns { get; set; }
        public DbSet<vwSalesReturns> vwSalesReturns { get; set; }
        public DbSet<vwSalesReturnsDetail> vwSalesReturnsDetail { get; set; }
        public DbSet<SalesReturnsDetail> SalesReturnsDetail { get; set; }
        public DbSet<CommisionStaff> CommisionStaff { get; set; }
        public DbSet<vwCommisionStaff> vwCommisionStaff { get; set; }
        public DbSet<TemplatePrint> TemplatePrint { get; set; }
        public DbSet<vwService> vwService { get; set; }
        public DbSet<UsingServiceLog> UsingServiceLog { get; set; }
        public DbSet<ServiceCombo> ServiceCombo { get; set; }
        public DbSet<vwServiceCombo> vwServiceCombo { get; set; }
        public DbSet<vwUsingServiceLog> vwUsingServiceLog { get; set; }
        public DbSet<vwProductAndService> vwProductAndService { get; set; }
        public DbSet<RequestInbound> RequestInbound { get; set; }
        public DbSet<RequestInboundDetail> RequestInboundDetail { get; set; }
        public DbSet<UsingServiceLogDetail> UsingServiceLogDetail { get; set; }
        public DbSet<vwUsingServiceLogDetail> vwUsingServiceLogDetail { get; set; }
        public DbSet<vwRequestInbound> vwRequestInbound { get; set; }
        public DbSet<vwRequestInboundDetail> vwRequestInboundDetail { get; set; }
        public DbSet<ProductDamaged> ProductDamaged { get; set; }
        public DbSet<ServiceReminder> ServiceReminder { get; set; }
        public DbSet<ServiceReminderGroup> ServiceReminderGroup { get; set; }
        public DbSet<vwServiceReminderGroup> vwServiceReminderGroup { get; set; }
        public DbSet<LogServiceRemminder> LogServiceRemminder { get; set; }
        public DbSet<vwLogServiceRemminder> vwLogServiceRemminder { get; set; }

        public DbSet<CommisionCustomer> CommisionCustomer { get; set; }
        public DbSet<vwCommisionCustomer> vwCommisionCustomer { get; set; }
    //    public DbSet<CommisionCustomerLog> CommisionCustomerLog { get; set; }

        public DbSet<ServiceSchedule> ServiceSchedule { get; set; }
        public DbSet<vwServiceSchedule> vwServiceSchedule { get; set; }
        public DbSet<LogLoyaltyPoint> LogLoyaltyPoint { get; set; }
        public DbSet<vwLogLoyaltyPoint> vwLogLoyaltyPoint { get; set; }
        public DbSet<LoyaltyPoint> LoyaltyPoint { get; set; }
        public DbSet<CommissionCus> CommissionCus { get; set; }
        public DbSet<TotalDiscountMoneyNT> TotalDiscountMoneyNT { get; set; }
        public DbSet<vwTotalDiscountMoneyNT> vwTotalDiscountMoneyNT { get; set; }
        public DbSet<vwWarehouse> vwWarehouse { get; set; }
        public DbSet<LogVip> LogVip { get; set; }
        //<append_content_DbSet_here>

        // mapping bằng scan Assembly
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var addMethod = typeof(System.Data.Entity.ModelConfiguration.Configuration.ConfigurationRegistrar)
              .GetMethods()
              .Single(m =>
                m.Name == "Add"
                && m.GetGenericArguments().Any(a => a.Name == "TEntityType"));

            var domainCurrent = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name == "Erp.Domain.Sale");
            foreach (var assembly in domainCurrent)
            {
                var configTypes = assembly
                  .GetTypes()
                  .Where(t => t.BaseType != null
                    && t.BaseType.IsGenericType
                    && t.BaseType.GetGenericTypeDefinition() == typeof(System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<>));

                foreach (var type in configTypes)
                {
                    var entityType = type.BaseType.GetGenericArguments().Single();

                    var entityConfig = assembly.CreateInstance(type.FullName);
                    addMethod.MakeGenericMethod(entityType).Invoke(modelBuilder.Configurations, new object[] { entityConfig });
                }
            }
        }
    }

    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void Dispose();
    }
}
