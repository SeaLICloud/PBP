using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            return View(await context.Accounts.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await context.Accounts.FirstOrDefaultAsync(m => m.Guid == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Password,Guid,CreateTime,UpdateTime")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.Guid = Guid.NewGuid();
                context.Add(account);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        public async Task<IActionResult> Edit(Guid? id)
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
            return View(account);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserName,Password,Guid,CreateTime,UpdateTime")] Account account)
        {
            if (id != account.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(account);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Guid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
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

            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var account = await context.Accounts.FindAsync(id);
            context.Accounts.Remove(account);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(Guid id)
        {
            return context.Accounts.Any(e => e.Guid == id);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ActionName("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Guid,UserName,Password,CreateTime,UpdateTime")] Account account)
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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,Password")] Account account)
        {
            if (ModelState.IsValid)
            {
                var result = await context.Accounts
                    .Where(a => (a.UserName == account.UserName && a.Password == account.Password))
                    .SingleAsync();
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                } 
            }
            return View(account);
        }

    }
}
