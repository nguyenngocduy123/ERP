//[assembly: WebActivator.PostApplicationStartMethod(typeof(Erp.BackOffice.App_Start.IoCContainer), "InitializeContainer")]

using Autofac;
using Autofac.Integration.Mvc;
using Erp.Domain;
using Erp.Domain.Account;
using Erp.Domain.Account.Repositories;
using Erp.Domain.Crm;
using Erp.Domain.Crm.Repositories;
using Erp.Domain.Interfaces;
using Erp.Domain.Repositories;
using Erp.Domain.Sale;
using Erp.Domain.Sale.Interfaces;
using Erp.Domain.Sale.Repositories;
using Erp.Domain.Staff;
using Erp.Domain.Staff.Interfaces;
using Erp.Domain.Staff.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Erp.BackOffice.App_Start
{
    public static class IoCContainer
    {
        public static void InitializeContainer()
        {
            // MVC setup documentation here:
            // http://autofac.readthedocs.io/en/latest/integration/mvc.html
            // WCF setup documentation here:
            // http://autofac.readthedocs.io/en/latest/integration/wcf.html
            //
            // First we'll register the MVC/WCF stuff...
            var builder = new ContainerBuilder();

            // MVC - Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //// MVC - OPTIONAL: Register model binders that require DI.
            //builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            //builder.RegisterModelBinderProvider();

            //// MVC - OPTIONAL: Register web abstractions like HttpContextBase.
            //builder.RegisterModule<AutofacWebTypesModule>();

            //// MVC - OPTIONAL: Enable property injection in view pages.
            //builder.RegisterSource(new ViewRegistrationSource());

            //// MVC - OPTIONAL: Enable property injection into action filters.
            //builder.RegisterFilterProvider();

            // Scan an assembly for components
            builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
                   .Where(t => t.FullName.StartsWith("Erp.Domain.Repositories") == true && t.FullName != "Erp.Domain.Repositories.GenericRepository")                   
                   .AsImplementedInterfaces()
                   .WithParameter("context", new ErpDbContext());

            // Scan an assembly for components (tên class repository nào cũng được)
            builder.RegisterAssemblyTypes(typeof(ProductOrServiceRepository).Assembly)
                   .Where(t => t.FullName.StartsWith("Erp.Domain.Sale.Repositories") == true && t.FullName != "Erp.Domain.Sale.Repositories.GenericRepository")
                   .AsImplementedInterfaces()
                   .WithParameter("context", new ErpSaleDbContext());

            // Scan an assembly for components (tên class repository nào cũng được)
            builder.RegisterAssemblyTypes(typeof(CampaignRepository).Assembly)
                   .Where(t => t.FullName.StartsWith("Erp.Domain.Crm.Repositories") == true && t.FullName != "Erp.Domain.Crm.Repositories.GenericRepository")
                   .AsImplementedInterfaces()
                   .WithParameter("context", new ErpCrmDbContext());

            // Scan an assembly for components (tên class repository nào cũng được)
            builder.RegisterAssemblyTypes(typeof(StaffsRepository).Assembly)
                   .Where(t => t.FullName.StartsWith("Erp.Domain.Staff.Repositories") == true && t.FullName != "Erp.Domain.Staff.Repositories.GenericRepository")
                   .AsImplementedInterfaces()
                   .WithParameter("context", new ErpStaffDbContext());

            // Scan an assembly for components (tên class repository nào cũng được)
            builder.RegisterAssemblyTypes(typeof(CustomerRepository).Assembly)
                   .Where(t => t.FullName.StartsWith("Erp.Domain.Account.Repositories") == true && t.FullName != "Erp.Domain.Account.Repositories.GenericRepository")
                   .AsImplementedInterfaces()
                   .WithParameter("context", new ErpAccountDbContext());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}