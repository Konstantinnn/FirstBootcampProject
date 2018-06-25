using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootampOnBoard.Controllers
{
    public class StoresController : Controller
    {
        private projectDBEntities db = new projectDBEntities();
        // GET: Stores
        public ActionResult Index()
        {
            return View(db.Stores.ToList());
        }

        // GET: Get All Stores from DB
        public JsonResult GetStores()
        {
            return this.Json((from obj in db.Stores select new { Id = obj.StoreId, Name = obj.StoreName, Address = obj.StoreAddress }), JsonRequestBehavior.AllowGet);
        }

        // GET: Take Store by Id
        public JsonResult GetStore(int StoreId)
        {
            var store = db.Stores.Where(x => x.StoreId == StoreId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(store, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        // GET: Store/Create
        public JsonResult Create(Store newStore)
        {
            var result = false;
            try
            {
                db.Stores.Add(newStore);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        // POST: Store Edit(Update)

        public JsonResult Update(Store updatedStore)
        {
            var result = false;
            try
            {
                var storeObj = db.Stores.Where(x => x.StoreId == updatedStore.StoreId).Single();
                storeObj.StoreName = updatedStore.StoreName;
                storeObj.StoreAddress = updatedStore.StoreAddress;
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Delete Store

        public JsonResult Delete(int StoreId)
        {
            var result = false;
            var store = db.Stores.Where(x => x.StoreId == StoreId).SingleOrDefault();
            if (store != null)
            {
                db.Stores.Remove(store);
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