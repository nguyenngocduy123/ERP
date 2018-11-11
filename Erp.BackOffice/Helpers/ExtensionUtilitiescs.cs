using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Erp.BackOffice.Helpers
{
    public static class ExtensionUtilitiescs
    {
        public const string DEFAULT_GROUP_CLASS = "control-group";
        public const string DEFAULT_LABEL_CLASS = "control-label";
        public const string DEFAULT_FIELD_CLASS = "controls";
        public const string DEFAULT_SWITCH_CLASS = "span3";

        public static TagBuilder BuildFormField(TagBuilder fieldBuilder, string id, string text, string groupClass, string labelClass, string fieldClass)
        {
            // create first div tag
            var div1 = new TagBuilder("div");

            // add attributes
            div1.MergeAttribute("class", groupClass);

            // create label tag
            var label = new TagBuilder("label");
            // add attributes
            label.MergeAttribute("class", labelClass);
            label.MergeAttribute("for", id);
            label.InnerHtml = text;

            // create second div tag
            var div2 = new TagBuilder("div");
            // add attributes
            div2.MergeAttribute("class", fieldClass);

            div2.InnerHtml += fieldBuilder.ToString();
            div1.InnerHtml += label.ToString();
            div1.InnerHtml += div2.ToString();
            return div1;
        }
    }
}