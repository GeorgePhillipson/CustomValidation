using System;
using System.ComponentModel;

namespace UmbracoPropertyValueValidation
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UmbracoGetPropertyValueDisplayName : DisplayNameAttribute
    {
        private readonly string _getPropertyValueKey;
        public UmbracoGetPropertyValueDisplayName(string propertyValueKey)
        {
            _getPropertyValueKey = propertyValueKey;
        }

        public override string DisplayName => UmbracoPropertyValidationHelper.GetPropertyValueItem(_getPropertyValueKey);
    }
}
