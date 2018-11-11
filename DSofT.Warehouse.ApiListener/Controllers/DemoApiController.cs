using System.Web.Http;
using DSofT.Framework.Internal.ApiListener.Controllers;
using DSofT.Framework.Internal.ApiListener.Routes;
using System;
using DSofT.Warehouse.Domain;

namespace DSofT.FoodWarehouse.ApiListener.Controllers
{
    [RoutePrefix(UrlCommon.C_System)]
    public class DemoApiController : BaseApiController
    {
        //private readonly IDemoBussiness _demoBussiness;

        //public DemoApiController()
        //{
        //    _demoBussiness = new DemoBussiness(AppId);
        //}

        //[HttpPost, DSofTRoute(UrlCommon.C_Demo_GetListCity)]
        //public IHttpActionResult GetListCity()
        //{
        //    //var userInfo = GetRequestData<i>();
        //    return DSofTResult(new DemoModel() { CITY_ID = 1, CITY_NAME = "a", CREATED_BY = 1, CREATED_DATE = DateTime.Now.Millisecond, IS_DELETE = 0 });
        //}
    }
}