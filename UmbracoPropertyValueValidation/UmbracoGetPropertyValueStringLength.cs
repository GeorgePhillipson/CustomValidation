using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;

namespace UmbracoPropertyValueValidation
{
    public class UmbracoGetPropertyValueStringLength : StringLengthAttribute, IClientValidatable
    {
        private readonly string _errorMessageKey;
        private readonly string _errorFieldName;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Needed if javascript disabled
            ErrorMessage        = UmbracoPropertyValidationHelper.GetPropertyValueItem(_errorMessageKey);
            var error           = FormatErrorMessage(validationContext.DisplayName);
            string errorMessage = StringBuilderMessageStringLength.Message(error, MinimumLength, MaximumLength);

            if (IsValid(value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(_errorFieldName + " " + errorMessage + " " + "Javascript disabled");
        }

        public UmbracoGetPropertyValueStringLength(string fieldName, string errorMessageKey, int maximumLength) : base(maximumLength)
        {
            _errorMessageKey    = errorMessageKey;
            _errorFieldName     = fieldName;

        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage        = UmbracoPropertyValidationHelper.GetPropertyValueItem(_errorMessageKey);
            var error           = FormatErrorMessage(metadata.DisplayName);
            string errorMessage = StringBuilderMessageStringLength.Message(error, MinimumLength, MaximumLength);

            var rule    = new ModelClientValidationStringLengthRule($"{_errorFieldName} - {errorMessage}", MinimumLength, MaximumLength);
   
            yield return rule;
        }
    }

    public static class StringBuilderMessageStringLength
    {
        public static string Message(string error, int minLength,int maxLength)
        {
            StringBuilder sb = new StringBuilder(error);
            sb.Replace("[Min]", minLength.ToString());
            sb.Replace("[Max]", maxLength.ToString());
            string errorMessage = sb.ToString();

            return errorMessage;
        }
    }
}
