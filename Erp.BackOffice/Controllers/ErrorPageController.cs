using System.Web.Mvc;

namespace Erp.BackOffice.Controllers
{
    public class ErrorPageController : Controller
    {
        //
        // GET: /ErrorPage/

        public ActionResult Index()
        {
            return View("404");
        }
        public ActionResult Page404()
        {
            return View("404");
        }
        public ActionResult Page500()
        {
            return View("500");
        }
    }
}
