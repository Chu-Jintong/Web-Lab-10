using Microsoft.AspNet.Identity.Owin;
using OnlineStore.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersAdminController : Controller
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

        // GET: UsersAdmin
        public ActionResult Index()
        {
            return View(UserManager.Users.ToList());
        }
    }
}