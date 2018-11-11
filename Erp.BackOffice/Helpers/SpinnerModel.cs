using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Erp.BackOffice.Helpers
{
    public enum SpinnerStyle
    {
        SpinnerStyle1,
        SpinnerStyle2,
        SpinnerStyle3
    }

    public class SpinnerModel
    {
        public MvcHtmlString HtmlInputControl { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public int step { get; set; }
        public SpinnerStyle style { get; set; }
    }
}