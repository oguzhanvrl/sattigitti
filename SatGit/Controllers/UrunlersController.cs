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
    public class UrunlersController : Controller
    {
        private SatGitDBEntities db = new SatGitDBEntities();

        // GET: Urunlers
        public ActionResult Index()
        {
            var urunlers = db.Urunlers.Include(u => u.Kategoriler).Include(u => u.Kullanicilar);
            return View(urunlers.ToList());
        }

        public ActionResult ProductBuy(int? id)
        {
            var urunlers = db.Urunlers.FirstOrDefault(u => u.UrunID == id);
            if (urunlers!=null)
            {
                urunlers.UrunSatildiMi = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Urunlers");
        }


        // GET: Urunlers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunlers.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // GET: Urunlers/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategorilers, "KategoriID", "KategoriAdi");
            ViewBag.KullaniciID = new SelectList(db.Kullanicilars, "KullaniciID", "KullaniciAdi");
            return View();
        }

        // POST: Urunlers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunID,UrunAdi,KullaniciID,UrunSatildiMi,UrunAciklamasi,UrunFiyati,KategoriID,UrunLokasyonu,UrunEklemeTarihi")] Urunler urunler)
        {
            if (ModelState.IsValid)
            {
                urunler.KullaniciID = (int)Session["musteri"];
                db.Urunlers.Add(urunler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategoriID = new SelectList(db.Kategorilers, "KategoriID", "KategoriAdi", urunler.KategoriID);
            ViewBag.KullaniciID = new SelectList(db.Kullanicilars, "KullaniciID", "KullaniciAdi", urunler.KullaniciID);
            return View(urunler);
        }

        // GET: Urunlers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunlers.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategorilers, "KategoriID", "KategoriAdi", urunler.KategoriID);
            ViewBag.KullaniciID = new SelectList(db.Kullanicilars, "KullaniciID", "KullaniciAdi", urunler.KullaniciID);
            return View(urunler);
        }

        // POST: Urunlers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunID,UrunAdi,KullaniciID,UrunSatildiMi,UrunAciklamasi,UrunFiyati,KategoriID,UrunLokasyonu,UrunEklemeTarihi")] Urunler urunler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urunler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategoriID = new SelectList(db.Kategorilers, "KategoriID", "KategoriAdi", urunler.KategoriID);
            ViewBag.KullaniciID = new SelectList(db.Kullanicilars, "KullaniciID", "KullaniciAdi", urunler.KullaniciID);
            return View(urunler);
        }

        // GET: Urunlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunlers.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // POST: Urunlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urunler urunler = db.Urunlers.Find(id);
            db.Urunlers.Remove(urunler);
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
