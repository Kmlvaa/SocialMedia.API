using Microsoft.AspNetCore.Mvc;

namespace SocialMediaAPİ.Controllers.Base
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
