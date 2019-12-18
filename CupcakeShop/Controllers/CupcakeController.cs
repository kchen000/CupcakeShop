using CupcakeShop.Domain;
using CupcakeShop.Models;
using CupcakeShop.Service;
using CupcakeShop.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CupcakeShop.Controllers
{
    public class CupcakeController : Controller
    {
        private ICupcakeService _cupcakeService;

        public CupcakeController(ICupcakeService cupcakeService)
        {
            //dependency injection
            this._cupcakeService = cupcakeService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var cupcakeModels = _cupcakeService.GetAllCupcakes()
                .Select(x => x.ToModel()).OrderBy(x => x.Name).ToList();
            return View(cupcakeModels);
        }

        public ActionResult Edit(int id)
        {
            var cupcake = _cupcakeService.GetCupcakeById(id);

            if (cupcake != null)
            {
                var model = cupcake.ToModel();
                return View(model);
            }
            else
            {
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, CupcakeModel model)
        {
            var cupcake = _cupcakeService.GetCupcakeById(id);
            if (cupcake != null)
            {
                var cupcakeEntity = model.ToEntity();
                //because imagefile is read only and only updated with file upload control so we pass entity file name instead of null
                cupcakeEntity.ImageFileName = cupcake.ImageFileName;
                if (ModelState.IsValid)
                {
                    _cupcakeService.UpdateCupcake(cupcakeEntity);
                }
                else
                {
                    return View(model);
                }
            }
            return RedirectToAction("List");
        }

        public ActionResult Create()
        {
            var model = new CupcakeModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CupcakeModel model)
        {
            //create cupcake
            if (ModelState.IsValid) 
            {
                var cupcake = model.ToEntity();
                _cupcakeService.InsertCupcake(cupcake);
                return RedirectToAction("List");
            }
            return View(model);
        }
        public ActionResult Delete(int Id)
        {
            //delete cupcake
            var cupcake = _cupcakeService.GetCupcakeById(Id);
            if (cupcake != null)
            {
                _cupcakeService.DeleteCupcake(cupcake);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult UploadFile(int id)
        {
            // upload image file and update cupcake image field
            var cupcake = _cupcakeService.GetCupcakeById(id);
            if (cupcake != null)
            {
                try
                {
                    foreach (string file in Request.Files)
                    {
                        var fileContent = Request.Files[file];
                        if (fileContent != null && fileContent.ContentLength > 0)
                        {
                            // get a stream
                            var stream = fileContent.InputStream;
                            // and optionally write the file to disk
                            var fileName = Path.GetFileName(fileContent.FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                            using (var fileStream = System.IO.File.Create(path))
                            {
                                stream.CopyTo(fileStream);
                            }
                            cupcake.ImageFileName = fileName;
                            _cupcakeService.UpdateCupcake(cupcake);
                        }
                    }
                
                }
                catch (Exception)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json("Upload failed");
                }
                return Json(cupcake.ImageFileName);
            }
            else
            {
                return Json(string.Format("Upload failed, can not find cupcake with id:{0}", cupcake.Id));
            }
        }
    }
}
 