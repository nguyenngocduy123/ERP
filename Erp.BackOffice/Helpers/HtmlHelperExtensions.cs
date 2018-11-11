using Erp.BackOffice.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Optimization;
using System.Web.Routing;

namespace Erp.BackOffice.Helpers
{
    #region Enums

    public enum SwitchesStyle
    {
        [Display(Name = "")]
        CheckboxStyle = 0,
        [Display(Name = "ace-switch")]
        SwitchesStyle1 = 1,
        [Display(Name = "ace-switch-2")]
        SwitchesStyle2 = 2,
        [Display(Name = "ace-switch-3")]
        SwitchesStyle3 = 3,
        [Display(Name = "ace-switch-4")]
        SwitchesStyle4 = 4,
        [Display(Name = "ace-switch-5")]
        SwitchesStyle5 = 5,
        [Display(Name = "ace-switch-6")]
        SwitchesStyle6 = 6,
        [Display(Name = "ace-switch-7")]
        SwitchesStyle7 = 7,
    }

    public enum DropdownListStyle
    {
        [Display(Name = "")]
        DropdownListStyleDefault = 0,
        [Display(Name = "chzn-select")]
        DropdownListStyleChosen = 1,
    }

    public enum WidthType
    {
        [Display(Name = "span")]
        span = 0,
        [Display(Name = "col-xs-1")]
        span1 = 1,
        [Display(Name = "col-xs-2")]
        span2 = 2,
        [Display(Name = "col-xs-3")]
        span3 = 3,
        [Display(Name = "col-xs-4")]
        span4 = 4,
        [Display(Name = "col-xs-5")]
        span5 = 5,
        [Display(Name = "col-xs-6")]
        span6 = 6,
        [Display(Name = "col-xs-7")]
        span7 = 7,
        [Display(Name = "col-xs-8")]
        span8 = 8,
        [Display(Name = "col-xs-9")]
        span9 = 9,
        [Display(Name = "col-xs-10")]
        span10 = 10,
        [Display(Name = "col-xs-11")]
        span11 = 11,
        [Display(Name = "col-xs-12")]
        span12 = 12,
        [Display(Name = "")]
        none = 13
    }

    #region ForButton

    public enum ButtonType
    {
        [Display(Name = "a")]
        aTag,
        [Display(Name = "button")]
        buttonTag
    }

    public enum ButtonColor
    {
        [Display(Name = "")]
        Default,
        [Display(Name = "btn-primary")]
        Primary,
        [Display(Name = "btn-info")]
        Info,
        [Display(Name = "btn-success")]
        Success,
        [Display(Name = "btn-warning")]
        Warning,
        [Display(Name = "btn-danger")]
        Danger,
        [Display(Name = "btn-inverse")]
        Inverse,
        [Display(Name = "btn-pink")]
        Pink,
        [Display(Name = "btn-purple")]
        Purple,
        [Display(Name = "btn-yellow")]
        Yellow,
        [Display(Name = "btn-grey")]
        Grey,
        [Display(Name = "btn-light")]
        Light,
        [Display(Name = "btn-white")]
        White
    }

    public enum ButtonSize
    {
        [Display(Name = "")]
        Default,
        [Display(Name = "btn-minier")]
        Minier,
        [Display(Name = "btn-mini")]
        Mini,
        [Display(Name = "btn-sm")]
        Small,
        [Display(Name = "btn-large")]
        Large,
        [Display(Name = "btn-block")]
        Block
    }

    #endregion

    #region ForIcon

    public enum IconType
    {
        [Display(Name = "")]
        None,
        [Display(Name = "ace-icon fa fa-plus")]
        Plus,
        [Display(Name = "ace-icon fa fa-pencil")]
        Pencil,
        [Display(Name = "ace-icon fa fa-beaker")]
        Beaker,
        [Display(Name = "ace-icon fa fa-print")]
        Print,
        [Display(Name = "ace-icon fa fa-check")]
        Ok,
        [Display(Name = "ace-icon fa fa-fire")]
        Fire,
        [Display(Name = "ace-icon fa fa-bolt")]
        Bolt,
        [Display(Name = "ace-icon fa fa-arrow-right")]
        ArrowRight,
        [Display(Name = "ace-icon fa fa-arrow-left")]
        ArrowLeft,
        [Display(Name = "ace-icon fa fa-wrench")]
        Wrench,
        [Display(Name = "ace-icon fa fa-trash")]
        Trash,
        [Display(Name = "ace-icon fa fa-reply")]
        Reply,
        [Display(Name = "ace-icon fa fa-cog")]
        Config,
        [Display(Name = "ace-icon fa fa-edit")]
        Edit,
        [Display(Name = "ace-icon fa fa-refresh")]
        Refresh,
        [Display(Name = "ace-icon fa fa-undo")]
        Undo,
        [Display(Name = "ace-icon fa fa-envelope")]
        Envelope,
        [Display(Name = "ace-icon fa fa-cloud-upload")]
        CloudUpload,
        [Display(Name = "ace-icon fa fa-share-alt")]
        Share,
        [Display(Name = "ace-icon fa fa-lock")]
        Lock,
        [Display(Name = "ace-icon fa fa-save")]
        Save,
        [Display(Name = "ace-icon fa fa-shopping-cart")]
        ShoppingCart,
        [Display(Name = "ace-icon fa fa-search")]
        Search,
        //and more ....
    }

    public enum IconSize
    {
        [Display(Name = "")]
        Default,
        [Display(Name = "icon-2x")]
        IconX2,
        [Display(Name = "icon-3x")]
        IconX3,
        [Display(Name = "icon-4x")]
        IconX4,
        [Display(Name = "bigger-110")]
        Bigger110,
        [Display(Name = "bigger-120")]
        Bigger120,
        [Display(Name = "bigger-130")]
        Bigger130,
        [Display(Name = "bigger-140")]
        Bigger140,
        [Display(Name = "bigger-150")]
        Bigger150,
        [Display(Name = "bigger-160")]
        Bigger160,
        [Display(Name = "bigger-170")]
        Bigger170,
        [Display(Name = "bigger-180")]
        Bigger180,
        [Display(Name = "bigger-190")]
        Bigger190,
        [Display(Name = "bigger-200")]
        Bigger200,
        [Display(Name = "bigger-210")]
        Bigger210,
        [Display(Name = "bigger-220")]
        Bigger220,
        [Display(Name = "bigger-230")]
        Bigger230,
        [Display(Name = "bigger-240")]
        Bigger240,
        [Display(Name = "bigger-250")]
        Bigger250,
        [Display(Name = "bigger-260")]
        Bigger260,
        [Display(Name = "bigger-270")]
        Bigger270,
        [Display(Name = "bigger-280")]
        Bigger280,
        [Display(Name = "bigger-290")]
        Bigger290,
        [Display(Name = "bigger-300")]
        Bigger300
    }

    public enum IconPositionDisplay
    {
        [Display(Name = "icon-on-right")]
        Right,
        //add more
    }
    #endregion

    #endregion

    public static class HtmlHelperExtensions
    {
        #region Validation
        public static MvcHtmlString ValidationErrorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string error)
        {
            if (HasError(htmlHelper, ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData), ExpressionHelper.GetExpressionText(expression)))
                return new MvcHtmlString(error);
            else
                return null;
        }

        private static bool HasError(this HtmlHelper htmlHelper, ModelMetadata modelMetadata, string expression)
        {
            string modelName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expression);
            FormContext formContext = htmlHelper.ViewContext.FormContext;
            if (formContext == null)
                return false;

            if (!htmlHelper.ViewData.ModelState.ContainsKey(modelName))
                return false;

            ModelState modelState = htmlHelper.ViewData.ModelState[modelName];
            if (modelState == null)
                return false;

            ModelErrorCollection modelErrors = modelState.Errors;
            if (modelErrors == null)
                return false;

            return (modelErrors.Count > 0);
        }
        #endregion

        #region Date time
        public static MvcHtmlString DatePicker<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, string inputMask, bool IsCheckValidation, bool bReadOnly, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8", string inputClass = "col-sm-12")
        {
            var result = ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label no-padding-right " + labelClass });

            MvcHtmlString inputUI = null;
            if (bReadOnly)
                inputUI = htmlHelper.TextBoxFor(expression, "{0:" + format + "}", new { style = "width:100px", @class = "date-picker " + inputClass, type = "text", placeholder = format.ToLower(), data_date_format = format.ToLower(), @readonly = bReadOnly });
            else
                inputUI = htmlHelper.TextBoxFor(expression, "{0:" + format + "}", new { style = "width:100px", @class = "date-picker " + inputClass, type = "text", placeholder = format.ToLower(), data_date_format = format.ToLower() });

            var inputUIPartial = htmlHelper.Partial("_DatePickerPartial", new ViewDataDictionary { { "inputUI", inputUI }, { "inputName", fullHtmlFieldName }, { "inputMask", inputMask } });

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", inputUIPartial }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", controlClass } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString DatePicker<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, string inputMask, bool IsCheckValidation)
        {
            return DatePicker(htmlHelper, expression, format, inputMask, IsCheckValidation, true);
        }

        public static MvcHtmlString MonthPicker<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8", string inputClass = "col-sm-12")
        {
            var result = ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label no-padding-right " + labelClass });

            MvcHtmlString inputUI = htmlHelper.DropDownListFor(expression, SelectListHelper.GetSelectList_Number(1, 12, result).Where(item => !string.IsNullOrEmpty(item.Value)), new { style = "width:50px" });

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", inputUI }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", controlClass } };
            
            return htmlHelper.Partial("_InputFieldTemplateOpenPartial", viewDataDictionary);
        }

        public static MvcHtmlString YearPicker<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int before = 5, int after = 5)
        {
            var result = ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model);

            MvcHtmlString inputUI = htmlHelper.DropDownListFor(expression, SelectListHelper.GetSelectList_Number(DateTime.Now.Year - before, DateTime.Now.Year + after, result).Where(item => !string.IsNullOrEmpty(item.Value)), new { style = "width:70px" });

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", inputUI } };
            viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));

            return htmlHelper.Partial("_InputFieldTemplateClosePartial", viewDataDictionary);
        }

        public static MvcHtmlString DateTimePicker<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, string inputMask, bool IsCheckValidation, bool bReadOnly, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8", string inputClass = "col-sm-12")
        {
            var result = ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label no-padding-right " + labelClass });

            MvcHtmlString inputUI = null;
            if (bReadOnly)
                inputUI = htmlHelper.TextBoxFor(expression, "{0:" + format + "}", new { style = "", @class = "date-time-picker " + inputClass, type = "text", placeholder = format.ToLower(), @readonly = bReadOnly });
            else
                inputUI = htmlHelper.TextBoxFor(expression, "{0:" + format + "}", new { style = "", @class = "date-time-picker " + inputClass, type = "text", placeholder = format.ToLower()});

            var inputUIPartial = htmlHelper.Partial("_DateTimePickerPartial", new ViewDataDictionary { { "inputUI", inputUI }, { "inputName", fullHtmlFieldName }, { "format", format }, { "inputMask", inputMask } });

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", inputUIPartial }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", controlClass } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString DateInput<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, string inputMask, bool IsCheckValidation, bool bReadOnly, WidthType widthType = WidthType.span12, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8", string name = null)
        {
            var result = ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            if (!string.IsNullOrEmpty(name))
                fullHtmlFieldName = name;

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label no-padding-right " + labelClass });

            MvcHtmlString inputUI = null;
            if (bReadOnly)
                inputUI = htmlHelper.TextBoxFor(expression, "{0:" + format + "}", new { style = "width:100px", @readonly = true, @class = "date-input " + widthType.GetName(), type = "text", placeholder = format.ToLower(), data_date_format = format.ToLower() });
            else
                inputUI = htmlHelper.TextBoxFor(expression, "{0:" + format + "}", new { style = "width:100px", @class = "date-input " + widthType.GetName(), type = "text", placeholder = format.ToLower(), data_date_format = format.ToLower() });

            var inputUIPartial = htmlHelper.Partial("_DateInputPartial", new ViewDataDictionary { { "inputUI", inputUI }, { "inputName", fullHtmlFieldName }, { "inputMask", inputMask } });

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", inputUIPartial }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", controlClass } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString DateInput<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, string inputMask, bool IsCheckValidation, WidthType widthType = WidthType.span12, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8")
        {
            return DateInput(htmlHelper, expression, format, inputMask, IsCheckValidation, false, widthType, labelClass, controlClass);
        }

        public static MvcHtmlString DateInput<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string format, string inputMask, bool IsCheckValidation, bool bReadOnly, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8")
        {
            return DateInput(htmlHelper, expression, format, inputMask, IsCheckValidation, bReadOnly, WidthType.span12, labelClass, controlClass);
        }

        public static MvcHtmlString TimePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string ControlId, bool IsCheckValidation)
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : string.Empty;
            result = result != null ? result : string.Empty;

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label" });

            var inputUI = htmlHelper.TextBoxFor(expression, new { @class = "input-small", @Id = ControlId });

            TagBuilder controls = new TagBuilder("div");
            controls.Attributes.Add("class", "controls input-append bootstrap-timepicker");
            controls.Attributes.Add("style", "display:block;");

            controls.InnerHtml = inputUI.ToHtmlString();

            TagBuilder ITag = new TagBuilder("i");
            ITag.Attributes.Add("class", "icon-time");

            TagBuilder spanTag = new TagBuilder("span");
            spanTag.Attributes.Add("class", "add-on");
            spanTag.InnerHtml = ITag.ToString(TagRenderMode.Normal);

            controls.InnerHtml += spanTag.ToString(TagRenderMode.Normal);

            if (IsCheckValidation)
            {
                var validUI = htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" });
                controls.InnerHtml += validUI.ToHtmlString();
            }

            TagBuilder ControlDiv = new TagBuilder("div");
            ControlDiv.Attributes.Add("class", "control-group");
            ControlDiv.InnerHtml = titleUI.ToHtmlString();
            ControlDiv.InnerHtml += controls.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(ControlDiv.ToString(TagRenderMode.Normal));



        }
        #endregion

        #region Image
        public static MvcHtmlString ImagePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expressionForPath, Expression<Func<TModel, TProperty>> expressionForString64)
        {
            var pathValue = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expressionForPath).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : string.Empty;
            pathValue = pathValue != null ? pathValue : string.Empty;
            var inputName_ForPath = ExpressionHelper.GetExpressionText(expressionForPath);

            var string64Value = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expressionForString64).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : string.Empty;
            var inputName_ForString64 = ExpressionHelper.GetExpressionText(expressionForString64);

            StringBuilder inputUI = new StringBuilder();

            inputUI.Append(htmlHelper.TextBoxFor(expressionForPath, new { @style = "visibility:hidden;" }).ToHtmlString());
            inputUI.Append(htmlHelper.HiddenFor(expressionForString64, null).ToHtmlString());

            string imagePickerName = inputName_ForPath + "-image-picker";
            inputUI.Append(htmlHelper.Partial("_ImagePickerPartial", new ViewDataDictionary { { "imagePickerName", imagePickerName }, { "inputName_ForString64", inputName_ForString64 }, { "pathValue", pathValue }, { "title", htmlHelper.DisplayNameFor(expressionForPath) }, { "string64Value", string64Value } }).ToHtmlString());

            var titleUI = htmlHelper.LabelFor(expressionForPath, new { @class = "control-label" });

            var validErrorClass = htmlHelper.ValidationErrorFor(expressionForPath, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputUI.ToString()) }, { "titleUI", titleUI }, { "validError", validErrorClass } };
            if (true)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expressionForPath, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }
        #endregion

        #region CustomSwitchesFor
        public static MvcHtmlString CustomSwitchesFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, SwitchesStyle style, bool IsCheckValidation, WidthType widthType = WidthType.none, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8")
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : false;
            result = result != null ? result : false;

            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label no-padding-right " + labelClass });

            string css = "";
            string marginTop = "";
            if (style.GetName() != "")
            {
                css = " ace-switch " + style.GetName();
            }
            else
            {
                marginTop = " style=\"margin-top:5px\"";
            }

            string inputUI = htmlHelper.CheckBox(fullHtmlFieldName, (bool)result, new { @class = "ace" + css + " " + widthType.GetName() }).ToHtmlString();
            var inputs = inputUI.Split(new string[] { "/><" }, StringSplitOptions.RemoveEmptyEntries);
            inputUI = "<label" + marginTop + ">" + inputs.ElementAt(0) + "/><span class=\"lbl\"></span></label><" + inputs.ElementAt(1);

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", htmlHelper.Raw(inputUI) }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", controlClass } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString CustomSwitches(this HtmlHelper htmlHelper, string title, string name, bool? value, SwitchesStyle style, WidthType widthType = WidthType.none, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8")
        {
            var result = value != null ? value : false;

            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(name);

            var titleUI = htmlHelper.Label("", title, new { @class = "control-label no-padding-right " + labelClass });
            //StringBuilder inputSB = new StringBuilder();
            string css = "";
            string marginTop = "";
            if (style.GetName() != "")
            {
                css = " ace-switch " + style.GetName();
            }
            else
            {
                marginTop = " style=\"margin-top:5px\"";
            }

            string inputUI = htmlHelper.CheckBox(fullHtmlFieldName, (bool)result, new { @class = "ace" + css + " " + widthType.GetName() }).ToHtmlString();
            var inputs = inputUI.Split(new string[] { "/><" }, StringSplitOptions.RemoveEmptyEntries);
            inputUI = "<label" + marginTop + ">" + inputs.ElementAt(0) + "/><span class=\"lbl\"></span></label><" + inputs.ElementAt(1);

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputUI.ToString()) }, { "titleUI", titleUI }, { "controlClass", controlClass } };
            //ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", htmlHelper.Raw(inputUI) }, { "titleUI", titleUI }, { "controlClass", controlClass } };

            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }
        #endregion        
        
        #region  CustomTextboxFor
        public static MvcHtmlString CustomTextboxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string placeHolder = null, string inputMask = null, WidthType widthType = WidthType.span12, bool IsCheckValidation = true, Dictionary<string, object> htmlAttributeInput = null, string stringFormat = null, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8")
        {
            return CustomTextboxFor(htmlHelper, expression, placeHolder, inputMask, widthType, IsCheckValidation, htmlAttributeInput, stringFormat, labelClass, controlClass, "", "");
        }

        public static MvcHtmlString CustomTextboxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string placeHolder, string inputMask, WidthType widthType, bool IsCheckValidation, Dictionary<string, object> htmlAttributeInput, string stringFormat, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8", string groupCSS = "", string groupID = "")
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : string.Empty;
            result = result == null ? string.Empty : result;
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = labelClass });


            var attribute = new Dictionary<string, object>();
            attribute.Add("placeholder", placeHolder);
            attribute.Add("class", widthType.GetName());
            attribute.Add("maskformat", inputMask);
            attribute.Add("autocomplete", "off");

            if (htmlAttributeInput != null)
            {
                attribute = htmlAttributeInput.Concat(attribute.Where(x => !htmlAttributeInput.Keys.Contains(x.Key))).ToDictionary(c => c.Key, c => c.Value);
            }

            //var inputUI = htmlHelper.TextBoxFor(expression, new { @placeholder = placeHolder, @class = widthType.GetName(), @readonly = true, @maskformat = inputMask, style = style });
            string inputUI = htmlHelper.TextBoxFor(expression, stringFormat, attribute).ToHtmlString();

            //remove attribute don't want to show with the value set by 'remove'
            for (int i = 0; i < attribute.Count; i++)
            {
                var item = attribute.ElementAtOrDefault(i);
                if (item.Value != null)
                {
                    if (item.Value.ToString() == "remove")
                    {
                        string removeString = item.Key + "=\"" + item.Value + "\"";
                        inputUI = inputUI.Replace(removeString, "");
                    }
                }
            }

            StringBuilder inputUISB = new StringBuilder();
            inputUISB.Append(inputUI);

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputUISB.ToString()) }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", controlClass }, { "css", groupCSS }, { "id", groupID } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }
        #endregion

        #region  EditTextboxFor
        public static MvcHtmlString EditTextboxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : string.Empty;
            result = result == null ? string.Empty : result;
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            string stringFormat = null;
            string labelContainerClass = null;
            string valueContainerClass = null;
            string groupCSS = null;
            string groupID = null;

            var attribute = new Dictionary<string, object>();
            //attribute.Add("placeholder", placeHolder);
            //attribute.Add("class", widthType.GetName());
            //attribute.Add("maskformat", inputMask);
            //attribute.Add("autocomplete", "off");

            //if (htmlAttributes != null)
            //{
            //    attribute = htmlAttributes.Concat(attribute.Where(x => !htmlAttributes.Keys.Contains(x.Key))).ToDictionary(c => c.Key, c => c.Value);
            //}

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label " + labelContainerClass });
            string inputUI = htmlHelper.TextBoxFor(expression, stringFormat, htmlAttributes).ToHtmlString();

            StringBuilder inputUISB = new StringBuilder();
            inputUISB.Append(inputUI);

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputUISB.ToString()) }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", valueContainerClass }, { "css", groupCSS }, { "id", groupID } };

            viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));

            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }
        #endregion

        #region CustomTextAreaFor/CkEditorFor
        public static MvcHtmlString CustomTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string placeHolder, WidthType widthType, bool IsCheckValidation = true, Dictionary<string, object> htmlAttributeInput = null, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8", string groupCSS = "", string groupID = "")
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : string.Empty;
            result = result == null ? string.Empty : result;
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = labelClass + " control-label-textarea" });

            var attribute = new Dictionary<string, object>();
            attribute.Add("placeholder", placeHolder);
            attribute.Add("class", widthType.GetName());

            if (htmlAttributeInput != null)
            {
                attribute = htmlAttributeInput.Concat(attribute.Where(x => !htmlAttributeInput.Keys.Contains(x.Key))).ToDictionary(c => c.Key, c => c.Value);
            }

            string inputUI = htmlHelper.TextAreaFor(expression, attribute).ToHtmlString();

            //remove attribute don't want to show with the value set by 'remove'
            for (int i = 0; i < attribute.Count; i++)
            {
                var item = attribute.ElementAtOrDefault(i);
                if (item.Value != null)
                {
                    if (item.Value.ToString() == "remove")
                    {
                        string removeString = item.Key + "=\"" + item.Value + "\"";
                        inputUI = inputUI.Replace(removeString, "");
                    }
                }
            }

            StringBuilder inputUISB = new StringBuilder();
            inputUISB.Append(inputUI);

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputUISB.ToString()) }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", controlClass }, { "css", groupCSS }, { "id", groupID } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString CkEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string placeHolder, bool IsCheckValidation, string labelClass = "control-label no-padding-right col-sm-3", string controlClass = "col-lg-7 col-md-8 col-sm-8", string groupCSS = "", string groupID = "")
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : string.Empty;
            result = result != null ? result : string.Empty;
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = labelClass });

            var inputUI = htmlHelper.TextAreaFor(expression, new { @placeholder = placeHolder, @class = EnumHelper<WidthType>.GetDisplayValue(WidthType.span) });
            var defaultValueUI = htmlHelper.Hidden("defaultValue_" + fullHtmlFieldName, result != null ? result.ToString() : string.Empty, new { @name = "defaultValue_" + fullHtmlFieldName });
            var CkEditorUI = htmlHelper.Partial("_CkEditorPartial", new ViewDataDictionary { { "inputUI", inputUI }, { "inputName", fullHtmlFieldName }, { "defaultValueUI", defaultValueUI } });

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", CkEditorUI }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "widthType", EnumHelper<WidthType>.GetDisplayValue(WidthType.span) }, { "controlClass", controlClass } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }
        #endregion

        #region CustomDropDownListFor
        public static MvcHtmlString CustomDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, Dictionary<string, object> htmlAttributeInput = null, bool IsCheckValidation = true, string emptyAlertString = null, DropdownListStyle style = DropdownListStyle.DropdownListStyleChosen, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8")
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : null;
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = labelClass });

            //thêm các thuộc tính nếu có truyền vào
            var attribute = new Dictionary<string, object>();
            if (htmlAttributeInput != null)
                attribute = htmlAttributeInput.Concat(attribute.Where(x => !htmlAttributeInput.Keys.Contains(x.Key))).ToDictionary(c => c.Key, c => c.Value);

            StringBuilder inputSB = new StringBuilder();
            if (style == DropdownListStyle.DropdownListStyleChosen)
            {
                inputSB.Append(htmlHelper.Hidden("defaultValue" + fullHtmlFieldName, result).ToHtmlString());
            }

            //tạo input dropdow list control
            MvcHtmlString inputUI = htmlHelper.DropDownListFor(expression, selectList, emptyAlertString, attribute);

            inputSB.Append(inputUI.ToHtmlString());


            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputSB.ToString()) }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", controlClass } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString CustomDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<object> selectObjectList, string fieldValue, string fieldText , Dictionary<string, object> htmlAttributeInput = null, bool IsCheckValidation = true, string emptyAlertString = null, DropdownListStyle style = DropdownListStyle.DropdownListStyleChosen, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8")
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            bool isSelectListItem = false;
            if (selectObjectList != null)
            {
                object first = selectObjectList.FirstOrDefault();
                if (first != null)
                {
                    Type typeOfSelectItem = first.GetType();
                    if (typeOfSelectItem.Name == "SelectListItem")
                    {
                        selectList = ((IEnumerable<SelectListItem>)selectObjectList).ToList();
                        isSelectListItem = true;
                    }
                }
            }

            if (isSelectListItem == false)
            {
                foreach (var item in selectObjectList)
                {
                    Type objectType = item.GetType();
                    if (objectType.GetProperty(fieldValue) == null || objectType.GetProperty(fieldValue) == null)
                    {
                        selectList.Add(new SelectListItem { Text = "Sai tên field!", Value = "" });
                        break;
                    }

                    string value = objectType.GetProperty(fieldValue).GetValue(item) != null ? objectType.GetProperty(fieldValue).GetValue(item).ToString() : "";
                    string text = objectType.GetProperty(fieldText).GetValue(item) != null ? objectType.GetProperty(fieldText).GetValue(item).ToString() : "";

                    selectList.Add(new SelectListItem { Text = text, Value = value });
                }
            }

            return CustomDropDownListFor<TModel, TProperty>(htmlHelper, expression, selectList, htmlAttributeInput, IsCheckValidation, emptyAlertString, style, labelClass, controlClass);
        }

        public static MvcHtmlString CustomDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, WidthType widthType, bool IsCheckValidation = true, string emptyAlertString = null, DropdownListStyle style = DropdownListStyle.DropdownListStyleChosen, bool isMultiple = false, bool disabled = false, string labelClass = "col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8")
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : null;
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label no-padding-right " + labelClass });

            StringBuilder inputSB = new StringBuilder();
            if (style == DropdownListStyle.DropdownListStyleChosen)
            {
                inputSB.Append(htmlHelper.Hidden("defaultValue" + fullHtmlFieldName, result).ToHtmlString());
            }
            MvcHtmlString inputUI = null;

            if (!isMultiple)
            {
                if (disabled)
                    inputUI = htmlHelper.DropDownListFor(expression, selectList, emptyAlertString, new { disabled = "disabled", @class = widthType.GetName() + " " + EnumHelper<DropdownListStyle>.GetDisplayValue(style) });
                else
                    inputUI = htmlHelper.DropDownListFor(expression, selectList, emptyAlertString, new { @class = widthType.GetName() + " " + style.GetName() });
            }
            else
            {
                //inputUI = htmlHelper.DropDownListFor(expression, selectList, emptyAlertString, new { @class = EnumHelper<WidthType>.GetDisplayValue(WidthType.span) + " " + EnumHelper<DropdownListStyle>.GetDisplayValue(style), @multiple = true });

                inputUI = htmlHelper.ListBoxFor(expression, selectList, new { @class = EnumHelper<WidthType>.GetDisplayValue(WidthType.span) + " " + EnumHelper<DropdownListStyle>.GetDisplayValue(style), @multiple = true });
            }

            inputSB.Append(inputUI.ToHtmlString());


            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputSB.ToString()) }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "widthType", EnumHelper<WidthType>.GetDisplayValue(widthType) }, { "controlClass", controlClass } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString CustomDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string TableName, string DisplayField, string ValueField, WidthType widthType, bool IsCheckValidation = true, string emptyAlertString = null, DropdownListStyle style = DropdownListStyle.DropdownListStyleChosen, bool isMultiple = false, bool disabled = false, string labelClass = "col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8")
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : null;
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label no-padding-right " + labelClass });

            StringBuilder inputSB = new StringBuilder();
            if (style == DropdownListStyle.DropdownListStyleChosen)
            {
                inputSB.Append(htmlHelper.Hidden("defaultValue" + fullHtmlFieldName, result).ToHtmlString());
            }
            MvcHtmlString inputUI = null;

            IEnumerable<SelectListItem> selectList = SelectListHelper.GetSelectList(TableName, DisplayField, ValueField, null);

            if (!isMultiple)
            {
                if (disabled)
                    inputUI = htmlHelper.DropDownListFor(expression, selectList, emptyAlertString, new { disabled = "disabled", @class = widthType.GetName() + " " + EnumHelper<DropdownListStyle>.GetDisplayValue(style) });
                else
                    inputUI = htmlHelper.DropDownListFor(expression, selectList, emptyAlertString, new { @class = widthType.GetName() + " " + style.GetName() });
            }
            else
            {
                //inputUI = htmlHelper.DropDownListFor(expression, selectList, emptyAlertString, new { @class = EnumHelper<WidthType>.GetDisplayValue(WidthType.span) + " " + EnumHelper<DropdownListStyle>.GetDisplayValue(style), @multiple = true });

                inputUI = htmlHelper.ListBoxFor(expression, selectList, new { @class = EnumHelper<WidthType>.GetDisplayValue(WidthType.span) + " " + EnumHelper<DropdownListStyle>.GetDisplayValue(style), @multiple = true });
            }

            inputSB.Append(inputUI.ToHtmlString());


            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputSB.ToString()) }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "widthType", EnumHelper<WidthType>.GetDisplayValue(widthType) }, { "controlClass", controlClass } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString CustomDropDownListSuggestTableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<Object> optionList,
            string valueField, string textField, string dataValueField,
            Dictionary<string, object> radComboBoxConfig,
            Dictionary<string, object> htmlAttributeInput = null, bool IsCheckValidation = true, string emptyLabel = null, 
            string labelClass = "control-label no-padding-right col-sm-3", string controlClass = "col-lg-7 col-md-8 col-sm-8", string groupAttr = "class=form-group")
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : null;
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            
            //ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, htmlHelper.ViewData);

            var lableUI = htmlHelper.LabelFor(expression, new { @class = labelClass });

            //thêm các thuộc tính nếu có truyền vào
            var attribute = new Dictionary<string, object>() { {"class", "col-sm-12"} };
            if (htmlAttributeInput != null)
                attribute = htmlAttributeInput.Concat(attribute.Where(x => !htmlAttributeInput.Keys.Contains(x.Key))).ToDictionary(c => c.Key, c => c.Value);

            var inputTemp = htmlHelper.TextBoxFor(expression, attribute).ToString();

            Regex regex = new Regex(@"(\bdata-\S+=""*"")[^""]*");
            var matchCollection = regex.Matches(inputTemp);
            string validationRule = string.Join(" \"", from Match match in matchCollection select match.Value) + "\"";

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary 
            { 
                {"validation", validationRule},
                {"optionList", optionList},
                {"valueField", valueField },
                {"textField", textField },
                {"dataValueField", dataValueField},
                { "inputID", fullHtmlFieldName },
                { "inputValue", result },
                {"emptyLabel", string.IsNullOrEmpty(emptyLabel) ? "" : emptyLabel},
                { "inputAttr", attribute }, 
                { "lableUI", lableUI }, { "groupAttr", groupAttr }, 
                { "controlClass", controlClass },
            };

            //nếu cấu hình của Rad combo box khác null
            if (radComboBoxConfig != null)
            {
                foreach (var item in radComboBoxConfig)
                    viewDataDictionary[item.Key] = item.Value;
            }
            

            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputDropdownSuggestTableTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString CustomDropDownList(this HtmlHelper htmlHelper, string IdControl, string title, string name, string value, IEnumerable<SelectListItem> selectList, WidthType widthType, DropdownListStyle style, string emptyAlertString, bool isMultiple, bool isGenerateDefaultValue, bool IsCheckValidation)
        {

            var titleUI = htmlHelper.Label(IdControl, title, new { @class = "control-label  no-padding-right " });

            StringBuilder inputSB = new StringBuilder();
            if (style == DropdownListStyle.DropdownListStyleChosen && isGenerateDefaultValue)
            {
                inputSB.Append(htmlHelper.Hidden("defaultValue" + name, value).ToHtmlString());
            }
            MvcHtmlString inputUI = null;

            if (!isMultiple)
            {
                for (int i = 0; i < selectList.Count(); i++)
                {
                    var item = selectList.ElementAt(i);
                    if (item.Value == value)
                    {
                        selectList.ElementAt(i).Selected = true;
                    }
                }
                SelectList SelectList = new System.Web.Mvc.SelectList(selectList, "Value", "Text", value);
                inputUI = htmlHelper.DropDownList(name, SelectList, emptyAlertString, new Dictionary<string, object> { { "class", EnumHelper<WidthType>.GetDisplayValue(WidthType.span) + " " + EnumHelper<DropdownListStyle>.GetDisplayValue(style)}, { "id", IdControl } });
            }
            else
            {
                inputUI = htmlHelper.ListBox(name, selectList, new { @class = EnumHelper<WidthType>.GetDisplayValue(WidthType.span) + " " + EnumHelper<DropdownListStyle>.GetDisplayValue(style), @multiple = true });
            }

            inputSB.Append(inputUI.ToHtmlString());

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputSB.ToString()) }, { "titleUI", titleUI }, { "widthType", EnumHelper<WidthType>.GetDisplayValue(widthType) }};

            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessage(IdControl,null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString CustomDropDownList(this HtmlHelper htmlHelper, string title, string name, string value, IEnumerable<SelectListItem> selectList, WidthType widthType, DropdownListStyle style, string emptyAlertString, bool isMultiple, bool isGenerateDefaultValue, bool IsCheckValidation = false)
        {
            return CustomDropDownList(htmlHelper, name, title, name, value, selectList, widthType, style, emptyAlertString, isMultiple, isGenerateDefaultValue, IsCheckValidation);
        }

        public static MvcHtmlString CustomUserDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, WidthType widthType, bool IsCheckValidation = true, string emptyAlertString = null, DropdownListStyle style = DropdownListStyle.DropdownListStyleChosen, bool isMultiple = false, bool disabled = false)
        {
            //Get data for SelectList
            IEnumerable<SelectListItem> selectList = SelectListHelper.GetSelectList_User(null);

            //Create html control
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : null;
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label no-padding-right " });

            StringBuilder inputSB = new StringBuilder();
            if (style == DropdownListStyle.DropdownListStyleChosen)
            {
                inputSB.Append(htmlHelper.Hidden("defaultValue" + fullHtmlFieldName, result).ToHtmlString());
            }
            MvcHtmlString inputUI = null;

            if (!isMultiple)
            {
                if (disabled)
                    inputUI = htmlHelper.DropDownListFor(expression, selectList, emptyAlertString, new { disabled = "disabled", @class = widthType.GetName() + " " + EnumHelper<DropdownListStyle>.GetDisplayValue(style) });
                else
                    inputUI = htmlHelper.DropDownListFor(expression, selectList, emptyAlertString, new { @class = widthType.GetName() + " " + style.GetName() });
            }
            else
            {
                //inputUI = htmlHelper.DropDownListFor(expression, selectList, emptyAlertString, new { @class = EnumHelper<WidthType>.GetDisplayValue(WidthType.span) + " " + EnumHelper<DropdownListStyle>.GetDisplayValue(style), @multiple = true });

                inputUI = htmlHelper.ListBoxFor(expression, selectList, new { @class = EnumHelper<WidthType>.GetDisplayValue(WidthType.span) + " " + EnumHelper<DropdownListStyle>.GetDisplayValue(style), @multiple = true });
            }

            inputSB.Append(inputUI.ToHtmlString());


            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputSB.ToString()) }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "widthType", EnumHelper<WidthType>.GetDisplayValue(widthType) } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }
        #endregion

        #region Spinner
        public static MvcHtmlString SpinnerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int max, int min, int step, SpinnerStyle spinnerStyle, bool IsCheckValidation, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8")
        {
            SpinnerModel spinner = new SpinnerModel();
            spinner.HtmlInputControl = htmlHelper.TextBoxFor(expression);
            spinner.min = min;
            spinner.max = max;
            spinner.step = step;
            spinner.style = spinnerStyle;
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : 0;
            result = result != null ? result : 0;
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);
            if (result != null && result is int)
            {
                spinner.value = (int)result;
            }
            else if (result != null && result is double)
            {
                spinner.value = Convert.ToInt32((double)result);
            }
            else if (result != null && result is decimal)
            {
                spinner.value = Convert.ToInt32((decimal)result);
            }

            spinner.name = fullHtmlFieldName;

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label no-padding-right " + labelClass });

            var inputUI = htmlHelper.Partial("_SpinnerPartial", spinner);

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", inputUI }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", controlClass } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString Spinner(this HtmlHelper htmlHelper, string name, int value, string label, int max, int min, int step, SpinnerStyle spinnerStyle, object htmlAttributes)
        {
            SpinnerModel spinner = new SpinnerModel();
            spinner.min = min;
            spinner.max = max;
            spinner.step = step;
            spinner.style = spinnerStyle;
            spinner.value = value;
            spinner.name = name;
            spinner.HtmlInputControl = htmlHelper.TextBox(name, value);

            MvcHtmlString titleUI = label != null ? htmlHelper.Label(label, new { @class = "control-label" }) : null;
            var inputUI = htmlHelper.Partial("_SpinnerPartial", spinner, new ViewDataDictionary { { "htmlAttributes", htmlAttributes } });

            if (label == null)
                return inputUI;
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", inputUI }, { "titleUI", titleUI } };
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }
        #endregion                

        #region FileUploadFor
        public static MvcHtmlString FileUploadFor<TModel, TProperty, FProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expressionPath, Expression<Func<TModel, FProperty>> expressionFile)
        {
            var pathValue = ((LambdaExpression)expressionPath).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            pathValue = pathValue != null ? pathValue : string.Empty;
            var expressionPathName = ExpressionHelper.GetExpressionText(expressionPath);

            var fileValue = ((LambdaExpression)expressionFile).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            var expressionFileName = ExpressionHelper.GetExpressionText(expressionFile);


            var titleUI = htmlHelper.LabelFor(expressionPath, new { @class = "control-label" });

            StringBuilder inputSB = new StringBuilder();
            inputSB.Append("<label>" + htmlHelper.DisplayFor(expressionPath).ToHtmlString() + "</label>");

            var inputFile = htmlHelper.TextBoxFor(expressionFile, new { @type = "file" });//, @accept = "image/*,video/*"
            inputSB.Append(htmlHelper.Partial("_FileUploadPartial", new ViewDataDictionary { { "inputFile", inputFile }, { "inputName", expressionFileName }, { "filter", null } }));

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputSB.ToString()) }, { "titleUI", titleUI }, { "widthType", EnumHelper<WidthType>.GetDisplayValue(WidthType.span6) } };
            viewDataDictionary.Add("validUI", MvcHtmlString.Create(htmlHelper.ValidationMessageFor(expressionFile, null, new { @class = "help-inline" }).ToString() + htmlHelper.ValidationMessageFor(expressionPath, null, new { @class = "help-inline" }).ToString()));
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString FileUploadFor<TModel, TProperty, FProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expressionPath, Expression<Func<TModel, FProperty>> expressionFile, WidthType widthType)
        {
            var pathValue = ((LambdaExpression)expressionPath).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            pathValue = pathValue != null ? pathValue : string.Empty;
            var expressionPathName = ExpressionHelper.GetExpressionText(expressionPath);

            var fileValue = ((LambdaExpression)expressionFile).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            var expressionFileName = ExpressionHelper.GetExpressionText(expressionFile);


            var titleUI = htmlHelper.LabelFor(expressionPath, new { @class = "control-label no-padding-right col-sm-3" });

            StringBuilder inputSB = new StringBuilder();
            //inputSB.Append("<label>" + htmlHelper.DisplayFor(expressionPath).ToHtmlString() + "</label>");

            var inputFile = htmlHelper.TextBoxFor(expressionFile, new { @type = "file" });//, @accept = "image/*,video/*"
            inputSB.Append(htmlHelper.Partial("_FileUploadPartial", new ViewDataDictionary { { "inputFile", inputFile }, { "inputName", expressionFileName }, { "filter", null } }));

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputSB.ToString()) }, { "titleUI", titleUI }, { "controlClass", "col-sm-9" } };
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString FileUploadFor<TModel, TProperty, FProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expressionPath, Expression<Func<TModel, FProperty>> expressionFile, string filterType)
        {
            var pathValue = ((LambdaExpression)expressionPath).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            pathValue = pathValue != null ? pathValue : string.Empty;
            var expressionPathName = ExpressionHelper.GetExpressionText(expressionPath);

            var fileValue = ((LambdaExpression)expressionFile).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            var expressionFileName = ExpressionHelper.GetExpressionText(expressionFile);


            var titleUI = htmlHelper.LabelFor(expressionPath, new { @class = "control-label" });

            StringBuilder inputSB = new StringBuilder();
            inputSB.Append("<label>" + htmlHelper.DisplayFor(expressionPath).ToHtmlString() + "</label>");

            var inputFile = htmlHelper.TextBoxFor(expressionFile, new { @type = "file" });//, @accept = "image/*,video/*"
            inputSB.Append(htmlHelper.Partial("_FileUploadPartial", new ViewDataDictionary { { "inputFile", inputFile }, { "inputName", expressionFileName }, { "filter", filterType } }));

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", MvcHtmlString.Create(inputSB.ToString()) }, { "titleUI", titleUI }, { "widthType", EnumHelper<WidthType>.GetDisplayValue(WidthType.span) } };
            viewDataDictionary.Add("validUI", MvcHtmlString.Create(htmlHelper.ValidationMessageFor(expressionFile, null, new { @class = "help-inline" }).ToString() + htmlHelper.ValidationMessageFor(expressionPath, null, new { @class = "help-inline" }).ToString()));

            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }
        #endregion

        #region Others
        public static MvcHtmlString CustomRadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, SelectList selectList, bool IsCheckValidation)
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : null;
            //var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label no-padding-right col-sm-3" });

            var inputUI = "";
            for (int i = 0; i < selectList.Count(); i++)
            {
                inputUI += "<label style=\"margin-right:5px; margin-top: 4px;\">" + htmlHelper.RadioButtonFor(expression, selectList.ElementAt(i).Value) + string.Format("<span class=\"help-inline lbl\">{0}</span>", selectList.ElementAt(i).Text) + "</label>";
            }

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "titleUI", titleUI }, { "inputUI", htmlHelper.Raw(inputUI) }, { "controlClass", "control-value col-sm-9" } };

            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }

            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, SelectList selectList, bool IsCheckValidation)
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : false;
            result = result != null ? result : false;
            string fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label no-padding-right col-sm-3" });

            StringBuilder inputUISB = new StringBuilder();
            foreach (var item in selectList)
            {
                var id = string.Format("{0}_{1}", fullHtmlFieldName, item.Value);

                var cb = new TagBuilder("input");
                cb.MergeAttribute("class", "ace");
                cb.MergeAttribute("type", "checkbox");
                cb.MergeAttribute("name", fullHtmlFieldName);
                cb.MergeAttribute("value", @item.Value);
                cb.MergeAttribute("id", id);

                if (result != null && item.Value != "" && ((string[])result).Contains(item.Value))
                {
                    cb.MergeAttribute("checked", "checked");
                }

                inputUISB.Append("<p><label>" + cb + "<span class=\"lbl\"> " + item.Text + "</span></label></p>");
            }

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", htmlHelper.Raw(inputUISB.ToString()) }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", "col-sm-9" } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString CustomDisplayFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string DisplayText, bool IsCheckValidation, string labelClass = "control-label col-lg-5 col-md-4 col-sm-4", string controlClass = "col-lg-7 col-md-8 col-sm-8")
        {
            var result = htmlHelper.ViewData.Model != null ? ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model) : string.Empty;
            result = result != null ? result : string.Empty;
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label no-padding-right " + labelClass, @style = "margin-top:-5px;" });

            var inputUI = string.Empty;
            if (DisplayText == null || DisplayText == string.Empty)
            {
                inputUI += htmlHelper.DisplayFor(expression);
            }
            else
            {
                inputUI += htmlHelper.Label(fullHtmlFieldName, DisplayText, null);
            }
            //var hiddenUI = htmlHelper.HiddenFor(expression);
            //inputUI += hiddenUI;

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "titleUI", titleUI }, { "inputUI", htmlHelper.Raw(inputUI) }, { "controlClass", controlClass } };

            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression));
            }

            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString ActionLinkWithImage(this HtmlHelper htmlHelper, string ImagePath, string ActionName, string ControllerName, object RouterValue)
        {
            TagBuilder imageTag = new TagBuilder("img");
            ImagePath = ImagePath.Replace("~/", "/");
            imageTag.Attributes.Add("src", ImagePath);

            TagBuilder linkTag = new TagBuilder("a");
            UrlHelper url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            linkTag.Attributes.Add("href", url.Action(ActionName, ControllerName, RouterValue));
            linkTag.InnerHtml = imageTag.ToString(TagRenderMode.SelfClosing);

            return MvcHtmlString.Create(linkTag.ToString(TagRenderMode.Normal));
        }
        #endregion

        #region DropDownList for Enumerations

        private static readonly Dictionary<Enum, string> NameCache = new Dictionary<Enum, string>();
        public static string GetName(this Enum type)
        {
            if (NameCache.ContainsKey(type))
                return NameCache[type];

            var enumType = type.GetType();
            var info = enumType.GetField(type.ToString());
            if (info == null)
                return string.Empty;

            var displayAttribute = info.GetCustomAttributes(false).OfType<DisplayAttribute>().FirstOrDefault();
            var value = string.Empty;
            if (displayAttribute != null)
                value = displayAttribute.GetName() ?? string.Empty;

            NameCache.Add(type, value);

            return value;
        }

        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.DropDownListForEnum(expression, null, null);
        }
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            //return htmlHelper.DropDownListForEnum(expression, new RouteValueDictionary(htmlAttributes));
            return htmlHelper.DropDownListForEnum(expression, null, htmlAttributes, false);
        }
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.DropDownListForEnum(expression, null, htmlAttributes);
        }
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string optionLabel)
        {
            return htmlHelper.DropDownListForEnum(expression, optionLabel, null);
        }
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string optionLabel, object htmlAttributes)
        {
            return htmlHelper.DropDownListForEnum(expression, optionLabel, null, true);
        }

        public static MvcHtmlString DropDownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string optionLabel, object htmlAttributes, bool IsCheckValidation)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentNullException("expression");

            var selectedValue = string.Empty;
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (metadata.Model != null)
            {
                selectedValue = metadata.Model.ToString();
            }
            var enumType = Nullable.GetUnderlyingType(member.Type) ?? member.Type;

            var listItems = new List<SelectListItem>();
            foreach (var name in Enum.GetNames(enumType))
            {
                var type = Enum.Parse(enumType, name) as Enum;
                listItems.Add(new SelectListItem
                {
                    Text = type.GetName(),
                    Value = name,
                    Selected = name == selectedValue
                });
            }

            var inputUI = htmlHelper.DropDownListFor(expression, listItems, optionLabel, htmlAttributes);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label" });

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", inputUI }, { "titleUI", titleUI } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression));
            }
            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        #endregion

        #region Script Helper

        // -------
        public static MvcHtmlString ScriptTop_CkEditor(this HtmlHelper htmlHelper)
        {
            return MvcHtmlString.Create(Scripts.Render("~/Scripts/ckeditor/ckeditor.js").ToHtmlString());
        }

        public static MvcHtmlString ScriptBottom_CkEditor(this HtmlHelper htmlHelper)
        {
            return htmlHelper.Partial("_ScriptBottom_CkEditorPartial");
        }

        // -------
        public static MvcHtmlString ScriptBottom_CompareDate_Validation(this HtmlHelper htmlHelper)
        {
            return htmlHelper.Partial("_ScriptBottom_CompareDatePartial");
        }

        // -------
        public static MvcHtmlString ScriptBottom_ValidationMvc(this HtmlHelper htmlHelper)
        {
            return htmlHelper.Partial("_ScriptBottom_ValidationMvcPartial");
        }

        // -------
        public static MvcHtmlString ScriptTop_ChosenStyle(this HtmlHelper htmlHelper)
        {
            string url = UrlHelper.GenerateContentUrl("~/assets/css/chosen.min.css", htmlHelper.ViewContext.HttpContext);
            return MvcHtmlString.Create("<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" />");
        }

        public static MvcHtmlString ScriptBottom_RequireEitherInput(this HtmlHelper htmlHelper)
        {
            return htmlHelper.Partial("_ScriptBottom_RequireEitherInputPartial");
        }

        public static MvcHtmlString ScriptBottom_ChosenStyle(this HtmlHelper htmlHelper)
        {
            return htmlHelper.Partial("_ScriptBottom_ChosenStylePartial");
        }

        public static MvcHtmlString ScriptBottom_InputMask(this HtmlHelper htmlHelper)
        {
            return htmlHelper.Partial("_ScriptBottom_InputMaskPartial");
        }

        // -------
        public static MvcHtmlString ScriptTop_DatePicker(this HtmlHelper htmlHelper)
        {
            string url = UrlHelper.GenerateContentUrl("~/Content/assets/css/datepicker.css", htmlHelper.ViewContext.HttpContext);
            return MvcHtmlString.Create("<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" />");
        }

        public static MvcHtmlString ScriptBottom_DatePicker(this HtmlHelper htmlHelper, string format)
        {
            return htmlHelper.Partial("_ScriptBottom_DatePickerPartial", new ViewDataDictionary { { "format", format } });
        }
        //============
        public static MvcHtmlString ScriptTop_TimePicker(this HtmlHelper htmlHelper)
        {
            string url = UrlHelper.GenerateContentUrl("~/Content/assets/css/bootstrap-timepicker.css", htmlHelper.ViewContext.HttpContext);
            return MvcHtmlString.Create("<link href=\"" + url + "\" rel=\"stylesheet\" type=\"text/css\" />");
        }
        public static MvcHtmlString ScriptBottom_TimePicker(this HtmlHelper htmlHelper, string ControlId)
        {
            string script = "<script src=\"@Url.Content(\"~/Content/assets/js/date-time/bootstrap-timepicker.min.js\")\">";
            script += "</script>";
            script += "<script type=\"text/javascript\">";
            script += "$(function () { $('#" + ControlId + "').timepicker({ minuteStep: 1, showSeconds: true, showMeridian: false }) });";
            script += "</script>";
            return MvcHtmlString.Create(script);
        }

        #endregion

        #region Button Helper

        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string action, string controller, string area, string text, ButtonType buttonType, ButtonColor buttonColor, ButtonSize buttonSize, bool haveBorder, IconType icon, IconSize iconSize, bool iconOnRight, object htmlAttributes)
        {
            if (Erp.BackOffice.Filters.SecurityFilter.AccessRight(action, controller, area))
            {
                if (iconOnRight)
                {
                    return Button(htmlHelper, text, buttonType, buttonColor, buttonSize, haveBorder, false, null, IconType.None, IconSize.Default, icon, iconSize, htmlAttributes);
                }
                return Button(htmlHelper, text, buttonType, buttonColor, buttonSize, haveBorder, false, null, icon, iconSize, IconType.None, IconSize.Default, htmlAttributes);
            }
            else
            {
                return MvcHtmlString.Create(null);
            }
        }

        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string text, ButtonType buttonType, ButtonColor buttonColor, ButtonSize buttonSize, bool haveBorder, IconType icon, IconSize iconSize, bool iconOnRight, object htmlAttributes)
        {
            if (iconOnRight)
            {
                return Button(htmlHelper, text, buttonType, buttonColor, buttonSize, haveBorder, false, null, IconType.None, IconSize.Default, icon, iconSize, htmlAttributes);
            }
            return Button(htmlHelper, text, buttonType, buttonColor, buttonSize, haveBorder, false, null, icon, iconSize, IconType.None, IconSize.Default, htmlAttributes);
        }
        
        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string text, ButtonType buttonType, ButtonColor buttonColor, ButtonSize buttonSize, bool haveBorder, string cusomIcon, IconSize iconSize, bool iconOnRight, object htmlAttributes)
        {
            return Button(htmlHelper, text, buttonType, buttonColor, buttonSize, haveBorder, true, cusomIcon, IconType.None, IconSize.Default, IconType.None, iconSize, htmlAttributes);
        }

        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string text, ButtonType buttonType, ButtonColor buttonColor, ButtonSize buttonSize, bool haveBorder, object htmlAttributes)
        {
            return Button(htmlHelper, text, buttonType, buttonColor, buttonSize, haveBorder, false, null, IconType.None, IconSize.Default, IconType.None, IconSize.Default, htmlAttributes);
        }

        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string text, ButtonType buttonType, ButtonColor buttonColor, ButtonSize buttonSize, bool haveBorder, bool isCustomIcon, string cusomIcon, IconType iconLeft, IconSize iconLeftSize, IconType iconRight, IconSize iconRightSize, object htmlAttributes)
        {
            IDictionary<string, object> htmlAttributesDic = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            StringBuilder sb = new StringBuilder();

            TagBuilder button = new TagBuilder(EnumHelper<ButtonType>.GetDisplayValue(buttonType));
            string buttonClass = "btn";
            buttonClass += " " + EnumHelper<ButtonColor>.GetDisplayValue(buttonColor);
            buttonClass += " " + EnumHelper<ButtonSize>.GetDisplayValue(buttonSize);

            if (!haveBorder)
            {
                buttonClass += " no-border";
            }

            if (htmlAttributesDic.ContainsKey("class"))
            {
                buttonClass += " " + htmlAttributesDic["class"].ToString();
            }
            button.MergeAttribute("class", buttonClass);

            foreach (var key in htmlAttributesDic.Keys)
            {
                if (key != "class")
                {
                    button.MergeAttribute(key, htmlAttributesDic[key].ToString());
                }
            }

            if (iconLeft != IconType.None)
            {
                TagBuilder icon = new TagBuilder("i");
                string iconClass = string.Empty;
                iconClass += " " + EnumHelper<IconType>.GetDisplayValue(iconLeft);

                iconClass += " " + EnumHelper<IconSize>.GetDisplayValue(iconLeftSize);
                icon.MergeAttribute("class", iconClass);
                button.InnerHtml += icon;
            }

            if (isCustomIcon)
            {
                TagBuilder icon = new TagBuilder("i");
                string iconClass = string.Empty;
                iconClass += " " + cusomIcon;

                iconClass += " " + EnumHelper<IconSize>.GetDisplayValue(iconLeftSize);
                icon.MergeAttribute("class", iconClass);
                button.InnerHtml += icon;
            }

            button.InnerHtml += text;

            if (iconRight != IconType.None)
            {
                TagBuilder icon = new TagBuilder("i");
                string iconClass = string.Empty;
                iconClass += " " + EnumHelper<IconType>.GetDisplayValue(iconRight);
                iconClass += " " + EnumHelper<IconSize>.GetDisplayValue(iconRightSize);
                iconClass += " " + "icon-on-right";
                icon.MergeAttribute("class", iconClass);
                button.InnerHtml += icon;
            }

            return MvcHtmlString.Create(button.ToString() + " ");
        }

        public static MvcHtmlString ButtonWithIconOnly(this HtmlHelper htmlHelper, ButtonType buttonType, ButtonColor buttonColor, ButtonSize buttonSize, bool haveBorder, IconType iconLeft, IconSize iconLeftSize, object htmlAttributes)
        {
            IDictionary<string, object> htmlAttributesDic = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            StringBuilder sb = new StringBuilder();

            TagBuilder button = new TagBuilder(EnumHelper<ButtonType>.GetDisplayValue(buttonType));
            string buttonClass = "btn";
            buttonClass += " " + EnumHelper<ButtonColor>.GetDisplayValue(buttonColor);
            buttonClass += " " + EnumHelper<ButtonSize>.GetDisplayValue(buttonSize);

            if (!haveBorder)
            {
                buttonClass += " no-border";
            }

            if (htmlAttributesDic.ContainsKey("class"))
            {
                buttonClass += " " + htmlAttributesDic["class"].ToString();
            }
            button.MergeAttribute("class", buttonClass);

            foreach (var key in htmlAttributesDic.Keys)
            {
                if (key != "class")
                {
                    button.MergeAttribute(key, htmlAttributesDic[key].ToString());
                }
            }

            if (iconLeft != IconType.None)
            {
                TagBuilder icon = new TagBuilder("i");
                string iconClass = "icon-only";
                iconClass += " " + EnumHelper<IconType>.GetDisplayValue(iconLeft);
                iconClass += " " + EnumHelper<IconSize>.GetDisplayValue(iconLeftSize);
                icon.MergeAttribute("class", iconClass);
                button.InnerHtml += icon;
            }

            return MvcHtmlString.Create(button.ToString() + " ");
        }

        #endregion

        #region DetailViewItemFor
        public static MvcHtmlString DetailViewItemDateTimeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool showTime = false, string css = null, string style = null, string label1 = "col-xs-2", string text1 = "col-xs-10")
        {
            return DetailViewItemFor2(htmlHelper, expression, false, showTime, css, style, null, label1, text1);
        }

        public static MvcHtmlString DetailViewItemFor2<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string css = null, string style = null, string label1 = "col-xs-2", string text1 = "col-xs-10")
        {
            return DetailViewItemFor2(htmlHelper, expression, false, false, css, style, null, label1, text1);
        }

        public static MvcHtmlString DetailViewItemFor2<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool isThousandsFormat, string css = null, string style = null, string label1 = "col-xs-2", string text1 = "col-xs-10")
        {
            return DetailViewItemFor2(htmlHelper, expression, isThousandsFormat, false, css, style, null, label1, text1);
        }

        public static MvcHtmlString DetailViewItemFor2<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool isThousandsFormat, bool showTime, string css, string style, string labelText = null, string label1 = "col-xs-2", string text1 = "col-xs-10")
        {
            var result = ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            var sConlumnName = ExpressionHelper.GetExpressionText(expression);
            var sLabelText = htmlHelper.LabelFor(expression);
            string sDisplayText = "";

            if (result != null)
            {
                switch (result.GetType().FullName)
                {
                    case "System.DateTime":
                        if ((DateTime)result != DateTime.MinValue)
                        {
                            if (showTime)
                            {
                                sDisplayText = ((DateTime)result).ToString("dd/MM/yyyy HH:mm");
                            }
                            else
                            {
                                sDisplayText = ((DateTime)result).ToString("dd/MM/yyyy");
                            }
                        }
                        break;
                    case "System.Boolean":
                        if (sConlumnName == "Gender")
                        {
                            sDisplayText = result == null ? "" : (((bool)result) ? "Nữ" : "Nam");
                        }
                        else
                        {
                            bool bChecked = ((bool)result);
                            string inputUI = "<label><input type=\"checkbox\" " + (bChecked ? "checked=\"checked\"" : "") + " class=\"ace\" disabled=\"disabled\" /><span class=\"lbl\"></span></label>";

                            sDisplayText = inputUI;
                        }
                        break;
                    case "System.Int32":
                    case "System.Int64":
                    case "System.Float":
                    case "System.Double":
                    case "System.Decimal":
                        if (isThousandsFormat)
                        {
                            sDisplayText = Helpers.Common.PhanCachHangNgan(result.ToString());
                        }
                        else
                        {
                            sDisplayText = result.ToString();
                        }
                        break;
                    default:
                        sDisplayText = result.ToString();
                        break;
                }
            }
            //item.Append("<div class=\"col-xs-4 control-label\">" + sLabelText + "</div>");
            StringBuilder item = new StringBuilder();
            item.Append("<div class=\"" + label1 + " control-label\">" + sLabelText + "</div>");
            item.Append("<div class=\"" + text1 + " control-value" + css + "\" style=\"" + style + "\">" + sDisplayText + "</div>");

            return MvcHtmlString.Create(item.ToString());
        }

        public static MvcHtmlString DetailViewItem(this HtmlHelper htmlHelper, object property, string propertyName, string label, string css, string style, string label1 = "col-xs-2", string text1 = "col-xs-10")
        {
            string sDisplayText = "";
            if (property != null)
            {
                switch (property.GetType().FullName)
                {
                    case "System.DateTime":
                        if ((DateTime)property != DateTime.MinValue)
                            sDisplayText = ((DateTime)property).ToString("dd/MM/yyyy");
                        break;
                    case "System.Boolean":
                        if (propertyName == "Gender")
                        {
                            sDisplayText = property == null ? "" : (((bool)property) ? "Nữ" : "Nam");
                        }
                        else
                        {
                            bool bChecked = ((bool)property);
                            string inputUI = "<label><input type=\"checkbox\" " + (bChecked ? "checked=\"checked\"" : "") + " class=\"ace\" disabled=\"disabled\" /><span class=\"lbl\"></span></label>";

                            sDisplayText = inputUI;
                        }
                        break;
                    default:
                        sDisplayText = property.ToString();
                        break;
                }
            }
            StringBuilder item = new StringBuilder();
            item.Append("<div class=\"" + label1 + " control-label\"><label>" + label + "</label></div>");
            item.Append("<div class=\"" + text1 + " control-value" + css + "\" style=\"" + style + "\">" + sDisplayText + "</div>");

            return MvcHtmlString.Create(item.ToString());
        }
        #endregion

        #region Edit view
        public static MvcHtmlString EditViewLayoutFor<TModel>(this HtmlHelper<TModel> htmlHelper, string ProcessEntity)
        {
            object model = htmlHelper.ViewData.Model;
            List<Erp.Domain.Crm.Entities.ProcessStage> ListProcessStage = null;
            var ListEditViewField = new List<Administration.Models.EditViewField>();

            //For process
            Erp.Domain.Crm.Repositories.ProcessRepository processRepository = new Domain.Crm.Repositories.ProcessRepository(new Domain.Crm.ErpCrmDbContext());
            Erp.Domain.Crm.Repositories.ProcessStageRepository processStageRepository = new Domain.Crm.Repositories.ProcessStageRepository(new Domain.Crm.ErpCrmDbContext());
            Erp.Domain.Crm.Repositories.ProcessStepRepository processStepRepository = new Domain.Crm.Repositories.ProcessStepRepository(new Domain.Crm.ErpCrmDbContext());

            var process = processRepository.GetAllProcess().Where(item => item.Category == "business_process_flow" && item.DataSource == ProcessEntity && item.IsActive.Value).FirstOrDefault();
            if (process != null)
            {
                ListProcessStage = processStageRepository.GetAllProcessStage()
                        .Where(item => item.ProcessId == process.Id).OrderBy(m => m.OrderNo).ToList();

                int activeStageId = ListProcessStage[0].Id;

                var q = processStepRepository.GetAllProcessStep()
                    .Where(item => item.StageId == activeStageId).OrderBy(m => m.OrderNo).ToList();


                foreach (var item in q)
                {
                    var editViewField = new Administration.Models.EditViewField();
                    editViewField.LabelName = item.Name;
                    editViewField.FieldName = item.StepValue;
                    editViewField.IsRequired = item.IsRequired;
                    editViewField.EditControl = item.EditControl;
                    editViewField.OrderNo = item.OrderNo;

                    ListEditViewField.Add(editViewField);
                }
            }

            StringBuilder editViewLayout = new StringBuilder();
            foreach (var item in ListEditViewField)
            {
                ParameterExpression fieldName = Expression.Parameter(model.GetType(), item.FieldName);
                Expression fieldExpr = Expression.PropertyOrField(Expression.Constant(model), item.FieldName);
                Expression<Func<TModel, object>> exp = Expression.Lambda<Func<TModel, object>>(fieldExpr, fieldName);

                switch (item.EditControl)
                {
                    case "TextBox":
                        break;
                    default:
                        editViewLayout.Append(htmlHelper.CustomTextboxFor(exp));
                        break;
                }
            }

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { 
                { "editViewLayout", MvcHtmlString.Create(editViewLayout.ToString()) },
                { "listProcessStage",  ListProcessStage}
            };
            return htmlHelper.Partial("_ProcessEditViewPartial", viewDataDictionary);
        }
        #endregion

        #region Module popup
 
        public static MvcHtmlString ModulePopupFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string moduleName, string displayText, bool showAddButton = true, bool IsCheckValidation = false, bool bReadOnly = false, string addClassForLabelField = null, string addClassForControlField = null,string actionName=null)
        {
            var result = ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label " + addClassForLabelField });

            MvcHtmlString inputUI = htmlHelper.HiddenFor(expression);

            var inputUIPartial = htmlHelper.Partial("_ModulePopupPartial", new ViewDataDictionary { { "inputUI", inputUI }, { "inputName", fullHtmlFieldName }, { "moduleName", moduleName }, { "displayText", displayText }, { "readOnly", bReadOnly }, { "showAddButton", showAddButton }, { "IsCheckValidation", IsCheckValidation }, { "actionName", actionName } });

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", inputUIPartial }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", addClassForControlField } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }

            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }

        public static MvcHtmlString ModulePopupFor2<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string moduleName, string displayText, bool showAddButton = true, bool IsCheckValidation = false, bool bReadOnly = false, string addClassForLabelField = null, string addClassForControlField = null, string actionName = null,string module_list=null)
        {
            //module_list dùng để truyền tên module gọi module popup này lên, nếu có lọc dữ liệu hiển thị trong popup thì truyền thêm cái này và xử lý dữ liệu hiển thị trong hàm get của popup.
            var result = ((LambdaExpression)expression).Compile().DynamicInvoke(htmlHelper.ViewData.Model);
            var fullHtmlFieldName = ExpressionHelper.GetExpressionText(expression);

            var titleUI = htmlHelper.LabelFor(expression, new { @class = "control-label " + addClassForLabelField });

            MvcHtmlString inputUI = htmlHelper.HiddenFor(expression);

            var inputUIPartial = htmlHelper.Partial("_ModulePopupPartial", new ViewDataDictionary { { "inputUI", inputUI }, { "inputName", fullHtmlFieldName }, { "moduleName", moduleName }, { "displayText", displayText }, { "readOnly", bReadOnly }, { "showAddButton", showAddButton }, { "IsCheckValidation", IsCheckValidation }, { "actionName", actionName },{ "module_list", module_list } });

            var validErrorClass = htmlHelper.ValidationErrorFor(expression, "error");
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary { { "inputUI", inputUIPartial }, { "titleUI", titleUI }, { "validError", validErrorClass }, { "controlClass", addClassForControlField } };
            if (IsCheckValidation)
            {
                viewDataDictionary.Add("validUI", htmlHelper.ValidationMessageFor(expression, null, new { @class = "help-inline" }));
            }

            return htmlHelper.Partial("_InputFieldTemplatePartial", viewDataDictionary);
        }
        #endregion
    }
} 