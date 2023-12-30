using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageUsers : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly string _admin = "Admin";
        private readonly string _user = "User";

        public ManageUsers(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ManageUsers
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            return View(users);
        }

        // GET: ManageUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManageUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Email,FirstName,LastName,Role")] ApplicationUser user)
        {
            if(user.Role.Equals(_admin) || user.Role.Equals(_user))
            {
                if (ModelState.IsValid)
                {
                    var result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        await _userManager.AddPasswordAsync(user, "Password123!");
                        await _userManager.AddToRoleAsync(user, user.Role);
                    }
                }
                return RedirectToAction("Index", "ManageUsers");
            }

            return View(user);
        }

        // GET: ManageUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: ManageUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,Role")] ApplicationUser user)
        {
            if (user.Role.Equals(_admin) || user.Role.Equals(_user))
            {
                            var editUser = await _userManager.FindByIdAsync(id);
            string currentRole = editUser.Role;

            editUser.Email = user.Email;
            editUser.Role = user.Role;

            if (ModelState.IsValid)
            {
                await _userManager.UpdateAsync(editUser);
                if (!(currentRole.Equals(editUser.Role)))
                {
                    await _userManager.AddToRoleAsync(editUser, editUser.Role);
                    await _userManager.RemoveFromRoleAsync(editUser, currentRole);
                }
            }
            }
            return View(user);
        }

        // GET: ManageUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: ManageUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
