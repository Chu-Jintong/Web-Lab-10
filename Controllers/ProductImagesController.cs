using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductImagesController : Controller
    {
        // GET: ProductImages
        public ActionResult Index()
        {
            return View();
        }
    }
}