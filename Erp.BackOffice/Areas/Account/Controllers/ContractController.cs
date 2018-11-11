using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Filters;
using Erp.BackOffice.Staff.Controllers;
using Erp.Domain.Account.Entities;
using Erp.Domain.Account.Interfaces;
using Erp.Domain.Interfaces;
using Erp.Domain.Staff.Interfaces;
using Erp.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Erp.BackOffice.Account.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class ContractController : Controller
    {
        private readonly IContractRepository ContractRepository;
        private readonly IUserRepository userRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IInfoPartyARepository infoPartyARepository;
        private readonly IContractLeaseRepository contractLeaseRepository;
        private readonly IContractSellRepository contractSellRepository;
        private readonly IContactRepository contactRepository;
        private readonly IProcessPaymentRepository processPaymentRepository;
        private readonly IDocumentFieldRepository DocumentFieldRepository;
        private readonly IDocumentAttributeRepository DocumentAttributeRepository;
        public ContractController(
            IContractRepository _Contract
            , IUserRepository _user
            , ICustomerRepository customer
            , IInfoPartyARepository infoParty
            , IContractLeaseRepository contractLease
            , IContractSellRepository contractSell
            , IContactRepository contact
            , IProcessPaymentRepository processPayment
            , IDocumentFieldRepository documentField
            , IDocumentAttributeRepository documentAttribute
            )
        {
            ContractRepository = _Contract;
            userRepository = _user;
            customerRepository = customer;
            infoPartyARepository = infoParty;
            contractLeaseRepository = contractLease;
            contractSellRepository = contractSell;
            contactRepository = contact;
            processPaymentRepository = processPayment;
            DocumentAttributeRepository = documentAttribute;
            DocumentFieldRepository = documentField;
        }

        #region Index

        public ViewResult Index(string Code, string Type, string TransactionCode, string Status)
        {

            IEnumerable<ContractViewModel> q = ContractRepository.GetAllvwContract()
                .Select(item => new ContractViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    CustomerId = item.CustomerId,
                    EffectiveDate = item.EffectiveDate,
                    IdTypeContract = item.IdTypeContract,
                    InfoPartyAId = item.InfoPartyAId,
                    Place = item.Place,
                    ContractQuantity = item.ContractQuantity,
                    TemplateFile = item.TemplateFile,
                    Type = item.Type,
                    TransactionCode = item.TransactionCode,
                    Status = item.Status,
                    CustomerName = item.CustomerName,
                    InfoPartyAName = item.InfoPartyAName,
                    InfoPartyCompanyName = item.InfoPartyCompanyName
                }).OrderByDescending(m => m.ModifiedDate).ToList();
            if (!string.IsNullOrEmpty(Code))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Code).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Code).ToLower()));

            }
            if (!string.IsNullOrEmpty(Type))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.Type).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(Type).ToLower()));

            }
            if (!string.IsNullOrEmpty(Status))
            {
                q = q.Where(item => item.Status == Status);

            }
            if (!string.IsNullOrEmpty(TransactionCode))
            {
                q = q.Where(item => Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(item.TransactionCode).ToLower().Contains(Erp.BackOffice.Helpers.Common.ChuyenThanhKhongDau(TransactionCode).ToLower()));

            }
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region List

        public ViewResult List(int? CondosId)
        {

            IQueryable<LogContractbyCondosViewModel> q = ContractRepository.GetvwLogContractbyCondos(CondosId)
                .Select(item => new LogContractbyCondosViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Code = item.Code,
                    CustomerId = item.CustomerId,
                    EffectiveDate = item.EffectiveDate,
                    IdTypeContract = item.IdTypeContract,
                    InfoPartyAId = item.InfoPartyAId,
                    Place = item.Place,
                    Quantity = item.Quantity,
                    TemplateFile = item.TemplateFile,
                    Type = item.Type,
                    TransactionCode = item.TransactionCode,
                    Status = item.Status,
                    CustomerName = item.CustomerName,
                    InfoPartyAName = item.InfoPartyAName,
                    InfoPartyCompanyName = item.InfoPartyCompanyName,
                    CondosId = item.CondosId,
                    ContractQuantity = item.ContractQuantity,
                    DayHandOver = item.DayHandOver,
                    DayPay = item.DayPay,
                    NameCondos = item.NameCondos,
                    Price = item.Price,
                    Unit = item.Unit,
                    UnitMoney = item.UnitMoney
                }).OrderByDescending(m => m.CreatedDate);
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(q);
        }
        #endregion

        #region Create
        public ActionResult Create(ContractViewModel model)
        {
            if (model == null)
                model = new ContractViewModel();
            model.ContractLeaseModel = new ContractLeaseViewModel();
            model.ContractSellModel = new ContractSellViewModel();
            var customerList = customerRepository.GetAllCustomer().AsEnumerable()
               .Select(item => new SelectListItem
               {
                   Text = item.CompanyName,
                   Value = item.Id.ToString()
               });
            ViewBag.customerList = customerList;

            var infoPartyAList = infoPartyARepository.GetAllvwInfoPartyA().AsEnumerable()
             .Select(item => new SelectListItem
             {
                 Text = item.Name,
                 Value = item.Id.ToString()
             });
            ViewBag.infoPartyAList = infoPartyAList;
            model.ContractSellModel.Quantity = 1;
            model.ContractSellModel.Unit = "căn hộ";
            model.CreatedUserName = Erp.BackOffice.Helpers.Common.CurrentUser.FullName;
            Session["file"] = null;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ContractViewModel model, FormCollection fc, string NameField, int? DocumentTypeId, string IsSearch)
        {
            if (ModelState.IsValid)
            {
                var Type = Request["group_choice"];
                var user = userRepository.GetUserById(WebSecurity.CurrentUserId);
                var Contract = new Domain.Account.Entities.Contract();
                AutoMapper.Mapper.Map(model, Contract);
                Contract.IsDeleted = false;
                Contract.CreatedUserId = WebSecurity.CurrentUserId;
                Contract.ModifiedUserId = WebSecurity.CurrentUserId;
                Contract.AssignedUserId = WebSecurity.CurrentUserId;
                Contract.CreatedDate = DateTime.Now;
                Contract.ModifiedDate = DateTime.Now;
                Contract.Type = Type;
                Contract.Status = App_GlobalResources.Wording.StatusNew;
                //var total = 0;//Gía trị hợp đồng

                //Get CustomerCode
                var customer = customerRepository.GetCustomerById(model.CustomerId.Value);
                //insert documentfield
                DocumentFieldController.SaveUpload(NameField, IsSearch, Contract.Id, "Contract");


                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                if (Request["IsPopup"] == "true")
                {
                    ViewBag.closePopup = "close and append to page parent";
                    model.Id = Contract.Id;

                    return View(model);
                }

                return RedirectToAction("Index");
            }
            return View("Create", model);
        }

        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Contract = ContractRepository.GetContractById(Id.Value);
            if (Contract != null && Contract.IsDeleted != true)
            {
                var model = new ContractViewModel();
                AutoMapper.Mapper.Map(Contract, model);
                if (Contract.Type == "ContractLease")
                {
                    string[] s = model.IdTypeContract.Split('_');
                    string idTypecontract = s[s.Length - 1];
                    var ContractLease = contractLeaseRepository.GetvwContractLeaseById(Convert.ToInt32(idTypecontract));
                    model.ContractLeaseModel = new ContractLeaseViewModel();
                    AutoMapper.Mapper.Map(ContractLease, model.ContractLeaseModel);
                }
                else
                    if (Contract.Type == "ContractSell")
                {
                    string[] s = model.IdTypeContract.Split('_');
                    string idTypecontract = s[s.Length - 1];
                    var ContractSell = contractSellRepository.GetvwContractSellById(Convert.ToInt32(idTypecontract));
                    model.ContractSellModel = new ContractSellViewModel();
                    AutoMapper.Mapper.Map(ContractSell, model.ContractSellModel);
                }
                if (model.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }
                var customerList = customerRepository.GetAllCustomer().AsEnumerable()
              .Select(item => new SelectListItem
              {
                  Text = item.Code + " - " + item.CompanyName,
                  Value = item.Id.ToString()
              });
                ViewBag.customerList = customerList;

                var infoPartyAList = infoPartyARepository.GetAllvwInfoPartyA().AsEnumerable()
                 .Select(item => new SelectListItem
                 {
                     Text = item.Name,
                     Value = item.Id.ToString()
                 });
                ViewBag.infoPartyAList = infoPartyAList;
                ViewBag.Type = model.Type;
                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ContractViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Contract = ContractRepository.GetContractById(model.Id);
                    AutoMapper.Mapper.Map(model, Contract);
                    Contract.ModifiedUserId = WebSecurity.CurrentUserId;
                    Contract.ModifiedDate = DateTime.Now;
                    ContractRepository.UpdateContract(Contract);
                    
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
        public ActionResult Detail(int? Id, string TransactionCode)
        {
            var Contract = new Contract();
            if (Id != null)
                Contract = ContractRepository.GetContractById(Id.Value);
            if (!string.IsNullOrEmpty(TransactionCode))
                Contract = ContractRepository.GetContractByTransactionCode(TransactionCode);


            if (Contract != null && Contract.IsDeleted != true)
            {
                var model = new ContractViewModel();
                AutoMapper.Mapper.Map(Contract, model);

                if (Contract.Type == "ContractLease")
                {
                    string[] s = model.IdTypeContract.Split('_');
                    string idTypecontract = s[s.Length - 1];
                    var ContractLease = contractLeaseRepository.GetvwContractLeaseById(Convert.ToInt32(idTypecontract));
                    model.ContractLeaseModel = new ContractLeaseViewModel();
                    AutoMapper.Mapper.Map(ContractLease, model.ContractLeaseModel);
                }
                else
                    if (Contract.Type == "ContractSell")
                {
                    string[] s = model.IdTypeContract.Split('_');
                    string idTypecontract = s[s.Length - 1];
                    var ContractSell = contractSellRepository.GetvwContractSellById(Convert.ToInt32(idTypecontract));
                    model.ContractSellModel = new ContractSellViewModel();
                    AutoMapper.Mapper.Map(ContractSell, model.ContractSellModel);
                }
                var infoA = infoPartyARepository.GetvwInfoPartyAById(model.InfoPartyAId.Value);
                model.InfoPartyAViewModel = new InfoPartyAViewModel();
                AutoMapper.Mapper.Map(infoA, model.InfoPartyAViewModel);

                var customer = customerRepository.GetCustomerById(model.CustomerId.Value);
                model.CustomerViewModel = new CustomerViewModel();
                AutoMapper.Mapper.Map(customer, model.CustomerViewModel);

                var contact = contactRepository.GetAllvwContactByCustomerId(customer.Id);
                model.contactList = contact.Select(item => new ContactViewModel
                {
                    Id = item.Id,
                    CreatedUserId = item.CreatedUserId,
                    //CreatedUserName = item.CreatedUserName,
                    CreatedDate = item.CreatedDate,
                    ModifiedUserId = item.ModifiedUserId,
                    //ModifiedUserName = item.ModifiedUserName,
                    ModifiedDate = item.ModifiedDate,
                    Address = item.Address,
                    Birthday = item.Birthday,
                    CityId = item.CityId,
                    CustomerId = item.CustomerId,
                    DistrictId = item.DistrictId,
                    Email = item.Email,
                    FirstName = item.FirstName,
                    FullName = item.FirstName + item.LastName,
                    Gender = item.Gender,
                    LastName = item.LastName,
                    Mobile = item.Mobile,
                    Note = item.Note,
                    Phone = item.Phone,
                    WardId = item.WardId,
                    DistrictName = item.DistrictName,
                    GenderName = item.GenderName,
                    ProvinceName = item.ProvinceName,
                    WardName = item.WardName
                }).OrderByDescending(m => m.ModifiedDate);
                ViewBag.Type = model.Type;
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
                    var item = ContractRepository.GetContractById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if (item != null)
                    {
                        if (item.CreatedUserId != Erp.BackOffice.Helpers.Common.CurrentUser.Id && Erp.BackOffice.Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        ContractRepository.UpdateContract(item);
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

        #region addProccessPayment
        [HttpPost]
        public ActionResult AddProcessPayment(int moneyPayment, DateTime? dayPayment, int orderNo)
        {
            ProcessPaymentViewModel model = new ProcessPaymentViewModel();
            model.DayPayment = dayPayment;
            model.MoneyPayment = moneyPayment;
            model.OrderNo = orderNo + 1;
            model.Name = "Đợt " + model.OrderNo;
            return View(model);
        }
        #endregion
    }
}
