namespace FootballManager.Controllers
{
    using BasicWebServer.Server.Attributes;
    using BasicWebServer.Server.Controllers;
    using BasicWebServer.Server.HTTP;
    using FootballManager.Services.Contracts;
    using FootballManager.ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(Request request, IUserService _userService)
            : base(request)
        {
            this.userService = _userService;
        }

        public Response Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            var model = new
            {
                IsAuthenticated = User.IsAuthenticated,
            };

            return View(model);
        }

        [HttpPost]
        public Response Register(RegisterUserViewModel model)
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            var (isRegister, errors) = userService.Register(model);


            if (!isRegister)
            {
                return View(new { IsAuthenticated = User.IsAuthenticated }, "/Users/Register");
            }

            return Redirect("/Users/Login");
        }

        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View(new { IsAuthenticated = false });
        }

        [HttpPost]
        public Response Login(LoginUserViewModel model)
        {
            Request.Session.Clear();
            var (isLogin, errors, userId) = userService.Login(model);
            
            if (userId == null)
            {
                return View(new { IsAuthenticated = User.IsAuthenticated }, "/Users/Login");
            }

            SignIn(userId);

            CookieCollection cookies = new CookieCollection();
            cookies.Add(Session.SessionCookieName,
                Request.Session.Id);

            return Redirect("/Players/All");
        }

        [Authorize]
        public Response Logout()
        {
            SignOut();

            return Redirect("/");
        }
    }
}
