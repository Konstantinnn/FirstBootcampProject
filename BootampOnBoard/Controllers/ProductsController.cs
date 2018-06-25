using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootampOnBoard.Controllers
{
    public class ProductsController : Controller
    {
        private projectDBEntities db = new projectDBEntities();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        // GET: Get All Products from DB
        public JsonResult GetProducts()
        {
            return this.Json((from obj in db.Products select new { Id = obj.ProductId, Name = obj.ProductName, Price = obj.ProductPrice }), JsonRequestBehavior.AllowGet);
        }

        // GET: Take Product by Id
        public JsonResult GetProduct(int ProductId)
        {
            var product = db.Products.Where(x => x.ProductId == ProductId).SingleOrDefault();
            string value = string.Empty;
            value = Newtonsoft.Json.JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        // GET: Products/Create
        public JsonResult Create(Product newProduct)
        {
            var result = false;
            try
            {
                db.Products.Add(newProduct);
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        // POST: Product Edit(Update)

        public JsonResult Update(Product updatedProduct)
        {
            var result = false;
            try
            {
                var productObj = db.Products.Where(x => x.ProductId == updatedProduct.ProductId).Single();
                productObj.ProductName = updatedProduct.ProductName;
                productObj.ProductPrice = updatedProduct.ProductPrice;
                db.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // POST: Delete Product

        public JsonResult Delete(int ProductId)
        {
            var result = false;
            var product = db.Products.Where(x => x.ProductId == ProductId).Single();
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}