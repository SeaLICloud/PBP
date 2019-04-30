using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBP.Web.Common;
using PBP.Web.Models.Context;
using PBP.Web.Models.Domain;

namespace PBP.Web.Controllers
{
    public class SuggestionController : Controller
    {
        private readonly SuggestionContext context;
        private readonly AccountContext aCContext;

        public SuggestionController(SuggestionContext context, AccountContext aCContext)
        {
            this.context = context;
            this.aCContext = aCContext;
        }

        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData[Key.CurrentSort] = sortOrder;
            ViewData[Key.CurrentPage] = pageNumber;
            ViewData[Key.Total] = context.Suggestions.Count();

            ViewData[Key.NameSortParm] = string.IsNullOrEmpty(sortOrder)
                ? Key.NameDesc
                : string.Empty;

            ViewData[Key.DateSortParm] = sortOrder == Key.Date
                ? Key.NameDesc
                : Key.Date;

            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;
            ViewData[Key.CurrentFilter] = searchString;
            var suggestions = context.Suggestions.Select(o => o);
            if (!string.IsNullOrEmpty(searchString))
            {
                suggestions = suggestions.Where(s => s.Title.Contains(searchString));
                ViewData[Key.Total] = suggestions.Count();
            }

            suggestions = suggestions.OrderByDescending(s => s.CreateTime);
            var pageSize = Key.PageSize;
            return View(await PaginatedList<Suggestion>.CreateAsync(suggestions.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestion = await context.Suggestions
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (suggestion == null)
            {
                return NotFound();
            }

            return View(suggestion);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(PKey.SPram)] Suggestion suggestion)
        {
            var currentUserName = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            var currentAccount = await aCContext.Accounts.FirstOrDefaultAsync(a => a.UserName == currentUserName);

            if (ModelState.IsValid)
            {
                suggestion.Guid = Guid.NewGuid();
                suggestion.CreateTime = DateTime.Now;
                suggestion.UpdateTime = DateTime.Now;
                suggestion.SendTime = DateTime.Now;
                suggestion.SendEmail = currentAccount.Email;
                context.Add(suggestion);
                await context.SaveChangesAsync();
                return View(suggestion);
            }
            return View(suggestion);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var suggestion = await context.Suggestions
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (suggestion == null) return NotFound();
            context.Suggestions.Remove(suggestion);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
