using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;
using UmbracoPropertyValueValidation;

namespace Web.Core.ViewModels
{
    public class ContactViewModel
    {
        [UmbracoGetPropertyValueDisplayName("labelFirstName")]
        [UmbracoGetPropertyValueRequired("firstNameRequired")]
        [UmbracoGetPropertyValueRegularExpression("First Name", "errorOnlyAlphaNumericCharactersAllowed", "^[0-9a-zA-Z .]+$")]
        [UmbracoGetPropertyValueStringLength("First Name", "errorMessageStringLength", 5, MinimumLength = 1)]
        public string FirstName { get; set; }

        [UmbracoGetPropertyValueDisplayName("labelLastName")]
        [UmbracoGetPropertyValueRequired("lastNameRequired")]
        [UmbracoGetPropertyValueRegularExpression("Last Name", "errorOnlyAlphaNumericCharactersAllowed", "^[0-9a-zA-Z .]+$")]
        [UmbracoGetPropertyValueStringLength("Last Name", "errorMessageStringLength", 30, MinimumLength = 1)]
        public string LastName { get; set; }

        public string FirstNamePlaceholder { get; set; }
        public string LastNamePlaceholder { get; set; }
    }

    //------------------The classes below should go into their own file-------------------------
    public static class UmbracoHelperHomePage
    {
        public static IPublishedContent HomePage()
        {
            UmbracoHelper umbracoHelper     = new UmbracoHelper(UmbracoContext.Current);
            IPublishedContent homePageNode  = umbracoHelper.TypedContentAtRoot().FirstOrDefault(x => x.DocumentTypeAlias.ToLower() == "home");
            return homePageNode;
        }
    }

    public static class PlaceholderFormText
    {
        public static string GetPropertyValueItem(string key)
        {
            var page                = UmbracoHelperHomePage.HomePage();
            string placeHolderText  = page.GetPropertyValue<string>(key);
            return placeHolderText;
        }
    }
}
