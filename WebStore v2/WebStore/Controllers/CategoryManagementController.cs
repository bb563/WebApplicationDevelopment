using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class CategoryManagementController : Controller
    {
        private ConnectDB _dbContext = new ConnectDB();

        // Kiểm tra quyền truy cập của admin
        private bool IsAdmin()
        {
            return Session["Role"] != null && Session["Role"].ToString() == "Admin";
        }

        // GET: Categories
        public ActionResult Index()
        {
            if (!IsAdmin())
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }

            return View(_dbContext.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (!IsAdmin())
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = _dbContext.Categories.Include(c => c.Products).SingleOrDefault(c => c.CategoryID == id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            if (!IsAdmin())
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }

            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,Description")] Category category)
        {
            if (!IsAdmin())
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!IsAdmin())
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,Description")] Category category)
        {
            if (!IsAdmin())
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Entry(category).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!IsAdmin())
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsAdmin())
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }

            Category category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            // Set trạng thái Hide của danh mục thành true (ẩn)
            category.Hide = true;

            // Lấy tất cả sản phẩm thuộc danh mục này
            var products = _dbContext.Products.Where(p => p.CategoryID == id).ToList();

            // Set trạng thái Hide của tất cả sản phẩm thành true (ẩn)
            foreach (var product in products)
            {
                product.Hide = true;
                _dbContext.Entry(product).State = EntityState.Modified;
            }

            _dbContext.Entry(category).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
