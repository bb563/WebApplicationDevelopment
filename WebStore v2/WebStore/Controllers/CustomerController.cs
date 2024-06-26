using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class CustomerController : Controller
    {
        private ConnectDB db = new ConnectDB();

        // Kiểm tra quyền truy cập của admin
        private bool IsAdmin()
        {
            return Session["Role"] != null && Session["Role"].ToString() == "Admin";
        }

        // GET: Customer
        public ActionResult Index()
        {
            if (IsAdmin())
            {
                var customers = db.Customers.Where(c => !c.Hide).ToList();
                return View(customers);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (IsAdmin())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = db.Customers.Find(id);
                if (customer == null || customer.Hide)
                {
                    return HttpNotFound();
                }

                // Include related invoices for the customer
                customer.Invoices = db.Invoices.Where(i => i.CustomerID == id).ToList();

                return View(customer);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (IsAdmin())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = db.Customers.Find(id);
                if (customer == null || customer.Hide)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (IsAdmin())
            {
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }

                // Set trạng thái Hide của khách hàng thành true (ẩn)
                customer.Hide = true;
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
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
