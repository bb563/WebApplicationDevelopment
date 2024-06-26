using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using WebStore.Helpers;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private ConnectDB db = new ConnectDB();

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = ComputeSha256Hash(model.Password);

                // Kiểm tra xem người dùng có phải là admin không
                var admin = db.Admins.FirstOrDefault(a => a.Email == model.Email && a.Password == hashedPassword);
                if (admin != null)
                {
                    // Đăng nhập thành công cho admin
                    Session["AdminID"] = admin.AdminID;
                    Session["AdminName"] = admin.FirstName + " " + admin.LastName;
                    Session["Role"] = "Admin";
                    return RedirectToAction("Index", "Product");  // Chuyển hướng đến trang quản lý sản phẩm cho Admin
                }

                // Kiểm tra xem người dùng có phải là customer không
                var customer = db.Customers.FirstOrDefault(c => c.Email == model.Email && c.Password == hashedPassword);
                if (customer != null)
                {
                    // Đăng nhập thành công cho customer
                    Session["CustomerID"] = customer.CustomerID;
                    Session["CustomerName"] = customer.FirstName + " " + customer.LastName;
                    Session["Role"] = "Customer";
                    return RedirectToAction("Index", "Home");  // Chuyển hướng đến trang chủ cho Customer
                }

                // Nếu không tìm thấy người dùng nào thỏa mãn điều kiện
                ModelState.AddModelError("", "Invalid email or password.");
            }

            // ModelState không hợp lệ, trở lại view đăng nhập với thông tin model
            return View(model);
        }


        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(CustomerRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Customers.Any(c => c.Email == model.Email))
                {
                    ModelState.AddModelError("", "Email already exists.");
                    return View(model);
                }

                Customer customer = new Customer
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Address = model.Address,
                    Password = ComputeSha256Hash(model.Password) // Mã hóa mật khẩu trước khi lưu
                };

                db.Customers.Add(customer);
                db.SaveChanges();

                // Đăng ký thành công
                Session["CustomerID"] = customer.CustomerID;
                Session["CustomerName"] = customer.FirstName + " " + customer.LastName;
                Session["Role"] = "Customer";
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult AccessDenied()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
