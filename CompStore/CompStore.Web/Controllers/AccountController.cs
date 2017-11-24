using CompStore.Web.Infrastructure;
using CompStore.Web.Models;
using System.Web.Mvc;

namespace CompStore.Web.Controllers
{
    public class AccountController : Controller
    {
        IAuthenticate authenticate;
        public AccountController(IAuthenticate auth)
        {
            authenticate = auth;
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if(authenticate.Authenticate(model.Login, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин/пароль");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}