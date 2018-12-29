using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SatGit.Models;

namespace SatGit.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["musteri"] == null)
                return RedirectToAction("Login", "Kullanicilar");

            return View();
        }

        public ActionResult GirisYap()
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
        public ActionResult GirisYap(Kullanicilar kul)
        {
            try
            {
                using (SatGitDBEntities db = new SatGitDBEntities())
                {
                    Kullanicilar kulOnline = db.Kullanicilars.FirstOrDefault(k => k.KullaniciAdi == kul.KullaniciAdi && k.KullaniciSifre == kul.KullaniciSifre);
                    if (kulOnline !=null)
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
    }
}
