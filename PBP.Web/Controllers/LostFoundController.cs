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
    public class LostFoundController : Controller
    {
        private readonly LostFoundContext context;

        public LostFoundController(LostFoundContext context)
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
            ViewData[Key.Total] = context.LostFounds.Count();

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
            var lostFounds = context.LostFounds.Select(o => o);
            if (!string.IsNullOrEmpty(searchString))
            {
                lostFounds = lostFounds.Where(s => s.Name.Contains(searchString));
                ViewData[Key.Total] = lostFounds.Count();
            }

            lostFounds = lostFounds.OrderByDescending(s => s.CreateTime);
            var pageSize = Key.PageSize;
            return View(await PaginatedList<LostFound>.CreateAsync(lostFounds.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(PKey.LFPram)] LostFound lostFound)
        {
            if (ModelState.IsValid)
            {
                lostFound.Guid = Guid.NewGuid();
                context.Add(lostFound);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lostFound);
        }
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostFound = await context.LostFounds.FindAsync(id);
            if (lostFound == null)
            {
                return NotFound();
            }
            return View(lostFound);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind(PKey.LFPram)] LostFound lostFound)
        {
            if (id != lostFound.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    lostFound.UpdateTime=DateTime.Now;
                    lostFound.State = LostFound.Lost.OreadyFound;
                    lostFound.FoundTime = DateTime.Now;
                    context.Update(lostFound);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LostFoundExists(lostFound.Guid))
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
            return View(lostFound);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var lostFound = await context.LostFounds
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (lostFound == null) return NotFound();
            context.LostFounds.Remove(lostFound);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LostFoundExists(Guid id)
        {
            return context.LostFounds.Any(e => e.Guid == id);
        }
    }
}
