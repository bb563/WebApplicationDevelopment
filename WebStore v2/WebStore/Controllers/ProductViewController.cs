using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebStore.Models;
using System.Data.Entity;
using System.Collections.Generic;

namespace WebStore.Controllers
{
    public class ProductViewController : Controller
    {
        private ConnectDB db = new ConnectDB();

        // GET: ProductView
        // GET: ProductView
        public ActionResult Index(int? categoryId)
        {
            List<Product> products;
            string categoryName = "All Products"; // Default category name

            if (categoryId.HasValue)
            {
                Category category = db.Categories.Find(categoryId);
                if (category == null)
                {
                    return HttpNotFound();
                }
                products = category.Products.ToList();
                categoryName = category.CategoryName;
            }
            else
            {
                products = db.Products.ToList();
            }

            ViewBag.CategoryName = categoryName; // Pass category name to view

            return View(products); // Passing list of products to the view
        }
      
        public ActionResult Category()
        {
            List<Category> cateList = this.db.Categories.ToList();
            return PartialView(cateList);
        }

        // GET: ProductView/Detail/{productId}
        public ActionResult Detail(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }
        // GET: ProductView/CategoryProducts/{categoryId}
        public ActionResult CategoryProducts(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            var products = category.Products.ToList();
            ViewBag.CategoryName = category.CategoryName;

            return View(products);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
