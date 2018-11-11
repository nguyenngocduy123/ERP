using System.Globalization;
using Erp.BackOffice.Crm.Models;
using Erp.BackOffice.Filters;
using Erp.Domain.Entities;
using Erp.Domain.Interfaces;
using Erp.Domain.Crm.Entities;
using Erp.Domain.Crm.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Erp.Utilities;
using WebMatrix.WebData;
using Erp.BackOffice.Helpers;
using Erp.Domain.Sale.Interfaces;
using Erp.BackOffice.Account.Models;
using Erp.BackOffice.Sale.Models;
using Erp.Domain.Account.Interfaces;

namespace Erp.BackOffice.Crm.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class VoteController : Controller
    {
        private readonly IVoteRepository voteRepository;
        private readonly IUserRepository userRepository;
        private readonly IQuestionRepository questionRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IProductInvoiceRepository productInvoiceRepository;
        private readonly ICustomerRepository CustomerRepository;
        private readonly IUsingServiceLogDetailRepository usingServiceLogDetailRepository;
        public VoteController(
            IVoteRepository _Vote
            , IUserRepository _user
            , IQuestionRepository _Question
            , IAnswerRepository _Answer
            , IProductInvoiceRepository productInvoice
            , ICustomerRepository _Customer
            ,IUsingServiceLogDetailRepository usingServiceLogDetail
            )
        {
            voteRepository = _Vote;
            userRepository = _user;
            questionRepository = _Question;
            answerRepository = _Answer;
            productInvoiceRepository = productInvoice;
            CustomerRepository = _Customer;
            usingServiceLogDetailRepository = usingServiceLogDetail;
        }

        #region Index

        public ViewResult Index(int? BranchId,string ProductInvoiceCode, string StaffName, string CustomerName)
        {
            var start_date = Request["start_date"];
            var end_date = Request["end_date"];
            IEnumerable<vwVoteViewModel> q = voteRepository.GetAllvwVote2().Where(x=>x.UsingServiceLogDetailId!=null).AsEnumerable()
             .Select(item => new vwVoteViewModel
             {
                 Id = item.Id,
                 CreatedDate = item.CreatedDate,
                 BranchId=item.BranchId,
                 BranchName=item.BranchName,
                 CustomerCode=item.CustomerCode,
                 CustomerId=item.CustomerId,
                 CustomerName=item.CustomerName,
                 ProductInvoiceCode=item.ProductInvoiceCode,
                 ServiceName=item.ServiceName,
                 StaffCode=item.StaffCode,
                 StaffName=item.StaffName,
                 UsingServiceLogDetailId=item.UsingServiceLogDetailId
             }).OrderByDescending(m => m.CreatedDate);
            if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
            {
                DateTime start_d;
                if (DateTime.TryParseExact(start_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_d))
                {
                    DateTime end_d;
                    if (DateTime.TryParseExact(end_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_d))
                    {
                        end_d = end_d.AddHours(23);
                        q = q.Where(x => start_d <= x.CreatedDate && x.CreatedDate <= end_d);
                    }
                }
            }
            if (BranchId != null && BranchId.Value > 0)
            {
                q = q.Where(x => x.BranchId != null && x.BranchId == BranchId);
            }
            if (!string.IsNullOrEmpty(ProductInvoiceCode))
            {
                q = q.Where(x => x.ProductInvoiceCode!=null&& x.ProductInvoiceCode.Contains(ProductInvoiceCode));
            }
            if (!string.IsNullOrEmpty(StaffName))
            {
                q = q.Where(x => x.StaffCode != null && Helpers.Common.ChuyenThanhKhongDau(x.StaffName).ToLower().Contains(Helpers.Common.ChuyenThanhKhongDau(StaffName).ToLower()));
            }
            if (!string.IsNullOrEmpty(CustomerName))
            {
                q = q.Where(x => x.CustomerCode != null&& Helpers.Common.ChuyenThanhKhongDau(x.CustomerCode).ToLower().Contains(Helpers.Common.ChuyenThanhKhongDau(CustomerName).ToLower()));
            }
            var group = q.GroupBy(x => x.UsingServiceLogDetailId).Select(x => new vwVoteViewModel
            {
                UsingServiceLogDetailId = x.Key,
                BranchId = x.FirstOrDefault().BranchId,
                BranchName = x.FirstOrDefault().BranchName,
                CustomerCode = x.FirstOrDefault().CustomerCode,
                CustomerName = x.FirstOrDefault().CustomerName,
                StaffCode = x.FirstOrDefault().StaffCode,
                StaffName = x.FirstOrDefault().StaffName,
                ServiceName = x.FirstOrDefault().ServiceName,
                CreatedDate = x.FirstOrDefault().CreatedDate,
                ProductInvoiceCode=x.FirstOrDefault().ProductInvoiceCode
            }).ToList();
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];
            return View(group);
        }
        #endregion

        #region Detail
        public ActionResult Detail(int? UsingServiceLogDetailId)
        {
            var list = voteRepository.GetAllvwVote2().Where(x => x.UsingServiceLogDetailId == UsingServiceLogDetailId);
            var using_service = usingServiceLogDetailRepository.GetvwUsingServiceLogDetailById(UsingServiceLogDetailId.Value);
            if (using_service != null && using_service.IsDeleted != true)
            {
                var model = new UsingServiceLogDetailViewModel();
                AutoMapper.Mapper.Map(using_service, model);
                model.list_vote = list.Select(item => new vwVoteViewModel
                                {
                                    Id = item.Id,
                                    AnswerContent = item.AnswerContent,
                                    AnswerId = item.AnswerId,
                                    QuestionId = item.QuestionId,
                                    QuestionName = item.QuestionName,
                                    CreatedDate=item.CreatedDate
                                }).OrderByDescending(m => m.CreatedDate).ToList();
                foreach (var item in model.list_vote)
                {
                    var answer = answerRepository.GetAllAnswer().Where(x => x.QuestionId == item.QuestionId && x.IsActivated == true).ToList();
                    //item.AnswerList = new List<AnswerViewModel>();
                    item.AnswerList = answer.Select(x => new AnswerViewModel { Id = x.Id, Content = x.Content, QuestionId = x.QuestionId }).ToList();
                }
                return View(model);
            }

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }
        #endregion

        #region Create
        public ActionResult Create(int CustomerId, int InvoiceId, int? UsingServiceLogDetailId)
        {
            ClientVoteViewModel model = new ClientVoteViewModel();

            //Thông tin KH
            var customer = CustomerRepository.GetvwCustomerById(CustomerId);
            if (customer != null && customer.IsDeleted != true)
            {
                model.Customer = new CustomerViewModel();
                AutoMapper.Mapper.Map(customer, model.Customer);

                //var customerImagePath = Helpers.Common.GetSetting("uploads_image_path_customer");
                //if (!string.IsNullOrEmpty(model.Customer.Image))
                //{
                //    model.Customer.Image = customerImagePath + model.Customer.Image;
                //}
            }
            //Thông tin su dung dich vu, tai kham
            if (UsingServiceLogDetailId != null && UsingServiceLogDetailId.Value > 0)
            {
                var detail = usingServiceLogDetailRepository.GetvwUsingServiceLogDetailById(UsingServiceLogDetailId.Value);
                model.UsingServiceLogDetail = new UsingServiceLogDetailViewModel();
                AutoMapper.Mapper.Map(detail, model.UsingServiceLogDetail);
            }
            //Thông tin hóa đơn KH được phục vụ
            var invoice = productInvoiceRepository.GetvwProductInvoiceById(InvoiceId);
            model.ProductInvoice = new ProductInvoiceViewModel();
            AutoMapper.Mapper.Map(invoice, model.ProductInvoice);

            //Danh sách câu hỏi đánh giá
            var listQuestion = questionRepository.GetAllQuestion()
                .Where(item => item.IsActivated.Value).ToList();

            model.ListQuestion = new List<Crm.Models.QuestionViewModel>();
            AutoMapper.Mapper.Map(listQuestion, model.ListQuestion);

            //Lấy danh sách đáp án
            foreach (var item in model.ListQuestion)
            {
                var listAnswer = answerRepository.GetAllAnswer()
                    .Where(i => i.QuestionId == item.Id && i.IsActivated.Value)
                    .OrderBy(i => i.OrderNo)
                    .ToList();

                item.DetailList = new List<Crm.Models.AnswerViewModel>();
                AutoMapper.Mapper.Map(listAnswer, item.DetailList);
            }

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.FailedMessage = TempData["FailedMessage"];
            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(int CustomerId, int InvoiceId, int QuestionId, int AnswerId, int? UsingServiceLogDetailId, string Note)
        {
            var votes = voteRepository.GetAllVote()
                .Where(item => item.CustomerId == CustomerId && item.InvoiceId == InvoiceId && item.QuestionId == QuestionId);
          
            if (UsingServiceLogDetailId != null && UsingServiceLogDetailId.Value > 0)
            {
                //cập nhật lại log sử dụng dịch vụ là khách hàng đã đánh giá
                var detail = usingServiceLogDetailRepository.GetUsingServiceLogDetailById(UsingServiceLogDetailId.Value);
                detail.IsVote = true;
                usingServiceLogDetailRepository.UpdateUsingServiceLogDetail(detail);
                //lưu đánh giá
                votes = votes.Where(x => x.UsingServiceLogDetailId == UsingServiceLogDetailId);
                var vote = votes.FirstOrDefault();
                if (vote == null)
                {
                    vote = new Vote();
                    vote.IsDeleted = false;
                    vote.CreatedUserId = WebSecurity.CurrentUserId;
                    vote.ModifiedUserId = WebSecurity.CurrentUserId;
                    vote.CreatedDate = DateTime.Now;
                    vote.ModifiedDate = DateTime.Now;
                    vote.CustomerId = CustomerId;
                    vote.InvoiceId = InvoiceId;
                    vote.QuestionId = QuestionId;
                    vote.AnswerId = AnswerId;
                    vote.UsingServiceLogDetailId = UsingServiceLogDetailId;
                    vote.Note = Note;
                    voteRepository.InsertVote(vote);
                }
                else
                {
                    vote.AnswerId = AnswerId;
                    vote.Note = Note;
                    voteRepository.UpdateVote(vote);
                }
            }
            else
            {
                //chỗ này hiện tại k sử dụng tới nên chưa viết hàm lưu vào log sử dụng dịch vụ là đã đánh giá khách hàng.
                var list_using = usingServiceLogDetailRepository.GetAllvwUsingServiceLogDetail().Where(x => x.ProductInvoiceId == InvoiceId).ToList();

                foreach (var item in list_using)
                {
                    votes = votes.Where(x => x.UsingServiceLogDetailId == item.Id);
                    var vote = votes.FirstOrDefault();
                    if (vote == null)
                    {
                        vote = new Vote();
                        vote.IsDeleted = false;
                        vote.CreatedUserId = WebSecurity.CurrentUserId;
                        vote.ModifiedUserId = WebSecurity.CurrentUserId;
                        vote.CreatedDate = DateTime.Now;
                        vote.ModifiedDate = DateTime.Now;
                        vote.CustomerId = CustomerId;
                        vote.InvoiceId = InvoiceId;
                        vote.QuestionId = QuestionId;
                        vote.AnswerId = AnswerId;
                        vote.UsingServiceLogDetailId = item.Id;
                        voteRepository.InsertVote(vote);
                    }
                    else
                    {
                        vote.AnswerId = AnswerId;
                        voteRepository.UpdateVote(vote);
                    }
                }
            }
          

            return Content("success");
        }
        #endregion

        #region Statistics
        public ViewResult Statistics()
        {
            var q = voteRepository.GetAllvwVote().ToList();

            List<VoteQuestionViewModel> model = q.GroupBy(item => item.QuestionId)
                .Select((question, index) => new VoteQuestionViewModel
                {
                    Id = question.FirstOrDefault().QuestionId,
                    Name = question.FirstOrDefault().QuestionName,
                    ListVoteAnswer = question.ToList().Select(answer => new VoteAnswerViewModel
                    {
                        Id = answer.AnswerId,
                        Name = answer.AnswerName,
                        NumberOfVote = answer.NumberOfVote,
                        PercentOfVote = Math.Round(Convert.ToDouble(answer.NumberOfVote) / question.Sum(i => i.NumberOfVote) * 100, 2)
                    }).ToList()
                }).ToList();

            return View(model);
        }
        #endregion
    }
}
