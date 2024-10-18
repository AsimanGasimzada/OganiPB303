using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Ogani.BLL.UI.ViewModel;

namespace Ogani.MVC.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        public async Task<ViewViewComponentResult> InvokeAsync()
        {
            string FacebookLink = "http://Facebook.com";
            string InstagramLink = "http://Instagram.com";
            var footerViewModel = new FooterViewModel
            {
                FacebookLink = FacebookLink,
                InstagramLink = InstagramLink,

            };
            return View(footerViewModel);
        }
    }
}
