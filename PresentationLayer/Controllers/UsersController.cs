using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;
using BussinessLayer.Services;
using DataLayer.Enums;
using System.Data;
using BussinessLayer.Helper;
using PresentationLayer.ViewModel;
using BussinessLayer.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Shared.Enums;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var listUsers = await userService.GetAllUsers();
            return View(listUsers);
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Create()
        {
            var roles = await roleService.GetAllRoles();
            ViewData["RoleId"] = new SelectList(roles, "Id", "RoleName");
            ViewData["GenderList"] = new SelectList(Enum.GetValues(typeof(Gender))
                                        .Cast<Gender>()
                                        .Select(gender => new { Id = (int)gender, Name = gender.ToString() }), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Password,PhoneNumber,Email,Avatar,Name,DateOfBirth,Gender,Position,RoleId,StartDate,CreatedAt,UpdatedAt,DeletedAt")] User user)
        {
            ModelState.Remove("Role");

            user.Id = Guid.NewGuid().ToString();
            user.Role = await roleService.GetRoleById(user.RoleId);
            user.Password = HashPasswordHelper.HashPassword(user.Password);
            user.DeletedAt = null;

            if (!ModelState.IsValid)
            {
                var roles = await roleService.GetAllRoles();
                ViewData["RoleId"] = new SelectList(roles, "Id", "RoleName", user.RoleId);

                TempData["ErrorMessage"] = "Failed to create user.";
                return View(user);
            }

            await userService.CreateUser(user);
            TempData["SuccessMessage"] = "User created successfully!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await roleService.GetAllRoles();
            ViewData["RoleId"] = new SelectList(roles, "Id", "RoleName", user.RoleId);
            ViewData["GenderList"] = new SelectList(Enum.GetValues(typeof(Gender))
                                        .Cast<Gender>()
                                        .Select(gender => new { Id = (int)gender, Name = gender.ToString() }), "Id", "Name");
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserName,Password,PhoneNumber,Email,Avatar,Name,DateOfBirth,Gender,Position,RoleId,StartDate")]
                                            User user)
        {
            user.Id = id;
            ModelState.Remove("Role");

            if (ModelState.IsValid)
            {
                try
                {
                    user.UpdatedAt = TimeHelper.GetVietnamTime();
                    await userService.UpdateUser(user);

                    TempData["SuccessMessage"] = "User updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["ErrorMessage"] = "Data has been changed, please try again!";
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                      .Select(e => e.ErrorMessage)
                      .ToList();
            Console.WriteLine(string.Join("\n", errors));
            var roles = await roleService.GetAllRoles();
            ViewData["RoleId"] = new SelectList(roles, "Id", "RoleName", user.RoleId);
            return View(user);
        }

        public IActionResult ResetPassword(string id)
        {
            var staff = userService.GetUserById(id);
            if (staff == null)
            {
                return NotFound();
            }

            var model = new ResetPasswordViewModel { StaffId = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPassword);
            }

            var staff = await userService.GetUserById(resetPassword.StaffId);
            if (staff == null)
            {
                ModelState.AddModelError("", "Staff not found.");
                return View(resetPassword);
            }

            staff.Password = HashPasswordHelper.HashPassword(resetPassword.NewPassword);
            await userService.UpdateUser(staff);

            TempData["SuccessMessage"] = "Password reset successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await userService.GetUserById(userId);
            if (user == null || user.DeletedAt != null)
            {
                return NotFound();
            }
            user.DeletedAt = TimeHelper.GetVietnamTime();

            await userService.UpdateUser(user);
            TempData["SuccessMessage"] = "User deleted successfully!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RestoreUser(string userId)
        {
            var user = await userService.GetUserById(userId);
            if (user == null || user.DeletedAt == null)
            {
                return NotFound();
            }

            user.DeletedAt = null;
            await userService.UpdateUser(user);
            TempData["SuccessMessage"] = "User restored successfully!";

            return RedirectToAction("Index");
        }

    }
}
