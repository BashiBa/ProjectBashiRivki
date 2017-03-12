using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;
using System.Web.Script.Serialization;

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
        public string Search(int? priceFrom, int? priceTo, string color, string manufacturer)
        {

            if (priceFrom == null)
                priceFrom = 0;
            if (priceTo == null)
                priceTo = 999999;
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            var result = db.ProductModels.Where(p => p.Price > priceFrom && p.Price < priceTo && p.Color == color && p.ManufacturerModels.Name == manufacturer).Include(m=>m.ManufacturerModels);
            var TheJson = TheSerializer.Serialize(result.ToList());

            return TheJson;

        }

        // GET: Product
        public ActionResult ProductsGrid(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var productModels = db.ProductModels.Include(p => p.ManufacturerModels);

            if (!String.IsNullOrEmpty(searchString))
            {
                productModels = productModels.Where(s => s.Name.Contains(searchString)
                                       || s.Color.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "name_desc":
                    productModels = productModels.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    productModels = productModels.OrderBy(s => s.Price);
                    break;
                case "date_desc":
                    productModels = productModels.OrderByDescending(s => s.Price);
                    break;
                default:  // Name ascending 
                    productModels = productModels.OrderBy(s => s.Price);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //return View(students.ToPagedList(pageNumber, pageSize));

            // var productModels = db.ProductModels.Include(p => p.ManufacturerModels);
            return View(productModels.ToPagedList(pageNumber, pageSize));
        }

        // GET: Product


        // GET: Product/Details/5
        public ActionResult Details(int? id)//או זה או את השני
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

        // GET: Product/Details/5
        public ActionResult ProductDetail(int? id)//או זה או את השני
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

        [Authorize(Roles = "Admin")]
        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.ManufacturerModelID = new SelectList(db.ManufacturerModels, "ManufacturerModelID", "Name");
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



        public ActionResult ProductsList(string sortOrder, string currentFilter, string searchString, int? page)
        {
       
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var productModels = db.ProductModels.Include(p => p.ManufacturerModels);
           
            if (!String.IsNullOrEmpty(searchString))
            {
                productModels = productModels.Where(s => s.Name.Contains(searchString)
                                       || s.Color.Contains(searchString));
            }


            switch (sortOrder)
            {
                case "name_desc":
                    productModels = productModels.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    productModels = productModels.OrderBy(s => s.Price);
                    break;
                case "date_desc":
                    productModels = productModels.OrderByDescending(s => s.Price);
                    break;
                default:  // Name ascending 
                    productModels = productModels.OrderBy(s => s.Price);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
           //return View(students.ToPagedList(pageNumber, pageSize));
        
       // var productModels = db.ProductModels.Include(p => p.ManufacturerModels);
            return View(productModels.ToPagedList(pageNumber, pageSize));
        }




    }
}
