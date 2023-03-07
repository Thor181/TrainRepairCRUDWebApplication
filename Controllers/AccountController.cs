using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TrainRepairCRUDWebApplication.Models;
using TrainRepairCRUDWebApplication.Models.Authorization;

namespace TrainRepairCRUDWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly TrainRepairDbContext _dbContext;
        public AccountController(TrainRepairDbContext context)
        {
            _dbContext = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Authorization authorization)
        {
            if (ModelState.IsValid)
            {
                Authorization user = new Authorization();
                try
                {
                    user = await _dbContext.Users.Include(x => x.IdRoleNavigation)
                                                 .Where(x => x.Login == authorization.Login && x.Password == authorization.Password)
                                                 .Select(x => new Authorization()
                                                 {
                                                    Id = x.Id,
                                                    Login = x.Login,
                                                    Password = x.Password,
                                                    RoleName = x.IdRoleNavigation!.Role1
                                                 }).FirstAsync();
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Некорректные логин и/или пароль");
                }
                
                if (user != null)
                {
                    await Authenticate(user.Login!, user.RoleName!);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и/или пароль");
            }
            return View(authorization);
        }

        private async Task Authenticate(string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        public IActionResult AccessDenied()
        {
            return Content("Вы не имеет доступа к данной странице");
        }
    }
}
