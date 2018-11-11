using Erp.BackOffice.Administration.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Erp.Utilities.Helpers;
using Erp.Utilities;
using Erp.BackOffice.Areas.Administration.Models;
using WebMatrix.WebData;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Erp.BackOffice.Helpers;
using System.Globalization;
using System.Resources;


namespace Erp.BackOffice.Administration.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ModuleController : Controller
    {
        string[] sColumnNotBuild = new string[] { "Id", "IsDeleted", "CreatedUserId", "CreatedDate", "ModifiedUserId", "ModifiedDate", "AssignedUserId", "Name" };

        private readonly IModuleRepository moduleRepository;
        private readonly IUserRepository userRepository;
        private readonly IMetadataFieldRepository metadataFieldRepository;
        private readonly IModuleRelationshipRepository moduleRelationshipRepository;

        public ModuleController(
            IModuleRepository _Module
            , IUserRepository _user
            , IMetadataFieldRepository _MetadataField
            , IModuleRelationshipRepository _ModuleRelationship
            )
        {
            moduleRepository = _Module;
            userRepository = _user;
            metadataFieldRepository = _MetadataField;
            moduleRelationshipRepository = _ModuleRelationship;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {
            IQueryable<ModuleViewModel> q = moduleRepository.GetAllModule()
                .Where(item => item.IsVisible == true)
                .Select(item => new ModuleViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Name = item.Name,
                }).OrderByDescending(m => m.ModifiedDate);

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q.ToList());
        }

        [HttpPost]
        public ActionResult Index()
        {
            List<string> moduleList = new List<string>();
            var q = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name == "Erp.BackOffice").FirstOrDefault().ExportedTypes;
            q = q.Where(item => item.Name.Contains("Controller")).ToList();
            foreach (var item in q)
            {
                string moduleName = item.Name.Replace("Controller", "");
                moduleList.Add(moduleName);
                string areaName = item.FullName.Replace("Erp.BackOffice.", "").Replace(".Controllers", "").Replace("." + item.Name, "");
                var module = moduleRepository.GetAllModule().Where(m => m.Name == moduleName).FirstOrDefault();
                if (module == null)
                {
                    module = new Module();
                    module.IsDeleted = false;
                    module.CreatedUserId = WebSecurity.CurrentUserId;
                    module.ModifiedUserId = WebSecurity.CurrentUserId;
                    module.CreatedDate = DateTime.Now;
                    module.ModifiedDate = DateTime.Now;
                    module.Name = moduleName;
                    module.AreaName = areaName;
                    module.IsVisible = true;
                    module.DisplayName = moduleName;
                    module.TableName = moduleName;
                    moduleRepository.InsertModule(module);
                }
            }

            //Update field IsDeleted = True for modules not exists
            var q2 = moduleRepository.GetAllModule().Where(m => !moduleList.Contains(m.Name)).ToList();
            foreach (var item in q2)
            {
                item.IsDeleted = true;
                moduleRepository.UpdateModule(item);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Module = moduleRepository.GetModuleById(Id.Value);
            if (Module != null && Module.IsDeleted != true)
            {
                var model = new ModuleViewModel();
                AutoMapper.Mapper.Map(Module, model);

                if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }
                model.ModuleName = model.Name;
                model.ModuleLabel = model.Name;
                model.SourceFolder = model.Name;
                model.AppName = model.Name;

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ModuleEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var module = moduleRepository.GetModuleById(model.Id);
                    module.ModifiedUserId = WebSecurity.CurrentUserId;
                    module.ModifiedDate = DateTime.Now;
                    module.TableName = model.TableName;
                    module.IsVisible = model.IsVisible;
                    model.Name = model.Name;
                    moduleRepository.UpdateModule(module);

                    //Update metadata field of this module
                    var sql = string.Format(@"SELECT ORDINAL_POSITION, " +
                                     "COLUMN_NAME as Name, " +
                                     "DATA_TYPE, " +
                                     "CHARACTER_MAXIMUM_LENGTH, " +
                                     "IS_NULLABLE " +
                                "FROM INFORMATION_SCHEMA.COLUMNS " +
                                "WHERE TABLE_NAME = '{0}'", model.TableName);
                    var q = Domain.Helper.SqlHelper.QuerySQL<ColumnFieldViewModel>(sql).ToList();

                    foreach(var item in q)
                    {
                        var q1 = metadataFieldRepository.GetAllMetadataField()
                            .Where(i => i.Name == item.Name && i.ModuleId == model.Id).FirstOrDefault();
                        if(q1 == null)
                        {
                            var metadataFiled = new MetadataField();
                            metadataFiled.IsDeleted = false;
                            metadataFiled.CreatedUserId = WebSecurity.CurrentUserId;
                            metadataFiled.ModifiedUserId = WebSecurity.CurrentUserId;
                            metadataFiled.CreatedDate = DateTime.Now;
                            metadataFiled.ModifiedDate = DateTime.Now;
                            metadataFiled.Name = item.Name;
                            metadataFiled.ModuleId = model.Id;
                            metadataFiled.IsVisible = true;

                            metadataFieldRepository.InsertMetadataField(metadataFiled);
                        }
                    }

                    TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                    return RedirectToAction("Index");
                }

                return View(model);
            }

            return View(model);

            //if (Request.UrlReferrer != null)
            //    return Redirect(Request.UrlReferrer.AbsoluteUri);
            //return RedirectToAction("Index");
        }

        #endregion

        #region Detail
        public ActionResult Detail(int? Id)
        {
            var Module = moduleRepository.GetModuleById(Id.Value);
            if (Module != null && Module.IsDeleted != true)
            {
                var model = new ModuleViewModel();
                AutoMapper.Mapper.Map(Module, model);

                if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete()
        {
            try
            {
                string idDeleteAll = Request["DeleteId-checkbox"];
                string[] arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    var item = moduleRepository.GetModuleById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsVisible = false;
                        moduleRepository.UpdateModule(item);
                    }
                }
                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.DeleteSuccess;
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = App_GlobalResources.Error.RelationError;
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region MetadataFieldsAssistant
        public ViewResult MetadataFieldsAssistant(string entity)
        {
            var model = new MetadataFieldsAssistantViewModel();
            model.Entity = entity;

            //Get module name from module relationship defind
            var moduleRelationship = moduleRelationshipRepository.GetAllModuleRelationship()
                .Where(item => item.First_ModuleName == entity)
                .ToList();

            moduleRelationship.Insert(0, new ModuleRelationship()
            {
                First_ModuleName = entity,
                Second_ModuleName = entity,
                Second_ModuleName_Alias = entity
            });

            var selectListItems = new List<SelectListItem>();
            SelectListItem itemEmpty = new SelectListItem();
            itemEmpty.Text = App_GlobalResources.Wording.Empty;
            itemEmpty.Value = null;
            selectListItems.Add(itemEmpty);

            foreach (var item in moduleRelationship)
            {
                var q = metadataFieldRepository.GetAllMetadataField()
                            .Where(i => i.ModuleName == item.Second_ModuleName)
                            .OrderBy(i => i.Id).ToList();

                
                if (q != null && q.Count > 0)
                {   
                    try
                    {
                        foreach (var i in q)
                        {
                            SelectListItem itemMetadataField = new SelectListItem();
                            itemMetadataField.Text = item.Second_ModuleName_Alias + "." + i.Name;
                            itemMetadataField.Value = "{" + item.Second_ModuleName_Alias + "." + i.Name.ToString() + "}";
                            selectListItems.Add(itemMetadataField);
                        }
                    }
                    catch { }
                }
            }

            model.SelectListMetadataFields = new SelectList(selectListItems, "Value", "Text", null);

            return View(model);
        }
        #endregion

        public ActionResult Create()
        {
            var model = new ModuleViewModel();
            model.SourceFolder = Erp.BackOffice.Helpers.Common.GetWebConfig("ModuleBuilder_SourceFolder");
            model.AppName = Erp.BackOffice.Helpers.Common.GetWebConfig("ModuleBuilder_AppName");
            model.Name = "ModuleBuilder_ModuleName";
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ModuleViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ListColumns = model.ListColumns.Where(item => item.Name != null).ToList();

                string sModuleName = model.ModuleName;
                string sModuleLabel = model.ModuleLabel;
                string sModulePath = model.SourceFolder + "\\Module\\" + sModuleName;
                string sEntitiesOutFilePath = sModulePath + "\\" + sModuleName + ".cs";
                string sMappingMapOutFilePath = sModulePath + "\\" + sModuleName + "Map.cs";
                string sIRepositoryOutFilePath = sModulePath + "\\I" + sModuleName + "Repository.cs";
                string sRepositoryOutFilePath = sModulePath + "\\" + sModuleName + "Repository.cs";

                string sTmpPath = model.SourceFolder + "\\" + model.AppName + ".BackOffice\\Areas\\Administration\\Views\\Module\\template";
                string sTmpPathModulename = model.SourceFolder + "\\" + model.AppName + ".BackOffice\\Areas\\Administration\\Views\\Module\\template\\MODULE_NAME";

                string sEntitiesTmpFilePath = sTmpPath + "\\Entities.cs";
                string sMappingMapTmpFilePath = sTmpPath + "\\MappingMap.cs";
                string sIRepositoryTmpFilePath = sTmpPath + "\\IRepository.cs";
                string sRepositoryTmpFilePath = sTmpPath + "\\Repository.cs";

                //DeleteDirectory(sModulePath);
                //Directory.CreateDirectory(sModulePath);

                //BuildEntitiesFile(model.AppName, model.AreaName, model.ListColumns, sModuleName, sEntitiesTmpFilePath, sEntitiesOutFilePath);
                //BuildMappingMapFile(model.AppName, model.AreaName, model.ListColumns, sModuleName, sMappingMapTmpFilePath, sMappingMapOutFilePath);
                //BuildIRepositoryFile(model.AppName, model.AreaName, sModuleName, sIRepositoryTmpFilePath, sIRepositoryOutFilePath);
                //BuildRepositoryFile(model.AppName, model.AreaName, sModuleName, sRepositoryTmpFilePath, sRepositoryOutFilePath);

                //Copy(model.AreaName, model.AppName, model.ListColumns, sTmpPathModulename, sModulePath + "\\" + sModuleName, sModuleName, sModuleLabel);

                string messageDisplay = "Build module successfull!";
                string entityDisplay = "public DbSet<" + sModuleName + "> " + sModuleName + " { get; set; }";
                //string entityMapDispay += "\r\n\r\nmodelBuilder.Configurations.Add(new " + sModuleName + "Map());";
                string routeDisplay = "context.MapRoute(" +
                                      "\r\n            \"" + model.AreaName + "_" + sModuleName + "\"," +
                                      "\r\n            \"" + sModuleName + "/{action}/{id}\"," +
                                      "\r\n            new { controller = \"" + sModuleName + "\", action = \"Index\", id = UrlParameter.Optional }" +
                                      "\r\n            );";
                string mapperDisplay = "AutoMapper.Mapper.CreateMap<Domain." + model.AreaName + ".Entities." + sModuleName + ", " + sModuleName + "ViewModel>();";
                mapperDisplay += "\r\n            AutoMapper.Mapper.CreateMap<" + sModuleName + "ViewModel, Domain." + model.AreaName + ".Entities." + sModuleName + ">();";
                //string injectionDispay += "\r\n\r\ncontainer.Register<I" + sModuleName + "Repository, " + sModuleName + "Repository>();";


                //cập nhật nội dung file [model.AreaName]AreaRegistration -----------------------------------------------------------------------------
                //UpdateContentFileAreaRegistration(model, routeDisplay, mapperDisplay);

                //thêm file các file tạo mới vào project backoffice
                string sAreaDirPath = model.SourceFolder + "\\" + model.AppName + ".BackOffice\\Areas\\" + model.AreaName;
                List<string> fileNameInArea = CreateModuleFileInArea(model.IsViewEntity, model.AreaName, model.AppName, model.ListColumns, sTmpPathModulename, sAreaDirPath, sModuleName, sModuleLabel);
                AddFileNameToProjectFile("BackOffice", fileNameInArea);

                //tạo wording resource
                List<DictionaryEntry> currentWording = new List<DictionaryEntry>();

                string wordingFileName = string.Format(@"{0}\{1}.BackOffice\App_GlobalResources\Wording.resx", model.SourceFolder, model.AppName);
                using (ResXResourceReader resx = new ResXResourceReader(wordingFileName))
                {
                    foreach (DictionaryEntry d in resx)
                    {
                        currentWording.Add(new DictionaryEntry { Key = d.Key, Value = d.Value });
                    }
                }

                using (ResXResourceWriter resx = new ResXResourceWriter(wordingFileName))
                {
                    foreach (DictionaryEntry d in currentWording)
                    {
                        resx.AddResource(d.Key.ToString(), d.Value);
                    }

                    resx.AddResource("PageCreate_" + model.ModuleName, "Thêm " + model.ModuleLabel);
                    resx.AddResource("PageEdit_" + model.ModuleName, "Cập nhật " + model.ModuleLabel);
                    resx.AddResource("PageIndex_" + model.ModuleName, "Danh sách " + model.ModuleLabel);
                    //resx.AddResource("Information", SystemIcons.Information);
                }


                //cập nhật nội dung file [model.AppName][model.AreaName]DbContext.cs -----------------------------------------------------------------
                UpdateContentFileDbContext(model, entityDisplay);

                //tạo các file trong domain
                string areaDomainDirPath = model.SourceFolder + "\\" + model.AppName + ".Domain." + model.AreaName;
                BuildEntitiesFile(model.AppName, model.AreaName, model.ListColumns, sModuleName, sEntitiesTmpFilePath, areaDomainDirPath + "\\Entities\\" + sModuleName + ".cs");
                BuildMappingMapFile(model.AppName, model.AreaName, model.ListColumns, sModuleName, sMappingMapTmpFilePath, areaDomainDirPath + "\\Entities\\Mapping\\" + sModuleName + "Map.cs");

                List<string> fileNameInDomain = new List<string>() {
                    areaDomainDirPath + "\\Entities\\" + sModuleName + ".cs",
                    areaDomainDirPath + "\\Entities\\Mapping\\" + sModuleName + "Map.cs",
                };

                if (model.IsViewEntity == false)
                {
                    BuildIRepositoryFile(model.AppName, model.AreaName, sModuleName, sIRepositoryTmpFilePath, areaDomainDirPath + "\\Interfaces\\I" + sModuleName + "Repository.cs");
                    BuildRepositoryFile(model.AppName, model.AreaName, sModuleName, sRepositoryTmpFilePath, areaDomainDirPath + "\\Repositories\\" + sModuleName + "Repository.cs");

                    fileNameInDomain.Add(areaDomainDirPath + "\\Interfaces\\I" + sModuleName + "Repository.cs");
                    fileNameInDomain.Add(areaDomainDirPath + "\\Repositories\\" + sModuleName + "Repository.cs");
                }

                //thêm file các file tạo mới vào project domain
                AddFileNameToProjectFile(model.AreaName, fileNameInDomain);

                //Them vao database
                var module = new Module();
                module.IsDeleted = false;
                module.CreatedUserId = WebSecurity.CurrentUserId;
                module.ModifiedUserId = WebSecurity.CurrentUserId;
                module.CreatedDate = DateTime.Now;
                module.ModifiedDate = DateTime.Now;
                module.Name = sModuleName;
                module.AreaName = model.AreaName;
                module.IsVisible = true;
                module.DisplayName = model.ModuleLabel;
                module.TableName = sModuleName;
                moduleRepository.InsertModule(module);

                //hiển thị ra view để xem
                string notifyLabel = string.Format("Run SQL file if need be <span class=\"red\">(important: move {0}.sql in Area\\{1}\\Views\\{0} outside folder you want.)</span>", sModuleName, model.AreaName);
                TempData["AlertMessage"] = messageDisplay + (model.IsViewEntity == false ? notifyLabel : ""); // +routeDisplay + mapperDisplay;
            }

            return RedirectToAction("Create");
        }

        void UpdateContentFileDbContext(ModuleViewModel model, string entityDisplay)
        {
            //cập nhật nội dung file [model.AppName][model.AreaName]DbContext.cs -----------------------------------------------------------------
            string areaDomainDirPath = model.SourceFolder + "\\" + model.AppName + ".Domain." + model.AreaName;
            string areaDomainDbContextFile = Path.Combine(areaDomainDirPath, Path.GetFileName(model.AppName + model.AreaName + "DbContext.cs"));
            if (!System.IO.File.Exists(areaDomainDbContextFile))
                areaDomainDbContextFile = Path.Combine(model.SourceFolder + "\\" + model.AppName + ".Domain", Path.GetFileName(model.AppName + "DbContext.cs"));
            string strAreaDomainDbContextFileOutput = System.IO.File.ReadAllText(areaDomainDbContextFile);
            entityDisplay += "\r\n        //<append_content_DbSet_here>";
            strAreaDomainDbContextFileOutput = strAreaDomainDbContextFileOutput.Replace("//<append_content_DbSet_here>", entityDisplay);
            //cập nhật nội dung mới vào file
            System.IO.File.WriteAllText(areaDomainDbContextFile, strAreaDomainDbContextFileOutput);
            // ------------------------------------------------------------------------------------------------------------------------------------
        }

        void UpdateContentFileAreaRegistration(ModuleViewModel model, string routeDisplay, string mapperDisplay)
        {
            //cập nhật nội dung file [model.AreaName]AreaRegistration -----------------------------------------------------------------------------
            string sPathAreaDir = model.SourceFolder + "\\" + model.AppName + ".BackOffice\\Areas\\" + model.AreaName;
            string areaRegistrationFilePath = Path.Combine(sPathAreaDir, Path.GetFileName(model.AreaName + "AreaRegistration.cs"));
            string strAreaRegistrationFileOutput = System.IO.File.ReadAllText(areaRegistrationFilePath);
            //thêm route
            routeDisplay += "\r\n\r\n            //<append_content_route_here>";
            strAreaRegistrationFileOutput = strAreaRegistrationFileOutput.Replace("//<append_content_route_here>", routeDisplay);
            //thêm khai báo auto mapper
            mapperDisplay += "\r\n\r\n            //<append_content_mapper_here>";
            strAreaRegistrationFileOutput = strAreaRegistrationFileOutput.Replace("//<append_content_mapper_here>", mapperDisplay);
            //cập nhật nội dung mới vào file
            System.IO.File.WriteAllText(areaRegistrationFilePath, strAreaRegistrationFileOutput);
            // ------------------------------------------------------------------------------------------------------------------------------------
        }

        void BuildEntitiesFile(string AppName, string AreaName, List<ColumnFieldViewModel> ListConlumns, string sModuleName, string TmpFilePath, string OutFilePath)
        {
            string strOutput = System.IO.File.ReadAllText(TmpFilePath);
            strOutput = strOutput.Replace("<MODULE_NAME>", sModuleName);
            strOutput = strOutput.Replace("<APP_NAME>", AppName);
            strOutput = strOutput.Replace("<AREA_NAME>", AreaName);

            string strItems = "";
            foreach (var item in ListConlumns)
            {
                string sCOLUMN_NAME = item.Name;
                string sDATA_TYPE = item.DataType;
                string sCHARACTER_MAXIMUM_LENGTH = item.Length;

                var results = Array.FindAll(sColumnNotBuild, s => s.Equals(sCOLUMN_NAME));
                if (results.Count() == 0)
                {
                    switch (sDATA_TYPE)
                    {
                        case "int":
                            strItems += "        public Nullable<int> " + sCOLUMN_NAME + " { get; set; }\r\n";
                            break;
                        case "float":
                            strItems += "        public double? " + sCOLUMN_NAME + " { get; set; }\r\n";
                            break;
                        case "decimal":
                            strItems += "        public decimal? " + sCOLUMN_NAME + " { get; set; }\r\n";
                            break;
                        case "bit":
                            strItems += "        public Nullable<bool> " + sCOLUMN_NAME + " { get; set; }\r\n";
                            break;
                        case "datetime":
                            strItems += "        public Nullable<System.DateTime> " + sCOLUMN_NAME + " { get; set; }\r\n";
                            break;
                        default:
                            strItems += "        public string " + sCOLUMN_NAME + " { get; set; }\r\n";
                            break;
                    }
                }
            }

            strOutput = strOutput.Replace("<CONTENT>", strItems);

            System.IO.File.WriteAllText(OutFilePath, strOutput);
        }

        void BuildMappingMapFile(string AppName, string AreaName, List<ColumnFieldViewModel> ListConlumns, string sModuleName, string TmpFilePath, string OutFilePath)
        {
            string strOutput = System.IO.File.ReadAllText(TmpFilePath);
            strOutput = strOutput.Replace("<MODULE_NAME>", sModuleName);
            strOutput = strOutput.Replace("<APP_NAME>", AppName);
            strOutput = strOutput.Replace("<AREA_NAME>", AreaName);

            string strContent1 = "", strContent2 = "";
            foreach (var item in ListConlumns)
            {
                string sCOLUMN_NAME = item.Name;
                string sDATA_TYPE = item.DataType;
                string sCHARACTER_MAXIMUM_LENGTH = item.Length;

                var results = Array.FindAll(sColumnNotBuild, s => s.Equals(sCOLUMN_NAME));
                if (results.Count() == 0)
                {
                    switch (sDATA_TYPE)
                    {
                        case "int":
                            break;
                        case "bit":
                            break;
                        case "datetime":
                            break;
                        case "nvarchar":
                        case "varchar":
                        case "char":
                            strContent1 += "            this.Property(t => t." + sCOLUMN_NAME + ").HasMaxLength(" + sCHARACTER_MAXIMUM_LENGTH + ");\r\n";
                            break;
                        default:
                            break;
                    }

                    strContent2 += "            this.Property(t => t." + sCOLUMN_NAME + ").HasColumnName(\"" + sCOLUMN_NAME + "\");\r\n";
                }
            }

            strOutput = strOutput.Replace("<CONTENT1>", strContent1);
            strOutput = strOutput.Replace("<CONTENT2>", strContent2);

            System.IO.File.WriteAllText(OutFilePath, strOutput);
        }

        void BuildIRepositoryFile(string AppName, string AreaName, string sModuleName, string TmpFilePath, string OutFilePath)
        {
            string strOutput = System.IO.File.ReadAllText(TmpFilePath);
            strOutput = strOutput.Replace("<MODULE_NAME>", sModuleName);
            strOutput = strOutput.Replace("<APP_NAME>", AppName);
            strOutput = strOutput.Replace("<AREA_NAME>", AreaName);

            System.IO.File.WriteAllText(OutFilePath, strOutput);
        }

        void BuildRepositoryFile(string AppName, string AreaName, string sModuleName, string TmpFilePath, string OutFilePath)
        {
            string strOutput = System.IO.File.ReadAllText(TmpFilePath);
            strOutput = strOutput.Replace("<MODULE_NAME>", sModuleName);
            strOutput = strOutput.Replace("<APP_NAME>", AppName);
            strOutput = strOutput.Replace("<AREA_NAME>", AreaName);

            System.IO.File.WriteAllText(OutFilePath, strOutput);
        }

        void DeleteDirectory(string sModulePath)
        {
            try
            {
                Directory.Delete(sModulePath, true);

                DirectoryInfo dir = new DirectoryInfo(sModulePath);
                foreach (DirectoryInfo d in dir.GetDirectories())
                    DeleteDirectory(d.FullName);
            }
            catch { }
        }

        void Copy(string AreaName, string AppName, List<ColumnFieldViewModel> ListConlumns, string sourceDir, string targetDir, string sModuleName, string sModuleLabel)
        {
            targetDir = targetDir.Replace("MODULE_NAME", sModuleName);
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                string targetFile = Path.Combine(targetDir, Path.GetFileName(file).Replace("MODULE_NAME", sModuleName));

                System.IO.File.Copy(file, targetFile);

                string strOutput = System.IO.File.ReadAllText(targetFile);
                strOutput = strOutput.Replace("<MODULE_NAME>", sModuleName);
                strOutput = strOutput.Replace("<MODULE_LABEL>", sModuleLabel);
                strOutput = strOutput.Replace("<APP_NAME>", AppName);
                strOutput = strOutput.Replace("<AREA_NAME>", AreaName);

                if (targetFile.EndsWith("ViewModel.cs") || targetFile.EndsWith(".sql"))
                {
                    string strItems = "";
                    foreach (var item in ListConlumns)
                    {
                        string sCOLUMN_NAME = item.Name;
                        string sDATA_TYPE = item.DataType;
                        string sCHARACTER_MAXIMUM_LENGTH = item.Length;

                        var results = Array.FindAll(sColumnNotBuild, s => s.Equals(sCOLUMN_NAME));
                        if (results.Count() == 0)
                        {
                            if (targetFile.EndsWith("ViewModel.cs"))
                            {
                                strItems += "        [Display(Name = \"" + sCOLUMN_NAME + "\", ResourceType = typeof(Wording))]\r\n";
                                switch (sDATA_TYPE)
                                {
                                    case "int":
                                        strItems += "        public Nullable<int> " + sCOLUMN_NAME + " { get; set; }\r\n";
                                        break;
                                    case "decimal":
                                        strItems += "        public decimal? " + sCOLUMN_NAME + " { get; set; }\r\n";
                                        break;
                                    case "float":
                                        strItems += "        public double? " + sCOLUMN_NAME + " { get; set; }\r\n";
                                        break;
                                    case "bit":
                                        strItems += "        public Nullable<bool> " + sCOLUMN_NAME + " { get; set; }\r\n";
                                        break;
                                    case "datetime":
                                        strItems += "        public Nullable<System.DateTime> " + sCOLUMN_NAME + " { get; set; }\r\n";
                                        break;
                                    default:
                                        strItems += "        public string " + sCOLUMN_NAME + " { get; set; }\r\n";
                                        break;
                                }
                            }
                            else if (targetFile.EndsWith(".sql"))
                            {
                                switch (sDATA_TYPE)
                                {
                                    case "int":
                                        strItems += "	, [" + sCOLUMN_NAME + "] " + sDATA_TYPE + "\r\n";
                                        break;
                                    case "bit":
                                        strItems += "	, [" + sCOLUMN_NAME + "] " + sDATA_TYPE + "\r\n";
                                        break;
                                    case "datetime":
                                        strItems += "	, [" + sCOLUMN_NAME + "] " + sDATA_TYPE + "\r\n";
                                        break;
                                    default:
                                        strItems += "	, [" + sCOLUMN_NAME + "] " + sDATA_TYPE + "(" + sCHARACTER_MAXIMUM_LENGTH + ")" + "\r\n";
                                        break;
                                }
                            }
                        }
                    }

                    strOutput = strOutput.Replace("<CONTENT>", strItems);
                }

                // for layout cshtml file
                if (targetFile.EndsWith(".cshtml") && targetFile.Contains("Index") == false)
                {
                    string strItems = "";
                    foreach (var item in ListConlumns)
                    {
                        string sCOLUMN_NAME = item.Name;
                        string sDATA_TYPE = item.DataType;

                        int results = Array.FindAll(sColumnNotBuild, s => s.Equals(sCOLUMN_NAME)).Count(); ;

                        string controlHTML = "input";
                        if (sDATA_TYPE == "int" && sCOLUMN_NAME.Contains("Id"))
                            controlHTML = "dropdown";

                        //if (targetFile.Contains("Edit.cshtml"))
                        //{
                        //    results = 0;
                        //    controlHTML = "hidden";
                        //}

                        if (results == 0)
                        {
                            switch (controlHTML)
                            {
                                case "input":
                                    strItems += "    @Html.CustomTextboxFor(model => model." + sCOLUMN_NAME + ", null, null, WidthType.span12)\r\n";
                                    break;
                                case "dropdown":
                                    strItems += "    @Html.CustomDropDownListFor(model => model." + sCOLUMN_NAME + ", " + sCOLUMN_NAME + "List, WidthType.span12, true, null, DropdownListStyle.DropdownListStyleDefault)\r\n";
                                    break;
                                default:
                                    strItems += "    @Html.HiddenFor(model => model." + sCOLUMN_NAME + ")\r\n";
                                    break;
                            }
                        }
                    }

                    strOutput = strOutput.Replace("<CONTENT>", strItems);
                }

                System.IO.File.WriteAllText(targetFile, strOutput);
            }

            foreach (var directory in Directory.GetDirectories(sourceDir))
                Copy(AreaName, AppName, ListConlumns, directory, Path.Combine(targetDir, Path.GetFileName(directory)), sModuleName, sModuleLabel);
        }


        List<string> CreateModuleFileInArea(bool IsViewEntity, string AreaName, string AppName, List<ColumnFieldViewModel> ListConlumns, string sourceDir, string targetDir, string sModuleName, string sModuleLabel)
        {
            List<string> fileNameList = new List<string>();

            targetDir = targetDir.Replace("MODULE_NAME", sModuleName);
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                if (IsViewEntity == false || (IsViewEntity == true && file.EndsWith("ViewModel.cs")))
                {
                    string targetDirCreateFile = targetDir;
                    if (file.EndsWith("ViewModel.cs"))
                        targetDirCreateFile += "\\Models";
                    else if (file.EndsWith("Controller.cs"))
                        targetDirCreateFile += "\\Controllers";
                    else
                    {
                        targetDirCreateFile += "\\Views\\" + sModuleName;
                        Directory.CreateDirectory(targetDirCreateFile);
                    }


                    string targetFile = Path.Combine(targetDirCreateFile, Path.GetFileName(file).Replace("MODULE_NAME", sModuleName));

                    //thêm tên file vào danh sách trả về
                    fileNameList.Add(targetFile);

                    System.IO.File.Copy(file, targetFile);

                    string strOutput = System.IO.File.ReadAllText(targetFile);
                    strOutput = strOutput.Replace("<MODULE_NAME>", sModuleName);
                    strOutput = strOutput.Replace("<MODULE_LABEL>", sModuleLabel);
                    strOutput = strOutput.Replace("<APP_NAME>", AppName);
                    strOutput = strOutput.Replace("<AREA_NAME>", AreaName);

                    if (targetFile.EndsWith("ViewModel.cs") || targetFile.EndsWith(".cshtml") || targetFile.EndsWith(".sql"))
                    {
                        string strItems = "";
                        foreach (var item in ListConlumns)
                        {
                            string sCOLUMN_NAME = item.Name;
                            string sDATA_TYPE = item.DataType;
                            string sCHARACTER_MAXIMUM_LENGTH = item.Length;

                            var results = Array.FindAll(sColumnNotBuild, s => s.Equals(sCOLUMN_NAME));
                            if (results.Count() == 0)
                            {
                                //for model file
                                if (targetFile.EndsWith("ViewModel.cs"))
                                {
                                    strItems += "        [Display(Name = \"" + sCOLUMN_NAME + "\", ResourceType = typeof(Wording))]\r\n";
                                    switch (sDATA_TYPE)
                                    {
                                        case "int":
                                            strItems += "        public Nullable<int> " + sCOLUMN_NAME + " { get; set; }\r\n";
                                            break;
                                        case "bit":
                                            strItems += "        public Nullable<bool> " + sCOLUMN_NAME + " { get; set; }\r\n";
                                            break;
                                        case "datetime":
                                            strItems += "        public Nullable<System.DateTime> " + sCOLUMN_NAME + " { get; set; }\r\n";
                                            break;
                                        default:
                                            strItems += "        public string " + sCOLUMN_NAME + " { get; set; }\r\n";
                                            break;
                                    }
                                }

                                //for sql file
                                if (targetFile.EndsWith(".sql"))
                                {
                                    switch (sDATA_TYPE)
                                    {
                                        case "int":
                                            strItems += "	, [" + sCOLUMN_NAME + "] " + sDATA_TYPE + "\r\n";
                                            break;
                                        case "bit":
                                            strItems += "	, [" + sCOLUMN_NAME + "] " + sDATA_TYPE + "\r\n";
                                            break;
                                        case "datetime":
                                            strItems += "	, [" + sCOLUMN_NAME + "] " + sDATA_TYPE + "\r\n";
                                            break;
                                        default:
                                            strItems += "	, [" + sCOLUMN_NAME + "] " + sDATA_TYPE + "(" + sCHARACTER_MAXIMUM_LENGTH + ")" + "\r\n";
                                            break;
                                    }
                                }

                                //for view file
                                if (targetFile.EndsWith(".cshtml") && targetFile.Contains("Index") == false)
                                {
                                    string controlHTML = "input";
                                    if (sDATA_TYPE == "int" && sCOLUMN_NAME.Contains("Id"))
                                        controlHTML = "dropdown";

                                    switch (controlHTML)
                                    {
                                        case "input":
                                            strItems += "    @Html.CustomTextboxFor(model => model." + sCOLUMN_NAME + ", null, null, WidthType.span12)\r\n";
                                            break;
                                        case "dropdown":
                                            strItems += "    @Html.CustomDropDownListFor(model => model." + sCOLUMN_NAME + ", " + sCOLUMN_NAME + "List, WidthType.span12, true, null, DropdownListStyle.DropdownListStyleDefault)\r\n";
                                            break;
                                        default:
                                            strItems += "    @Html.HiddenFor(model => model." + sCOLUMN_NAME + ")\r\n";
                                            break;
                                    }
                                }
                            }
                        }

                        strOutput = strOutput.Replace("<CONTENT>", strItems);

                    } //end  if (targetFile.EndsWith("ViewModel.cs") || targetFile.EndsWith(".cshtml"))

                    System.IO.File.WriteAllText(targetFile, strOutput);

                }// end if (IsViewEntity == false || (IsViewEntity == true && file.EndsWith("ViewModel.cs")))

            } // end foreach file in directory


            if (IsViewEntity == false)
            {
                foreach (var directory in Directory.GetDirectories(sourceDir))
                {
                    var fileNameListInDir = CreateModuleFileInArea(IsViewEntity, AreaName, AppName, ListConlumns, directory, targetDir, sModuleName, sModuleLabel);
                    fileNameList = fileNameList.Union(fileNameListInDir).ToList();
                }
            }

            return fileNameList;
        }

        void AddFileNameToProjectFile(string projectName, List<string> fileNameList)
        {
            EnvDTE80.DTE2 _dte2;
            _dte2 = (EnvDTE80.DTE2)System.Runtime.InteropServices.Marshal.
            GetActiveObject("VisualStudio.DTE.12.0");

            foreach (EnvDTE.Project proj in _dte2.Solution.Projects)
            {
                if (proj.Name.Contains(projectName))
                {
                    var project = new Microsoft.Build.Evaluation.Project(proj.FullName);
                    foreach (var file in fileNameList)
                        project.AddItem("Compile", file);

                    project.Save();
                    break;
                }
            }
        }


        public ActionResult GetDataTypeFromTable(string tableName)
        {
            List<object> listDataColumn = GetData(tableName);

            string text = string.Empty;
            foreach (var item in listDataColumn)
            {
                var types = item.GetType();
            }

            return View(listDataColumn);
        }

        public List<object> GetData(string tableName)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ErpDbContext"].ConnectionString.ToString();

            SqlConnection rConn = new SqlConnection(connectionString); //returns sql connection

            rConn.Open();
            SqlCommand SqlCmd = new SqlCommand();

            SqlCmd = rConn.CreateCommand();

            SqlCmd.CommandText = string.Format(@"SELECT ORDINAL_POSITION, " +
                                     "COLUMN_NAME, " +
                                     "DATA_TYPE, " +
                                     "CHARACTER_MAXIMUM_LENGTH, " +
                                     "IS_NULLABLE " +
                                "FROM INFORMATION_SCHEMA.COLUMNS " +
                                "WHERE TABLE_NAME = '{0}'", tableName);
            SqlDataReader SqlDr = SqlCmd.ExecuteReader();

            SqlDr.Read();

            List<object> listDataColumn = new List<object>();
            while (SqlDr.Read())
            {
                var OrdPos = SqlDr.GetValue(0);
                var ColName = SqlDr.GetValue(1);
                var DataType = SqlDr.GetValue(2);
                var CharMaxLen = SqlDr.GetValue(3);
                var IsNullable = SqlDr.GetValue(4);
                //Debug.WriteLine("ColName - " + ColName + " DataType - " + DataType + " CharMaxLen - " + CharMaxLen);

                listDataColumn.Add(new { ColumnName = ColName, DataType = DataType, CharMaxLen = CharMaxLen });
            }
            rConn.Close();
            return listDataColumn;
        }

        #region Add new report

        void UpdateControllerReport(string ModuleName, string actionContentReport, string sPathAreaDir)
        {
            //cập nhật nội dung file action cua report -----------------------------------------------------------------------------

            string areaRegistrationFilePath = Path.Combine(sPathAreaDir, Path.GetFileName(ModuleName + "Controller.cs"));
            string strAreaRegistrationFileOutput = System.IO.File.ReadAllText(areaRegistrationFilePath);
            //thêm action vào report
            actionContentReport += "\r\n\r\n            //<append_content_action_here>";
            strAreaRegistrationFileOutput = strAreaRegistrationFileOutput.Replace("//<append_content_action_here>", actionContentReport);
            //cập nhật nội dung mới vào file
            System.IO.File.WriteAllText(areaRegistrationFilePath, strAreaRegistrationFileOutput);
            // ------------------------------------------------------------------------------------------------------------------------------------
        }
        public ActionResult CreateReport()
        {
            var model = new ActionReportViewModel();
            model.SourceFolder = Erp.BackOffice.Helpers.Common.GetWebConfig("ModuleBuilder_SourceFolder");
            model.AppName = Erp.BackOffice.Helpers.Common.GetWebConfig("ModuleBuilder_AppName");
            model.Name = "ModuleBuilder_ModuleName";
            model.ModuleName = "SaleReport";
            model.AreaName = "Sale";
            //model.
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(model);
        }



        [HttpPost]
        public ActionResult CreateReport(ActionReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ListColumns = model.ListColumns.Where(item => item.Name != null).ToList();
                string sModuleName = model.ModuleName;
                string sActionName = model.ActionName;
                string sActionLabel = model.ActionLabel;
                string sModulePath = model.SourceFolder + "\\Module\\" + sModuleName;

                //string html_Report = sModulePath + "\\" + sActionName + ".cshtml";

                string sTmpPath_ActionName = model.SourceFolder + "\\" + model.AppName + ".BackOffice\\Areas\\Administration\\Views\\Module\\template\\REPORT_NAME";

                #region  tạo action mới vào sale report
                string actionContentReport = "public ActionResult " + model.ActionName + "()\r\n";
                actionContentReport += "        {\r\n ";
                actionContentReport += "                return View(); \r\n";
                actionContentReport += "        } \r\n";
                //cập nhật nội dung file [model.AreaName] AreaRegistration -----------------------------------------------------------------------------
                string sPathAreaDir = model.SourceFolder + "\\" + model.AppName + ".BackOffice\\Areas\\" + model.AreaName + "\\Controllers\\";
                UpdateControllerReport(model.ModuleName, actionContentReport, sPathAreaDir);
                #endregion

                #region  tạo action mới vào controllers api
                #region  khởi tạo các dữ liệu ban đầu để cập nhật action vào controllers api
                //cập nhật nội dung api
                string sPath_API_Dir = model.SourceFolder + "\\" + model.AppName + ".BackOffice\\Controllers\\";
                string ModuleName_API = "BackOfficeServiceAPI";
                string str_search = "";
                string str_Isnull = "";
                string str_DateTime = "";
                string str_content_search_sql = "";
                var list_search = model.ListColumns.Where(x => x.IsSearch == true).ToList();
                string str_image = "";
                string str_image_content_for = "";
                //phần khởi tạo trước code tìm kiếm trong file cshtml
                string str_Search_cshtml = "";
                //phần khởi tạo trước code tìm kiếm trong file jsx .. lát khỏi phải làm nữa.
                string str_Take_Value_Search_jsx = "";
                string str_Content_Search_jsx = "";
                #endregion

                #region  nếu có DateTime thì bắt lỗi datetime null
                var count_DateTime = list_search.Where(x => x.Type == "DateTime").Count();
                if (count_DateTime >= 1)
                {
                    str_DateTime += "        DateTime aDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); \r\n";
                    str_DateTime += "        DateTime retDateTime = aDateTime.AddMonths(1).AddDays(-1); \r\n";
                    str_DateTime += "       if(StartDate == null && EndDate == null && string.IsNullOrEmpty(dien_lai_cho_nay)) \r\n";
                    str_DateTime += "       { \r\n";
                    str_DateTime += "            StartDate = aDateTime.ToString(\"dd/MM/yyyy\"); \r\n";
                    str_DateTime += "            EndDate = retDateTime.ToString(\"dd/MM/yyyy\"); \r\n";
                    str_DateTime += "       } \r\n";
                    str_DateTime += "       var d_startDate = (!string.IsNullOrEmpty(StartDate) ? DateTime.ParseExact(StartDate.ToString(), \"dd/MM/yyyy\", null).ToString(\"yyyy-MM-dd HH:mm:ss\") : \"\"); \r\n";
                    str_DateTime += "       var d_endDate = (!string.IsNullOrEmpty(EndDate) ? DateTime.ParseExact(EndDate.ToString(), \"dd/MM/yyyy\", null).ToString(\"yyyy-MM-dd HH:mm:ss\") : \"\"); \r\n";
                }
                #endregion

                #region  nếu có image thì lấy đường dẫn cho image
                var list_image = model.ListColumns.Where(x => x.Name.Contains("Image")).ToList();
                var count_image = list_image.Count();
                if (count_image > 0)
                {
                    str_image += "          foreach (var item in data) \r\n";
                    str_image += "           { \r\n";
                    str_image += "                  <str_image_content_for> \r\n";
                    str_image += "           } \r\n";

                    foreach (var item in list_image)
                    {
                        str_image_content_for += item.Name + "=Erp.API.Helpers.Common.KiemTraTonTaiAnhKhacUrl(item.ProductImage, \"upload_path_PurchaseOrder\", \"product\"); \r\n";
                    }
                    str_image = str_image.Replace("<str_image_content_for>", str_image_content_for);

                }
                #endregion

                #region  duyệt list columns để tạo action lấy list dữ liệu trong api
                var count_list_search = list_search.Count();
                for (int i = 0; i < count_list_search; i++)
                {
                    //phần này của api
                    // str_search
                    if (i + 1 < list_search.Count())
                    {
                        if (list_search[i].Type == "DateTime")
                        {
                            str_search += "string " + list_search[i].Name + ", ";
                        }
                        else
                        {
                            if (list_search[i].Type == "int" || list_search[i].Type == "float" || list_search[i].Type == "decimal")
                            {
                                str_search += list_search[i].Type + "? " + list_search[i].Name + ", ";
                            }
                            else
                            {
                                str_search += list_search[i].Type + " " + list_search[i].Name + ", ";
                            }
                        }
                    }
                    else
                    {
                        if (list_search[i].Type == "DateTime")
                        {
                            str_search += "string " + list_search[i].Name + " ";
                        }
                        else
                        {
                            if (list_search[i].Type == "int" || list_search[i].Type == "float" || list_search[i].Type == "decimal")
                            {
                                str_search += list_search[i].Type + "? " + list_search[i].Name + " ";
                            }
                            else
                            {
                                str_search += list_search[i].Type + " " + list_search[i].Name + " ";
                            }
                        }
                    }

                    //str_Isnull
                    if (list_search[i].Type == "int" || list_search[i].Type == "decimal" || list_search[i].Type == "float")
                    {
                        str_Isnull += list_search[i].Name + "=" + list_search[i].Name + "==null?0:" + list_search[i].Name + "; \r\n";
                    }
                    else
                    {
                        str_Isnull += list_search[i].Name + "=" + list_search[i].Name + "==null?\"\":" + list_search[i].Name + "; \r\n";
                    }
                    //str_content_search_sql
                    if (i + 1 < list_search.Count())
                    {
                        str_content_search_sql += list_search[i].Name + "=" + list_search[i].Name + ", \r\n";
                    }
                    else
                    {
                        str_content_search_sql += list_search[i].Name + "=" + list_search[i].Name + "\r\n";
                    }
                    //phần này của cshtml
                    str_Search_cshtml += "          <span class=\"ctl inline\"> \r\n";
                    str_Search_cshtml += "              @Html.TextBox(\"" + list_search[i].Name + "\", Request[\"" + list_search[i].Name + "\"], new { autocomplete = \"off\", placeholder = \"" + list_search[i].Label + "...\" }) \r\n";
                    str_Search_cshtml += "           </span>  \r\n";

                    // phần này của jsx
                    //str_Take_Value_Search_jsx
                    str_Take_Value_Search_jsx += " var " + list_search[i].Name + " = $('#" + list_search[i].Name + "').val();  \r\n";
                    //str_Content_Search_jsx
                    if (i + 1 < count_list_search)
                    {
                        str_Content_Search_jsx += list_search[i].Name + ": " + list_search[i].Name + ",  \r\n";
                    }
                    else
                    {
                        str_Content_Search_jsx += list_search[i].Name + ": " + list_search[i].Name + "\r\n";
                    }

                }
                #endregion

                #region  cập nhật action vào controllers api
                string actionContent_API_Report = "public object GetList" + model.ActionName + "(<str_search>) \r\n";
                actionContent_API_Report += "       { \r\n";

                actionContent_API_Report += "                <str_Isnull> \r\n";

                actionContent_API_Report += "                <str_DateTime> \r\n";

                actionContent_API_Report += "            var data = SqlHelper.QuerySP<" + model.ActionName + "ViewModel>(\"sp" + model.ActionName + "\", new \r\n";
                actionContent_API_Report += "            { \r\n";
                actionContent_API_Report += "                   <str_content_search_sql>";
                actionContent_API_Report += "            }).ToList(); \r\n";
                //image
                actionContent_API_Report += "           <str_image>";
                actionContent_API_Report += "            return data; \r\n";
                actionContent_API_Report += "       } \r\n";

                actionContent_API_Report = actionContent_API_Report.Replace("<str_search>", str_search);
                actionContent_API_Report = actionContent_API_Report.Replace("<str_Isnull>", str_Isnull);
                actionContent_API_Report = actionContent_API_Report.Replace("<str_DateTime>", str_DateTime);
                actionContent_API_Report = actionContent_API_Report.Replace("<str_content_search_sql>", str_content_search_sql);
                actionContent_API_Report = actionContent_API_Report.Replace("<str_image>", str_image);
                UpdateControllerReport(ModuleName_API, actionContent_API_Report, sPath_API_Dir);
                #endregion
                #endregion

                //thêm file các file tạo mới vào project backoffice
                var list_column_active = model.ListColumns.Where(x => x.IsTable == true).ToList();
                List<string> fileNameInArea = CreateActionFileInArea(model.AreaName, model.AppName, list_column_active, sTmpPath_ActionName, sActionName, sActionLabel, model.SourceFolder, model.ModuleName, str_Search_cshtml, str_Take_Value_Search_jsx, str_Content_Search_jsx);
                AddFileNameToProjectFile("BackOffice", fileNameInArea);
            }

            return RedirectToAction("CreateReport");
        }

        List<string> CreateActionFileInArea(string AreaName, string AppName, List<ColumnViewModel> ListConlumns, string sourceDir, string ActionName, string ActionLabel, string SourceFolder, string ModuleName, string str_Search_cshtml, string str_Take_Value_Search_jsx, string str_Content_Search_jsx)
        {
            List<string> fileNameList = new List<string>();

            //đường dẫn file teamplete sourceDir
            //đường dẫn lưu file targetDir
            #region thêm file html, model, jsx vào project
            string path_file_html_report = SourceFolder + "\\" + AppName + ".BackOffice\\Areas\\" + AreaName + "\\Views\\" + ModuleName;
            string path_file_model_report = SourceFolder + "\\" + AppName + ".BackOffice\\Areas\\Sale\\Models";
            string path_file_jsx_report = SourceFolder + "\\" + AppName + ".BackOffice\\Scripts\\cmreactjs";
            #endregion

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                if (file.EndsWith(".cshtml") || file.EndsWith("ViewModel.cs") || file.EndsWith(".jsx"))
                {
                    string targetDirCreateFile = "";
                    if (file.EndsWith(".cshtml"))
                    {
                        targetDirCreateFile = path_file_html_report;
                    }
                    else if (file.EndsWith("ViewModel.cs"))
                    {
                        targetDirCreateFile = path_file_model_report;
                    }
                    else
                    {
                        targetDirCreateFile = path_file_jsx_report;
                    }
                    string targetFile = Path.Combine(targetDirCreateFile, Path.GetFileName(file).Replace("ACTION_NAME", ActionName));

                    //thêm tên file vào danh sách trả về
                    fileNameList.Add(targetFile);

                    System.IO.File.Copy(file, targetFile);

                    string strOutput = System.IO.File.ReadAllText(targetFile);
                    strOutput = strOutput.Replace("<ACTION_NAME>", ActionName);
                    strOutput = strOutput.Replace("<ACTION_NAME_TITLE>", ActionLabel);
                    strOutput = strOutput.Replace("<APP_NAME>", AppName);
                    strOutput = strOutput.Replace("<AREA_NAME>", AreaName);

                    #region trường hợp file cshtml
                    if (targetFile.EndsWith(".cshtml"))
                    {
                        strOutput = strOutput.Replace("<SEARCH_CONTENT>", str_Search_cshtml);
                    }
                    #endregion

                    #region trường hợp file model
                    if (targetFile.EndsWith("ViewModel.cs"))
                    {
                        string strItems = "";
                        foreach (var item in ListConlumns)
                        {
                            string sCOLUMN_NAME = item.Name;
                            string sDATA_TYPE = item.Type;
                            string sCOLUMN_LABEL = item.Label;

                            var results = Array.FindAll(sColumnNotBuild, s => s.Equals(sCOLUMN_NAME));
                            if (results.Count() == 0)
                            {
                                if (sDATA_TYPE == "string")
                                {
                                    strItems += "public " + sDATA_TYPE + " " + sCOLUMN_NAME + " { get; set; }\r\n";
                                }
                                else
                                {
                                    strItems += "public " + sDATA_TYPE + "? " + sCOLUMN_NAME + " { get; set; }\r\n";
                                }
                            }
                        }
                        strOutput = strOutput.Replace("<CONTENT>", strItems);
                    }
                    #endregion

                    #region trường hợp file  jsx
                    if (targetFile.EndsWith(".jsx"))
                    {
                        string str_Table_Report = "";
                        string str_Body_Table_Report = "";
                        string str1 = "";
                        string str2 = "";
                        string str3 = "";
                        string str4 = "";
                        string strFirstColor = "";
                        string str_tr_BODY = "";
                        int Count_Column = ListConlumns.Count() + 1;
                        string strHeader = "";

                        #region khởi tạo table str_Table_Report
                        str_Table_Report += " var TableReport = React.createClass({";
                        str_Table_Report += "\r\n render: function() {";
                        str_Table_Report += "\r\n <str1>";
                        str_Table_Report += "\r\n var tbody = this.props.datatable.map(function(comment_obj, i) {";
                        str_Table_Report += "\r\n <str2>";
                        str_Table_Report += "\r\n   return (";

                        str_Table_Report += "\r\n  <BodyTable stt ={i + 1}";
                        str_Table_Report += "\r\n <str3>";
                        str_Table_Report += "\r\n   key ={i}>";
                        str_Table_Report += "\r\n    </BodyTable>";

                        str_Table_Report += "\r\n   );";
                        str_Table_Report += "\r\n    });";
                        str_Table_Report += "\r\n  return (";

                        str_Table_Report += "\r\n   <table className =\"table table-bordered table-responsive\" id = \"cTable\">";

                        str_Table_Report += "\r\n <thead>";
                        str_Table_Report += "\r\n  <tr>";
                        str_Table_Report += "\r\n  <th colSpan = \"" + Count_Column + "\" className =\"cell-center\">< h4 >{ title} từ { startdate} đến {enddate}</h4></th>";
                        str_Table_Report += "\r\n  </tr>";
                        str_Table_Report += "\r\n  <tr>";
                        str_Table_Report += "\r\n <th> STT </th>";
                        str_Table_Report += "\r\n <strHeader>";
                        str_Table_Report += "\r\n  </tr>";
                        str_Table_Report += "\r\n   </thead>";

                        str_Table_Report += "\r\n  <tbody>";
                        str_Table_Report += "\r\n  {tbody}";
                        str_Table_Report += "\r\n  <tr className=\"tr-bold\">";
                        str_Table_Report += "\r\n  <td></td>";
                        str_Table_Report += "\r\n  <str4>";
                        str_Table_Report += "\r\n   </tr>";
                        str_Table_Report += "\r\n    </tbody>";
                        str_Table_Report += "\r\n    </table>";
                        str_Table_Report += "\r\n     );";
                        str_Table_Report += "\r\n     }";
                        str_Table_Report += "\r\n     });";
                        str_Table_Report += "\r\n     };";

                        #endregion

                        #region khởi tạo table str_Body_Table_Report
                        str_Body_Table_Report += "\r\n  var BodyTable = React.createClass({";
                        str_Body_Table_Report += "\r\n    render: function() {";
                        str_Body_Table_Report += "\r\n     var name = this.props.stt % 2 == 0 ? \"alert-warning\" : \"\";";

                        str_Body_Table_Report += "\r\n  <strFirstColor>";

                        str_Body_Table_Report += "\r\n  return (";
                        str_Body_Table_Report += "\r\n   <tr className ={name}>";
                        str_Body_Table_Report += "\r\n  <td>{this.props.stt}</td>";

                        str_Body_Table_Report += "\r\n   <str_tr_BODY>";

                        str_Body_Table_Report += "\r\n   </ tr >";
                        str_Body_Table_Report += "\r\n   );";
                        str_Body_Table_Report += "\r\n   }";
                        str_Body_Table_Report += "\r\n   });";
                        #endregion

                        foreach (var item in ListConlumns)
                        {
                            string sCOLUMN_NAME = item.Name;
                            string sDATA_TYPE = item.Type;
                            string sCOLUMN_LABEL = item.Label;

                            var results = Array.FindAll(sColumnNotBuild, s => s.Equals(sCOLUMN_NAME));
                            if (results.Count() == 0)
                            {
                                //for model file
                                if (targetFile.EndsWith(".jsx"))
                                {
                                    if (item.Type == "int" || item.Type == "float" || item.Type == "decimal")
                                    {
                                        //khởi tạo hàm sum trong component
                                        str1 += "\r\n sum" + sCOLUMN_NAME + "=0;";
                                        //cộng dồn giá trị cần sum
                                        str2 += "\r\n sum" + sCOLUMN_NAME + " += comment_obj." + sCOLUMN_NAME + ";";
                                        //thẻ <td> hiển thị cột sum
                                        str4 += "\r\n   <td className=\"text-right\">{ConvertSoAm(sum" + sCOLUMN_NAME + ")}</td>";

                                        strFirstColor += "  var color_" + sCOLUMN_NAME + " = parseInt(this.props." + sCOLUMN_NAME + ") < 0 ? \"text-right red\":\"text-right green\"; \r\n";
                                        str_tr_BODY += "\r\n   <td className ={color_" + sCOLUMN_NAME + "}>{ConvertSoAm(this.props." + sCOLUMN_NAME + ")}</td>";

                                    }
                                    else
                                    {
                                        //nếu cột nào là string thì không sum và chèn ô trống.
                                        str4 += "\r\n <td className =\"cell-center text-right\"></td> ";
                                        str_tr_BODY += "\r\n  <td>{this.props." + sCOLUMN_NAME + "}</td>";
                                    }

                                    //title của table
                                    strHeader += "\r\n <th className=\"text-center\">" + sCOLUMN_LABEL + "</th>";
                                    //gán giá trị của model và table
                                    str3 += "\r\n" + sCOLUMN_NAME + "= { comment_obj." + sCOLUMN_NAME + " }";
                                }

                            }
                        }
                        //replace các cột của str_Table_Report vào strong table..
                        str_Table_Report = str_Table_Report.Replace("<str1>", str1);
                        str_Table_Report = str_Table_Report.Replace("<str2>", str2);
                        str_Table_Report = str_Table_Report.Replace("<str3>", str3);
                        str_Table_Report = str_Table_Report.Replace("<str4>", str4);
                        str_Table_Report = str_Table_Report.Replace("<strHeader>", strHeader);
                        str_Body_Table_Report = str_Body_Table_Report.Replace("<strFirstColor>", strFirstColor);
                        str_Body_Table_Report = str_Body_Table_Report.Replace("<str_tr_BODY>", str_tr_BODY);
                        //gắn TableReport vào file jsx
                        strOutput = strOutput.Replace("//<TABLE_REPORT>", str_Table_Report);
                        strOutput = strOutput.Replace("//<BODY_TABLE_REPORT>", str_Body_Table_Report);
                        strOutput = strOutput.Replace("//<Take_Value_Search>", str_Take_Value_Search_jsx);
                        strOutput = strOutput.Replace("//<Content_Search>", str_Content_Search_jsx);

                    } //end  if (targetFile.EndsWith("ViewModel.cs") || targetFile.EndsWith(".cshtml"))
                    #endregion

                    System.IO.File.WriteAllText(targetFile, strOutput);

                }// end if (IsViewEntity == false || (IsViewEntity == true && file.EndsWith("ViewModel.cs")))

            } // end foreach file in directory

            return fileNameList;
        }



        #endregion
    }
}
