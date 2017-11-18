using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbracoPropertyValueValidation
{
    public static class UmbracoPropertyValidationHelper
    {
        public static string GetPropertyValueItem(string key)
        {
            UmbracoHelper umbracoHelper     = new UmbracoHelper(UmbracoContext.Current);
            IPublishedContent homePageNode  = umbracoHelper.TypedContentAtRoot().FirstOrDefault(x=>x.DocumentTypeAlias.ToLower() == "home");
            string errorMessage             = homePageNode.GetPropertyValue<string>(key);

           return errorMessage;
        }
    }
}
