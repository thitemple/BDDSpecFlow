using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MeAnota.Models;

namespace MeAnota.Controllers
{
    [Authorize]
    public class BlocoController : Controller
    {
        private MeAnotaContext db = new MeAnotaContext();

        //
        // GET: /Bloco/

        public ActionResult Index()
        {
            return View(db.Blocos.Where(b => b.Proprietario.Email == User.Identity.Name));
        }

        //
        // GET: /Bloco/Details/5

        public ActionResult Details(int id = 0)
        {
            Bloco bloco = db.Blocos.Find(id);
            if (bloco == null)
            {
                return HttpNotFound();
            }
            return View(bloco);
        }

        //
        // GET: /Bloco/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Bloco/Create

        [HttpPost]
        public ActionResult Create(Bloco bloco)
        {
            if (ModelState.IsValid)
            {
                bloco.Proprietario = db.Usuarios.FirstOrDefault(u => u.Email == User.Identity.Name);
                db.Blocos.Add(bloco);
                db.SaveChanges();
                TempData["mensagem"] = "Bloco adicionado com sucesso";
                return RedirectToAction("Index");
            }

            return View(bloco);
        }

        //
        // GET: /Bloco/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Bloco bloco = db.Blocos.Find(id);
            if (bloco == null)
            {
                return HttpNotFound();
            }
            return View(bloco);
        }

        //
        // POST: /Bloco/Edit/5

        [HttpPost]
        public ActionResult Edit(Bloco bloco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bloco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bloco);
        }

        //
        // GET: /Bloco/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Bloco bloco = db.Blocos.Find(id);
            if (bloco == null)
            {
                return HttpNotFound();
            }
            return View(bloco);
        }

        //
        // POST: /Bloco/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Bloco bloco = db.Blocos.Find(id);
            db.Blocos.Remove(bloco);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}