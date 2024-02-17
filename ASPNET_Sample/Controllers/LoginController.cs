using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Sample.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
