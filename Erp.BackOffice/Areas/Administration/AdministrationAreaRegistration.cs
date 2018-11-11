using System.Web.Mvc;
using Erp.BackOffice.Administration.Models;
using Erp.Domain.Entities;
using Erp.BackOffice.Areas.Administration.Models;

namespace Erp.BackOffice.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administration_UserType",
                "UserType/{action}/{id}",
                new { controller = "UserType", action = "Index", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "Administration_Setting",
                "Setting/{action}/{id}",
                new { controller = "Setting", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Administration_Page",
                "Page/{action}/{id}",
                new { controller = "Page", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Administration_PageMenu",
                "PageMenu/{action}/{id}",
                new { controller = "PageMenu", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Administration_User",
                "User/{action}/{id}",
                new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Administration_AccessRight",
                "AccessRight/{action}/{id}",
                new { controller = "AccessRight", action = "Create", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Administration_Module",
                "Module/{action}/{id}",
                new { controller = "Module", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Administration_default",
                "Administration/{controller}/{action}/{id}",
                new { controller = "User", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Administration_MetadataFiled",
                "MetadataFiled/{controller}/{action}/{id}",
                new { controller = "MetadataFiled", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
            "Administration_ModuleRelationship",
            "ModuleRelationship/{action}/{id}",
            new { controller = "ModuleRelationship", action = "Index", id = UrlParameter.Optional }
            );

     
            //<append_content_route_here>
            RegisterAutoMapperMap();
        }

        private static void RegisterAutoMapperMap()
        {
            AutoMapper.Mapper.CreateMap<User, AddOtherUserImportViewModel>();
            AutoMapper.Mapper.CreateMap<AddOtherUserImportViewModel, User>();

            AutoMapper.Mapper.CreateMap<User, EditUserViewModel>();
            AutoMapper.Mapper.CreateMap<EditUserViewModel, User>();
            AutoMapper.Mapper.CreateMap<vwUsers, EditUserViewModel>();
            AutoMapper.Mapper.CreateMap<EditUserViewModel, vwUsers>();

            AutoMapper.Mapper.CreateMap<User, CreateUserViewModel>();
            AutoMapper.Mapper.CreateMap<CreateUserViewModel, User>();

            AutoMapper.Mapper.CreateMap<UserType, EditUserTypeModel>();
            AutoMapper.Mapper.CreateMap<EditUserTypeModel, UserType>();

            AutoMapper.Mapper.CreateMap<Language, EditSettingLanguageModel>();
            AutoMapper.Mapper.CreateMap<EditSettingLanguageModel, Language>();

            AutoMapper.Mapper.CreateMap<Page, ListPagesModel>();
            AutoMapper.Mapper.CreateMap<ListPagesModel, Page>();

            AutoMapper.Mapper.CreateMap<Page, EditPageModel>();
            AutoMapper.Mapper.CreateMap<EditPageModel, Page>();

            AutoMapper.Mapper.CreateMap<Page, PageModel>();
            AutoMapper.Mapper.CreateMap<PageModel, Page>();

            AutoMapper.Mapper.CreateMap<PageMenu, PageMenuViewModel>();
            AutoMapper.Mapper.CreateMap<PageMenuViewModel, PageMenu>();

            AutoMapper.Mapper.CreateMap<vwPageMenu, PageMenuViewModel>();
            AutoMapper.Mapper.CreateMap<PageMenuViewModel, vwPageMenu>();

            AutoMapper.Mapper.CreateMap<vwPage, ListPagesModel>();
            AutoMapper.Mapper.CreateMap<ListPagesModel, vwPage>();

            AutoMapper.Mapper.CreateMap<Setting, SettingViewModel>();
            AutoMapper.Mapper.CreateMap<SettingViewModel, Setting>();

            AutoMapper.Mapper.CreateMap<UserTypePageViewModel, UserTypePage>();
            AutoMapper.Mapper.CreateMap<UserTypePage, UserTypePageViewModel>();

            AutoMapper.Mapper.CreateMap<UserPageViewModel, UserPage>();
            AutoMapper.Mapper.CreateMap<UserPage, UserPageViewModel>();

            AutoMapper.Mapper.CreateMap<ModuleViewModel, Module>();
            AutoMapper.Mapper.CreateMap<Module, ModuleViewModel>();

            AutoMapper.Mapper.CreateMap<MetadataFieldViewModel, MetadataField>();
            AutoMapper.Mapper.CreateMap<MetadataField, MetadataFieldViewModel>();

            AutoMapper.Mapper.CreateMap<ColumnFieldViewModel, MetadataField>();
            AutoMapper.Mapper.CreateMap<MetadataField, ColumnFieldViewModel>();

            AutoMapper.Mapper.CreateMap<ColumnFieldViewModel, vwMetadataField>();
            AutoMapper.Mapper.CreateMap<vwMetadataField, ColumnFieldViewModel>();

            AutoMapper.Mapper.CreateMap<Domain.Entities.ModuleRelationship, ModuleRelationshipViewModel>();
            AutoMapper.Mapper.CreateMap<ModuleRelationshipViewModel, Domain.Entities.ModuleRelationship>();

           

            //<append_content_mapper_here>
        }
    }
}
