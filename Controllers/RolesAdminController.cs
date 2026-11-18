
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using OnlineStore.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace OnlineStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesAdminController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??
                    HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ??
                    HttpContext.GetOwinContext()
                    .Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: RolesAdmin
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }
        //GET:RolesAdmin.Details/5
        public async Task<ActionResult> Details(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return View(role);
        }
        //GET:/RolesAdmin/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: /RolesAdmin/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Name")] FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(collection["Name"]);
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        // GET: RolesAdmin/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }

        // POST: RolesAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [Bind(Include = "Id,Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var existingRole = await RoleManager.FindByIdAsync(role.Id);

                if (existingRole == null)
                {
                    return HttpNotFound();
                }

                existingRole.Name = role.Name;

                IdentityResult result = await RoleManager.UpdateAsync(existingRole);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View(role);
                }

                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: RolesAdmin/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }
        // POST: RolesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var role = await RoleManager.FindByIdAsync(id);

                if (role == null)
                {
                    return HttpNotFound();
                }

                IdentityResult result = await RoleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }

                return RedirectToAction("Index");
            }

            return View();
        }

    }
}
