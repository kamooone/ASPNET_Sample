using Microsoft.AspNetCore.Mvc;
using ASPNET_Sample.ViewModel;

namespace ASPNET_Sample.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("ログイン画面表示");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (Validate.ValidateUser(model.Mail, model.Password))
            {
                _logger.LogInformation("ログインボタンを押してログイン成功");
                return RedirectToAction("Index", "Member");
            }
            else
            {
                _logger.LogInformation("ログインボタンを押してログイン失敗");
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View(model);
            }
        }
    }

    public class Validate
    {
        public static bool ValidateUser(string email, string password)
        {
            // ユーザーの認証ロジックをここに実装する
            // 例えば、データベースや外部サービスを使用してユーザーを認証する
            // この例では、単純化のためにユーザー名とパスワードがハードコーディングされています
            return email == "test@example.com" && password == "password";
        }
    }
}
