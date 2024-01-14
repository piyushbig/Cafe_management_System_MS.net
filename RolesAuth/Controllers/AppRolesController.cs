using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RolesAuth.Data;

namespace RolesAuth.Controllers
{
    //[Authorize (Roles ="Admin")]
    public class AppRolesController : Controller
    {
        
        private readonly RoleManager<IdentityRole> roleManager;

        public AppRolesController( RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            
        }

        //lists all the roles created by users
        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {

            //avoid duplicate role
            var isThere = roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult();

            if (!isThere)
            {
                roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || roleManager.Roles == null)
            {
                return NotFound();
            }

            var roleEntity = await roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roleEntity == null)
            {
                return NotFound();
            }

            return View(roleEntity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (roleManager.Roles == null)
            {
                return Problem("Entity set 'AppDbContext.Roles' is null.");
            }

            var roleEntity = await roleManager.FindByIdAsync(id);
            if (roleEntity != null)
            {
                var result = await roleManager.DeleteAsync(roleEntity);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the error, e.g., return a view with error messages
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
    
}


