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
    public class PartyCostController : Controller
    {
        private readonly AccountPartyMemberContext aPMcontext;
        private readonly PartyCostContext context;

        public PartyCostController(PartyCostContext context, AccountPartyMemberContext aPMcontext)
        {
            this.context = context;
            this.aPMcontext = aPMcontext;
        }

        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData[Key.CurrentSort] = sortOrder;
            ViewData[Key.CurrentPage] = pageNumber;
            ViewData[Key.Total] = context.PartyCosts.Count();

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
            var partyCosts = context.PartyCosts.Select(o => o);
            if (!string.IsNullOrEmpty(searchString))
            {
                partyCosts = partyCosts.Where(s => s.PartyMemberID.Contains(searchString));
                ViewData[Key.Total] = partyCosts.Count();
            }

            partyCosts = partyCosts.OrderByDescending(s => s.CreateTime);
            var pageSize = Key.PageSize;
            return View(await PaginatedList<PartyCost>.CreateAsync(partyCosts.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var partyCost = await context.PartyCosts
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (partyCost == null) return NotFound();

            return View(partyCost);
        }

        public async Task<IActionResult> FillInfo()
        {
            var currentUserName =
                HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;

            var currentPartyMember =
                await aPMcontext.AccountPartyMembers.FirstOrDefaultAsync(aPM => aPM.AccountID == currentUserName);

            var currentPartyCost =
                await context.PartyCosts.FirstOrDefaultAsync(pC =>
                    pC.PartyMemberID == currentPartyMember.PartyMemberID);

            ViewData[Key.CurrentPartyMember] = currentPartyMember.PartyMemberID;
            return View(currentPartyCost);
        }

        [HttpPost]
        public async Task<IActionResult> FillInfo([Bind(PKey.PCPram)] PartyCost partyCost)
        {
            var currentRole = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Role).Value;
            if (ModelState.IsValid)
            {
                try
                {
                    partyCost.CostBase = partyCost.Base();
                    partyCost.Payable = partyCost.Pay();
                    context.Update(partyCost);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartyCostExists(partyCost.Guid))
                        return NotFound();
                    throw;
                }

                if (string.Equals(currentRole, UserRole.Admin.ToString(),
                        StringComparison.CurrentCultureIgnoreCase))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(partyCost);
            }

            return View(partyCost);
        }


        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var partyCost = await context.PartyCosts.FindAsync(id);
            if (partyCost == null) return NotFound();
            return View(partyCost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind(PKey.PCPram)] PartyCost partyCost)
        {
            if (id != partyCost.Guid) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    partyCost.CostBase = partyCost.Base();
                    partyCost.Payable = partyCost.Pay();
                    partyCost.UpdateTime =DateTime.Now;
                    context.Update(partyCost);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartyCostExists(partyCost.Guid))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(partyCost);
        }

        public async Task<IActionResult> Vertify(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var partyCost = await context.PartyCosts.FirstOrDefaultAsync(o => o.Guid == id);
            if (partyCost == null)
            {
                return NotFound();
            }
            if (partyCost.State==Verify.Unaudited)
            {
                partyCost.State = Verify.Audited;
            }
            partyCost.UpdateTime = DateTime.Now;
            context.Update(partyCost);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool PartyCostExists(Guid id)
        {
            return context.PartyCosts.Any(e => e.Guid == id);
        }
    }
}