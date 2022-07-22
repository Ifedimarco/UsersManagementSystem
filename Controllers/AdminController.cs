using EgolePayUsersManagementSystem.Areas.Identity.Pages.Account;
using EgolePayUsersManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EgolePayUsersManagementSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminController/Details/5
        public ActionResult Users()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // GET: AdminController/Create
        public ActionResult CreateUser()
        {
            return View(new CreateAccountModel());
        }

        // POST: AdminController/Create
        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateAccountModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    DateOfBirth = model.DateOfBirth,
                    Address = model.Address,
                    UserName = model.Email,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    return RedirectToAction("Users");
                }
            }
            return View(model);
        }

        
        [HttpPost]
        public ActionResult Deactivate(string id)
        {
            var user = _context.Users.Find(id);
            user.Status = UserStatus.Deactivated;
            _context.SaveChanges();
            return RedirectToAction("Users");
        }
/*
        [HttpPost]
        public ActionResult Activated(string id)
        {
            var request = _context.Users.Find(id);
            var user = _context..Find(request.UserId);
            user.Role = request.Role;
            request.Status = UserStatus.Activated;
            _context.SaveChanges();
            return RedirectToAction("user");
        }*/

        public ActionResult UpgradeRequest()
        {
            var fileupload = _context.FileUploads.Include(itm => itm.User).ToList();
            return View(fileupload);
        }

        [HttpPost]
        public ActionResult Approve(int id)
        {
            var request = _context.FileUploads.Find(id);
            var user = _context.Users.Find(request.UserId);
            user.Role = request.Role;
            request.Status = UpgradeRequestStatus.Approved;
            _context.SaveChanges();
            return RedirectToAction("UpgradeRequest");
        }

        [HttpPost]
        public ActionResult Reject(int id)
        {
            var request = _context.FileUploads.Find(id);
            request.Status = UpgradeRequestStatus.Rejected;
            _context.SaveChanges();
            return RedirectToAction("UpgradeRequest");
        }

    }
}
