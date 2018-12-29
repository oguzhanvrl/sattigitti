using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SatGit.Models;

namespace SatGit.Controllers
{
    public class KullanicilarController : Controller
    {
        private SatGitDBEntities db = new SatGitDBEntities();

        // GET: Kullanicilar
        public ActionResult Index()
        {
            return View();
        }

        // GET: Kullanicilar
        public ActionResult Login()
        {
            try
            {
                if (Session["musteri"] != null)
                    return RedirectToAction("Index", "Home");
                else
                {
                    Session["musteri"] = null;
                    return View();
                }

            }
            catch (Exception e)
            {
                return View();
            }
        }


        [HttpPost]
        public ActionResult Login(Kullanicilar kul)
        {
            try
            {
                using (SatGitDBEntities db = new SatGitDBEntities())
                {
                    Kullanicilar kulOnline = db.Kullanicilars.FirstOrDefault(k => k.KullaniciAdi == kul.KullaniciAdi && k.KullaniciSifre == kul.KullaniciSifre);
                    if (kulOnline != null)
                    {
                        Session["musteri"] = kulOnline.KullaniciID;
                        return RedirectToAction("Index", "Home");
                    }

                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Kullanicilar/Details/5
        public ActionResult Details()
        {

            return View();
        }

        // GET: Kullanicilar/Create
        public ActionResult Create()
        {
            if (Session["musteri"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }


        // GET: Kullanicilar/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // GET: Kullanicilar/Delete/5
        public ActionResult Delete()
        {
            if (Session["musteri"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult DeleteConfirmed()
        {
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
