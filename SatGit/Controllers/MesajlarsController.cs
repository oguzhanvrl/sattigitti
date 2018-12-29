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
    public class MesajlarsController : ApiController
    {
        private SatGitDBEntities db = new SatGitDBEntities();

        // GET: api/Mesajlars
        public IQueryable<Mesajlar> GetMesajlars()
        {
            return db.Mesajlars;
        }

        // GET: api/Mesajlars/5
        [ResponseType(typeof(Mesajlar))]
        public IHttpActionResult GetMesajlar(int id)
        {
            Mesajlar mesajlar = db.Mesajlars.Find(id);
            if (mesajlar == null)
            {
                return NotFound();
            }

            return Ok(mesajlar);
        }

        // PUT: api/Mesajlars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMesajlar(int id, Mesajlar mesajlar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mesajlar.MesajID)
            {
                return BadRequest();
            }

            db.Entry(mesajlar).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MesajlarExists(id))
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

        // POST: api/Mesajlars
        [ResponseType(typeof(MesajlarModel))]
        public IHttpActionResult PostMesajlar(MesajlarModel mesaj)
        {
            var mesajlar = mesaj;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mesajlar msj = new Mesajlar {
            AliciID=db.Urunlers.FirstOrDefault(x => x.UrunID == mesajlar.UrunID).KullaniciID,
            GondericiID = mesajlar.GondericiID,
            MesajIcerik= mesajlar.MesajIcerik,
            MesajTarihi= DateTime.Now.Date,
            UrunID=mesajlar.UrunID
        };


            db.Mesajlars.Add(msj);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mesajlar.MesajID }, mesajlar);
        }



        // DELETE: api/Mesajlars/5
        [ResponseType(typeof(Mesajlar))]
        public IHttpActionResult DeleteMesajlar(int id)
        {
            Mesajlar mesajlar = db.Mesajlars.Find(id);
            if (mesajlar == null)
            {
                return NotFound();
            }

            db.Mesajlars.Remove(mesajlar);
            db.SaveChanges();

            return Ok(mesajlar);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MesajlarExists(int id)
        {
            return db.Mesajlars.Count(e => e.MesajID == id) > 0;
        }
    }
}