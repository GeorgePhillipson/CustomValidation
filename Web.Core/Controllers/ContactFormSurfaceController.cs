using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Web.Core.ViewModels;

namespace Web.Core.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        [ChildActionOnly]
        public ActionResult DisplayForm()
        {
            string firstNamePlaceholder = PlaceholderFormText.GetPropertyValueItem("firstNamePlaceholder");
            string lastNamePlaceholder  = PlaceholderFormText.GetPropertyValueItem("lastNamePlaceholder");

            var model = new ContactViewModel
            {
                FirstNamePlaceholder    = firstNamePlaceholder,
                LastNamePlaceholder     = lastNamePlaceholder
            };

            return PartialView("~/Views/Partials/pvContactForm.cshtml", model);
        }

        [HttpPost, ValidateInput(true)]
        [ValidateAntiForgeryToken]
        public ActionResult ContactForm(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            { 
                return CurrentUmbracoPage();
            }

            TempData["Message"] = "Done";
            return RedirectToCurrentUmbracoPage();
        }
    }
}
