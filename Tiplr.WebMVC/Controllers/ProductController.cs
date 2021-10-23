using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tiplr.Services;

namespace Tiplr.WebMVC.Controllers
{
    public class ProductController : Controller
    {
        [Authorize]
        // GET: Product
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProductService(userId);
            var model = service.GetProducts();
            return View(model);
        }

        //Helper Methods
        private ProductService CreateProductService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProductService(userId);
            return service;
        }
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CategoryService(userId);
            return service;
        }

    }
}