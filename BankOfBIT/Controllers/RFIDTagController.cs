﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankOfBIT.Models;

namespace BankOfBIT.Controllers
{
    public class RFIDTagController : Controller
    {
        private BankOfBITContext db = new BankOfBITContext();

        //
        // GET: /RFIDTag/

        public ActionResult Index()
        {
            var rfidtags = db.RFIDTags.Include(r => r.Client);
            return View(rfidtags.ToList());
        }

        //
        // GET: /RFIDTag/Details/5

        public ActionResult Details(int id = 0)
        {
            RFIDTag rfidtag = db.RFIDTags.Find(id);
            if (rfidtag == null)
            {
                return HttpNotFound();
            }
            return View(rfidtag);
        }

        //
        // GET: /RFIDTag/Create

        public ActionResult Create()
        {
            // Modified to display Full Name instead of First Name
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");
            return View();
        }

        //
        // POST: /RFIDTag/Create

        [HttpPost]
        public ActionResult Create(RFIDTag rfidtag)
        {
            if (ModelState.IsValid)
            {
                db.RFIDTags.Add(rfidtag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Modified to display Full Name instead of First Name
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", rfidtag.ClientId);
            return View(rfidtag);
        }

        //
        // GET: /RFIDTag/Edit/5

        public ActionResult Edit(int id = 0)
        {
            RFIDTag rfidtag = db.RFIDTags.Find(id);
            if (rfidtag == null)
            {
                return HttpNotFound();
            }
            // Modified to display Full Name instead of First Name
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", rfidtag.ClientId);
            return View(rfidtag);
        }

        //
        // POST: /RFIDTag/Edit/5

        [HttpPost]
        public ActionResult Edit(RFIDTag rfidtag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rfidtag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Modified to display Full Name instead of First Name
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", rfidtag.ClientId);
            return View(rfidtag);
        }

        //
        // GET: /RFIDTag/Delete/5

        public ActionResult Delete(int id = 0)
        {
            RFIDTag rfidtag = db.RFIDTags.Find(id);
            if (rfidtag == null)
            {
                return HttpNotFound();
            }
            return View(rfidtag);
        }

        //
        // POST: /RFIDTag/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            RFIDTag rfidtag = db.RFIDTags.Find(id);
            db.RFIDTags.Remove(rfidtag);
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