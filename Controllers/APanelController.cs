using Attractions.Dbcontext;
using Attractions.Models;
using Attractions.Models.dboModels;
using Attractions.Models.dtoModels;
using Attractions.Models.SupportModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Attractions.Controllers
{
    public class APanelController : Controller
    {
        private const string KeyLogin = "email";
        private const string KeyPassword = "Password";

        private readonly ILogger<APanelController> _logger;
        private readonly dbContext _context;
        public IActionResult Index() => View();
        public APanelController(ILogger<APanelController> logger, dbContext context)
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
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        private async Task<UserAuthorizationWrapper> Authorization(string email, string password)
        {
            if(email == "" && password == "") return new UserAuthorizationWrapper();
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
