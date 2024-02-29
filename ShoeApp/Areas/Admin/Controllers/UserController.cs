using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoeApp.Helper;
using ShoeApp.Models;
using ShoeApp.ViewModels;

namespace ShoeApp.Areas.Admin.Controllers
{

    [Area("Admin")]
    //[AdminAreaAuthorization]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var users = await _userManager.Users.Where(c => /*c.EmailConfirmed == true && */c.UserName != "Guest").ToListAsync();

            var userViewModels = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result.ToList(),
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd,
                DateOfBirth = user.DateOfBirth,
                RankId = user.RankId,
                ProfilePicture = user.ProfilePicture
                // thêm được nữa
            }).ToList();

            return View(userViewModels);
        }
        public async Task<IActionResult> CreateAsync()
        {
            var roles = await _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }).ToListAsync();



            // Truyền danh sách role vào ViewBag hoặc ViewModel để sử dụng trong Razor view
            ViewBag.Roles = roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            var usermodel = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Points = 0,
                RankId = Guid.Parse("2FA0118D-B530-421F-878E-CE4D54BFC6AB")
            };
            var result = await _userManager.CreateAsync(usermodel, user.Password);

            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync(user.Roles.First());
                if (role != null)
                {
                    var roleResult = await _userManager.AddToRoleAsync(usermodel, role.Name);
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }


        public async Task<IActionResult> Edit(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user),
                PhoneNumber = user.PhoneNumber,
                // Thêm các thông tin khác nếu cần
            };
            var roles = await _roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            }).ToListAsync();



            // Truyền danh sách role vào ViewBag hoặc ViewModel để sử dụng trong Razor view
            ViewBag.Roles = roles;

            return View(userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }
            user.UserName = model.UserName;
            var updateu = await _userManager.UpdateAsync(user);
            var currentRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to update account.");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user, model.Roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to update account.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> LockUnlockAsync(string userId)

        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }


            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                var result = await _userManager.SetLockoutEndDateAsync(user, null);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Khoá tài khoản thất bại.");
                }
            }
            else
            {
                var result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Khoá tài khoản thất bại.");
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Xoá tài khoản thất bại.");
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
