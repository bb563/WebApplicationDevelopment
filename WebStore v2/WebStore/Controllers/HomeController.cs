using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        readonly ConnectDB db;

        public HomeController()
        {
            this.db = new ConnectDB();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";
            List<Product> proList = this.db.Products.ToList();

            return View(proList);
        }

        public ActionResult About()
        {
            return View();
    
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

}