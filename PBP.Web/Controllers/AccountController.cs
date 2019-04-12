using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBP.Web.Common;
using PBP.Web.Models.Context;
using PBP.Web.Models.Domain;

namespace PBP.Web.Controllers
{
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

        public async Task<IActionResult> Index()
        {
            return View(await context.Accounts.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,UserName,Password,CreateTime,UpdateTime")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.Guid = Guid.NewGuid();
                account.CreateTime = DateTime.Now;
                account.UpdateTime = DateTime.Now;
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

        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Guid,UserName,Password,CreateTime,UpdateTime")] Account account)
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
                context.Add(account);
                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Login));
            }
            return View(account);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,Password")] Account account)
        {
            ViewData[VKey.LOGINFAILED] = null;
            if (ModelState.IsValid)
            {
                var fAccount = await context.Accounts
                    .FirstOrDefaultAsync(
                        a => a.UserName == account.UserName && 
                             a.Password == account.Password);
                if (fAccount != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewData[VKey.LOGINFAILED] = CKey.UDNOTNULL;
            }
            return View(account);
        }

        public IActionResult LogOut()
        {
            return RedirectToAction(nameof(Login));
        }

    }
}
