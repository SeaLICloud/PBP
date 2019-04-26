using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PBP.Web.Common;
using PBP.Web.Models.Context;
using PBP.Web.Models.Domain;

namespace PBP.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AccountContext context;

        public AccountController(AccountContext context)
        {
            this.context = context;
        }
        private bool AccountExisted(Account account)
        {
            return context.Accounts.Any(a => a.UserName == account.UserName);
        }

        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData[Key.CurrentSort] = sortOrder;
            ViewData[Key.CurrentPage] = pageNumber;
            ViewData[Key.Total] = context.Accounts.Count();
            ViewData[Key.NameSortParm] = string.IsNullOrEmpty(sortOrder)
                ? Key.NameDesc
                : string.Empty;

            ViewData[Key.DateSortParm] = sortOrder == Key.Date
                ? Key.NameDesc
                : Key.Date;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData[Key.CurrentFilter] = searchString;
            var accounts = context.Accounts.Select(a => a);
            if (!string.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(s => s.UserName.Contains(searchString));
                ViewData[Key.Total] = accounts.Count();
            }

            switch (sortOrder)
            {
                case Key.NameDesc:
                    accounts = accounts.OrderByDescending(s => s.UserName);
                    break;
                case Key.Date:
                    accounts = accounts.OrderBy(s => s.CreateTime);
                    break;
                case Key.DateDesc:
                    accounts = accounts.OrderByDescending(s => s.CreateTime);
                    break;
                default:
                    accounts = accounts.OrderBy(s => s.UserName);
                    break;
            }
            var pageSize = Key.PageSize;
            return View(await PaginatedList<Account>.CreateAsync(accounts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,UserName,Password,Role,Email,CreateTime,UpdateTime")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.Guid = Guid.NewGuid();
                account.CreateTime = DateTime.Now;
                account.UpdateTime = DateTime.Now;
                account.Role = UserRole.Ordinary;
                context.Add(account);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        public async Task<IActionResult> ResetPassword(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var account = await context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            account.Password = Key.DefaultPwd;
            account.UpdateTime = DateTime.Now;
            context.Accounts.Update(account);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var account = await context.Accounts
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (account == null)
            {
                return NotFound();
            }
            context.Accounts.Remove(account);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Guid,UserName,Password,Role,Email,CreateTime,UpdateTime")] Account account)
        {
            ViewData[VKey.ACCOUNTEXIST] = null;
            if (ModelState.IsValid)
            {
                if (AccountExisted(account))
                {
                    ViewData[VKey.ACCOUNTEXIST] = CKey.ACCOUNTEXIST;
                    return View(account);
                }
                account.Guid = Guid.NewGuid();
                account.CreateTime = DateTime.Now;
                account.UpdateTime = DateTime.Now;
                account.Role = UserRole.Ordinary;
                context.Add(account);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Login));
            }
            return View(account);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Bind("UserName,Password")] Account account)
        {
            ViewData[VKey.LOGINFAILED] = string.Empty;
            var user = await context.Accounts
                    .FirstOrDefaultAsync(
                        a => a.UserName == account.UserName && 
                             a.Password == account.Password);
            if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserRole), user.Role)),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    Task.Run(async () =>
                    {
                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity));
                    }).Wait();

                    return RedirectToAction(nameof(Index));
                }
                ViewData[VKey.LOGINFAILED] = CKey.UDNOTNULL;
            return View(account);
        }

        public IActionResult LogOut()
        {
            Task.Run(async () =>
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }).Wait();

            return RedirectToAction(nameof(Login));
        }

    }
}
