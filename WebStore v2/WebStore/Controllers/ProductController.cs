using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private ConnectDB db = new ConnectDB();

        // GET: Product
        public ActionResult Index()
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                // Chỉ lấy các sản phẩm không bị ẩn
                var products = db.Products.Where(p => p.Hide == false).ToList();
                return View(products);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                var viewModel = new ProductViewModel
                {
                    Product = new Product(), // Initialize a new Product object
                    Categories = db.Categories.Select(c => new SelectListItem
                    {
                        Value = c.CategoryID.ToString(),
                        Text = c.CategoryName
                    }).ToList()
                };

                return View(viewModel);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel viewModel)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                if (ModelState.IsValid)
                {
                    // Set default values or perform necessary operations
                    viewModel.Product.Hide = false;

                    db.Products.Add(viewModel.Product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                // If ModelState is not valid, rebind Categories and return to the view
                viewModel.Categories = db.Categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryID.ToString(),
                    Text = c.CategoryName
                }).ToList();

                return View(viewModel);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }


        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }

                var viewModel = new ProductViewModel
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    ImageURL = product.ImageURL,
                    CategoryID = product.CategoryID ?? 0,
                    SaleOff = product.SaleOff ?? 0,
                    Quantity = product.Quantity ?? 0,
                    IsHot = product.IsHot,
                    IsBestSeller = product.IsBestSeller,
                    Categories = db.Categories.Select(c => new SelectListItem
                    {
                        Value = c.CategoryID.ToString(),
                        Text = c.CategoryName
                    }).ToList()
                };

                return View(viewModel);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,Description,Price,ImageURL,CategoryID,SaleOff,Quantity,IsHot,IsBestSeller")] Product product)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                var viewModel = new ProductViewModel
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    ImageURL = product.ImageURL,
                    CategoryID = product.CategoryID ?? 0,
                    SaleOff = product.SaleOff ?? 0,
                    Quantity = product.Quantity ?? 0,
                    IsHot = product.IsHot,
                    IsBestSeller = product.IsBestSeller,
                    Categories = db.Categories.Select(c => new SelectListItem
                    {
                        Value = c.CategoryID.ToString(),
                        Text = c.CategoryName
                    }).ToList()
                };

                return View(viewModel);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                Product product = db.Products.Find(id);

                if (product != null)
                {
                    // Thay vì xóa, đánh dấu sản phẩm là bị ẩn
                    product.Hide = true;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
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
