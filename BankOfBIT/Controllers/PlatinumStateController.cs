using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankOfBIT.Models;

namespace BankOfBIT.Controllers
{
    public class PlatinumStateController : Controller
    {
        private BankOfBITContext db = new BankOfBITContext();

        //
        // GET: /PlatinumState/

        public ActionResult Index()
        {
            // Updated to Platinum States table, and get Instance
            return View(PlatinumState.GetInstance());
        }

        //
        // GET: /PlatinumState/Details/5

        public ActionResult Details(int id = 0)
        {
            // Updated to to PlatinumStates table
            PlatinumState platinumstate = db.PlatinumStates.Find(id);
            if (platinumstate == null)
            {
                return HttpNotFound();
            }
            return View(platinumstate);
        }

        //
        // GET: /PlatinumState/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /PlatinumState/Create

        [HttpPost]
        public ActionResult Create(PlatinumState platinumstate)
        {
            if (ModelState.IsValid)
            {
                db.AccountStates.Add(platinumstate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(platinumstate);
        }

        //
        // GET: /PlatinumState/Edit/5

        public ActionResult Edit(int id = 0)
        {
            // Updated to to PlatinumStates table
            PlatinumState platinumstate = db.PlatinumStates.Find(id);
            if (platinumstate == null)
            {
                return HttpNotFound();
            }
            return View(platinumstate);
        }

        //
        // POST: /PlatinumState/Edit/5

        [HttpPost]
        public ActionResult Edit(PlatinumState platinumstate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(platinumstate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(platinumstate);
        }

        //
        // GET: /PlatinumState/Delete/5

        public ActionResult Delete(int id = 0)
        {
            // Updated to to PlatinumStates table
            PlatinumState platinumstate = db.PlatinumStates.Find(id);
            if (platinumstate == null)
            {
                return HttpNotFound();
            }
            return View(platinumstate);
        }

        //
        // POST: /PlatinumState/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // Updated to to PlatinumStates table
            PlatinumState platinumstate = db.PlatinumStates.Find(id);
            db.AccountStates.Remove(platinumstate);
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