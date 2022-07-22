using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EgolePayUsersManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EgolePayUsersManagementSystem.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EgolePayUsersManagementSystem.Controllers
{
    [Authorize]
    public class UpgradeRequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public UpgradeRequestController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: UpgradeRequest
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var applicationDbContext = _context.FileUploads.Where(itm => itm.UserId == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UpgradeRequest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.FileUploads
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            return View(fileUpload);
        }

        // GET: UpgradeRequest/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: UpgradeRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpgradeRequest fileUpload)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            if (ModelState.IsValid)
            {
                //UPLOAD PROFILE PICTURE
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName1 = Guid.NewGuid().ToString() + ".jpg";
                string fileName2 = Guid.NewGuid().ToString() + ".jpg";
                string path1 = Path.Combine(wwwRootPath + "/docs/", fileName1);
                string path2 = Path.Combine(wwwRootPath + "/docs/", fileName2);
                using (var fileStream = new FileStream(path1, FileMode.Create))
                {
                    await fileUpload.DocumentPath1.CopyToAsync(fileStream);
                }
                using (var fileStream = new FileStream(path2, FileMode.Create))
                {
                    await fileUpload.DocumentPath2.CopyToAsync(fileStream);
                }
                var model = new FileUpload
                {
                    Role = fileUpload.Role,
                    DocumentPath1 = fileName1,
                    DocumentPath2 = fileName2,
                    UserId = userId,
                    Status = UpgradeRequestStatus.Pending,
                    

                };
                _context.FileUploads.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userId);
            return View(fileUpload);
        }

        /*// GET: UpgradeRequest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.FileUploads.FindAsync(id);
            if (fileUpload == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", fileUpload.UserId);
            return View(fileUpload);
        }*/

        // POST: UpgradeRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      /*  [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Role,Status,DocumentPath1,DocumentPath2")] FileUpload fileUpload)
        {
            if (id != fileUpload.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileUpload);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileUploadExists(fileUpload.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", fileUpload.UserId);
            return View(fileUpload);
        }*/

        // GET: UpgradeRequest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.FileUploads
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            return View(fileUpload);
        }

        // POST: UpgradeRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileUpload = await _context.FileUploads.FindAsync(id);
            _context.FileUploads.Remove(fileUpload);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileUploadExists(int id)
        {
            return _context.FileUploads.Any(e => e.Id == id);
        }
    }
}
