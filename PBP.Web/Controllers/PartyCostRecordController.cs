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
    public class PartyCostRecordController : Controller
    {
        private readonly PartyCostRecordContext context;
        private readonly AccountPartyMemberContext aPMcontext;
        private readonly PartyCostContext pCContext;
        private readonly PartyMemberContext pMContext;

        public PartyCostRecordController(PartyCostRecordContext context,
            AccountPartyMemberContext aPMcontext, PartyCostContext pCContext, PartyMemberContext pMContext)
        {
            this.context = context;
            this.aPMcontext = aPMcontext;
            this.pCContext = pCContext;
            this.pMContext = pMContext;
        }

        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData[Key.CurrentSort] = sortOrder;
            ViewData[Key.CurrentPage] = pageNumber;
            ViewData[Key.Total] = context.PartyCostRecords.Count();

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
            var partyCostRecords = context.PartyCostRecords.Select(o => o);
            if (!string.IsNullOrEmpty(searchString))
            {
                partyCostRecords = partyCostRecords.Where(s => s.PartyMemberID.Contains(searchString));
                ViewData[Key.Total] = partyCostRecords.Count();
            }

            partyCostRecords = partyCostRecords.OrderByDescending(s => s.CreateTime);
            var pageSize = Key.PageSize;
            return View(await PaginatedList<PartyCostRecord>.CreateAsync(partyCostRecords.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Pay()
        {
            var currentUserName =
                HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            var currentAPM =
                await aPMcontext.AccountPartyMembers.FirstOrDefaultAsync(aPM => aPM.AccountID == currentUserName);
            var currentPartyCost =
                await pCContext.PartyCosts.FirstOrDefaultAsync(pC =>
                    pC.PartyMemberID == currentAPM.PartyMemberID);

            ViewData[Key.Amount] = currentPartyCost.Payable;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay([Bind(PKey.PCRPram)] PartyCostRecord partyCostRecord)
        {
            var currentUserName =
                HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
            var currentAPM =
                await aPMcontext.AccountPartyMembers.FirstOrDefaultAsync(aPM => aPM.AccountID == currentUserName);
            var currentPartyMember =
                await pMContext.PartyMembers.FirstOrDefaultAsync(pM => pM.PartyMemberID == currentAPM.PartyMemberID);
            var currentPartyCost =
                await pCContext.PartyCosts.FirstOrDefaultAsync(pC =>
                    pC.PartyMemberID == currentAPM.PartyMemberID);
            partyCostRecord.Guid = Guid.NewGuid();
            partyCostRecord.CreateTime = DateTime.Now;
            partyCostRecord.UpdateTime = DateTime.Now;
            partyCostRecord.PayTime = DateTime.Now;
            partyCostRecord.PartyCostID = currentPartyCost.PartyCostID;
            partyCostRecord.PartyMemberName = currentPartyMember.Name;
            partyCostRecord.PartyMemberID = currentPartyMember.PartyMemberID;
            partyCostRecord.State = Verify.Unaudited;
            context.Add(partyCostRecord);
            await context.SaveChangesAsync();
            return View(partyCostRecord);
        }

        public async Task<IActionResult> Vertify(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var partyCostRecord = await context.PartyCostRecords
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (partyCostRecord == null)
            {
                return NotFound();
            }
            if (partyCostRecord.State==Verify.Unaudited)
            {
                partyCostRecord.State = Verify.Audited;
            }
            context.Update(partyCostRecord);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
