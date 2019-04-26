using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PBP.Web.Common;
using PBP.Web.Models.Context;
using PBP.Web.Models.Domain;

namespace PBP.Web.Controllers
{
    [Authorize]
    public class PartyMemberController : Controller
    {
        private readonly PartyMemberContext context;
        private readonly OrganizationContext orgContext;
        private readonly AccountContext accContext;
        public PartyMemberController(PartyMemberContext context, OrganizationContext orgContext, AccountContext accContext)
        {
            this.context = context;
            this.orgContext = orgContext;
            this.accContext = accContext;
        }

        private bool PartyMemberExists(Guid id)
        {
            return context.PartyMembers.Any(e => e.Guid == id);
        }

        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData[Key.CurrentSort] = sortOrder;
            ViewData[Key.CurrentPage] = pageNumber;
            ViewData[Key.Total] = context.PartyMembers.Count();

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
            var partyMembers = context.PartyMembers.Select(o => o);
            if (!string.IsNullOrEmpty(searchString))
            {
                partyMembers = partyMembers.Where(s => s.Name.Contains(searchString));
                ViewData[Key.Total] = partyMembers.Count();
            }
            partyMembers = partyMembers.OrderByDescending(s => s.CreateTime);
            var pageSize = Key.PageSize;
            return View(await PaginatedList<PartyMember>.CreateAsync(partyMembers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partyMember = await context.PartyMembers
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (partyMember == null)
            {
                return NotFound();
            }

            return View(partyMember);
        }

        public IActionResult Create()
        {
            var type = from t in orgContext.Organizations
                select new SelectListItem { Value = t.OrgID, Text = t.Name };
            ViewBag.TypeList = new SelectList(type, "Value", "Text");

            ViewData[Key.CurrentUser] = HttpContext.User.Claims.First().Type;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(PKey.PMPram)] PartyMember partyMember)
        {
            if (ModelState.IsValid)
            {
                partyMember.Guid = Guid.NewGuid();
                partyMember.PartyMemberID = new SeriaNumber().Seria(context.PartyMembers.Count() + 1,Key.PMPre);
                partyMember.Stage = PartyMember.DevelopmentStage.NotInput;
                partyMember.CreateTime=DateTime.Now;
                partyMember.UpdateTime=DateTime.Now;
                context.Add(partyMember);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(partyMember);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var type = from t in orgContext.Organizations
                select new SelectListItem { Value = t.OrgID, Text = t.Name };
            ViewBag.TypeList = new SelectList(type, "Value", "Text");

            var partyMember = await context.PartyMembers.FindAsync(id);
            if (partyMember == null)
            {
                return NotFound();
            }
            return View(partyMember);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind(PKey.PMPram)] PartyMember partyMember)
        {
            if (id != partyMember.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    partyMember.UpdateTime=DateTime.Now;
                    context.Update(partyMember);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartyMemberExists(partyMember.Guid))
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
            return View(partyMember);
        }

        public async Task<IActionResult> Turn(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var partyMember = await context.PartyMembers.FirstOrDefaultAsync(o => o.Guid == id);
            if (partyMember == null)
            {
                return NotFound();
            }

            switch (partyMember.Stage)
            {
                case PartyMember.DevelopmentStage.NotInput:
                    partyMember.Stage = PartyMember.DevelopmentStage.Activist;
                    partyMember.BeginDate = DateTime.Now;
                    break;

                case PartyMember.DevelopmentStage.Activist:
                    partyMember.Stage = PartyMember.DevelopmentStage.Prepare;
                    partyMember.PrepareDate = DateTime.Now;
                    break;

                case PartyMember.DevelopmentStage.Prepare:
                    partyMember.Stage = PartyMember.DevelopmentStage.Formal;
                    partyMember.FormalDate = DateTime.Now;
                    break;

                default:
                    partyMember.Stage = PartyMember.DevelopmentStage.Formal;
                    break;
            }

            partyMember.UpdateTime = DateTime.Now;
            context.Update(partyMember);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var account = await context.PartyMembers
                .FirstOrDefaultAsync(o => o.Guid == id);
            if (account == null)
            {
                return NotFound();
            }
            context.PartyMembers.Remove(account);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
