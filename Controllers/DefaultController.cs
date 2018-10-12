using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UygulamaTestProjesi.Models;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;
using UygulamaTestProjesi.Data;



namespace UygulamaTestProjesi.Controllers
{
    public class DefaultController : Controller
    {
        UygulamaTestDBEntities db = new UygulamaTestDBEntities();
        // GET: Default
        public ActionResult Home(int? id)
        {
            return View(db.tbl_Test.ToList());
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult TestDetay(int id)
        {
            List<tbl_Image> images = db.tbl_Image.Where(a => a.Test_ID == id).ToList();
            ViewBag.testimage = images;
            tbl_Test test = db.tbl_Test.Find(id);
            ViewBag.category = Data.Data.getcategoryname(test.Kategori_ID);
            ViewBag.sonuc = Data.Data.gettestsonuc(test.Sonuc_ID);
            ViewBag.kullanici = Data.Data.getkullanici(test.Kullanici_ID);
            ViewBag.uygulamaadi = db.tbl_Test.Where(a => a.Test_ID == id).Select(a => a.UygulamaAdi).SingleOrDefault();
            ViewBag.testdate = db.tbl_Test.Where(a => a.Test_ID == id).Select(a => a.Test_Tarihi).SingleOrDefault();
         
      
            ViewBag.testdetay = db.tbl_Test.Where(a => a.Test_ID == id).Select(a => a.Test_Detay).SingleOrDefault();
            return View(test);
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            tbl_Kullanici kullanici = Data.Data.KullaniciGiris(username, password);
            if (kullanici != null)
            {
                Session["UserID"] = kullanici.Kullanici_ID;
                return RedirectToAction("Home", "Default");
            }
            else
            {
                ViewBag.errormessage = "Kullanıcı Adı ya da Şifre Hatalı.";
                return View();
            }
        }
        public ActionResult TestKayit()
        {
        
            return View();
        }
        [HttpPost]
        public ActionResult TestKayit(FormCollection form,HttpPostedFileBase[] TestImage)
        {
            tbl_Test test = new tbl_Test();
            test.Test_Detay = form["UygulamaAdi"];
            test.Kategori_ID = int.Parse(form["kategori"].ToString());
            test.Test_Tarihi = DateTime.Today;
            test.Sonuc_ID = int.Parse(form["testsonuc"].ToString());
            test.UygulamaAdi = form["UygulamaAdi"];
            test.Kullanici_ID = int.Parse(Session["UserID"].ToString());
            db.tbl_Test.Add(test);
            db.SaveChanges();


            if (TestImage != null)
            {
                tbl_Image testImage = new tbl_Image();
                foreach (var item in TestImage)
                {
                    string yol = System.IO.Path.GetFileName(item.FileName);
                    string fileadress = Server.MapPath("~/Images/" + yol);
                    item.SaveAs(fileadress);
                    testImage.Image = (yol);
             
                 
                    db.tbl_Image.Add(testImage);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Home","Default");
        }


    }
}