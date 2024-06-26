using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class OrderController : Controller
    {
        private ConnectDB db = new ConnectDB();

        // GET: Order
        public ActionResult Index()
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                // Load invoices including customer details where Hide is false
                var invoices = db.Invoices.Include(i => i.Customer).Where(i => !i.Hide);
                return View(invoices.ToList());
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // Load invoice details including customer and product details
                Invoice invoice = db.Invoices
                                    .Include(i => i.Customer)
                                    .Include(i => i.InvoiceDetails.Select(d => d.Product))
                                    .FirstOrDefault(i => i.InvoiceID == id && !i.Hide);

                if (invoice == null)
                {
                    return HttpNotFound();
                }

                return View(invoice);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // Retrieve the Invoice including Customer details
                Invoice invoice = db.Invoices
                                    .Include(i => i.Customer)
                                    .FirstOrDefault(i => i.InvoiceID == id && !i.Hide);

                if (invoice == null)
                {
                    return HttpNotFound();
                }

                return View(invoice);
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào trang này.";
                return RedirectToAction("AccessDenied", "Account");
            }
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceID,ShipAddress,Phone,Status,Customer")] Invoice invoice)
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        // Update the Invoice details
                        var existingInvoice = db.Invoices.Find(invoice.InvoiceID);
                        if (existingInvoice != null && !existingInvoice.Hide)
                        {
                            existingInvoice.ShipAddress = invoice.ShipAddress;
                            existingInvoice.Phone = invoice.Phone;
                            existingInvoice.Status = invoice.Status;

                            // Update Customer details if necessary
                            existingInvoice.Customer.FirstName = invoice.Customer.FirstName;
                            existingInvoice.Customer.LastName = invoice.Customer.LastName;

                            // Mark the entities as modified
                            db.Entry(existingInvoice).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return HttpNotFound();
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        // Log the exception
                        ModelState.AddModelError("", "Unable to save changes. Please try again or contact support.");
                        // Optionally, rethrow the exception for debugging purposes
                        throw;
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        ModelState.AddModelError("", "An unexpected error occurred while saving. Please contact support.");
                        // Optionally, rethrow the exception for debugging purposes
                        throw;
                    }
                }

                // If ModelState is not valid, return the view with validation errors
                return View(invoice);
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
