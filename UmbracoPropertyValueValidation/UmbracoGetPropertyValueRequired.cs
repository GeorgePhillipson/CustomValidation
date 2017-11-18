using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UmbracoPropertyValueValidation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class UmbracoGetPropertyValueRequired : RequiredAttribute, IClientValidatable
    {
        private readonly string _errorMessageKey;

        public UmbracoGetPropertyValueRequired(string errorMessageKey)
        {
            _errorMessageKey    = errorMessageKey;
            ErrorMessage        = UmbracoPropertyValidationHelper.GetPropertyValueItem(_errorMessageKey);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ErrorMessage    = UmbracoPropertyValidationHelper.GetPropertyValueItem(_errorMessageKey);
            var error       = FormatErrorMessage(metadata.DisplayName);
            var rule        = new ModelClientValidationRequiredRule(error);

            yield return rule;
        }
    }
}
