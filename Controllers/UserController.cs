using Attractions.Dbcontext;
using Attractions.Models.dboModels;
using Attractions.Models.SupportModels;
using Attractions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Attractions.Models.dtoModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography.Xml;

namespace Attractions.Controllers
{
    public class UserController : Controller
    {
        private const string KeyLogin = "email";
        private const string KeyPassword = "Password";

        private readonly ILogger<UserController> _logger;
        private readonly dbContext _context;
        public async Task<IActionResult> Account()
        {
            string email = Request.Cookies[KeyLogin] ?? "";
            string password = Request.Cookies[KeyPassword] ?? "";

            User? _user = await _context.Users.Where(u => u.Email == email && u.Password == password && u.UserType).FirstOrDefaultAsync();
            if (_user == null)
            {
                return LogOut();
            }
            else
            {
                ViewData["name"] = _user.LastName;
            }
            return View();
        }
        public UserController(ILogger<UserController> logger, dbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Authorization(dtoAuthorizationUser userModel)
        {
            string email = Request.Cookies[KeyLogin] ?? "";
            string password = Request.Cookies[KeyPassword] ?? "";

            UserAuthorizationWrapper authorizationWrapper;

            //Авторизация из куков
            authorizationWrapper = await Authorization(email, password);

            //Авторизация через переданный логин и пароль
            if (authorizationWrapper.IsError || authorizationWrapper.IsWarning)
                authorizationWrapper = await Authorization(userModel.Login ?? "", userModel.Password ?? "");

            if (authorizationWrapper.IsError)
            {
                ViewData["Authorization_Error"] = authorizationWrapper.Message;
                return View(userModel);
            }
            else if (!authorizationWrapper.IsWarning)
            {
                SaveCooKie(authorizationWrapper.User);
                return RedirectToAction("Account", new { controller = "User", action = "Account" });
            }
            return View();
        }
        public IActionResult LogOut()
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append(KeyLogin, "", options);
            Response.Cookies.Append(KeyPassword, "", options);
            return RedirectToAction("Authorization");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        private async Task<UserAuthorizationWrapper> Authorization(string email, string password)
        {
            if (email == "" && password == "") return new UserAuthorizationWrapper();
            UserAuthorizationWrapper wrapper = new();
            if (email == "") wrapper.SetErrorMessage("Не указан логин!!!");
            if (password == "") wrapper.SetErrorMessage("Не указан пароль!!!");

            if (!wrapper.IsError)
            {
                User? _u = await _context.Users.Where(u => u.Password == password && u.Email == email).SingleOrDefaultAsync();

                if (_u == null) wrapper.SetErrorMessage("Пользователь не найден!!!!");
                else wrapper = new UserAuthorizationWrapper(_u);

            }
            return wrapper;
        }
        public void SaveCooKie(User _user)
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append(KeyLogin, _user.Email, cookieOptions);
            Response.Cookies.Append(KeyPassword, _user.Password, cookieOptions);
        }
    }
}
