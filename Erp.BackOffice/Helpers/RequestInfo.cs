using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Helpers
{
    public class RequestInfo
    {
        public string IP;
        public List<RequestUrl> UrlList;
        public int RequestCount;
        public DateTime FirstDate;
        public DateTime LastDate;
        public bool IsLocked;
        public Erp.Domain.Entities.vwUsers User;

        public void AddUrl(string url)
        {
            string[] pageNotTrack = new string[] {    "/Home/StatisticsInvoiceFee"
                                                    , "/Home/TrackRequest"
                                                    , "/Home/StatisticsRentByUser"
                                                    , "/Home/StatisticsStudent"
            , "/ErpHouse/FetchErpHouse"
            , "/ErpRoom/FetchErpRoom"};
            foreach(var item in pageNotTrack)
            {
                if (url.Contains(item))
                    return;
            }

            var requestUrl = new RequestUrl();
            requestUrl.Url = url;
            requestUrl.Date = DateTime.Now;

            if (UrlList == null)
                UrlList = new List<RequestUrl>();
            UrlList.Add(requestUrl);
            RequestCount++;
        }

        public string GetUserName()
        {
            if (User != null)
                return User.UserName;
            else
                return "";
        }
    }

    public class RequestUrl
    {
        public string Url;
        public DateTime Date;
    }
}