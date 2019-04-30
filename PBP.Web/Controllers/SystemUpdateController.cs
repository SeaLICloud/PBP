using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PBP.Web.Common;
using PBP.Web.Models.Context;
using PBP.Web.Models.Domain;

namespace PBP.Web.Controllers
{
    public class SystemUpdateController : Controller
    {
        private readonly SystemUpdateContext context;

        public SystemUpdateController(SystemUpdateContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await context.SystemUpdates.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(PKey.SUPram)] SystemUpdate systemUpdate)
        {
            if (ModelState.IsValid)
            {
                systemUpdate.Guid = Guid.NewGuid();
                systemUpdate.CreateTime = DateTime.Now;
                systemUpdate.UpdateTime = DateTime.Now;
                systemUpdate.Time = DateTime.Now;
                context.Add(systemUpdate);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(systemUpdate);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var systemUpdate = await context.SystemUpdates
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (systemUpdate == null) return NotFound();
            context.SystemUpdates.Remove(systemUpdate);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
