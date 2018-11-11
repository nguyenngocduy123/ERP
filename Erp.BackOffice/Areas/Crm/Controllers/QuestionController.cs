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

namespace Erp.BackOffice.Crm.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository QuestionRepository;
        private readonly IUserRepository userRepository;
        private readonly IAnswerRepository answerRepository;

        public QuestionController(
            IQuestionRepository _Question
            , IUserRepository _user
            , IAnswerRepository _Answer
            )
        {
            QuestionRepository = _Question;
            userRepository = _user;
            answerRepository = _Answer;
        }

        #region Index

        public ViewResult Index(string txtSearch)
        {

            IQueryable<QuestionViewModel> q = QuestionRepository.GetAllQuestion()
                .Select(item => new QuestionViewModel
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
            return View(q);
        }
        #endregion

        #region Create
        public ViewResult Create()
        {
            var model = new QuestionViewModel();
            model.IsActivated = true;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var question = new Question();
                AutoMapper.Mapper.Map(model, question);
                question.IsDeleted = false;
                question.CreatedUserId = WebSecurity.CurrentUserId;
                question.ModifiedUserId = WebSecurity.CurrentUserId;
                question.AssignedUserId = WebSecurity.CurrentUserId;
                question.CreatedDate = DateTime.Now;
                question.ModifiedDate = DateTime.Now;
                QuestionRepository.InsertQuestion(question);

                //Thêm đáp án
                if(model.DetailList != null && model.DetailList.Count > 0)
                {
                    foreach(var item in model.DetailList)
                    {
                        var answer = new Answer();
                        AutoMapper.Mapper.Map(item, answer);
                        answer.IsDeleted = false;
                        answer.CreatedUserId = WebSecurity.CurrentUserId;
                        answer.ModifiedUserId = WebSecurity.CurrentUserId;
                        answer.AssignedUserId = WebSecurity.CurrentUserId;
                        answer.CreatedDate = DateTime.Now;
                        answer.ModifiedDate = DateTime.Now;

                        answer.QuestionId = question.Id;
                        answerRepository.InsertAnswer(answer);
                    }
                }

                TempData[Globals.SuccessMessageKey] = App_GlobalResources.Wording.InsertSuccess;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? Id)
        {
            var Question = QuestionRepository.GetQuestionById(Id.Value);
            if (Question != null && Question.IsDeleted != true)
            {
                var model = new QuestionViewModel();
                AutoMapper.Mapper.Map(Question, model);
                
                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                {
                    TempData["FailedMessage"] = "NotOwner";
                    return RedirectToAction("Index");
                }     
          
                //Lấy danh sách đáp án
                var listAnswer = answerRepository.GetAllAnswer()
                    .Where(item => item.QuestionId == Question.Id).ToList();
                model.DetailList = new List<AnswerViewModel>();
                AutoMapper.Mapper.Map(listAnswer, model.DetailList);

                return View(model);
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request["Submit"] == "Save")
                {
                    var Question = QuestionRepository.GetQuestionById(model.Id);
                    AutoMapper.Mapper.Map(model, Question);
                    Question.ModifiedUserId = WebSecurity.CurrentUserId;
                    Question.ModifiedDate = DateTime.Now;
                    QuestionRepository.UpdateQuestion(Question);

                    if (model.DetailList != null && model.DetailList.Count > 0)
                    {
                        //Thêm/Hoặc cập nhật đáp án
                        var listAnswerOld = answerRepository.GetAllAnswer()
                        .Where(item => item.QuestionId == Question.Id).ToList();

                        //Xóa những cái cũ
                        if (listAnswerOld != null && listAnswerOld.Count > 0)
                        {
                            foreach (var item in listAnswerOld)
                            {
                                var answer = model.DetailList.Where(i => i.Id == item.Id).FirstOrDefault();
                                if (answer == null)
                                {
                                    answerRepository.DeleteAnswer(item.Id);
                                }
                            }
                        }

                        //Thêm/cập nhật
                        foreach (var item in model.DetailList)
                        {
                            var answer = listAnswerOld.Where(i => i.Id == item.Id).FirstOrDefault();
                            if (answer == null)
                            {
                                answer = new Answer();
                                AutoMapper.Mapper.Map(item, answer);
                                answer.IsDeleted = false;
                                answer.CreatedUserId = WebSecurity.CurrentUserId;
                                answer.ModifiedUserId = WebSecurity.CurrentUserId;
                                answer.AssignedUserId = WebSecurity.CurrentUserId;
                                answer.CreatedDate = DateTime.Now;
                                answer.ModifiedDate = DateTime.Now;

                                answer.QuestionId = Question.Id;
                                answerRepository.InsertAnswer(answer);
                            }
                            else
                            {
                                answer.OrderNo = item.OrderNo;
                                answer.Content = item.Content;
                                answer.IsActivated = item.IsActivated;
                                answerRepository.UpdateAnswer(answer);
                            }
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
            var Question = QuestionRepository.GetQuestionById(Id.Value);
            if (Question != null && Question.IsDeleted != true)
            {
                var model = new QuestionViewModel();
                AutoMapper.Mapper.Map(Question, model);
                
                if (model.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
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
                    var item = QuestionRepository.GetQuestionById(int.Parse(arrDeleteId[i], CultureInfo.InvariantCulture));
                    if(item != null)
                    {
                        if (item.CreatedUserId != Helpers.Common.CurrentUser.Id && Helpers.Common.CurrentUser.UserTypeId != 1)
                        {
                            TempData["FailedMessage"] = "NotOwner";
                            return RedirectToAction("Index");
                        }

                        item.IsDeleted = true;
                        QuestionRepository.UpdateQuestion(item);
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
    }
}
