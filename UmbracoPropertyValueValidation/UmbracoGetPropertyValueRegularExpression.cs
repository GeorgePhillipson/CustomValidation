using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace UmbracoPropertyValueValidation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class UmbracoGetPropertyValueRegularExpression : RegularExpressionAttribute, IClientValidatable
    {
        private readonly string _errorMessageKey;
        private readonly string _errorFieldName;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Needed if javascript disabled
            ErrorMessage = UmbracoPropertyValidationHelper.GetPropertyValueItem(_errorMessageKey);
            var removeRegex = ExtractStringBetweenTags.ExtractString(Pattern);
            
            var error           = FormatErrorMessage(validationContext.DisplayName);

            string errorMessage = StringBuilderMessage.Message(error, removeRegex);

            if (IsValid(value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(_errorFieldName + " " + errorMessage + " " + "Javascript disabled");
        }
        

        public UmbracoGetPropertyValueRegularExpression(string fieldName, string errorMessageKey, string pattern) : base(pattern)
        {
            _errorMessageKey    = errorMessageKey;
            _errorFieldName     = fieldName;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoPropertyValidationHelper.GetPropertyValueItem(_errorMessageKey);

            var removeRegex = ExtractStringBetweenTags.ExtractString(Pattern);

            var error           = FormatErrorMessage(metadata.DisplayName);
            string errorMessage = StringBuilderMessage.Message(error, removeRegex);

            var rule = new ModelClientValidationRule
            {
                ErrorMessage    = $"{_errorFieldName} - {errorMessage}",
                ValidationType  = "regex"
            };

            rule.ValidationParameters.Add("pattern", Pattern);

            yield return rule;
        }
    }

    /// <summary>
    /// Extract the regex pattern between the tags
    /// </summary>
    public static class ExtractStringBetweenTags
    {
        public static string ExtractString(string regPattern)
        {

            string removeRegex = regPattern.Replace("^[",string.Empty)
                                           .Replace("]+$",string.Empty)
                                           .Replace("\\r\\n", string.Empty)
                                           .Replace("\\n", string.Empty);
            return removeRegex;
        }
    }

    public static class StringBuilderMessage
    {
        public static string Message(string error,string removeRegex)
        {
            StringBuilder sb = new StringBuilder(error);
            sb.Replace("[RegexPattern]", removeRegex);
            string errorMessage = sb.ToString();

            return errorMessage;
        }
    }
}
