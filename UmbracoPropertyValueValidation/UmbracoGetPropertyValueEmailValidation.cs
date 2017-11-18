using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UmbracoPropertyValueValidation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class UmbracoGetPropertyValueEmailValidation : ValidationAttribute, IClientValidatable
    {
        private readonly string _errorMessageKey;

        public UmbracoGetPropertyValueEmailValidation(string errorMessageKey)
        {
            _errorMessageKey    = errorMessageKey;
            ErrorMessage        = UmbracoPropertyValidationHelper.GetPropertyValueItem(_errorMessageKey);
        }

        public override bool IsValid(Object value)
        {
            return !string.IsNullOrEmpty((string) value);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage = UmbracoPropertyValidationHelper.GetPropertyValueItem(_errorMessageKey);

            var error   = FormatErrorMessage(metadata.DisplayName);
            var rule    = new ModelClientValidationRule
            {
                ErrorMessage    = error,
                ValidationType  = "email"
            };

            yield return rule;
        }
    }
}
