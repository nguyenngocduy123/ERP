using System.Globalization;
using System.Web.Mvc;


namespace Erp.BackOffice.Administration.Controllers
{
    using Domain.Interfaces;

    [Erp.BackOffice.Helpers.NoCacheHelper]
    public class HomeController : Controller
    {
        private readonly IBOLogRepositoty _boLogRepositoty;

        public HomeController(IBOLogRepositoty boLogRepositoty)
        {
            _boLogRepositoty = boLogRepositoty;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "This administration home page.";
            return View();
        }

        //
        // GET: /Administration/Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Administration/Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Administration/Home/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Administration/Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Administration/Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Administration/Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Administration/Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ChangeLanguage(string language, string returnUrl)
        {
            Session["CurrentLanguage"] = new CultureInfo(language);
            return RedirectToLocal(returnUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewLog()
        {
            var boLog = _boLogRepositoty.GetAllBOLog();

            return View(boLog);
        }
    }
}
