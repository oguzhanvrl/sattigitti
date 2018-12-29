using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SatGit.Models;

namespace SatGit.Controllers
{
    public class KullanicilarsController : ApiController
    {
        private SatGitDBEntities db = new SatGitDBEntities();

        // GET: api/Kullanicilars
        public IEnumerable<Kullanicilar> GetKullanicilars()
        {
            return db.Kullanicilars;
        }

        // GET: api/Kullanicilars/5
        [ResponseType(typeof(Kullanicilar))]
        public IHttpActionResult GetKullanicilar(int id)
        {
            Kullanicilar kullanicilar = db.Kullanicilars.Find(id);
            if (kullanicilar == null)
            {
                return NotFound();
            }

            return Ok(kullanicilar);
        }

        // PUT: api/Kullanicilars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKullanicilar(int id, Kullanicilar kullanicilar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kullanicilar.KullaniciID)
            {
                return BadRequest();
            }

            db.Entry(kullanicilar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KullanicilarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Kullanicilars
        [ResponseType(typeof(KullaniciModel))]
        public IHttpActionResult PostKullanicilar(KullaniciModel kullanicilar)
        {
            var kullanici = kullanicilar;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Kullanicilar kul = new Kullanicilar
            {
                KullaniciAdi = kullanicilar.KullaniciAdi,
                KullaniciMail = kullanicilar.KullaniciMail,
                KullaniciSifre = kullanicilar.KullaniciSifre
            };

            db.Kullanicilars.Add(kul);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kullanicilar.KullaniciID }, kullanicilar);
        }

        // DELETE: api/Kullanicilars/5
        [ResponseType(typeof(Kullanicilar))]
        public IHttpActionResult DeleteKullanicilar(int id)
        {
            Kullanicilar kullanicilar = db.Kullanicilars.Find(id);
            if (kullanicilar == null)
            {
                return NotFound();
            }

            db.Kullanicilars.Remove(kullanicilar);
            db.SaveChanges();

            return Ok(kullanicilar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KullanicilarExists(int id)
        {
            return db.Kullanicilars.Count(e => e.KullaniciID == id) > 0;
        }
    }
}