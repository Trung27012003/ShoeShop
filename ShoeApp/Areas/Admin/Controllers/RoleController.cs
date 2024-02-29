using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeApp.Data;
using ShoeApp.Helper;
using ShoeApp.ViewModels;
using System.Security.Claims;

namespace ShoeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AdminAreaAuthorization]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Where(c => c.Name != "Guest").ToListAsync();
            var roleViewModels = roles.Select(role => new RoleViewModels
            {
                Id = role.Id,
                Name = role.Name,
                Claims = _roleManager.GetClaimsAsync(role).Result.Select(claim => claim.Type).ToList()
            }).ToList();

            return View(roleViewModels);
        }
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return View();
            }
            else
            {
                // Update
                var obj = await _roleManager.FindByIdAsync(roleId);
                return View(obj);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IdentityRole roleObj)
        {
            if (await _roleManager.RoleExistsAsync(roleObj.Name))
            {
                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrEmpty(roleObj.Id))
            {
                // create
                await _roleManager.CreateAsync(new IdentityRole() { Name = roleObj.Name });
            }
            else
            {
                // update
                var objRoleFromDb = await _roleManager.FindByIdAsync(roleObj.Id);

                if (objRoleFromDb == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                objRoleFromDb.Name = roleObj.Name;
                objRoleFromDb.NormalizedName = roleObj.Name.ToUpper();
                var result = await _roleManager.UpdateAsync(objRoleFromDb);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string roleId)
        {
            var objFromDb = await _roleManager.FindByIdAsync(roleId);
            await _roleManager.DeleteAsync(objFromDb);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoleClaims(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }

            var existingClaims = await _roleManager.GetClaimsAsync(role);
            var model = new RoleClaimsViewModel
            {
                RoleId = roleId,
            };
            foreach (Claim claim in ClaimStore.claimsList)
            {
                RoleClaim roleClaim = new()
                {
                    ClaimType = claim.Type
                };

                if (existingClaims.Any(c => c.Type == claim.Type))
                    roleClaim.IsSelected = true;

                model.Claims.Add(roleClaim);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoleClaims(RoleClaimsViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                return NotFound();
            }

            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            var selectedClaims = model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, "True")).ToList();
            foreach (var claim in selectedClaims)
            {
                await _roleManager.AddClaimAsync(role, claim);
            }
            return RedirectToAction("Index");

        }
    }
}
