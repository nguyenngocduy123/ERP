using Erp.BackOffice.App_GlobalResources;
using Erp.BackOffice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Erp.BackOffice.Helpers
{
    public static class FormHelper
    {
        private class Form : IDisposable
        {
            private HtmlHelper _html;
            private readonly TextWriter _writer;
            public Form(TextWriter writer, HtmlHelper htmlHelper)
            {
                _writer = writer;
                _html = htmlHelper;
            }

            public void Dispose()
            {
                _writer.Write(_html.Partial("_BottomFormPartial"));
            }
        }

        public static IDisposable BeginForm_AceStyle(this HtmlHelper html, string box_header, string action, string controller, object routeValues, FormMethod method, object htmlAttributes, string form_header = null)
        {
            string topForm = html.Partial("_TopFormPartial", new ViewDataDictionary { { "box_header", box_header }, { "form_header", form_header }, { "action", action }, { "controller", controller }, { "routeValues", routeValues }, { "method", method }, { "htmlAttributes", htmlAttributes } }).ToHtmlString();

            var writer = html.ViewContext.Writer;
            writer.Write(topForm);
            return new Form(writer, html);
        }

        public static IDisposable BeginForm_AceStyle(this HtmlHelper html, string box_header, string form_header, string action, string controller, object routeValues, FormMethod method, object htmlAttributes)
        {
            return BeginForm_AceStyle(html, box_header, action, controller, routeValues, method, htmlAttributes, form_header);
        }

        private class ButtonContainer : IDisposable
        {
            private HtmlHelper _html;
            private readonly TextWriter _writer;
            public ButtonContainer(TextWriter writer, HtmlHelper htmlHelper)
            {
                _writer = writer;
                _html = htmlHelper;
            }

            public void Dispose()
            {
                _writer.Write("</div>\r\n</div>");
            }
        }

        public static IDisposable BeginButtonContainer(this HtmlHelper html, PageSetting pageSetting)
        {
            string topContainer = html.Partial("_TopButtonContainerPartial", pageSetting).ToHtmlString();
            var writer = html.ViewContext.Writer;
            writer.Write(topContainer);
            return new ButtonContainer(writer, html);
        }

        private class PageHeaderContainer : IDisposable
        {
            private HtmlHelper _html;
            private readonly TextWriter _writer;
            private PageSetting _pageSetting;
            public PageHeaderContainer(TextWriter writer, HtmlHelper htmlHelper, PageSetting pageSetting)
            {
                _writer = writer;
                _html = htmlHelper;
                _pageSetting = pageSetting;
            }

            public void Dispose()
            {
               // _writer.Write("</div>\r\n</div>\r\n</div>");
                string bottomContainer = _html.Partial("_BottomPageHeaderPartial", _pageSetting).ToHtmlString();
                _writer.Write(bottomContainer);
            }
        }

        public static IDisposable BeginPageHeaderContainer(this HtmlHelper html, PageSetting pageSetting)
        {
            string topContainer = html.Partial("_TopPageHeaderContainerPartial", pageSetting).ToHtmlString();
            var writer = html.ViewContext.Writer;
            writer.Write(topContainer);
            return new PageHeaderContainer(writer, html, pageSetting);
        }
    }
}