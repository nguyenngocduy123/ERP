using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using Erp.BackOffice.Administration.Models;
using Erp.BackOffice.Filters;
using Erp.BackOffice.Models;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using WebMatrix.WebData;
using System.Text;
using Erp.BackOffice.Areas.Administration.Models;
using Erp.Utilities;
using Erp.Utilities.Helpers;
using System.Data.OleDb;
using System.Data;
using Erp.BackOffice.Helpers;
using System.Diagnostics;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Erp.BackOffice.App_GlobalResources;
using System.Configuration;
using System.IO;
using Excel;
using Erp.Domain.Staff.Entities;
using Erp.Domain.Staff.Interfaces;
using Erp.BackOffice.Staff.Models;
using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
//using Excel;

namespace Erp.BackOffice.Administration.Controllers
{
    
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTypeRepository _userTypeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserSettingRepository _userSettingRepository;
        private readonly ISettingRepository _settingRepository;
        private readonly IBranchRepository branchRepository;
        //static HttpApplicationState application = 
        private readonly IStaffsRepository staffRepository;
        private readonly IUserTypeRepository user_typeRepository;
        private readonly IPositionRepository positionRepository;
        private readonly ICustomerRepository customerRepository;        
        public static readonly int MAX_LOGIN_FAILED = 5;

        public UserController(IUserRepository userRepo,
            IUserTypeRepository userType,
            ICategoryRepository category,
            IUserSettingRepository userSetting,
            ISettingRepository setting,
            IBranchRepository branch,
            IStaffsRepository staff,
            IUserTypeRepository user_type,
            IPositionRepository position,
            ICustomerRepository customer
            )
        {
            _userRepository = userRepo;
            _userTypeRepository = userType;
            _categoryRepository = category;
            _userSettingRepository = userSetting;
            _settingRepository = setting;
            branchRepository = branch;
            staffRepository = staff;
            user_typeRepository = user_type;
            positionRepository = position;
            customerRepository = customer;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            User user1 = null;
            if (ModelState.IsValid)
            {
                user1 = _userRepository.GetByUserName(model.UserName.ToLower());
                if (user1 != null && user1.LoginFailedCount >= MAX_LOGIN_FAILED)
                {
                    ModelState.AddModelError("", "Bạn đăng nhập sai quá 5 lần. Tài khoản này đã bị khóa. Vui lòng liên hệ quản trị để được hỗ trợ");
                    return View(model);
                }
            }

            if (ModelState.IsValid && WebSecurity.Login(model.UserName.ToLower(), model.Password, persistCookie: model.RememberMe))
            {
                var user = _userRepository.GetByUserName(model.UserName.ToLower());
                if (user != null && user.Status == UserStatus.Deleted)
                    return RedirectToAction("LogOff");
                if (user == null)
                    return RedirectToAction("LogOff");

                if (user != null)
                {
                    _userRepository.resetLoginFailed(user.Id);

                    //var ListRequest = HttpContext.Application["ListRequest"] as List<RequestInfo>;
                    //if (ListRequest != null)
                    //{
                    //    var ip = HttpContext.Request.UserHostAddress;
                    //    var requestInfo = ListRequest.Where(item => item.IP == ip).FirstOrDefault();
                    //    if (requestInfo != null)
                    //    {
                    //        requestInfo.UserName = user.FullName;
                    //    }
                    //}
                }

                var sessionId = HttpContext.Session.SessionID;

                //HttpContext.ApplicationInstance.Application
                if (HttpContext.Application[user.UserName.ToLower()] != null)
                {
                    string sessionIdOther = Convert.ToString(HttpContext.Application[user.UserName.ToLower()]);
                    if (sessionIdOther != sessionId)
                    {
                        HttpContext.Session.Remove(sessionIdOther);
                        HttpContext.Application[user.UserName.ToLower()] = sessionId;
                    }
                    else
                    {
                        // Save the user id retrieved from membership database to application state.
                        HttpContext.Application[user.UserName.ToLower()] = sessionId;
                    }
                }
                else
                {
                    HttpContext.Application[user.UserName.ToLower()] = sessionId;
                }


                //if (!string.IsNullOrEmpty(returnUrl))
                //{
                //    return Redirect(returnUrl);
                //}
                //var id = Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId;
                ////var usertype = userTypeRepository.GetUserTypeById(id);
                //if (id == 21)
                //{
                //    var url = "/Customer/Client";
                //    return Redirect(url);
                //}
                //string home_page = Helpers.Common.GetSetting("home_page");
                return Redirect("/");
            }
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "User name hoặc password không chính xác.");
                if (user1 != null)
                    _userRepository.increaseLoginFailed(user1.Id);
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            return View("ForgetPassword");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgetPassword(ForgetPasswordViewModel model)
        {
            //string newPassword = LibraryErp.BackOffice.Helpers.Common.GenerateNewPassword(6);
            //var user = _userRepository.GetUsers().FirstOrDefault(u => u.Email == model.Email);
            //if (user != null)
            //{
            //    string confirmationToken =
            //WebSecurity.GeneratePasswordResetToken(user.UserName);

            //    WebSecurity.ResetPassword(confirmationToken, newPassword);

            //    StringBuilder sbMailContent = new StringBuilder();
            //    sbMailContent.Append("<div>" + App_GlobalResources.Wording.SiteName + "</div> <br/>")
            //                    .AppendFormat("<div> Chúng tôi đã hỗ trỡ bạn reset mật khẩu. Đây là mật khẩu mới của bạn: <b>{0}<b/></div> <br/>", newPassword)
            //                    .Append("<div>Trân trọng! <br/> Cảm ơn<div>");
            //    MailHelper.MailHelper.SendEmailDirect(model.Email, null, null, App_GlobalResources.Wording.SiteName + " - Reset mật khẩu", sbMailContent.ToString());

            //    ViewBag.SuccessMessage = MvcHtmlString.Create(App_GlobalResources.Wording.RetrievePasswordSuccess);

            //    return View("ForgetPassword");
            //}
            //ViewBag.SuccessMessage = MvcHtmlString.Create("Địa chỉ Email không tồn tại.");
            return View("ForgetPassword");

        }

        public ActionResult ChangePasswordForUsers(int? id)
        {
            var model = new UserUserViewModel();
            model.UserId = id.Value;
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePasswordForUsers(UserUserViewModel model)
        {
            var user = _userRepository.GetUserById(model.UserId);
            if (user != null)
            {
                string confirmationToken = WebSecurity.GeneratePasswordResetToken(user.UserName);

                WebSecurity.ResetPassword(confirmationToken, model.NewPassword);

                ViewBag.SuccessMessage = MvcHtmlString.Create(App_GlobalResources.Wording.RetrievePasswordSuccess);
            }

            ViewBag.status = "success";

            return View(model);
        }

        //
        // POST: /User/LogOff
        public ActionResult LogOff()
        {
            Erp.BackOffice.Helpers.Common.CurrentUser = null;

            WebSecurity.Logout();

            // xóa biến Application[userName] với userName là userName đang đăng nhập
            var userName = User.Identity.Name;
            var sessionID = Session.SessionID;
            if (HttpContext.Application[userName] != null)
            {
                string sessionIdUserLogging = Convert.ToString(HttpContext.Application[userName]);
                if (sessionIdUserLogging == sessionID)
                {
                    HttpContext.Application.Contents.Remove(userName);
                }
            }

            return RedirectToAction("Login", "User", new { @area = "Administration" });
        }

        //
        // POST: /User/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerUser = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the User if the currently logged in user is the owner
            if (ownerUser == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
                {
                    bool hasLocalUser = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalUser || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /User/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");

            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View();
        }

        //
        // POST: /User/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalUser = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalUser;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalUser)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                        if (changePasswordSucceeded)
                        {
                            var user = _userRepository.GetByUserName(User.Identity.Name);
                            if (user != null)
                            {
                                //var UPL = _userRepository.GetUPLevelByUId(user.Id);
                                //if (UPL != null && UPL.Position != null)
                                //{
                                //    string userCode = UPL.Position.Code;
                                //    if (!string.IsNullOrEmpty(userCode))
                                //    {
                                //        if (userCode.ToUpper() != Globals.PositionCodeFSM.ToUpper())
                                //        {
                                //            //using (SavinaForumRegisterService.RegisterServiceSoapClient client = new SavinaForumRegisterService.RegisterServiceSoapClient())
                                //            //{
                                //            //    client.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                                //            //}
                                //        }
                                //    }

                                //}

                            }

                        }
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        TempData["AlertMessage"] = App_GlobalResources.Wording.ChangePasswordSuccess;
                        return RedirectToAction("Manage");
                    }
                    TempData["AlertMessage"] = App_GlobalResources.Wording.ChangePasswordError;
                    return RedirectToAction("Manage");
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /User/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /User/ExternalLoginCallback
        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new User
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            // User is new, ask for their desired membership name
            string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
        }

        //
        // POST: /User/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (var db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToUpper() == model.UserName.ToUpper());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);
                        return RedirectToLocal(returnUrl);
                    }
                    ModelState.AddModelError("UserName", "User name already exists. Please enter a different username.");
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        public ViewResult Index(int? TypeId, int? StatusId, string txtSearch)
        {
            string a = "Admin".ToUpper();
            string h = "Host".ToUpper();

            //var catId = Request["CategoryId"] != null ? Convert.ToInt32(Request["CategoryId"]) : 0;
            //var status = Request["StatusId"] != null ? Convert.ToInt32(Request["StatusId"]) : 0;
            //var type = Request["TypeId"] != null ? Convert.ToInt32(Request["TypeId"]) : 0;

            var model = _userRepository.GetAllvwUsers()
                .Select(item => new UserViewModel
                {
                    Id = item.Id,
                    ModifiedDate = item.ModifiedDate,
                    UserName = item.UserName,
                    FullName = item.FullName,
                    LoginFailedCount = item.LoginFailedCount,
                    Status = item.Status,
                    UserTypeName = item.UserTypeName,
                    UserTypeId = item.UserTypeId
                }).OrderByDescending(m => m.ModifiedDate).ToList();

            if (TypeId != null && TypeId.Value > 0)
            {
                model = model.Where(x => x.UserTypeId == TypeId.Value).ToList();
            }
            UserStatus us = UserStatus.Active;
            if (StatusId.HasValue)
            {
                if (StatusId == 0)
                    us = UserStatus.Deleted;
                if (StatusId == 1)
                    us = UserStatus.Active;
                if (StatusId == -1)
                    us = UserStatus.Pending;

            }
            if (StatusId != null && StatusId.Value > 0)
            {
                model = model.Where(x => x.Status == us).ToList();
            }
            if (!string.IsNullOrEmpty(txtSearch))
            {
                model = model.Where(u => u.UserName.ToUpper().Contains(txtSearch.ToUpper())).ToList();
            }
            var listCategories = _categoryRepository.GetAllCategories();
            List<SelectListItem> listCats = new SelectList(listCategories, "Id", "Name").ToList();
            ViewBag.listCategories = listCats;


            var listUserType = _userTypeRepository.GetUserTypes();
            List<SelectListItem> listUserTypes = new SelectList(listUserType, "Id", "Name").ToList();
            ViewBag.listUserTypes = listUserTypes;

            List<SelectListItem> listSts = new List<SelectListItem>();
            listSts.Add(new SelectListItem { Value = "0", Text = "Đã xóa" });
            listSts.Add(new SelectListItem { Value = "1", Text = "Hoạt động" });
            listSts.Add(new SelectListItem { Value = "-1", Text = "Pending" });

            //statistic
            ViewBag.Sum = model.Count();
            ViewBag.Pending = model.Where(t => t.Status == UserStatus.Pending).Count();
            ViewBag.Active = model.Where(t => t.Status == UserStatus.Active).Count();
            ViewBag.Deleted = model.Where(t => t.Status == UserStatus.Deleted).Count();
            ViewBag.listStatus = listSts;
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(model);
        }

        public ActionResult ExportExcel()
        {
            var aaa = _userRepository.GetUsersAvailable();
            IQueryable<vwUsers> Users = aaa;
            var txtSearch = Request.QueryString["txtSearch"];
            var type = Request.QueryString["TypeId"];
            var status = Request.QueryString["Status"];

            if (!string.IsNullOrEmpty(txtSearch))
            {
                Users = Users.Where(m => m.UserName.ToUpper().Contains(txtSearch.ToUpper()));
            }
            if (status != null)
            {
                var us = UserStatus.Active;
                if (status == "0")
                    us = UserStatus.Deleted;
                if (status == "1")
                    us = UserStatus.Active;
                if (status == "-1")
                    us = UserStatus.Pending;
                Users = Users.Where(m => m.Status == us);
            }
            if (type != null)
            {
                int UserTypeId = Convert.ToInt32(type);
                Users = Users.Where(m => m.UserTypeId == UserTypeId);
            }
            var list = Users.OrderByDescending(m => m.ModifiedDate)
                .ToList();

            using (var pck = new ExcelPackage())
            {
                #region excel
                ExcelWorksheet firstSheet = pck.Workbook.Worksheets.Add("Raw Data");
                firstSheet.Cells[1, 1].Value = "STT";
                firstSheet.Cells[1, 2].Value = "Họ và tên";
                firstSheet.Cells[1, 3].Value = "Username";
                firstSheet.Cells[1, 4].Value = "NewUserName";
                firstSheet.Cells[1, 5].Value = "Password";
                firstSheet.Cells[1, 6].Value = "Ngành hàng";
                firstSheet.Cells[1, 7].Value = "Loại người dùng";
                firstSheet.Cells[1, 8].Value = "Giới Tính";
                firstSheet.Cells[1, 9].Value = "Mã Nhân Viên";
                firstSheet.Cells[1, 10].Value = "Email";
                firstSheet.Cells[1, 11].Value = "Cấp bậc";
                firstSheet.Cells[1, 12].Value = "Chức vụ";
                firstSheet.Cells[1, 13].Value = "MCS ID";
                firstSheet.Cells[1, 14].Value = "Mã cửa hàng";
                firstSheet.Cells[1, 15].Value = "Cửa Hàng";
                firstSheet.Cells[1, 16].Value = "Địa Chỉ Cửa Hàng";
                firstSheet.Cells[1, 17].Value = "BlueId";
                firstSheet.Cells[1, 18].Value = "Grade";
                firstSheet.Cells[1, 19].Value = "Blue Point";
                firstSheet.Cells[1, 20].Value = "Ngày Sinh";
                firstSheet.Cells[1, 21].Value = "Nơi Sinh";
                firstSheet.Cells[1, 22].Value = "CMND";
                firstSheet.Cells[1, 23].Value = "Ngày Cấp CMND";
                firstSheet.Cells[1, 24].Value = "Nơi Cấp CMND";
                firstSheet.Cells[1, 25].Value = "Địa Chỉ thường trú/tạm trú";
                firstSheet.Cells[1, 26].Value = "SĐT";
                firstSheet.Cells[1, 27].Value = "Khu Vực";
                firstSheet.Cells[1, 28].Value = "Team Leader";
                firstSheet.Cells[1, 29].Value = "Số lần đăng nhập thất bại";

                using (ExcelRange rng = firstSheet.Cells["A1:Y1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                for (int i = 0; i < list.Count(); i++)
                {
                    var item = list.ElementAt(i);

                    // STT
                    firstSheet.Cells[i + 2, 1].Value = i + 1;

                    var user = item;
                    // Họ tên
                    firstSheet.Cells[i + 2, 2].Value = user.FullName;

                    // Username
                    firstSheet.Cells[i + 2, 3].Value = user.UserName;

                    // pwd
                    firstSheet.Cells[i + 2, 5].Value = "";

                    // Region
                    firstSheet.Cells[i + 2, 8].Value = user.Sex == true ? "Nam" : "Nữ";

                    // LotterySystem
                    firstSheet.Cells[i + 2, 9].Value = item.UserCode;

                    // Points
                    firstSheet.Cells[i + 2, 10].Value = item.Email;

                    // NumberTicket
                    //var UserPositionLevel = _userRepository.GetUPLevelByUId(user.Id);
                    //string LevelName = "";
                    //string PositionName = "";
                    //if (UserPositionLevel != null)
                    //{
                    //    LevelName = UserPositionLevel.Level != null ? UserPositionLevel.Level.Name : string.Empty;

                    //    PositionName = UserPositionLevel.Position != null ? UserPositionLevel.Position.Name : string.Empty;
                    //}
                    //firstSheet.Cells[i + 2, 11].Value = LevelName;
                    //firstSheet.Cells[i + 2, 12].Value = PositionName;
                    //firstSheet.Cells[i + 2, 13].Value = item.MCS_ID;
                    //firstSheet.Cells[i + 2, 14].Value = item.CHANNEL_CODE;
                    //firstSheet.Cells[i + 2, 15].Value = item.CHANNEL_NAME;
                    //firstSheet.Cells[i + 2, 16].Value = item.CHANNEL_ADDRESS;
                    //firstSheet.Cells[i + 2, 17].Value = string.IsNullOrEmpty(item.BlueId) ? "0" : item.BlueId;
                    //firstSheet.Cells[i + 2, 18].Value = item.Grade == null ? "0" : item.Grade.Value.ToString();
                    //firstSheet.Cells[i + 2, 19].Value = item.BluePoint == null ? "0" : item.BluePoint.Value.ToString();
                    firstSheet.Cells[i + 2, 20].Value = item.DateOfBirth == null ? "" : item.DateOfBirth.Value.ToString("dd/MM/yyyy");
                    //firstSheet.Cells[i + 2, 21].Value = item.PlaceOfBirth;
                    //firstSheet.Cells[i + 2, 22].Value = item.IDCardNo;
                    //firstSheet.Cells[i + 2, 23].Value = item.IDCardDate == null ? "" : item.IDCardDate.Value.ToString("dd/MM/yyyy");
                    //firstSheet.Cells[i + 2, 24].Value = item.IDCardPlace;
                    firstSheet.Cells[i + 2, 25].Value = item.Address;
                    firstSheet.Cells[i + 2, 26].Value = item.Mobile;
                    //firstSheet.Cells[i + 2, 27].Value = item.Region != null ? item.Region.Name : "";
                    //firstSheet.Cells[i + 2, 28].Value = item.TeamLeader;
                    firstSheet.Cells[i + 2, 29].Value = item.LoginFailedCount;

                }

                #endregion

                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=ExportUsers.xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
            }


            return Content("ok");
        }

        #region EditUser
        public ActionResult EditUser(int userId)
        {
            vwUsers user = _userRepository.GetvwUserById(userId);
            if (user != null)
            {
                //if (user.UserName.ToUpper() == "Admin".ToUpper() || user.UserName.ToUpper() == "Host".ToUpper())
                //{
                //    return RedirectToAction("Index");
                //}

                var model = new EditUserViewModel();
                AutoMapper.Mapper.Map(user, model);
                model.ListUserType = _userTypeRepository.GetUserTypes();
                var departmentList = branchRepository.GetAllBranch().Where(x => x.ParentId != null || x.ParentId > 0)
                     .Select(item => new BranchViewModel
                     {
                         Name = item.Name,
                         Id = item.Id,
                         ParentId = item.ParentId
                     }).ToList();
                ViewBag.departmentList = departmentList;
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var DrugStore = Request["DrugStore"];
                    User user = _userRepository.GetUserById(model.Id);
                    var user_parent_old = user.ParentId;
                    if (user != null)
                    {
                        if (user.UserName.ToUpper() == "Admin".ToUpper() || user.UserName.ToUpper() == "Host".ToUpper())
                        {
                            return RedirectToAction("Index");
                        }

                        AutoMapper.Mapper.Map(model, user);
                        user.ModifiedDate = DateTime.Now;
                        user.FullName = user.FirstName + " " + user.LastName;
                        user.DrugStore = DrugStore;
                        _userRepository.UpdateUser(user);
                        var user_type = user_typeRepository.GetUserTypeById(user.UserTypeId.Value);
                        var List_staff = staffRepository.GetAllStaffs().Where(x=>x.UserId!=null);
                        if (user_type.Code == "ASM" || user_type.Code == "TDV")
                        {
                            var staff = List_staff.Where(x=>x.UserId==user.Id).ToList().FirstOrDefault();
                            if (staff != null)
                            {
                                staff.CreatedDate = DateTime.Now;
                                staff.ModifiedDate = DateTime.Now;
                                staff.Name = user.FirstName + " " + user.LastName;
                                staff.DrugStore = DrugStore;
                                staff.Address = user.Address;
                                staff.Birthday = user.DateOfBirth;
                                staff.Email = user.Email;
                                staff.FirstName = user.FirstName;
                                staff.LastName = user.LastName;
                                staff.Phone = user.Mobile;
                                staff.Gender = user.Sex;
                                staff.UserId = user.Id;
                                if (user.ParentId != null && user.ParentId.Value > 0)
                                {
                                    //var parentId = staffRepository.GetvwStaffsById(user.ParentId.Value);
                                    staff.StaffParentId = user.ParentId;
                                }
                                var position = positionRepository.GetPositionByCode(user_type.Code);
                                staff.PositionId = position.Id;
                                staffRepository.UpdateStaffs(staff);
                            }
                            
                        }
                        if (user_type.Code == "ADS" || user_type.Code == "SDS")
                        {
                            var cus = customerRepository.GetCustomerById(user.Id);
                            if (cus != null)
                              {
                                  cus.CreatedDate = DateTime.Now;
                                  cus.ModifiedDate = DateTime.Now;
                                  cus.CompanyName = user.FirstName + " " + user.LastName;
                                  cus.BranchId = Convert.ToInt32(DrugStore);
                                  cus.Address = user.Address;
                                  cus.Birthday = user.DateOfBirth;
                                  cus.Email = user.Email;
                                  cus.FirstName = user.FirstName;
                                  cus.LastName = user.LastName;
                                  cus.Phone = user.Mobile;
                                  cus.Gender = user.Sex;
                                  cus.PositionCode = user_type.Code;
                                  cus.UserId = user.Id;
                                  //lấy mã.
                                  customerRepository.UpdateCustomer(cus);
                              }
                        }

                        if (user_parent_old != null && user_parent_old.Value > 0)
                        {
                            var List_user_parent = List_staff.Where(x => x.StaffParentId == user_parent_old.Value).ToList();
                            var user_parent = List_staff.Where(x => x.Id == user_parent_old.Value).ToList().FirstOrDefault();
                            for (int i = 0; i < List_user_parent.Count(); i++)
                            {
                                if (i == 0)
                                {
                                    user_parent.DrugStore = List_user_parent[i].DrugStore;
                                }
                                else
                                {
                                    user_parent.DrugStore += "," + List_user_parent[i].DrugStore;
                                }
                            }
                            staffRepository.UpdateStaffs(user_parent);
                            if (user_parent.UserId != null)
                            {
                                var parent = _userRepository.GetUserById(user_parent.UserId.Value);
                                if (parent != null)
                                {
                                    parent.DrugStore = user_parent.DrugStore;
                                    _userRepository.UpdateUser(parent);
                                }
                            }
                        }
                        if (model.ParentId != null && model.ParentId.Value > 0)
                        {
                            var List_user_parent = List_staff.Where(x => x.StaffParentId == model.ParentId.Value).ToList();
                            var user_parent = List_staff.Where(x => x.Id == model.ParentId.Value).ToList().FirstOrDefault();
                            for (int i = 0; i < List_user_parent.Count(); i++)
                            {
                                if (i == 0)
                                {
                                    user_parent.DrugStore = List_user_parent[i].DrugStore;
                                }
                                else
                                {
                                    user_parent.DrugStore += "," + List_user_parent[i].DrugStore;
                                }
                            }
                            staffRepository.UpdateStaffs(user_parent);
                            if (user_parent.UserId != null)
                            {
                                var parent = _userRepository.GetUserById(user_parent.UserId.Value);
                                if (parent != null)
                                {
                                    parent.DrugStore = user_parent.DrugStore;
                                    _userRepository.UpdateUser(parent);
                                }
                            }
                        }
                     
                        TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.UpdateSuccess;
                        if (Erp.BackOffice.Filters.SecurityFilter.AccessRight("Index", "User", "Administration"))
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return RedirectToAction("DashboardSale", "Home", new { Area=""});
                        }
                    }
                }

                return RedirectToAction("Edit", new { Id = model.Id });
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        #endregion

        //[HttpPost]
        //public ActionResult EditUserInfo(EditUserViewmodel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Request["Submit"] == "Save")
        //        {
        //            if (!string.IsNullOrEmpty(model._UserInfoViewModel.AvatarString64))
        //            {
        //                model._UserInfoViewModel.Avatar = FileHelper.WriteFileFromBase64String(model._UserInfoViewModel.AvatarString64, Globals.UploadedFilePath);
        //                model._UserInfoViewModel.AvatarString64 = string.Empty;
        //            }
        //            if (!string.IsNullOrEmpty(model._UserInfoViewModel.FrontIDCardImageString64))
        //            {
        //                model._UserInfoViewModel.FrontIDCardImage = FileHelper.WriteFileFromBase64String(model._UserInfoViewModel.FrontIDCardImageString64, Globals.UploadedFilePath);
        //                model._UserInfoViewModel.FrontIDCardImageString64 = string.Empty;
        //            }
        //            if (!string.IsNullOrEmpty(model._UserInfoViewModel.BackIDCardImageString64))
        //            {
        //                model._UserInfoViewModel.BackIDCardImage = FileHelper.WriteFileFromBase64String(model._UserInfoViewModel.BackIDCardImageString64, Globals.UploadedFilePath);
        //                model._UserInfoViewModel.BackIDCardImageString64 = string.Empty;
        //            }
        //            //Update User
        //            User existingUser = _userRepository.GetUserById(model._UserInfoViewModel.Id);
        //            AutoMapper.Mapper.Map(model._UserInfoViewModel, existingUser);
        //            existingUser.FullName = existingUser.LastName + " " + existingUser.FirstName;
        //            _userRepository.UpdateUser(existingUser);
        //            //Update position and level
        //            //UserPositionLevel userPositionLevel = _userRepository.GetUPLevelByUId(existingUser.Id);
        //            //userPositionLevel.LevelId = model._UserInfoViewModel.LevelId;
        //            //userPositionLevel.PositionId = model._UserInfoViewModel.PositionId;
        //            //_userRepository.UpdateUPLevel(userPositionLevel);

        //            TempData["AlertMessage"] = " Chỉnh sửa User thành công.";

        //            //var upl = _userRepository.GetUPLevelByUId(existingUser.Id);
        //            //if (upl != null && upl.Position != null)
        //            //{
        //            //    string userCode = upl.Position.Code;
        //            //    if (!string.IsNullOrEmpty(userCode))
        //            //    {
        //            //        if (userCode.ToUpper() != Globals.PositionCodeFSM.ToUpper())
        //            //        {
        //            //            try
        //            //            {
        //            //                //forum
        //            //                //SavinaForumRegisterService.RegisterServiceSoapClient client = new SavinaForumRegisterService.RegisterServiceSoapClient();
        //            //                //client.EditUser(existingUser.UserName, existingUser.Email, existingUser.FullName, existingUser.DateOfBirth.GetHashCode() == 0 ? DateTime.Now : existingUser.DateOfBirth.Value, existingUser.Sex.Value ? 1 : 2, existingUser.Status == UserStatus.Deleted ? false : true);
        //            //            }
        //            //            catch { TempData["AlertMessage"] = " Chỉnh sửa User không thành công."; }
        //            //        }
        //            //    }

        //            //}
        //            return RedirectToAction("ListUsers");
        //        }
        //        if (Request["Delete"] == "Delete")
        //        {
        //            return RedirectToAction("DeleteUser", new { DeleteUserId = model._UserInfoViewModel.Id });
        //        }
        //        return RedirectToAction("EditUser", new { userId = model._UserInfoViewModel.Id });
        //    }
        //    if (Request.UrlReferrer != null)
        //        return Redirect(Request.UrlReferrer.AbsoluteUri);
        //    return RedirectToAction("ListUsers");
        //}

        //[HttpPost]
        //public ActionResult EditUserUser(EditUserViewmodel model)
        //{

        //    bool? isExist;
        //    var userCurrent = _userRepository.GetUserById(model._UserUserViewModel.UserId);
        //    if (model._UserUserViewModel.UserName == userCurrent.UserName)
        //    {
        //        isExist = false;
        //    }
        //    else
        //    {
        //        isExist = CheckUsernameExists(model._UserUserViewModel.UserName);
        //    }
        //    if (isExist == true)
        //    {
        //        ModelState.AddModelError("UserName", "UserName đã tồn tại");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        if (!string.IsNullOrEmpty(model._UserUserViewModel.UserName) && isExist != true)
        //        {
        //            userCurrent.UserName = model._UserUserViewModel.UserName;
        //            _userRepository.UpdateUser(userCurrent);
        //            TempData["AlertMessage"] = "Thay đổi  thành công.";
        //        };
        //        if (!string.IsNullOrEmpty(model._UserUserViewModel.NewPassword))
        //        {
        //            string resetToken = WebSecurity.GeneratePasswordResetToken(model._UserUserViewModel.UserName);
        //            bool boll = WebSecurity.ResetPassword(resetToken, model._UserUserViewModel.NewPassword);

        //            if (boll)
        //                TempData["AlertMessage"] = "Thay đổi  thành công.";
        //            else
        //                TempData["AlertMessage"] = "Thay đổi  không thành công.";

        //            var user = _userRepository.GetByUserName(User.Identity.Name);
        //            if (user != null)
        //            {
        //                //var upl = _userRepository.GetUPLevelByUId(user.Id);
        //                //if (upl != null && upl.Position != null)
        //                //{
        //                //    string userCode = upl.Position.Code;
        //                //    if (!string.IsNullOrEmpty(userCode))
        //                //    {
        //                //        if (userCode.ToUpper() != Globals.PositionCodeFSM.ToUpper())
        //                //        {
        //                //            try
        //                //            {
        //                //                //forum
        //                //                //SavinaForumRegisterService.RegisterServiceSoapClient client = new SavinaForumRegisterService.RegisterServiceSoapClient();
        //                //                //client.ResetPassword(model._UserUserViewModel.UserName, model._UserUserViewModel.NewPassword);
        //                //            }
        //                //            catch { TempData["AlertMessage"] = " Thay đổi password không thành công."; }
        //                //        }
        //                //    }
        //                //}
        //            }
        //        }
        //    }
        //    else
        //    {

        //        TempData["AlertMessage"] = "<script>alert('UserName Tồn tại');</script>";
        //        return RedirectToAction("EditUser", new { userId = model._UserUserViewModel.UserId });
        //    }
        //    return RedirectToAction("ListUsers");
        //}

        public ActionResult ResetLoginFailed(int resetUserId)
        {
            if (ModelState.IsValid)
            {
                _userRepository.resetLoginFailed(resetUserId);
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteUser(int deleteUserId)
        {
            if (ModelState.IsValid)
            {
                _userRepository.DeleteUser(deleteUserId);
                TempData["AlertMessage"] = "Xóa User thành công.";

                //var upl = _userRepository.GetUPLevelByUId(deleteUserId);
                //if (upl != null && upl.Position != null)
                //{
                //    string userCode = upl.Position.Code;
                //    if (!string.IsNullOrEmpty(userCode))
                //    {
                //        if (userCode.ToUpper() != Globals.PositionCodeFSM.ToUpper())
                //        {
                //            //forum
                //            User user = _userRepository.GetUserById(deleteUserId);
                //            if (user != null)
                //            {
                //                try
                //                {
                //                    //SavinaForumRegisterService.RegisterServiceSoapClient client = new SavinaForumRegisterService.RegisterServiceSoapClient();
                //                    //client.IsApproved(user.UserName);
                //                }
                //                catch { TempData["AlertMessage"] = " Xóa User không thành công."; }
                //            }
                //        }
                //    }
                //}
                return RedirectToAction("Index");
            }
            return RedirectToAction("EditUsers", new { userId = deleteUserId });
        }

        #region Create
        public ViewResult CreateUser()
        {
            //Position fsmPosition = _positionRepository.GetPositionByCode(Globals.PositionCodeFSM);
            //ViewBag.FSM_Id = fsmPosition != null ? fsmPosition.Id : 0;
            
            var model = new CreateUserViewModel
                            {
                                listUserType = _userTypeRepository.GetUserTypes(),
                                //listPosition = _positionRepository.GetPositions(),
                                //listLevel = _levelRepository.GetAllLevels(),
                                //ListCategory = _categoryRepository.GetAllCategories(),
                                Status = UserStatus.Active
                            };
            return View(model);
        }

        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!CheckUsernameExists(model.UserName.ToLower()))
                {
                    //bool registryUserSucceed = false;
                    try
                    {
                        WebSecurity.CreateUserAndAccount(model.UserName.ToLower(), model.Password);
                        {
                            //model.Avatar = FileHelper.WriteFileFromBase64String(model.AvatarString64, Globals.UploadedFilePath);
                            //model.AvatarString64 = string.Empty;
                        }
                        var DrugStore = Request["DrugStore"];
                        //==== Update User ===//
                        int userId = WebSecurity.GetUserId(model.UserName.ToLower());
                        User user = _userRepository.GetUserById(userId);
                        AutoMapper.Mapper.Map(model, user);
                        user.FullName = user.FirstName + " " + user.LastName;
                        user.DrugStore = DrugStore;
                        user.Status = UserStatus.Active;

                        // set UserCode
                        //var category = _categoryRepository.GetCategoryById(model.CategoryId.Value);
                        //var position = _positionRepository.GetPositionById(model.PositionId.Value);
                        //user.UserCode = string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}", category.Name, position.Code, user.Id);

                        _userRepository.UpdateUser(user);
                        var user_type = user_typeRepository.GetUserTypeById(user.UserTypeId.Value);
                        
                        if (user_type.Code == "ASM" || user_type.Code == "TDV")
                        {
                            var Staffs = new Staffs();
                            Staffs.IsDeleted = false;
                            Staffs.CreatedUserId = WebSecurity.CurrentUserId;
                            Staffs.ModifiedUserId = WebSecurity.CurrentUserId;
                            Staffs.CreatedDate = DateTime.Now;
                            Staffs.ModifiedDate = DateTime.Now;
                            Staffs.Name = user.FirstName + " " + user.LastName;
                            Staffs.DrugStore = DrugStore;
                            Staffs.Address = user.Address;
                            Staffs.Birthday = user.DateOfBirth;
                            Staffs.Email = user.Email;
                            Staffs.FirstName = user.FirstName;
                            Staffs.LastName = user.LastName;
                            Staffs.Phone = user.Mobile;
                            Staffs.Gender = user.Sex;
                            Staffs.IsWorking = true;
                            Staffs.UserId = user.Id;
                            if (user.ParentId != null && user.ParentId.Value > 0)
                            {
                                //var parentId = staffRepository.GetvwStaffsById(user.ParentId.Value);
                                Staffs.StaffParentId = user.ParentId;
                            }
                            var position = positionRepository.GetPositionByCode(user_type.Code);
                            Staffs.PositionId = position.Id;
                            //lấy mã.
                            staffRepository.InsertStaffs(Staffs);
                            var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_Staff");
                            Staffs.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix1, Staffs.Id);
                            staffRepository.UpdateStaffs(Staffs);
                           
                        }
                        if (user_type.Code == "ADS" || user_type.Code == "SDS")
                        {
                            var cus = new Customer();
                            cus.IsDeleted = false;
                            cus.CreatedUserId = WebSecurity.CurrentUserId;
                            cus.ModifiedUserId = WebSecurity.CurrentUserId;
                            cus.CreatedDate = DateTime.Now;
                            cus.ModifiedDate = DateTime.Now;
                            cus.CompanyName = user.FirstName + " " + user.LastName;
                            cus.BranchId = Convert.ToInt32(DrugStore);
                            cus.Address = user.Address;
                            cus.Birthday = user.DateOfBirth;
                            cus.Email = user.Email;
                            cus.FirstName = user.FirstName;
                            cus.LastName = user.LastName;
                            cus.Phone = user.Mobile;
                            cus.Gender = user.Sex;
                            cus.PositionCode = user_type.Code;
                            cus.UserId = user.Id;
                            //lấy mã.
                            customerRepository.InsertCustomer(cus);
                            var prefix1 = Erp.BackOffice.Helpers.Common.GetSetting("prefixOrderNo_CustomerNT");
                            cus.Code = Erp.BackOffice.Helpers.Common.GetCode(prefix1, cus.Id);
                            customerRepository.UpdateCustomer(cus);
                        }
                        var List_staff = staffRepository.GetAllStaffs();
                        if (model.ParentId != null && model.ParentId.Value > 0)
                        {
                            var List_user_parent = List_staff.Where(x => x.StaffParentId == model.ParentId.Value).ToList();
                            var user_parent = List_staff.Where(x => x.Id == model.ParentId.Value).ToList().FirstOrDefault();
                            for (int i = 0; i < List_user_parent.Count(); i++)
                            {
                                if (i == 0)
                                {
                                    user_parent.DrugStore = List_user_parent[i].DrugStore;
                                }
                                else
                                {
                                    user_parent.DrugStore += "," + List_user_parent[i].DrugStore;
                                }
                            }
                            staffRepository.UpdateStaffs(user_parent);
                            if (user_parent.UserId != null)
                            {
                                var parent = _userRepository.GetUserById(user_parent.UserId.Value);
                                if (parent != null)
                                {
                                    parent.DrugStore = user_parent.DrugStore;
                                    _userRepository.UpdateUser(parent);
                                }
                            }
                        }
                        TempData["AlertMessage"] = " Đã tạo User thành công.";
                       
                        return RedirectToAction("Index");
                    }
                    catch (MembershipCreateUserException e)
                    {
                        //Position fsmPosition = _positionRepository.GetPositionByCode(Globals.PositionCodeFSM);
                        //ViewBag.FSM_Id = fsmPosition != null ? fsmPosition.Id : 0;

                        ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                        model.listUserType = _userTypeRepository.GetUserTypes();
                        //model.listPosition = _positionRepository.GetPositions();
                        //model.listLevel = _levelRepository.GetAllLevels();
                        //model.ListCategory = _categoryRepository.GetAllCategories();
                        model.Status = UserStatus.Active;
                        return View(model);
                    }

                }
                else
                {
                    //Position fsmPosition = _positionRepository.GetPositionByCode(Globals.PositionCodeFSM);
                    //ViewBag.FSM_Id = fsmPosition != null ? fsmPosition.Id : 0;

                    ModelState.AddModelError("", "Email đã được đăng ký");
                    model.listUserType = _userTypeRepository.GetUserTypes();
                    //model.listPosition = _positionRepository.GetPositions();
                    //model.ListCategory = _categoryRepository.GetAllCategories();
                    //model.listLevel = _levelRepository.GetAllLevels();
                    model.Status = UserStatus.Active;
                    return View(model);
                }

            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> Users = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount user in Users)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(user.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = user.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = user.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

        public ActionResult getImagefromWebservice(string id)
        {
            // vn.qtsoftware.ssclub.ssclubservice service = new vn.qtsoftware.ssclub.ssclubservice();
            string urlId = "";//service.getImagePath("ID", id);
            string urlRe = "";//service.getImagePath("RE", id);
            string urlAg = "";//service.getImagePath("AG", id);

            if (urlId != "#")
            {
                urlId = urlId.Substring(1, urlId.Length - 1);
                urlId = ConfigurationManager.AppSettings["SSClub"] + urlId;
            }
            if (urlRe != "#")
            {
                urlRe = urlRe.Substring(1, urlRe.Length - 1);
                urlRe = ConfigurationManager.AppSettings["SSClub"] + urlRe;
            }
            if (urlId != "#")
            {
                urlAg = urlAg.Substring(1, urlAg.Length - 1);
                urlAg = ConfigurationManager.AppSettings["SSClub"] + urlAg;
            }
            return Json(new { urlImageId = urlId, urlImageRe = urlRe, urlImageAg = urlAg });
        }

        private bool CheckEmailExists(string email)
        {
            var user = _userRepository.GetByUserEmail(email);

            return user != null;
        }

        private bool CheckUsernameExists(string username)
        {
            var user = _userRepository.GetByUserName(username);

            return user != null;
        }

        public DataTable GetDataFromFileExcel(string FilePath)
        {
            bool IsEx2003 = false;
            DataTable dt = new DataTable();
            try
            {
                IExcelDataReader excelReader;
                FileStream stream = System.IO.File.Open(FilePath, FileMode.Open, FileAccess.Read);

                if (Path.GetExtension(FilePath).Equals(".xlsx"))
                {
                    //1. Reading from a binary Excel file ('2007 format; *.xls)
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                {
                    //2003
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    IsEx2003 = true;
                }
                //...
                //3. DataSet - The result of each spreadsheet will be created in the result.Tables
                DataSet result = excelReader.AsDataSet();
                //...
                //4. DataSet - Create column names from first row
                excelReader.IsFirstRowAsColumnNames = true;
                DataSet ds = excelReader.AsDataSet();

                excelReader.Close();

                excelReader.Dispose();

                dt = ds.Tables[0];

                //Remove row[0], Because this is header
                if (dt != null && dt.Rows.Count > 0 && IsEx2003 == true)
                    dt.Rows.RemoveAt(0);
            }
            catch (Exception ex)
            {
                return null;
            }
            return dt;
        }

        public ViewResult ImportOtherUser()
        {
            var model = new ImportOtherUserViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ImportOtherUser(ImportOtherUserViewModel model)
        {
            if (Request["Show"] == "Show" && model.SourceFile != null)
            {
                model.ListUserImport = GetListOtherUserImport(model.SourceFile);
                return View(model);
            }
            if (Request["Submit"] == "Save")
            {
                if (Erp.BackOffice.Helpers.Common.ListOtherUser != null)
                {
                    int numberUserCreated = 0;
                    foreach (var item in Erp.BackOffice.Helpers.Common.ListOtherUser)
                    {
                        if (item.CheckStatus == AddOtherUserImportViewModel.Status.Successfull)
                        {
                            bool registryUserSucceed = false;
                            try
                            {
                                // ========= Created User =======//
                                WebSecurity.CreateUserAndAccount(item.UserName, item.Password);

                                //==== Update User ===//
                                int userId = WebSecurity.GetUserId(item.UserName);
                                User user = _userRepository.GetUserById(userId);
                                AutoMapper.Mapper.Map(item, user);
                                user.FullName = item.FullName;
                                user.Status = UserStatus.Active;

                                // set UserCode
                                var category = _categoryRepository.GetCategoryById(item.CategoryId.Value);
                                //var position = _positionRepository.GetPositionById(item.PositionId.Value);
                                //user.UserCode = string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}", category.Name, position.Code, user.Id);
                                _userRepository.UpdateUser(user);
                                numberUserCreated++;

                                ////==== Create UserPositionLevel ===//
                                //var upLevel = new UserPositionLevel
                                //                  {
                                //                      UserId = userId,
                                //                      PositionId = item.PositionId,
                                //                      LevelId = item.LevelId,
                                //                      FromDate = DateTime.Now,
                                //                      ToDate = null,
                                //                      IsActive = true,
                                //                      ParentId = null
                                //                  };
                                //_userRepository.InsertUPLevel(upLevel);
                                //string userCode = position.Code;
                                //if (string.IsNullOrEmpty(userCode) || (!string.IsNullOrEmpty(userCode) && userCode.ToUpper() != Globals.PositionCodeFSM.ToUpper()))
                                //{
                                //    try
                                //    {
                                //        //=== forum =//
                                //        //SavinaForumRegisterService.RegisterServiceSoapClient client = new SavinaForumRegisterService.RegisterServiceSoapClient();
                                //        //registryUserSucceed = client.CreateUser(item.UserName, item.Password, item.Email, item.FullName, item.DateOfBirth.HasValue ? item.DateOfBirth.Value : DateTime.Now, item.Sex.Value ? 1 : 2);
                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        Debug.WriteLine(ex.Message);
                                //    }
                                //}
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }

                    if (numberUserCreated > 0)
                    {
                        TempData["AlertMessage"] = App_GlobalResources.Wording.InsertSuccess;
                        return RedirectToAction("ListUsers");
                    }
                    return View("ImportOtherUser", new ImportOtherUserViewModel());
                }

                return View("ImportOtherUser", new ImportOtherUserViewModel());

            }
            return View(model);
        }

        public IEnumerable<AddOtherUserImportViewModel> GetListOtherUserImport(HttpPostedFileBase SourceFile)
        {
            var listUserImport = new List<AddOtherUserImportViewModel>();
            var oconn = new OleDbConnection();
            string fileName = "";
            try
            {
                fileName = Server.MapPath(FileHelper.SaveFile(SourceFile, Globals.UploadedFilePath));
                string connectionString = string.Format(CultureInfo.InvariantCulture, "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"{0}\"; Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\";", fileName);

                oconn.ConnectionString = connectionString;
                oconn.Open();

                string sql = String.Format(CultureInfo.InvariantCulture, @"SELECT * FROM [{0}];", oconn.GetSchema("Tables").Rows[0]["TABLE_NAME"]);
                var ocmd = new OleDbCommand(sql, oconn);
                var da = new OleDbDataAdapter(ocmd);
                var dt = new DataTable { Locale = CultureInfo.InvariantCulture };
                da.Fill(dt);
                oconn.Close();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(dr[0].ToString())
                            && string.IsNullOrEmpty(dr[1].ToString())
                            && string.IsNullOrEmpty(dr[2].ToString())
                            && string.IsNullOrEmpty(dr[3].ToString())
                            && string.IsNullOrEmpty(dr[4].ToString())
                            && string.IsNullOrEmpty(dr[5].ToString())
                            && string.IsNullOrEmpty(dr[6].ToString())
                            && string.IsNullOrEmpty(dr[7].ToString())
                            && string.IsNullOrEmpty(dr[8].ToString())
                            && string.IsNullOrEmpty(dr[9].ToString())
                            && string.IsNullOrEmpty(dr[10].ToString())
                            && string.IsNullOrEmpty(dr[11].ToString())
                            && string.IsNullOrEmpty(dr[12].ToString())
                            && string.IsNullOrEmpty(dr[13].ToString())
                            && string.IsNullOrEmpty(dr[14].ToString())
                            && string.IsNullOrEmpty(dr[15].ToString())
                            && string.IsNullOrEmpty(dr[16].ToString())
                            && string.IsNullOrEmpty(dr[17].ToString())
                            && string.IsNullOrEmpty(dr[18].ToString())
                            && string.IsNullOrEmpty(dr[19].ToString())
                            && string.IsNullOrEmpty(dr[20].ToString())
                            && string.IsNullOrEmpty(dr[21].ToString())
                            && string.IsNullOrEmpty(dr[22].ToString())
                            && string.IsNullOrEmpty(dr[23].ToString())
                            && string.IsNullOrEmpty(dr[24].ToString())
                            )
                        {
                            continue;
                        }

                        var userImport = new AddOtherUserImportViewModel();

                        string fullName = dr[1].ToString().Trim();
                        string username = dr[2].ToString().Trim();
                        string passWord = dr[3].ToString().Trim();
                        string userTypeName = dr[4].ToString().Trim();
                        string categoryName = dr[5].ToString().Trim();
                        string sex = dr[6].ToString().Trim();
                        string userCode = dr[7].ToString().Trim();
                        string email = dr[8].ToString().Trim();
                        string positionName = dr[9].ToString().Trim();
                        string Level = dr[10].ToString().Trim();
                        string shopName = dr[11].ToString().Trim();
                        string birthday = dr[12].ToString().Trim();
                        string placeBirth = dr[13].ToString().Trim();
                        string cmnd = dr[14].ToString().Trim();
                        string dateCmnd = dr[15].ToString().Trim();
                        string placeCmnd = dr[16].ToString().Trim();
                        string adress = dr[17].ToString().Trim();
                        string phone = dr[18].ToString().Trim();
                        string place = dr[19].ToString().Trim();
                        string teamLeader = dr[20].ToString().Trim();

                        string BankName = dr[21].ToString().Trim();
                        string BankOwnerName = dr[22].ToString().Trim();
                        string BankBranch = dr[23].ToString().Trim();
                        string BankUserNumber = dr[24].ToString().Trim();



                        bool error = false;

                        try
                        {
                            DateTime? birthdayDateTime = null;
                            if (!string.IsNullOrEmpty(birthday))
                            {
                                try
                                {
                                    birthdayDateTime = DateTime.Parse(birthday, CultureInfo.InvariantCulture);
                                }
                                catch (Exception ex) { Debug.WriteLine(ex.Message); error = true; }
                            }

                            // check.....
                            if (error) goto Error;

                            DateTime? dateCmndDateTime = null;
                            if (!string.IsNullOrEmpty(dateCmnd))
                            {
                                try
                                {
                                    dateCmndDateTime = DateTime.Parse(dateCmnd, CultureInfo.InvariantCulture);
                                }
                                catch (Exception ex) { Debug.WriteLine(ex.Message); error = true; }
                            }

                            // check.....
                            if (error) goto Error;

                            // Gender
                            bool sexBoll = sex.ToUpper().Trim() == "nam".ToUpper();

                            // IsTeamleader
                            bool teamLeaderBool = !string.IsNullOrEmpty(teamLeader);

                            // UserType
                            var userType = _userTypeRepository.GetUserTypes().FirstOrDefault(u => u.Name.Trim().ToUpper() == userTypeName.Trim().ToUpper());
                            error = userType == null;
                            int? userTypeId = userType != null ? (int?)userType.Id : null;

                            // check.....
                            if (error) goto Error;

                            // Category
                            var category = _categoryRepository.GetAllCategories().FirstOrDefault(u => u.Name.Trim().ToUpper() == categoryName.Trim().ToUpper());
                            error = category == null;
                            int? categoryId = category != null ? (int?)category.Id : null;

                            // check.....
                            if (error) goto Error;

                            //// Level
                            //var level = _levelRepository.GetAllLevels().FirstOrDefault(u => u.Name.Trim().ToUpper() == Level.Trim().ToUpper());
                            //error = level == null;
                            //int? levelId = level != null ? (int?)level.Id : null;

                            // check.....
                            if (error) goto Error;

                            // Position
                            //var position = _positionRepository.GetPositions().FirstOrDefault(u => u.Name.Trim().ToUpper() == positionName.Trim().ToUpper());
                            //error = position == null;
                            //int? positionId = position != null ? (int?)position.Id : null;

                            // check.....
                            if (error) goto Error;

                            if (CheckEmailExists(email) || CheckUsernameExists(username))
                                userImport.CheckStatus = AddOtherUserImportViewModel.Status.Failed;
                            else
                                userImport.CheckStatus = AddOtherUserImportViewModel.Status.Successfull;

                            if (!string.IsNullOrEmpty(adress))
                                userImport.Address = adress;

                            userImport.DateOfBirth = birthdayDateTime;
                            userImport.Email = email;
                            userImport.FirstName = fullName.Split(' ').First().ToString(CultureInfo.InvariantCulture);
                            userImport.IDCardDate = dateCmndDateTime;
                            userImport.IDCardNo = cmnd;
                            userImport.IDCardPlace = placeCmnd;
                            userImport.LastName = fullName.Split(' ').Last().ToString(CultureInfo.InvariantCulture);
                            userImport.FullName = fullName;
                            userImport.Mobile = phone;
                            userImport.Password = passWord;
                            userImport.PlaceOfBirth = placeBirth;

                            if (!string.IsNullOrEmpty(sex))
                                userImport.Sex = sexBoll;

                            userImport.GroupName = shopName;

                            userImport.TeamLeader = teamLeaderBool;
                            userImport.UserName = username;
                            userImport.Level = Level;
                            //userImport.LevelId = levelId;
                            userImport.CategoryName = categoryName;
                            userImport.CategoryId = categoryId;
                            userImport.UserTypeName = userTypeName;
                            userImport.UserTypeId = userTypeId;
                            userImport.PositionName = positionName;
                            //userImport.PositionId = positionId;
                            userImport.BankUserNumber = BankUserNumber;
                            userImport.BankBranch = BankBranch;
                            userImport.BankName = BankName;
                            userImport.BankOwnerName = BankOwnerName;
                        }
                        catch (DbUpdateException ex)
                        {
                            Debug.WriteLine(ex.Message);
                            error = true;
                        }

                    Error:
                        if (error)
                        {
                            userImport.CheckStatus = AddOtherUserImportViewModel.Status.Failed;

                            userImport.Address = adress;
                            userImport.Email = email;
                            userImport.FirstName = fullName.Split(' ').First().ToString(CultureInfo.InvariantCulture);
                            userImport.IDCardNo = cmnd;
                            userImport.IDCardPlace = placeCmnd;
                            userImport.LastName = fullName.Split(' ').Last().ToString(CultureInfo.InvariantCulture);
                            userImport.FullName = fullName;
                            userImport.Mobile = phone;
                            userImport.Password = passWord;
                            userImport.PlaceOfBirth = placeBirth;
                            userImport.GroupName = shopName;
                            userImport.UserName = username;
                            userImport.Level = Level;
                            userImport.CategoryName = categoryName;
                            userImport.UserTypeName = userTypeName;
                            userImport.PositionName = positionName;
                            userImport.BankUserNumber = BankUserNumber;
                            userImport.BankBranch = BankBranch;
                            userImport.BankName = BankName;
                            userImport.BankOwnerName = BankOwnerName;
                        }
                        listUserImport.Add(userImport);
                    }
                }
                listUserImport = listUserImport.OrderBy(u => u.CheckStatus).ToList();
                Erp.BackOffice.Helpers.Common.ListOtherUser = listUserImport;
                return listUserImport;
            }
            catch
            {
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }
                return null;
            }
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            try
            {
                var idDeleteAll = Request["DeleteAll-checkbox"];
                var arrDeleteId = idDeleteAll.Split(',');
                for (int i = 0; i < arrDeleteId.Count(); i++)
                {
                    _userRepository.DeleteUser(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                }
                TempData[Globals.SuccessMessageKey] = Wording.DeleteSuccess;
                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                TempData[Globals.FailedMessageKey] = Error.RelationError;
                return RedirectToAction("Index");
            }
        }

        #region - ChangePassword -
        public ActionResult ChangePassword()
        {
            var UserId = WebSecurity.CurrentUserId;
            //var user = _userRepository.GetUserById(UserId);
            UserUserViewModel model = new UserUserViewModel();
            model.UserId = UserId;
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(UserUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var UserId = WebSecurity.CurrentUserId;
                var user = _userRepository.GetUserById(UserId);
                user.LastChangedPassword = DateTime.Now;
                var flag = WebSecurity.ChangePassword(WebSecurity.CurrentUserName, model.OldPassword, model.NewPassword);
                if (flag == false)
                {
                    ModelState.AddModelError("OldPassword", "Mật khẩu cũ không chính xác.");
                    return View(model);
                }
                return RedirectToAction("LogOff");
            }
            return View(model);
        }
        #endregion

        #region UserSetting
        public ActionResult UserSetting()
        {
            var UserId = WebSecurity.CurrentUserId;

            var listUserSetting = _userSettingRepository.GetAllUserSettings().Where(item => item.UserId == UserId).ToList();
            var listSettingKey = _settingRepository.GetAlls().Where(item => item.Code == "UserSetting").OrderBy(item => item.Value).ToList();
            var model = new List<UserSettingViewModel>();

            foreach (var item in listSettingKey)
            {
                var modelItem = new UserSettingViewModel();
                modelItem.SettingKey = item.Key;
                modelItem.SettingName = item.Value;
                modelItem.SettingNote = item.Note;
                modelItem.SettingId = item.Id;
                modelItem.UserId = UserId;

                var ItemIsAdded = listUserSetting.Where(i => i.SettingId == item.Id).FirstOrDefault();
                if (ItemIsAdded != null)
                {
                    modelItem.Id = ItemIsAdded.Id;
                    modelItem.Value = ItemIsAdded.Value;
                }

                model.Add(modelItem);
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult UserSetting(int settingId, string value)
        {
            var UserId = WebSecurity.CurrentUserId;

            var userSetting = _userSettingRepository.GetAllUserSettings()
                .Where(item => item.SettingId == settingId && item.UserId == UserId).FirstOrDefault();

            if (userSetting != null)
            {
                userSetting.Value = value;

                _userSettingRepository.UpdateUserSetting(userSetting);
            }
            else
            {
                userSetting = new UserSetting();
                userSetting.SettingId = settingId;
                userSetting.UserId = UserId;
                userSetting.Value = value;

                _userSettingRepository.InsertUserSetting(userSetting);
            }

            return Json(new { message = App_GlobalResources.Wording.UpdateSuccess }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
