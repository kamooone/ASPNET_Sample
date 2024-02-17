using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASPNET_Sample.ViewModel;
using ASPNET_Sample.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_Sample.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ASPNET_SampleContext _context;

        public LoginController(ILogger<LoginController> logger, ASPNET_SampleContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("ログイン画面表示");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == model.UserName && u.PasswordHash == model.Password);
            if (user != null)
            {
                _logger.LogInformation("ログインボタンを押してログイン成功");
                return RedirectToAction("Index", "Member");
            }
            else
            {
                _logger.LogInformation("ログインボタンを押してログイン失敗");
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }
        }
    }
}
