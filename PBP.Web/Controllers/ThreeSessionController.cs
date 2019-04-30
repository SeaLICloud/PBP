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
    public class ThreeSessionController : Controller
    {
        private readonly ThreeSessionContext context;

        public ThreeSessionController(ThreeSessionContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData[Key.CurrentSort] = sortOrder;
            ViewData[Key.CurrentPage] = pageNumber;
            ViewData[Key.Total] = context.ThreeSessions.Count();

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
            var threeSessions = context.ThreeSessions.Select(o => o);
            if (!string.IsNullOrEmpty(searchString))
            {
                threeSessions = threeSessions.Where(s => s.Theme.Contains(searchString));
                ViewData[Key.Total] = threeSessions.Count();
            }

            threeSessions = threeSessions.OrderByDescending(s => s.CreateTime);
            var pageSize = Key.PageSize;
            return View(await PaginatedList<ThreeSession>.CreateAsync(threeSessions.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(PKey.TSPram)] ThreeSession threeSession)
        {
            if (ModelState.IsValid)
            {
                threeSession.Guid = Guid.NewGuid();
                context.Add(threeSession);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(threeSession);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threeSession = await context.ThreeSessions.FindAsync(id);
            if (threeSession == null)
            {
                return NotFound();
            }
            return View(threeSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind(PKey.TSPram)] ThreeSession threeSession)
        {
            if (id != threeSession.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(threeSession);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThreeSessionExists(threeSession.Guid))
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
            return View(threeSession);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var threeSession = await context.ThreeSessions
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (threeSession == null) return NotFound();
            context.ThreeSessions.Remove(threeSession);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThreeSessionExists(Guid id)
        {
            return context.ThreeSessions.Any(e => e.Guid == id);
        }
    }
}
