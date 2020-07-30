namespace Restaraunt.UI.Controllers
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Mvc;
    using Restaraunt.Core;
    using Restaraunt.Entities;

    #endregion

    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = NHibernateHelperCore.GetSingleOrDefault<User>(r => r.Email == model.Email);
            if (user == null)
            {
                user = NHibernateHelperCore.SaveOrUpdate(new User { Email = model.Email, Password = model.Password, Role = Role.User, Name = model.Name });

                await Authenticate(user);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = NHibernateHelperCore.GetSingleOrDefault<User>(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                await Authenticate(user);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");

            return View(model);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
                         {
                                 new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                                 new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
                         };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                                        ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult PersonalArea()
        {
            var id = int.Parse(User.Identity.Name);

            var user = NHibernateHelperCore.GetSingleOrDefault<User>(a => a.Id == id);
            var userVm = new UserVM
                         {
                                 Id = user.Id,
                                 Name = user.Name,
                                 Role = Role.User,
                                 Email = user.Email,
                                 Password = user.Password
                         };

            return View(userVm);
        }

        [HttpPost]
        public IActionResult PersonalArea(UserVM userVm)
        {
            if (!ModelState.IsValid)
                return View(userVm);

            var user = NHibernateHelperCore.GetSingleOrDefault<User>(a => a.Id == userVm.Id);
            user.Name = userVm.Name;
            user.Email = userVm.Email;
            user.Password = userVm.Password;
            user.Role = userVm.Role;
            NHibernateHelperCore.SaveOrUpdate<User>(user);
            return RedirectToAction("PersonalArea", "Account");
        }
    }
}