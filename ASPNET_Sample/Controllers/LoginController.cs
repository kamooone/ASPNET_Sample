using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASPNET_Sample.ViewModel;
using ASPNET_Sample.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using System.Security.Cryptography;

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
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == model.UserName);

            if (user != null && PasswordMatches(model.Password, user.PasswordHash))
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

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // パスワードをバイト配列に変換してハッシュ化
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // ハッシュ化されたバイト配列をBase64文字列に変換して返す
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool PasswordMatches(string inputPassword, string hashedPassword)
        {
            // 入力されたパスワードをハッシュ化
            string hashedInputPassword = HashPassword(inputPassword);

            // ハッシュ化されたパスワードが一致するかどうかを確認
            return hashedPassword == hashedInputPassword;
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // ログアウト処理
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("ログインアウト完了");
            return RedirectToAction("Index", "Home");
        }
    }
}
