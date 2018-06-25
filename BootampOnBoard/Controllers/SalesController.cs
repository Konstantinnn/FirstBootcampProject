using BootampOnBoard.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootampOnBoard.Controllers
{
    public class SalesController : Controller
    {
        private projectDBEntities db = new projectDBEntities();

        // GET: Sales
        public ActionResult Index()
        {
            ViewBag.Ps_CustomerId = new SelectList(db.Customers, "CustomerId", "CustomerName");
            ViewBag.Ps_ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            ViewBag.Ps_StoreId = new SelectList(db.Stores, "StoreId", "StoreName");
            return View();
        }

        //Get All Records from db
        public JsonResult GetRecordList()
        {
            List<SalesViewModel> viewList = db.ProductsSolds.Select(x => new SalesViewModel
            {

                Ps_Id = x.Ps_Id,
                Ps_CustomerId = x.Ps_CustomerId,
                Ps_ProductId = x.Ps_ProductId,
                Ps_StoreId = x.Ps_StoreId,
                DateSold = x.DateSold,
                ProductName = x.Product.ProductName,
                CustomerName = x.Customer.CustomerName,
                StoreName = x.Store.StoreName
            }).ToList();
            return Json(viewList, JsonRequestBehavior.AllowGet);
        }

        // Get Record By Id
        public JsonResult GetRecordById(int RecordId)
        {
            var model = db.ProductsSolds.Where(p => p.Ps_Id == RecordId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }


        // Save new Record
        public JsonResult SaveRecord(SalesViewModel model)
        {
            var result = false;
            try
            {
                if (model.Ps_Id > 0)
                {
                    var record = db.ProductsSolds.SingleOrDefault(x => x.Ps_Id == model.Ps_Id);
                    record.Ps_CustomerId = model.Ps_CustomerId;
                    record.Ps_ProductId = model.Ps_ProductId;
                    record.Ps_StoreId = model.Ps_StoreId;
                    record.DateSold = model.DateSold;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    var record = new ProductsSold();
                    record.Ps_CustomerId = model.Ps_CustomerId;
                    record.Ps_ProductId = model.Ps_ProductId;
                    record.Ps_StoreId = model.Ps_StoreId;
                    record.DateSold = model.DateSold;
                    db.ProductsSolds.Add(record);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRecord(int RecordId)
        {
            bool result = false;
            var del = db.ProductsSolds.SingleOrDefault(x => x.Ps_Id == RecordId);
            if (del != null)
            {
                db.ProductsSolds.Remove(del);
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