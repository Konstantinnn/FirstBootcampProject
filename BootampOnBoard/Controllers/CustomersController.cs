using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootampOnBoard.Controllers
{
    public class CustomersController : Controller
    {
        private projectDBEntities db = new projectDBEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Get All Customers from DB
        public JsonResult GetCustomers()
        {
            return this.Json((from obj in db.Customers select new { Id = obj.CustomerId, Name = obj.CustomerName, Address = obj.CustomerAddress }), JsonRequestBehavior.AllowGet);
        }

        // GET: Take Customer by Id
        public JsonResult GetCustomer(int customerId)
        {
            var customer = db.Customers.Where(x => x.CustomerId == customerId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(customer, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        // GET: Customers/Create
        public JsonResult Create(Customer newCustomer)
        {
            var result = false;
            try
            {
                db.Customers.Add(newCustomer);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        // POST: Customer Edit(Update)

        public JsonResult Update(Customer updatedCustomer)
        {
            var result = false;
            try
            {
                var customerObj = db.Customers.Where(x => x.CustomerId == updatedCustomer.CustomerId).Single();
                customerObj.CustomerName = updatedCustomer.CustomerName;
                customerObj.CustomerAddress = updatedCustomer.CustomerAddress;
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Delete Customer

        public JsonResult Delete(int CustomerId)
        {
            var result = false;
            var customer = db.Customers.Where(x => x.CustomerId == CustomerId).SingleOrDefault();
            if (customer != null)
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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