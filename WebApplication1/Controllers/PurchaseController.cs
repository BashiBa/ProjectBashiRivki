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
    public class PurchaseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Purchase
        public ActionResult Index()
        {
            return View(db.PurchaseModels.ToList());
        }

        // GET: Purchase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseModel purchaseModel = db.PurchaseModels.Find(id);
            if (purchaseModel == null)
            {
                return HttpNotFound();
            }
            return View(purchaseModel);
        }

        // GET: Purchase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Purchase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseModelID,ApplicationUserId,IsDone,CreditNum,ValidityMonth,ValidityYear")] PurchaseModel purchaseModel)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseModels.Add(purchaseModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchaseModel);
        }

        // GET: Purchase/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseModel purchaseModel = db.PurchaseModels.Find(id);
            if (purchaseModel == null)
            {
                return HttpNotFound();
            }
            return View(purchaseModel);
        }

        // POST: Purchase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseModelID,ApplicationUserId,IsDone,CreditNum,ValidityMonth,ValidityYear")] PurchaseModel purchaseModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchaseModel);
        }

        // GET: Purchase/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseModel purchaseModel = db.PurchaseModels.Find(id);
            if (purchaseModel == null)
            {
                return HttpNotFound();
            }
            return View(purchaseModel);
        }

        // POST: Purchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseModel purchaseModel = db.PurchaseModels.Find(id);
            db.PurchaseModels.Remove(purchaseModel);
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
