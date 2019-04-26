using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBP.Web.Common;
using PBP.Web.Models.Context;
using PBP.Web.Models.Domain;

namespace PBP.Web.Controllers
{
    [Authorize]
    public class OrganizationController : Controller
    {
        private readonly OrganizationContext context;

        public OrganizationController(OrganizationContext context)
        {
            this.context = context;
        }
        private bool OrganizationExists(string id)
        {
            return context.Organizations.Any(o => o.OrgID == id);
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["Total"] = context.Organizations.Count();
            ViewData["CurrentPage"] = pageNumber;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var organizations = context.Organizations.Select(o => o);
            if (!string.IsNullOrEmpty(searchString))
            {
                organizations = organizations.Where(s => s.Name.Contains(searchString));
                ViewData["Total"] = organizations.Count();
            }
            organizations = organizations.OrderByDescending(s => s.CreateTime);
            var pageSize = Key.PageSize;
            return View(await PaginatedList<Organization>.CreateAsync(organizations.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,CreateTime,UpdateTime,OrgID,Name,ShortName,OrgType")] Organization organization)
        {
            if (ModelState.IsValid)
            {
                organization.Guid = Guid.NewGuid();
                organization.CreateTime = DateTime.Now;
                organization.UpdateTime = DateTime.Now;
                organization.OrgID = new SeriaNumber().Seria(context.Organizations.Count() + 1);

                context.Add(organization);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organization);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
             
            var organization = await context.Organizations
                .FirstOrDefaultAsync(o => o.Guid == id);
            if (organization == null)
            {
                return NotFound();
            }
            return View(organization);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("OrgID,Name,ShortName,OrgType,Guid,CreateTime,UpdateTime")] Organization organization)
        {
            if (id != organization.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(organization);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizationExists(organization.OrgID))
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
            return View(organization);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var account = await context.Organizations
                .FirstOrDefaultAsync(o => o.Guid == id);
            if (account == null)
            {
                return NotFound();
            }
            context.Organizations.Remove(account);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
