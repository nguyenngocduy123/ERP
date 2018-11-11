using Erp.BackOffice.Areas.Cms.Models;
using Erp.Domain.Entities;
using System.Web.Mvc;

namespace Erp.BackOffice.Cms
{
    public class CmsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Cms";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {            
            context.MapRoute(
                "Cms_Categories",
                "Category/{action}/{id}",
                new { controller = "Category", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Cms_News",
                "News/{action}/{id}",
                new { controller = "News", action = "ListNews", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Cms_Report",
                "Report/{action}/{id}",
                new { controller = "Report", action = "List", id = UrlParameter.Optional }
            );

            //<append_content_route_here>
            RegisterAutoMapperMap();
        }

        private static void RegisterAutoMapperMap()
        {
            AutoMapper.Mapper.CreateMap<Category, CategoryViewModel>();
            AutoMapper.Mapper.CreateMap<CategoryViewModel, Category>();
            AutoMapper.Mapper.CreateMap<Domain.Entities.News, NewsViewModel>();
            AutoMapper.Mapper.CreateMap<NewsViewModel, Domain.Entities.News>();
            //<append_content_mapper_here>
        }
    }
}
