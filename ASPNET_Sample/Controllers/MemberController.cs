using Microsoft.AspNetCore.Mvc;

namespace ASPNET_Sample.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
