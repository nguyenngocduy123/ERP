namespace Erp.BackOffice.Report.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using Telerik.Reporting.Cache.Interfaces;
    using Telerik.Reporting.Services.Engine;
    using Telerik.Reporting.Services.WebApi;

    //// The class name determines the service URL. 
    //// ReportsController class name defines /api/report/ service URL.
    public class ReportsController : ReportsControllerBase
    {
        protected override IReportResolver CreateReportResolver()
        {
            //This is the folder that contains the XML (trdx) report definitions.
            var reportsPath = HttpContext.Current.Server.MapPath(@"~/");

            //Resolver for trdx report definitions.
            var reportFileResolver = new ReportFileResolver(reportsPath);

            // Resolver for class report definitions.
            var reportTypeResolver = new ReportTypeResolver();

            // Add the ReportTypeResolver as fallback resolver and 
            // return the ReportFileResolver instance.
            return reportFileResolver.AddFallbackResolver(reportTypeResolver);
        }

        protected override ICache CreateCache()
        {
            // Create instance of the default file cache.
            return Telerik.Reporting.Services.Engine.CacheFactory.CreateFileCache(HttpContext.Current.Server.MapPath(@"~/") + "ReportCache");
        }
    }
}