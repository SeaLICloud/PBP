using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using PBP.Web.Common;
using PBP.Web.Models.Context;
using File = PBP.Web.Models.Domain.File;

namespace PBP.Web.Controllers
{
    public class FileController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly FileContext context;
        public FileController(IHostingEnvironment hostingEnvironment, FileContext context)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData[Key.CurrentSort] = sortOrder;
            ViewData[Key.CurrentPage] = pageNumber;
            ViewData[Key.Total] = context.Files.Count();

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
            var files = context.Files.Select(f => f);
            if (!string.IsNullOrEmpty(searchString))
            {
                files = files.Where(s => s.Name.Contains(searchString));
                ViewData[Key.Total] = files.Count();
            }

            files = files.OrderByDescending(s => s.CreateTime);
            var pageSize = Key.PageSize;
            return View(await PaginatedList<File>.CreateAsync(files.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult UpLoad()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpLoad(List<IFormFile> files, [Bind("Guid,CreateTime,UpdateTime,Title,Name,FileName,Length,ContentType")] File file)
        {
            foreach (var f in files)
            {
                var fileName = f.FileName;
                fileName = $@"{hostingEnvironment.WebRootPath}\UpLoad\{fileName}";
                if (!Directory.Exists($@"{hostingEnvironment.WebRootPath}\UpLoad"))
                {
                    Directory.CreateDirectory($@"{hostingEnvironment.WebRootPath}\UpLoad");
                }
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await f.CopyToAsync(fileStream);
                    file.Guid =Guid.NewGuid();
                    file.CreateTime = DateTime.Now;
                    file.UpdateTime = DateTime.Now;
                    file.Name = f.FileName;
                    file.FileName = fileName;
                    file.Length = fileStream.Length;
                    file.ContentType = fileName.Substring(fileName.LastIndexOf(".", StringComparison.Ordinal) + 1);
                    fileStream.Flush();
                }
                context.Add(file);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownLoad(Guid? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var file = await context.Files.FirstOrDefaultAsync(o => o.Guid == id);
            if (file == null)
            {
                return NotFound();
            }
            var addrUrl = file.FileName;
            var fileStream = System.IO.File.OpenRead(addrUrl);
            var fileExt = "." + fileStream.Name.Substring(fileStream.Name.LastIndexOf(".",
                StringComparison.Ordinal) + 1);
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];
            return File(fileStream, memi, Path.GetFileName(addrUrl));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var file = await context.Files
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (file == null) return NotFound();
            if (System.IO.File.Exists(file.FileName))
            {
                System.IO.File.Delete(file.FileName);
            }
            context.Files.Remove(file);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}