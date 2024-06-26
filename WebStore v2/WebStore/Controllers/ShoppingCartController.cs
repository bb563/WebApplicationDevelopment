using System;
using System.Linq;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ConnectDB db = new ConnectDB();

        // Phương thức hỗ trợ để lấy giỏ hàng từ session
        private Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        // GET: ShoppingCart/AddtoCart/{id}
        public ActionResult AddtoCart(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home"); // Chuyển hướng về trang chủ nếu id không hợp lệ
            }

            var product = db.Products.SingleOrDefault(p => p.ProductID == id);
            if (product != null)
            {
                GetCart().Add(product);
            }

            // Cập nhật tổng tiền của giỏ hàng
            UpdateCartTotal();

            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        // POST: ShoppingCart/Update_Quantity_Cart
        [HttpPost]
        public ActionResult Update_Quantity_Cart(int ID_Product, int Quantity)
        {
            Cart cart = GetCart();
            cart.UpdateQuantity(ID_Product, Quantity);

            // Cập nhật tổng tiền của giỏ hàng
            UpdateCartTotal();

            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        // GET: ShoppingCart/ShowToCart
        public ActionResult ShowToCart()
        {
            Cart cart = GetCart();
            return View(cart);
        }

        // GET: ShoppingCart/RemoveCart/{id}
        public ActionResult RemoveCart(int id)
        {
            Cart cart = GetCart();
            cart.Remove(id);

            // Cập nhật tổng tiền của giỏ hàng
            UpdateCartTotal();

            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        // Cập nhật tổng tiền của giỏ hàng
        private void UpdateCartTotal()
        {
            Cart cart = GetCart();
            decimal total = (decimal)cart.Total_Money();
            Session["CartTotal"] = total; // Lưu tổng tiền vào Session để sử dụng sau này
        }

        // GET: ShoppingCart/Checkout
        public ActionResult Checkout()
        {
            // Lấy giỏ hàng từ session
            Cart cart = GetCart();

            // Lấy CustomerID từ session (giả sử bạn đã lưu CustomerID trong session ở phần đăng nhập hoặc đăng ký của AccountController)
            int? customerId = Session["CustomerID"] as int?;
            if (!customerId.HasValue)
            {
                return RedirectToAction("Login", "Account"); // Chuyển hướng đến trang đăng nhập nếu chưa đăng nhập
            }

            // Lấy thông tin khác từ form hoặc từ session
            string shipAddress = ""; // Thay bằng phương thức lấy địa chỉ giao hàng từ form hoặc từ session
            string phone = ""; // Thay bằng phương thức lấy số điện thoại từ form hoặc từ session

            // Tính tổng số tiền của các sản phẩm trong giỏ hàng
            decimal totalPrice = (decimal)Session["CartTotal"]; // Lấy tổng tiền từ Session

            // Tạo hóa đơn mới
            Invoice newInvoice = new Invoice
            {
                CustomerID = customerId.Value,
                InvoiceDate = DateTime.Now,
                ShipAddress = shipAddress,
                Phone = phone,
                Total = totalPrice,
                Status = "Pending",  // Thiết lập trạng thái mặc định là đang chờ
                Date = DateTime.Now   // Thiết lập ngày hóa đơn
                                      // Bạn có thể thiết lập các thuộc tính khác như địa chỉ giao hàng, số điện thoại, ...
            };

            db.Invoices.Add(newInvoice);
            db.SaveChanges();

            // Lưu chi tiết hóa đơn (các sản phẩm trong giỏ hàng)
            foreach (var item in cart.Items)
            {
                InvoiceDetail detail = new InvoiceDetail
                {
                    InvoiceID = newInvoice.InvoiceID,
                    ProductID = item.shopping_Product.ProductID,
                    Quantity = item.shopping_Quantity,
                    UnitPrice = item.shopping_Product.Price
                };

                db.InvoiceDetails.Add(detail);
            }

            db.SaveChanges();

            // Xóa session giỏ hàng sau khi thanh toán
            Session["Cart"] = null;

            // Chuyển hướng đến trang xác nhận hóa đơn
            return RedirectToAction("InvoiceConfirmation", new { invoiceId = newInvoice.InvoiceID });
        }


        // GET: ShoppingCart/InvoiceConfirmation/{invoiceId}
        public ActionResult InvoiceConfirmation(int invoiceId)
        {
            // Lấy thông tin chi tiết của hóa đơn từ cơ sở dữ liệu và truyền đến view
            Invoice invoice = db.Invoices.Find(invoiceId);
            if (invoice == null)
            {
                return HttpNotFound(); // Xử lý khi không tìm thấy hóa đơn
            }

            return View(invoice);
        }

        // GET: ShoppingCart/Payment
        public ActionResult Payment()
        {
            // Lấy giỏ hàng từ session
            Cart cart = GetCart();

            // Lấy CustomerID từ session (giả sử bạn đã lưu CustomerID trong session ở phần đăng nhập hoặc đăng ký của AccountController)
            int? customerId = Session["CustomerID"] as int?;
            if (!customerId.HasValue)
            {
                return RedirectToAction("Login", "Account"); // Chuyển hướng đến trang đăng nhập nếu chưa đăng nhập
            }

            // Tính tổng số tiền của các sản phẩm trong giỏ hàng
            decimal totalPrice = (decimal)Session["CartTotal"]; // Lấy tổng tiền từ Session

            // Tạo hóa đơn mới
            Invoice newInvoice = new Invoice
            {
                CustomerID = customerId.Value,
                InvoiceDate = DateTime.Now,
                Total = totalPrice,
                Status = "Pending",  // Thiết lập trạng thái mặc định là đang chờ
                Date = DateTime.Now   // Thiết lập ngày hóa đơn
                                      // Bạn có thể thiết lập các thuộc tính khác như địa chỉ giao hàng, số điện thoại, ...
            };

            db.Invoices.Add(newInvoice);
            db.SaveChanges();

            // Lưu chi tiết hóa đơn (các sản phẩm trong giỏ hàng)
            foreach (var item in cart.Items)
            {
                InvoiceDetail detail = new InvoiceDetail
                {
                    InvoiceID = newInvoice.InvoiceID,
                    ProductID = item.shopping_Product.ProductID,
                    Quantity = item.shopping_Quantity,
                    UnitPrice = item.shopping_Product.Price
                };

                db.InvoiceDetails.Add(detail);
            }

            db.SaveChanges();

            // Xóa session giỏ hàng sau khi thanh toán
            Session["Cart"] = null;

            // Chuyển hướng đến trang xác nhận hóa đơn
            return RedirectToAction("InvoiceConfirmation", new { invoiceId = newInvoice.InvoiceID });
        }

        // POST: ShoppingCart/SaveInvoice
        [HttpPost]
        public ActionResult SaveInvoice(int invoiceId)
        {
            try
            {
                // Lấy thông tin hóa đơn từ cơ sở dữ liệu để lưu lại
                Invoice invoice = db.Invoices.Find(invoiceId);
                if (invoice == null)
                {
                    return HttpNotFound(); // Xử lý khi không tìm thấy hóa đơn
                }

                // Thiết lập trạng thái hóa đơn thành "Completed"
                invoice.Status = "Completed";

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();

                return RedirectToAction("Index", "Home"); // Chuyển hướng về trang chủ sau khi lưu hóa đơn thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi lưu hóa đơn
                ViewBag.Error = "Error saving invoice: " + ex.Message;
                return View("Error");
            }
        }
        // GET: ShoppingCart/Orders
        public ActionResult Orders()
        {
            // Lấy CustomerID từ session
            int? customerId = Session["CustomerID"] as int?;
            if (!customerId.HasValue)
            {
                return RedirectToAction("Login", "Account", new { ReturnUrl = Url.Action("Orders", "ShoppingCart") }); // Chuyển hướng đến trang đăng nhập và trả về lại đường dẫn Orders nếu chưa đăng nhập
            }

            // Lấy danh sách các đơn hàng của khách hàng từ cơ sở dữ liệu
            var orders = db.Invoices.Where(i => i.CustomerID == customerId.Value).OrderByDescending(i => i.InvoiceDate).ToList();

            if (orders == null || !orders.Any())
            {
                return View("NoOrdersFound"); // Trả về view thông báo khi không tìm thấy đơn hàng nào của khách hàng
            }

            return View(orders);
        }

        // GET: ShoppingCart/InvoiceDetails/{invoiceId}
        public ActionResult InvoiceDetails(int invoiceId)
        {
            // Lấy CustomerID từ session
            int? customerId = Session["CustomerID"] as int?;

            var invoice = db.Invoices.Include("InvoiceDetails").SingleOrDefault(i => i.InvoiceID == invoiceId && i.CustomerID == customerId);
            if (invoice == null)
            {
                return HttpNotFound(); // Xử lý khi không tìm thấy hóa đơn hoặc không có quyền truy cập
            }

            return View(invoice);
        }


    }
}
