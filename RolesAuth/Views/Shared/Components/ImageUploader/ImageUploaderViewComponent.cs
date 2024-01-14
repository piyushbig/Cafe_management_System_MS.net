using Microsoft.AspNetCore.Mvc;

namespace RolesAuth.Views.Shared.Components.ImageUploader
{
    public class ImageUploaderViewComponent : ViewComponent
    {
        public ImageUploaderViewComponent()
        {

        }
        public IViewComponentResult Invoke(string FieldName)
        {
            return View("Default", FieldName);
        }
    }
}