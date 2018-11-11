using System.Data.Entity.Infrastructure;
using System.Globalization;
using Erp.BackOffice.Areas.Administration.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Interfaces;
using Erp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;
using System.Text;

namespace Erp.BackOffice.Administration.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class AccessRightController : Controller
    {
        private readonly IUserPageRepository _userPageRepository;
        private readonly IUserTypePageRepository _userTypePageRepository;
        private readonly IUserTypeRepository _userTypeRepository;        
        private readonly IPageRepository _pageRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISettingLanguageRepository _languageRepository;
        public AccessRightController(IUserTypePageRepository userTypePageRepository, IUserTypeRepository userType, IPageRepository page, IUserRepository userRepository, IUserPageRepository userPageRepository, ISettingLanguageRepository languageRepository)
        {
            _userTypePageRepository = userTypePageRepository;
            _pageRepository = page;            
            _userTypeRepository = userType;
            _userRepository = userRepository;
            _userPageRepository = userPageRepository;
            _languageRepository = languageRepository;
        }
        
        #region Create UserTypePage
        public ActionResult Create()
        {
            var model = new AccessRightViewModel
            {
                //AccessRightPageMenuViewModels = new List<AccessRightPageMenuViewModel>(),
                ListPages = _pageRepository.GetPages().ToList(),
                UserTypes = _userTypeRepository.GetUserTypes().ToList(),
                UserType_AccessRightPageViewModel = new List<UserType_AccessRightPageViewModel>()
            };

            foreach(var userType in model.UserTypes)
            {
                var list = _pageRepository.GetPagesAccessRight(0, userType.Id).Select(item => item.Id).ToList();
                if(list != null && list.Count > 0)
                {
                    var userType_AccessRightPageViewModel = new UserType_AccessRightPageViewModel();
                    userType_AccessRightPageViewModel.UserTypeId = userType.Id;
                    userType_AccessRightPageViewModel.ListAccessRightPage = list;
                    model.UserType_AccessRightPageViewModel.Add(userType_AccessRightPageViewModel);
                }
            }

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Create(AccessRightCreateModel model)
        {
            if (model.UserTypePage != null)
            {
                //Reset cache for pagerightaccess & pagemenu
                Erp.BackOffice.Helpers.CacheHelper.PagesAccessRight = null;
                Erp.BackOffice.Helpers.CacheHelper.PagesMenu = null;

                using (var scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        // TODO: Add insert logic here

                        _userTypePageRepository.DeleteAll();

                        foreach (string userTypePage in model.UserTypePage)
                        {
                            // value PageId-UserTypeId
                            string[] arrVal = userTypePage.Split('-');
                            var userTypePageViewModel = new UserTypePageViewModel
                                                            {
                                                                PageId = int.Parse(arrVal[0], CultureInfo.InvariantCulture),
                                                                UserTypeId =
                                                                    int.Parse(arrVal[1], CultureInfo.InvariantCulture)
                                                            };

                            //add
                            var userTypePageModel = new UserTypePage();
                            AutoMapper.Mapper.Map(userTypePageViewModel, userTypePageModel);
                            _userTypePageRepository.Insert(userTypePageModel);
                        }
                        scope.Complete();

                        ViewBag.AlertMessage = App_GlobalResources.Wording.SaveAccessRightSuccessful;
                        return RedirectToAction("Create");
                    }
                    catch (DbUpdateException)
                    {
                        ViewBag.AlertMessage = App_GlobalResources.Wording.SaveAccessRightUnsuccessful;
                        return View("Create");
                    }
                }
            }
            return RedirectToAction("Create");
        }

        //public ActionResult CreateUserTypePage()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult CreateUserTypePage(FormCollection fc)
        //{
        //    int UserTypeId = 0;
        //    int PageId = 0;

        //    if (int.TryParse(Request["UserTypeId"], out UserTypeId))
        //    {
        //        if (int.TryParse(Request["PageId"], out PageId))
        //        {
        //            string sql = string.Format("insert into [dbo].[UserTypePage]([UserTypeId], [PageId]) values({0}, {1})", UserTypeId, PageId);
        //            Erp.Domain.Helper.SqlHelper.ExecuteSQL(sql);

        //            ViewBag.SuccessMessage = "Thêm thành công!!!";
        //        }
        //    }

        //    return View();
        //}
        #endregion Create UserTypePage

        //#region Create UserPage
        //public ActionResult CreateUserPage()
        //{
        //    var viewModel = new AccessRightViewModel
        //                        {
        //                            AccessRightPageMenuViewModels = new List<AccessRightPageMenuViewModel>(),
        //                            Users = new List<User>()
        //                        };
        //    if (TempData["ListUserIds"] != null)
        //    {
        //        //lam?
        //        //viewModel.Users = _userRepository.GetUsers().Where(x => ((List<string>)TempData["ListUserIds"]).Contains(x.Id.ToString(CultureInfo.InvariantCulture))).ToList();
        //        viewModel.Users = _userRepository.GetUsers().Where(x => ((List<string>)TempData["ListUserIds"]).Contains(x.Id.ToString())).ToList();
        //    }
        //    //Load pagemenus
        //    var pageMenus = new List<PageMenuViewModel>();
        //    IEnumerable<vwPage> viewwPages = _pageRepository.GetPages(_languageRepository.GetDefaultLanguage()).Where(x => x.IsDeleted.HasValue ? x.IsDeleted.Value == false : true);
        //    AutoMapper.Mapper.Map(viewwPages, pageMenus);
        //    string currLang = _languageRepository.GetDefaultLanguage();
        //    foreach (PageMenuViewModel item in pageMenus)
        //    {

        //        item.IsParent = _pageRepository.GetPages(currLang).Where(x => (x.IsDeleted.HasValue ? x.IsDeleted.Value == false : true) && x.ParentId == item.Id).Count() > 0 ? true : false;
                
        //        //get userTypePage by pageId
        //        IEnumerable<UserTypePage> userTypePages = _userTypePageRepository.GetAllItemByPageId(item.Id).ToList();
        //        item.UserTypePages = userTypePages != null ? new List<UserTypePageViewModel>() : null;
        //        AutoMapper.Mapper.Map(userTypePages, item.UserTypePages);

        //        //get userPage by pageId
        //        IEnumerable<UserPage> userPages = _userPageRepository.GetAllItemByPageID(item.Id).ToList();
        //        item.UserPages = userPages != null ? new List<UserPageViewModel>() : null;
        //        AutoMapper.Mapper.Map(userPages, item.UserPages);
        //    }
        //    viewModel.PageMenuViewModels = pageMenus;

        //    ViewBag.Users = SelectListUser(TempData["UserId"] == null ? "" : TempData["UserId"].ToString());

        //    return View("UserPage", viewModel);
        //}

        //[HttpPost]
        //public ActionResult UserPageDetail(string arrayUserId)
        //{
        //    string[] arrUserIds = arrayUserId.Split(',');
        //    var viewModel = new AccessRightViewModel
        //                        {
        //                            AccessRightPageMenuViewModels = new List<AccessRightPageMenuViewModel>(),
        //                            Users =
        //                                _userRepository.GetUsers().Where(x => arrUserIds.Contains(x.Id.ToString(CultureInfo.InvariantCulture))).ToList()
        //                        };

        //    //Load pagemenus
        //    var pageMenus = new List<PageMenuViewModel>();
        //    IEnumerable<vwPage> viewPages = _pageRepository.GetPages(_languageRepository.GetDefaultLanguage()).ToList().Where(x => x.IsDeleted.HasValue ? x.IsDeleted.Value == false : true);
        //    AutoMapper.Mapper.Map(viewPages, pageMenus);
        //    foreach (PageMenuViewModel item in pageMenus)
        //    {
        //        item.IsParent = _pageRepository.GetPages(_languageRepository.GetDefaultLanguage()).ToList().Where(x => (x.IsDeleted.HasValue ? x.IsDeleted.Value == false : true) && x.ParentId == item.Id).ToList().Count > 0 ? true : false;

        //        //get userTypePage by pageId
        //        IEnumerable<UserTypePage> userTypePages = _userTypePageRepository.GetAllItemByPageId(item.Id).ToList();
        //        item.UserTypePages = userTypePages != null ? new List<UserTypePageViewModel>() : null;
        //        AutoMapper.Mapper.Map(userTypePages, item.UserTypePages);

        //        //get userPage by pageId
        //        IEnumerable<UserPage> userPages = _userPageRepository.GetAllItemByPageID(item.Id).ToList();
        //        item.UserPages = userPages != null ? new List<UserPageViewModel>() : null;
        //        AutoMapper.Mapper.Map(userPages, item.UserPages);
        //    }
        //    viewModel.PageMenuViewModels = pageMenus;

        //    return View(viewModel);
        //}

        ////
        //// POST: /Administration/AccessRight/Create

        //[HttpPost]
        //public ActionResult CreateUserPage(AccessRightCreateModel model)
        //{
        //    if (model.UserPage != null)
        //    {
        //        using (var scope = new TransactionScope(TransactionScopeOption.Required))
        //        {
        //            try
        //            {
        //                // TODO: Add insert logic here
        //                foreach (string sUserId in model.UserIds)
        //                {
        //                    _userPageRepository.Delete(int.Parse(sUserId, CultureInfo.InvariantCulture));
        //                }

        //                foreach (string userPage in model.UserPage)
        //                {
        //                    // value PageId-UserId
        //                    string[] arrVal = userPage.Split('-');                            
        //                    var userPageViewModel = new UserPageViewModel
        //                                                              {
        //                                                                  PageId =
        //                                                                      int.Parse(arrVal[0],
        //                                                                                CultureInfo.InvariantCulture),
        //                                                                  UserId =
        //                                                                      int.Parse(arrVal[1],
        //                                                                                CultureInfo.InvariantCulture)
        //                                                              };
        //                    //add
        //                    var userPageModel = new UserPage();
        //                    AutoMapper.Mapper.Map(userPageViewModel, userPageModel);
        //                    _userPageRepository.Insert(userPageModel);
        //                }
        //                scope.Complete();
        //                ViewBag.AlertMessage = App_GlobalResources.Wording.SaveAccessRightSuccessful;
        //                TempData.Add("ListUserIds",model.UserIds);
        //                TempData.Add("AlertMessage", App_GlobalResources.Wording.SaveAccessRightSuccessful);
        //                TempData.Add("UserId", model.UserIds.SingleOrDefault());
        //                return RedirectToAction("CreateUserPage");
        //            }
        //            catch (DbUpdateException)
        //            {
        //                ViewBag.AlertMessage = App_GlobalResources.Wording.SaveAccessRightUnsuccessful;
        //                return RedirectToAction("CreateUserPage");
        //            }
        //        }
        //    }
        //    return RedirectToAction("CreateUserPage");
        //}

        ////lam!
        //public SelectList SelectListUser(string userIdSelected)
        //{
        //    //var rs=new SelectList(_userRepository.GetUsersAvailable().Where(x => x.Status == UserStatus.Active).Where(x => x.UserName != "host" && x.UserTypeId.Value != 0 && x.Id != WebMatrix.WebData.WebSecurity.CurrentUserId).Take(10).ToList().Select(x => new { Id = x.Id, UserName = (x.FullName + " - " + x.Email) }), "Id", "UserName", userIdSelected);
        //    //var list=_userRepository.GetUsersOrther(WebMatrix.WebData.WebSecurity.CurrentUserId).ToList();
        //    //var rs = new SelectList(list.Select(x => new { Id = x.UserId, UserName = x.UserName }), "Id", "UserName", userIdSelected);
        //    //return rs;

        //    return null;
        //}
        //public MvcHtmlString FindUser(string name)
        //{
        //    if (name != null)
        //    {
        //        name = name.ToLower();
        //        var tests = _userRepository.GetUsersAvailable().Where(x => x.UserName.ToLower().Contains(name)).Take(10);
        //        var options = new StringBuilder();
        //        foreach (var item in tests)
        //        {
        //            options.Append("<option value='" + item.Id + "'>" + item.UserName + "</option>");
        //        }
        //        return MvcHtmlString.Create(options.ToString());
        //    }

        //    return null;
        //}
        //#endregion Create UserPage               
    }
}
