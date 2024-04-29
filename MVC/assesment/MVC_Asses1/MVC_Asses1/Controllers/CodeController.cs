using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Asses1.Models;



namespace MVC_Asses1.Controllers
{
    public class CodeController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();
        // GET: Code
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CustomersInGermany()
        {
            var customersInGermany = db.Customers.Where(c => c.Country == "Germany").ToList();
            return View(customersInGermany);
        }

        public ActionResult CustomerWithOrderId10248()
        {
            var customer = db.Customers.FirstOrDefault(c => c.Orders.Any(o => o.OrderID == 10248));
            return View(customer);
        }
    }
}
