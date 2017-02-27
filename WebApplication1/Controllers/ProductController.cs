using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            var productModels = db.ProductModels.Include(p => p.ManufacturerModels);
            return View(productModels.ToList());
           //var r= db.ProductModels.Where(p=>p.Price<from&&p.Price>ghgh&&)
        }

        // GET: Product
        public ActionResult Search(int? priceFrom, int? priceUntil, string model, string color)
        {
            var result = db.ProductModels.Where(p => p.Price > priceFrom && p.Price < priceUntil && p.Type == model && p.Color == color);
            return View(result.ToList());
        }

        // GET: Product
        public ActionResult ProductsGrid()
        {
            var productModels = db.ProductModels.Include(p => p.ManufacturerModels);
            return View(productModels.ToList());
        }

        // GET: Product
        public ActionResult ProductsList()
        {
            var productModels = db.ProductModels.Include(p => p.ManufacturerModels);
            return View(productModels.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.ProductModels.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.ManufacturerModelID = new SelectList(db.ManufacturerModels, "ManufacturerModelID", "Name");
            return View();
        }


        public ActionResult AddToCart()
        {
            var productModels = db.ProductModels.Include(p => p.ManufacturerModels);
            return View();
        }
        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductModelID,URLImage,Name,Type,Color,Price,ManufacturerModelID")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                db.ProductModels.Add(productModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManufacturerModelID = new SelectList(db.ManufacturerModels, "ManufacturerModelID", "Name", productModel.ManufacturerModelID);
            return View(productModel);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.ProductModels.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManufacturerModelID = new SelectList(db.ManufacturerModels, "ManufacturerModelID", "Name", productModel.ManufacturerModelID);
            return View(productModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductModelID,URLImage,Name,Type,Color,Price,ManufacturerModelID")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManufacturerModelID = new SelectList(db.ManufacturerModels, "ManufacturerModelID", "Name", productModel.ManufacturerModelID);
            return View(productModel);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.ProductModels.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductModel productModel = db.ProductModels.Find(id);
            db.ProductModels.Remove(productModel);
            db.SaveChanges();
            return RedirectToAction("Index");
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
