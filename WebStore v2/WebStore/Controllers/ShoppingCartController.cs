using System;
using System.Linq;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ConnectDB db = new ConnectDB();

        // Method to get user ID
        private int GetUserId()
        {
            var customerID = Convert.ToInt32(Session["CustomerID"]);
            return customerID;
        }

        // Method to get cart from the database
        private Cart GetCart()
        {
            var userId = GetUserId();
            var cart = db.Carts.Include("CartDetails").SingleOrDefault(c => c.CustomerID == userId);
            if (cart == null)
            {
                cart = new Cart { CustomerID = userId, CreatedDate = DateTime.Now };
                db.Carts.Add(cart);
                db.SaveChanges();
            }
            return cart;
        }

        public ActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home"); // Redirect to home if id is not valid
            }

            var product = db.Products.SingleOrDefault(p => p.ProductID == id);
            if (product != null)
            {
                var cart = GetCart();
                var cartDetail = cart.CartDetails.SingleOrDefault(cd => cd.ProductID == product.ProductID);
                if (cartDetail != null)
                {
                    cartDetail.Quantity++;
                }
                else
                {
                    cartDetail = new CartDetail
                    {
                        ProductID = product.ProductID,
                        CartID = cart.CartID,
                        Quantity = 1
                    };
                    cart.CartDetails.Add(cartDetail);
                }
                db.SaveChanges();

                // Update cart total in session
                UpdateCartTotal();
            }

            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        // GET: ShoppingCart/ShowToCart
        public ActionResult ShowToCart()
        {
            var cart = GetCart();
            return View(cart);
        }

        public ActionResult RemoveCart(int id)
        {
            var cart = GetCart();
            cart.RemoveCartItem(id);

            // Update cart total in session
            UpdateCartTotal();

            return RedirectToAction("ShowToCart", "ShoppingCart");
        }




        // Method to update cart total
        private void UpdateCartTotal()
        {
            var cart = GetCart();
            decimal total = cart.TotalMoney();
            Session["CartTotal"] = total;
        }

        // GET: ShoppingCart/Checkout
        public ActionResult Checkout()
        {
            var cart = GetCart();

            int? customerId = Session["CustomerID"] as int?;
            if (!customerId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            string shipAddress = ""; // Replace with method to get ship address
            string phone = ""; // Replace with method to get phone number

            decimal totalPrice = (decimal)Session["CartTotal"];

            var newInvoice = new Invoice
            {
                CustomerID = customerId.Value,
                InvoiceDate = DateTime.Now,
                ShipAddress = shipAddress,
                Phone = phone,
                Total = totalPrice,
                Status = "Pending",
                Date = DateTime.Now
            };

            db.Invoices.Add(newInvoice);
            db.SaveChanges();

            foreach (var item in cart.CartDetails)
            {
                var detail = new InvoiceDetail
                {
                    InvoiceID = newInvoice.InvoiceID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                };

                db.InvoiceDetails.Add(detail);
            }

            db.SaveChanges();
            Session["Cart"] = null;

            return RedirectToAction("InvoiceConfirmation", new { invoiceId = newInvoice.InvoiceID });
        }

        // GET: ShoppingCart/InvoiceConfirmation/{invoiceId}
        public ActionResult InvoiceConfirmation(int invoiceId)
        {
            var invoice = db.Invoices.Find(invoiceId);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            return View(invoice);
        }

        // GET: ShoppingCart/Payment
        public ActionResult Payment()
        {
            var cart = GetCart();

            if (cart == null)
            {
                // Log the error or handle it as per your application needs
                throw new Exception("Cart not found.");
            }

            int? customerId = Session["CustomerID"] as int?;
            if (!customerId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            decimal totalPrice;
            if (Session["CartTotal"] == null || !(Session["CartTotal"] is decimal))
            {
                // Calculate total directly from cart if session variable is missing
                totalPrice = cart.TotalMoney();
            }
            else
            {
                totalPrice = (decimal)Session["CartTotal"];
            }

            var newInvoice = new Invoice
            {
                CustomerID = customerId.Value,
                InvoiceDate = DateTime.Now,
                Total = totalPrice,
                Status = "Pending",
                Date = DateTime.Now
            };

            db.Invoices.Add(newInvoice);
            db.SaveChanges();

            if (cart.CartDetails == null)
            {
                // Log the error or handle it as per your application needs
                throw new Exception("Cart details are null.");
            }

            foreach (var item in cart.CartDetails)
            {
                if (item.Product == null)
                {
                    // Log the error or handle it as per your application needs
                    throw new Exception($"Product not found for CartDetailID: {item.CartDetailID}");
                }

                var detail = new InvoiceDetail
                {
                    InvoiceID = newInvoice.InvoiceID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                };

                db.InvoiceDetails.Add(detail);
            }

            db.SaveChanges();
            Session["Cart"] = null;

            return RedirectToAction("InvoiceConfirmation", new { invoiceId = newInvoice.InvoiceID });
        }

        // POST: ShoppingCart/SaveInvoice
        [HttpPost]
        public ActionResult SaveInvoice(int invoiceId)
        {
            try
            {
                var invoice = db.Invoices.Find(invoiceId);
                if (invoice == null)
                {
                    return HttpNotFound();
                }

                invoice.Status = "Completed";
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error saving invoice: " + ex.Message;
                return View("Error");
            }
        }

        // GET: ShoppingCart/Orders
        public ActionResult Orders()
        {
            int? customerId = Session["CustomerID"] as int?;
            if (!customerId.HasValue)
            {
                return RedirectToAction("Login", "Account", new { ReturnUrl = Url.Action("Orders") });
            }

            var orders = db.Invoices.Where(i => i.CustomerID == customerId.Value)
                                    .OrderByDescending(i => i.InvoiceDate).ToList();

            if (!orders.Any())
            {
                return View("NoOrdersFound");
            }

            return View(orders);
        }

        // GET: ShoppingCart/InvoiceDetails/{invoiceId}
        public ActionResult InvoiceDetails(int invoiceId)
        {
            int? customerId = Session["CustomerID"] as int?;

            var invoice = db.Invoices.Include("InvoiceDetails")
                                     .SingleOrDefault(i => i.InvoiceID == invoiceId && i.CustomerID == customerId);
            if (invoice == null)
            {
                return HttpNotFound();
            }

            return View(invoice);
        }
    }
}
