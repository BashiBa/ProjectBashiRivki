using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Controllers;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
       

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Deals()
        {
            ViewBag.Message = "Your branches page .";

            return View();
        }
        public ActionResult canvas()
        {
           

            return View();
        }
        public ActionResult Branches()
        {
            ViewBag.Message = "Your branches page .";

            return View();
        }

        public ActionResult AboutTheShop()
        {
            ViewBag.Message = "Your branches page .";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult ViewModels()
        //{

        //    var ProductCtrl = new ProductController();
        //    ProductCtrl.ControllerContext = ControllerContext;
        //    var Manufacturerctrl = new ManufacturerController();
        //    Manufacturerctrl.ControllerContext = ControllerContext;
        //    //call action

        //    ViewModels vm = new ViewModels();
        //    vm.Products = ProductCtrl.GetProducts();
        //    vm.Manufacturers = Manufacturerctrl.GetManufacturers();

        //    return View(vm);
        //}


    }
}