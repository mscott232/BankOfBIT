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
    public class ChequingAccountController : Controller
    {
        private BankOfBITContext db = new BankOfBITContext();

        //
        // GET: /ChequingAccount/

        public ActionResult Index()
        {
            // Updated to chequing accounts table
            var bankaccounts = db.ChequingAccounts.Include(c => c.Client).Include(c => c.AccountState);
            return View(bankaccounts.ToList());
        }

        //
        // GET: /ChequingAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            // Updated to to ChequingAccounts table
            ChequingAccount chequingaccount = db.ChequingAccounts.Find(id);
            if (chequingaccount == null)
            {
                return HttpNotFound();
            }
            return View(chequingaccount);
        }

        //
        // GET: /ChequingAccount/Create

        public ActionResult Create()
        {
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description");
            return View();
        }

        //
        // POST: /ChequingAccount/Create

        [HttpPost]
        public ActionResult Create(ChequingAccount chequingaccount)
        {
            if (ModelState.IsValid)
            {
                // Updated to add the set next account number method
                chequingaccount.SetNextAccountNumber();
                db.BankAccounts.Add(chequingaccount);
                db.SaveChanges();
                // Testing of the change state method needs to call the method and then save the changes to the database
                chequingaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", chequingaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", chequingaccount.AccountStateId);
            return View(chequingaccount);
        }

        //
        // GET: /ChequingAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            // Updated to to ChequingAccounts table
            ChequingAccount chequingaccount = db.ChequingAccounts.Find(id);
            if (chequingaccount == null)
            {
                return HttpNotFound();
            }
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", chequingaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", chequingaccount.AccountStateId);
            return View(chequingaccount);
        }

        //
        // POST: /ChequingAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(ChequingAccount chequingaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chequingaccount).State = EntityState.Modified;
                db.SaveChanges();
                // Testing of the change state method needs to call the method and then save the changes to the database
                chequingaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", chequingaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", chequingaccount.AccountStateId);
            return View(chequingaccount);
        }

        //
        // GET: /ChequingAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            // Updated to to ChequingAccounts table
            ChequingAccount chequingaccount = db.ChequingAccounts.Find(id);
            if (chequingaccount == null)
            {
                return HttpNotFound();
            }
            return View(chequingaccount);
        }

        //
        // POST: /ChequingAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // Updated to to ChequingAccounts table
            ChequingAccount chequingaccount = db.ChequingAccounts.Find(id);
            db.BankAccounts.Remove(chequingaccount);
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