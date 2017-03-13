using Microsoft.AspNet.Identity;
using Microsoft.Owin.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using WebApplication1.Models;
using System.Data.Entity.Infrastructure;



namespace WebApplication1.Controllers
{
    public class PurchaseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult SuccessPurchase()
        {
            return View();
        }

        // POST: Purchase/AddToCart
        public ActionResult AddToSessionCart(int id)//, string url, string name, string type, string color, double price, int manufacturerID, string manufacturerName
        {
            try
            {
                ProductModel productToSession = db.ProductModels.Find(id);
                //ProductModel product = new ProductModel();
                //product.ProductModelID = id;
                //product.URLImage = url;
                //product.Name = name;
                //product.Type = type;
                //product.Color = color;
                //product.Price = price;
                //ManufacturerModel modelPhone = new ManufacturerModel();
                //modelPhone.ManufacturerModelID = manufacturerID;
                //modelPhone.Name = manufacturerName;
                //product.ManufacturerModels = modelPhone;
                List<ProductModel> products = Session["cart"] as List<ProductModel>;
                if (products == null)
                {
                    products = new List<ProductModel>();
                }
                products.Add(productToSession);
                Session["cart"] = products;
                //List<ProductModel> products = Session["cart"] as List<ProductModel>;
                //ProductModel product2 = products[(products.Count) - 1];
                //PurchaseModel purchase = new PurchaseModel();
                //purchase.ProductModels = products;

                return this.Json(new { success = "Success adding to cart !" });
            }
            catch (Exception e)
            {
                return this.Json(new { success = "Adding to cart failed" });
            }
        }


        public ActionResult ClearFromSession(int id)
        {
            List<ProductModel> products = Session["cart"] as List<ProductModel>;
            //ProductModel productToRemove = products.Find(id);
            List<ProductModel> newProducts = products.Where(product => !(product.ProductModelID.Equals(id.ToString()))).ToList();
            Session["Cart"] = newProducts;
            //products.Remove();
            
            return this.Json(new { success = "Success removing from cart !" });
        }

        public ActionResult TotalPriceInSession()
        {
            List<ProductModel> products = Session["cart"] as List<ProductModel>;
            int sumInSession = (int)products.Sum(product => product.Price);

            return this.Json(new { success = sumInSession });
        }

        // GET: Purchase
        //public ActionResult AddToCart()
        //{
        //    return View();
        //}


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
        public ActionResult AddToCart()//Create
        {
            //לכאן מגיעים בעת לחיצה על הצג סל
            //שולפים את המוצרים כמו שעשית. לא הייתי מרוקנת עדיין את הסשן אלא מחקה לאישור הקניה בHTTPPOST
            List<ProductModel> products = Session["cart"] as List<ProductModel>;
            //Session["Cart"] = null;
            PurchaseModel purchase = new PurchaseModel();
            purchase.ProductModels = products;
            string Id= User.Identity.GetUserId();
            ApplicationUser currentUser=   db.Users.Where(u => u.Id == Id).FirstOrDefault();
            purchase.ApplicationUser = currentUser;
            purchase.ApplicationUserId =  Guid.Parse( currentUser.Id);
          
            //יצירת אוביקט קניה, השמתרשימת המוצרים ששלפת ושליחת המודל קניה בהחזרת הVIEW
            //הVIEW מחזיר את עמוד הצג סל+ פרטי תשלום
            return View(purchase);
        }

        // POST: Purchase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
       // [Bind(Include = "PurchaseModelID,ApplicationUserId,IsDone,CreditNum,ValidityMonth,ValidityYear")]
        public ActionResult AddToCart( PurchaseModel purchaseModel)
        {
            //לכאן מגיעים בעת לחיצה על SUBMIT אישור הקניה
            //הוספת הקניה ושמירתה לדיבי
            // שליחת מייל למנהל / או למשתמש על שהקניה בוצעה בהצלחה
            //הייתי משתמשת כאן בשדה קניה  טופלה והופכת אותו לאמת ואז בדף של הויו אם המאפיין אמת מציגים קניתך בוצעה בהצלחה מייל ישחל בעוד מספר דקות
            // לדעתי קל יותר לשלוח מייל לקונה היות והוא במצב רגיסטר ואז לוקחיםן מהיוזר הנוכחי את המייל שלו
            // אשלח קוד מייל בהמשך
            if (ModelState.IsValid)
            {
                Session["Cart"] = null;
                purchaseModel.IsDone = true;
              //  var user= db.Users.Where(u => u.Id == purchaseModel.ApplicationUser.Id).FirstOrDefault();
               //db.Users.Update()
                db.PurchaseModels.Add(purchaseModel);
               
                db.SaveChanges();
                //string id = HttpContext.User.Identity.Name.ToString();
                //var userId = User.Identity.GetUserId();
          
                //db.PurchaseModels.AddRange(purchaseModel.ApplicationUserId);
                //db.Users.Join<ApplicationUser>
                //ProfileBase profileBase;
                //if (!String.IsNullOrEmpty(id))
                //    profileBase = ProfileBase.Create(id);
                //else
                //    profileBase = HttpContext.Profile as ProfileBase;
                //profileBase.GetPropertyValue("PersonalInformation.Country");
                ////var user = System.Web.HttpContext.Current.User.Identity.();
                //string mail = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                SendClienEmail("HEY :)", "rbphones2017@gmail.com", "BASHIRIVKI", "rivkiaha@gmail.com", "R&B PHONES", "קניתך התבצעה בהצלחה כפיםםםםם ;-)", @"~\images\product-images\big-pimg1.jpg");
                //send menager email
                return RedirectToAction("SuccessPurchase");
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


        public string SendClienEmail(string displayFrom, string UserAddress, string UserPassword, string emailTo,
                 string subject,
                 string body, string logoRef, string enableSsl = "true")
        {
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient();
            client.Port = 587;//587
            client.Host = "smtp.gmail.com";
            if (enableSsl == "true")
                client.EnableSsl = true;
            else
                client.EnableSsl = false;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(UserAddress, UserPassword);
            MailMessage mail = new MailMessage();

            //set the addresses
            mail.From = new MailAddress(UserAddress, displayFrom);
            mail.To.Add(emailTo);

            //set the content
            mail.Subject = subject;
            // add the body of the message
            string ourmessage = "<p align=\"right\">" + body;
            AlternateView htmlView =
                AlternateView.CreateAlternateViewFromString(
                    ourmessage + " <br /> <br /> <br />.<img src=cid:companylogo>", null, "text/html");

            //create the LinkedResource (embedded image)
           // LinkedResource logo = new LinkedResource(logoRef);
            //logo.ContentId = "companylogo";
            //add the LinkedResource to the appropriate view
            //htmlView.LinkedResources.Add(logo);

            //add the views
            mail.AlternateViews.Add(htmlView);

            try
            {
                client.Send(mail);
                return "Email Send SuccessFully";
            }
            catch (Exception ex)
            {
                //ILogger.Instance.WriteLog("Problem during send mail error= " + ex.ToString());
                return "Email Send failed";
            }
        }

    }
}
