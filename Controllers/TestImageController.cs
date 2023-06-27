using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestImage.Models;

namespace TestImage.Controllers
{
    public class TestImageController : Controller
    {
        // GET: TestImage
        public ActionResult Index()
        {
            using (DBModel dBmodel = new DBModel())
            {
                return View(dBmodel.tb1_Image.ToList());
            }
            
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tb1_Image tb1_Image)
        {
            string fileName = Path.GetFileNameWithoutExtension(tb1_Image.ImageFile.FileName);
            string extension = Path.GetExtension(tb1_Image.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            tb1_Image.Image = "../Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("../Image/"), fileName);
            tb1_Image.ImageFile.SaveAs(fileName);
            using (DBModel dBmodel = new DBModel())
            {
                dBmodel.tb1_Image.Add(tb1_Image);
                dBmodel.SaveChanges();
            }
            ModelState.Clear();
            return View();
        }
    }
}