using DSofT.Framework.Internal.ApiListener.Controllers;
using DSofT.Framework.Internal.ApiListener.Routes;
using System.Web;
using System.Web.Http;

namespace DSofT.FoodWarehouse.ApiListener.Controllers
{
    [RoutePrefix(Warehouse.Domain.UrlCommon.C_System)]
    public class SystemApiController : BaseApiController
    {

        public SystemApiController()
        {
        }

        [HttpGet, Route("testapi")]
        public string TestApi()
        {
            return "Success!";
        }
        
    }
}