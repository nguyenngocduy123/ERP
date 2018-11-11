using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Erp.BackOffice.Helpers
{
    public sealed class DateStartAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateStart = (DateTime)value;
            // Meeting must start in the future time.
            return (dateStart > DateTime.Now);
        }
    }

    public sealed class DateEndAttribute : ValidationAttribute, IClientValidatable
    {
        public string DateStartProperty { get; set; }
        public override bool IsValid(object value)
        {
            // Get Value of the DateStart property
            string dateStartString = HttpContext.Current.Request[DateStartProperty];
            if (dateStartString == null)
                return false;

            DateTime dateEnd = (DateTime)value;
            DateTime dateStart = DateTime.Parse(dateStartString);

            // Meeting start time must be before the end time
            return dateStart < dateEnd;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule mcvrTwo = new ModelClientValidationRule();
            mcvrTwo.ValidationType = "checktodate";
            mcvrTwo.ErrorMessage = "ToDate have to be greater FromDate.";//DateStartProperty
            mcvrTwo.ValidationParameters.Add("fromdatename", DateStartProperty);
            return new List<ModelClientValidationRule> { mcvrTwo };
        }
    }
    /*
    public enum CompareType
    {
        LessThan = 0,
        LessThanOrEqual = 1,
        Equal = 2,
        GreaterThan = 3,
        GreaterThanOrEqual = 4,
    }

    public enum DataType
    {
        int_data = 0,
        float_data = 1,
        double_data = 2,
        date_data = 3,
        //datetime_data = 4,
    }*/

    /*public class CompareWithValueAttribute : ValidationAttribute, IClientValidatable
    {
        public string FormatString { get; set; }

        public DataType DataType { get; set; }

        public DateTime WithValue { get; set; }

        public CompareType CompareType { get; set; }



        private bool CheckCompareDate(CompareType compareType, DateTime mainValue, DateTime comapareValue)
        {
            switch (compareType)
            {
                case CompareType.LessThan:
                    break;
                case CompareType.LessThanOrEqual:
                    break;
                case CompareType.Equal:
                    break;
                case CompareType.GreaterThan:
                    break;
                case CompareType.GreaterThanOrEqual:
                    break;
                default:
                    break;
            }
            return false;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime)
            {
                switch (CompareType)
                {
                    case CompareType.LessThan:
                        if ((DateTime)value < WithValue)
                            return ValidationResult.Success;
                        break;
                    case CompareType.LessThanOrEqual:
                        if ((DateTime)value <= WithValue)
                            return ValidationResult.Success;
                        break;
                    case CompareType.Equal:
                        if ((DateTime)value == WithValue)
                            return ValidationResult.Success;
                        break;
                    case CompareType.GreaterThan:
                        if ((DateTime)value > WithValue)
                            return ValidationResult.Success;
                        break;
                    case CompareType.GreaterThanOrEqual:
                        if ((DateTime)value >= WithValue)
                            return ValidationResult.Success;
                        break;
                    default:
                        break;
                }
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "comparewithvalue",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };

            modelClientValidationRule.ValidationParameters.Add("withvalue", WithValue.ToString(FormatString));

            modelClientValidationRule.ValidationParameters.Add("comparetype", (int)CompareType);
            modelClientValidationRule.ValidationParameters.Add("datatype", (int)DataType);
            modelClientValidationRule.ValidationParameters.Add("formatstring", FormatString);

            yield return modelClientValidationRule;
        }
    }*/


    /// <summary>
    ///  if the current date less than OtherDate, the validator return Success.
    ///  in other words, if current date is greater of equal to otherDate, the validator return error.
    /// </summary>
    public class CompareDateLessThanAttribute : ValidationAttribute, IClientValidatable
    {
        public string OtherDatePropertyName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var OtherPropertyValue = GetValue<DateTime>(validationContext.ObjectInstance, OtherDatePropertyName);
            if ((DateTime)value > OtherPropertyValue)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }

        private static T GetValue<T>(object objectInstance, string propertyName)
        {
            if (objectInstance == null) throw new ArgumentNullException("objectInstance");
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException("propertyName");

            var propertyInfo = objectInstance.GetType().GetProperty(propertyName);
            return (T)propertyInfo.GetValue(objectInstance);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "comparedatelessthan",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };
            modelClientValidationRule.ValidationParameters.Add("dateprop", OtherDatePropertyName);
            yield return modelClientValidationRule;
        }
    }

    public class CompareDateAttribute : ValidationAttribute, IClientValidatable
    {
        public string OtherDatePropertyName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var OtherPropertyValue = GetValue<string>(validationContext.ObjectInstance, OtherDatePropertyName).Replace(":", "");
            if (int.Parse(OtherPropertyValue) > int.Parse(value.ToString().Replace(":", "")))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }

        private static T GetValue<T>(object objectInstance, string propertyName)
        {
            if (objectInstance == null) throw new ArgumentNullException("objectInstance");
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException("propertyName");

            var propertyInfo = objectInstance.GetType().GetProperty(propertyName);
            return (T)propertyInfo.GetValue(objectInstance);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "comparedate",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };
            modelClientValidationRule.ValidationParameters.Add("dateprop", OtherDatePropertyName);
            yield return modelClientValidationRule;
        }
    }

    #region MyRegion

    public class RequiredUrlWithTypeAndIsCheckedAttibute : ValidationAttribute, IClientValidatable
    {
        public string TypePropertyName { get; set; }
        public string IsCheckedPropertyName { get; set; }

        private static T GetValue<T>(object objectInstance, string propertyName)
        {
            if (objectInstance == null) throw new ArgumentNullException("objectInstance");
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException("propertyName");

            var propertyInfo = objectInstance.GetType().GetProperty(propertyName);
            return (T)propertyInfo.GetValue(objectInstance);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "requiredurlwithtypeandischecked",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };
            modelClientValidationRule.ValidationParameters.Add("typeprop", TypePropertyName);
            modelClientValidationRule.ValidationParameters.Add("ischeckedprop", IsCheckedPropertyName);
            yield return modelClientValidationRule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var TypePropertyValue = GetValue<int>(validationContext.ObjectInstance, TypePropertyName);

            var IsCheckedPropertyValue = GetValue<bool>(validationContext.ObjectInstance, IsCheckedPropertyName);

            //if (TypePropertyValue == (int)Erp.Domain.Entities.QuizQuestionType.Image || TypePropertyValue == (int)Erp.Domain.Entities.QuizQuestionType.Video)
            //{
            //    if (IsCheckedPropertyValue)
            //    {
            //        if ((string)value == string.Empty || (string)value == null)
            //            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            //    }
            //}

            if (IsCheckedPropertyValue)
            {
                if ((string)value == string.Empty || (string)value == null)
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }

    public class RequiredSourceFileWithTypeAndIsCheckedAttribute : ValidationAttribute, IClientValidatable
    {
        public string TypePropertyName { get; set; }
        public string IsCheckedPropertyName { get; set; }

        private static T GetValue<T>(object objectInstance, string propertyName)
        {
            if (objectInstance == null) throw new ArgumentNullException("objectInstance");
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException("propertyName");

            var propertyInfo = objectInstance.GetType().GetProperty(propertyName);
            return (T)propertyInfo.GetValue(objectInstance);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "requiredsourcefilewithtypeandischecked",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };
            modelClientValidationRule.ValidationParameters.Add("typeprop", TypePropertyName);
            modelClientValidationRule.ValidationParameters.Add("ischeckedprop", IsCheckedPropertyName);
            yield return modelClientValidationRule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var TypePropertyValue = GetValue<int>(validationContext.ObjectInstance, TypePropertyName);

            var IsCheckedPropertyValue = GetValue<bool>(validationContext.ObjectInstance, IsCheckedPropertyName);

            return ValidationResult.Success;
        }
    }

    public class MaxLengthSourceFileWithTypeAndIsCheckedAttribute : ValidationAttribute, IClientValidatable
    {
        public string TypePropertyName { get; set; }
        public string IsCheckedPropertyName { get; set; }

        private static T GetValue<T>(object objectInstance, string propertyName)
        {
            if (objectInstance == null) throw new ArgumentNullException("objectInstance");
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException("propertyName");

            var propertyInfo = objectInstance.GetType().GetProperty(propertyName);
            return (T)propertyInfo.GetValue(objectInstance);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "maxlengthsourcefilewithtypeandischecked",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };
            modelClientValidationRule.ValidationParameters.Add("typeprop", TypePropertyName);
            modelClientValidationRule.ValidationParameters.Add("ischeckedprop", IsCheckedPropertyName);
            yield return modelClientValidationRule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var TypePropertyValue = GetValue<int>(validationContext.ObjectInstance, TypePropertyName);

            var IsCheckedPropertyValue = GetValue<bool>(validationContext.ObjectInstance, IsCheckedPropertyName);

            return ValidationResult.Success;
        }
    }

    public class RequiredAnswerAttribute : ValidationAttribute, IClientValidatable
    {
        public string TypePropertyName { get; set; }

        private static T GetValue<T>(object objectInstance, string propertyName)
        {
            if (objectInstance == null) throw new ArgumentNullException("objectInstance");
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException("propertyName");

            var propertyInfo = objectInstance.GetType().GetProperty(propertyName);
            return (T)propertyInfo.GetValue(objectInstance);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "requiredanswer",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };
            modelClientValidationRule.ValidationParameters.Add("typeprop", TypePropertyName);
            //modelClientValidationRule.ValidationParameters.Add("ischeckedprop", IsCheckedPropertyName);
            yield return modelClientValidationRule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }
    }

    public class RequiredAnswerCorrectAttribute : ValidationAttribute, IClientValidatable
    {
        public string TypePropertyName { get; set; }

        private static T GetValue<T>(object objectInstance, string propertyName)
        {
            if (objectInstance == null) throw new ArgumentNullException("objectInstance");
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException("propertyName");

            var propertyInfo = objectInstance.GetType().GetProperty(propertyName);
            return (T)propertyInfo.GetValue(objectInstance);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "requiredanswercorrect",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };
            modelClientValidationRule.ValidationParameters.Add("typeprop", TypePropertyName);
            //modelClientValidationRule.ValidationParameters.Add("ischeckedprop", IsCheckedPropertyName);
            yield return modelClientValidationRule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }
    }

    public class RequiredPositionWithTypeAttribute : ValidationAttribute, IClientValidatable
    {
        public string TypePropertyName { get; set; }

        private static T GetValue<T>(object objectInstance, string propertyName)
        {
            if (objectInstance == null) throw new ArgumentNullException("objectInstance");
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException("propertyName");

            var propertyInfo = objectInstance.GetType().GetProperty(propertyName);
            return (T)propertyInfo.GetValue(objectInstance);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "requiredpositionwithtype",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };
            modelClientValidationRule.ValidationParameters.Add("typeprop", TypePropertyName);
            //modelClientValidationRule.ValidationParameters.Add("ischeckedprop", IsCheckedPropertyName);
            yield return modelClientValidationRule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }
    }

    #endregion

    public class RequireEitherInputField : ValidationAttribute, IClientValidatable
    {
        public string OtherPropertyName { get; set; }

        private static T GetValue<T>(object objectInstance, string propertyName)
        {
            if (objectInstance == null) throw new ArgumentNullException("objectInstance");
            if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullException("propertyName");

            var propertyInfo = objectInstance.GetType().GetProperty(propertyName);
            return (T)propertyInfo.GetValue(objectInstance);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherNames = OtherPropertyName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            var otherName = otherNames.Count() > 1 ? otherNames[otherNames.Count() - 1] : otherNames[0];

            var OtherPropertyValue = GetValue<string>(validationContext.ObjectInstance, otherName);

            if ((value != null && (string)value != string.Empty) || (OtherPropertyValue != null && OtherPropertyValue != string.Empty))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var modelClientValidationRule = new ModelClientValidationRule
            {
                ValidationType = "requireeitherinput",
                ErrorMessage = FormatErrorMessage(metadata.DisplayName)
            };
            modelClientValidationRule.ValidationParameters.Add("string64prop", OtherPropertyName);
            yield return modelClientValidationRule;
        }

    }

}