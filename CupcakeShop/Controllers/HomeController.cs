using CupcakeShop.Models;
using CupcakeShop.Service;
using CupcakeShop.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CupcakeShop.Controllers
{
    public class HomeController : Controller
    {
        private ICupcakeService _cupcakeService;
        public HomeController(ICupcakeService cupcakeService)
        {
            this._cupcakeService = cupcakeService;
        }

        

        public ActionResult Index()
        {
            //load list featured cupcakes and pass them to view
            var cupcakeModels = _cupcakeService.GetAllCupcakes()
                .Select(x => x.ToModel())
                .Where(x => x.Featured)
                .OrderByDescending(x => x.LastModified).ToList();
            return View(cupcakeModels);
        }
    }
}
