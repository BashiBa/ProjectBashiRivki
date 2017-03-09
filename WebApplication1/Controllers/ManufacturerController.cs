using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ManufacturerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manufacturer
        public ActionResult Index()
        {
            return View(db.ManufacturerModels.ToList());
        }

        // GET: Manufacturer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManufacturerModel manufacturerModel = db.ManufacturerModels.Find(id);
            if (manufacturerModel == null)
            {
                return HttpNotFound();
            }
            return View(manufacturerModel);
        }

        // GET: Manufacturer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacturer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManufacturerModelID,Name")] ManufacturerModel manufacturerModel)
        {
            if (ModelState.IsValid)
            {
                db.ManufacturerModels.Add(manufacturerModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manufacturerModel);
        }

        // GET: Manufacturer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManufacturerModel manufacturerModel = db.ManufacturerModels.Find(id);
            if (manufacturerModel == null)
            {
                return HttpNotFound();
            }
            return View(manufacturerModel);
        }

        // POST: Manufacturer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManufacturerModelID,Name")] ManufacturerModel manufacturerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacturerModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manufacturerModel);
        }

        // GET: Manufacturer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManufacturerModel manufacturerModel = db.ManufacturerModels.Find(id);
            if (manufacturerModel == null)
            {
                return HttpNotFound();
            }
            return View(manufacturerModel);
        }

        // POST: Manufacturer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ManufacturerModel manufacturerModel = db.ManufacturerModels.Find(id);
            db.ManufacturerModels.Remove(manufacturerModel);
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

        //public List<ManufacturerModel> GetManufacturers()
        //{
        //    List<ManufacturerModel> a = new List<ManufacturerModel>();
        //    a.Add(db.ManufacturerModels.ToList()[0]);

        //    return a;
        //}

        [WebMethod]
        public string  GetManufactures()
        {
            // instantiate a serializer
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();

            //optional: you can create your own custom converter
            //TheSerializer.RegisterConverters(new JavaScriptConverter[] { new MyCustomJson() });

            var manufactors = db.ManufacturerModels.ToList();

            var TheJson = TheSerializer.Serialize(manufactors);

            //context.Response.Write(TheJson);
            return TheJson;
        }
       
    }
}
