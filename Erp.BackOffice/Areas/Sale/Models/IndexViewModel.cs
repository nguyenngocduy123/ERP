using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Erp.BackOffice.Sale.Models
{
    public class IndexViewModel<T>
    {
        public List<T> Items { get; set; }
        public Pager Pager { get; set; }
    }

    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 10, string pageName = "page")
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            PageName = pageName;
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public string PageName { get; private set; }
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int No { get; private set; }

        public int GetOrderNoNumber(int index)
        {
            return index + 1 + ((CurrentPage - 1) * PageSize);
        }

        public string Paging(string UrlCurrent)
        {
            string result = string.Empty;

            //
            //?page=1
            //?page=1&name=tuan
            //?name=tuan
            //?name=tuan&page=1
            string pageNameCurrent = PageName + "=" + CurrentPage;

            if (UrlCurrent.Contains(pageNameCurrent) == true)
            {
                var UrlCurrentSplit = UrlCurrent.Split('?');
                if(UrlCurrentSplit.Length >= 2)
                {
                    var arrSplit = Regex.Split(UrlCurrentSplit[1], pageNameCurrent);
                    if (string.IsNullOrEmpty(arrSplit[0]) == true)
                    {
                        if (string.IsNullOrEmpty(arrSplit[1]) == true)
                            UrlCurrent = UrlCurrentSplit[0];
                        else
                            UrlCurrent = UrlCurrentSplit[0] + "?" + arrSplit[1].Split('&')[1] + "&";
                    }
                    else
                    {
                        UrlCurrent = UrlCurrentSplit[0] + "?" + arrSplit[0];
                    }
                }
                UrlCurrent = UrlCurrent.Contains("?") == true ? UrlCurrent : UrlCurrent + "?";
            }
            else
            {
                if (UrlCurrent.Contains("?") == true)
                    UrlCurrent += "&";
                else
                    UrlCurrent += "?";
            }


            if (EndPage > 1)
			{
				result += "<ul class=\"pagination\">";
				if (CurrentPage > 1)
				{
					result += "<li>";
                    result += "<a href=\"" + (UrlCurrent.Contains("=") == false ? UrlCurrent.Replace("?", "").Replace("&", "") : UrlCurrent.Replace("&", "")) + "\">«</a>";
					result += "</li>";
					result += "<li>";
                    result += "<a href=\"" + UrlCurrent + "page=" + (CurrentPage - 1) + "\">←</a>";
					result += "</li>";
				}

				for(var page = StartPage; page <= EndPage; page++)
				{
					result += "<li class=\"" + (page == CurrentPage ? "active" : "") + "\">";
                    result += "<a href=\"" + UrlCurrent + "page=" + page + "\">" + page + "</a>";
					result += "</li>";
				}

				if(CurrentPage < TotalPages)
				{
					result += "<li>";
                    result += "<a href=\"" + UrlCurrent + "page=" + (CurrentPage + 1) + "\">→</a>";
					result += "</li>";
					result += "<li>";
                    result += "<a href=\"" + UrlCurrent + "page=" + TotalPages + "\">»</a>";
					result += "</li>";
				}
                result += "</ul>";
			}

            return result;
        }//end method Paging()
    } // end class Pager
}