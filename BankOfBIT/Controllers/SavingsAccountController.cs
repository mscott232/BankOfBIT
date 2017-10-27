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
    public class SavingsAccountController : Controller
    {
        private BankOfBITContext db = new BankOfBITContext();

        //
        // GET: /SavingsAccount/

        public ActionResult Index()
        {
            // Updated to SavingsAccount table
            var bankaccounts = db.SavingsAccounts.Include(s => s.Client).Include(s => s.AccountState);
            return View(bankaccounts.ToList());
        }

        //
        // GET: /SavingsAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            // Updated to to SavingsAccounts table
            SavingsAccount savingsaccount = db.SavingsAccounts.Find(id);
            if (savingsaccount == null)
            {
                return HttpNotFound();
            }
            return View(savingsaccount);
        }

        //
        // GET: /SavingsAccount/Create

        public ActionResult Create()
        {
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description");
            return View();
        }

        //
        // POST: /SavingsAccount/Create

        [HttpPost]
        public ActionResult Create(SavingsAccount savingsaccount)
        {
            if (ModelState.IsValid)
            {
                // Updated to add the set next account number method
                savingsaccount.SetNextAccountNumber();
                db.BankAccounts.Add(savingsaccount);
                db.SaveChanges();
                // Testing of the change state method needs to call the method and then save the changes to the database
                savingsaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", savingsaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", savingsaccount.AccountStateId);
            return View(savingsaccount);
        }

        //
        // GET: /SavingsAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            // Updated to to SavingsAccounts table
            SavingsAccount savingsaccount = db.SavingsAccounts.Find(id);
            if (savingsaccount == null)
            {
                return HttpNotFound();
            }
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", savingsaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", savingsaccount.AccountStateId);
            return View(savingsaccount);
        }

        //
        // POST: /SavingsAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(SavingsAccount savingsaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(savingsaccount).State = EntityState.Modified;
                db.SaveChanges();
                // Testing of the change state method needs to call the method and then save the changes to the database
                savingsaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", savingsaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", savingsaccount.AccountStateId);
            return View(savingsaccount);
        }

        //
        // GET: /SavingsAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            // Updated to to SavingsAccounts table
            SavingsAccount savingsaccount = db.SavingsAccounts.Find(id);
            if (savingsaccount == null)
            {
                return HttpNotFound();
            }
            return View(savingsaccount);
        }

        //
        // POST: /SavingsAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // Updated to to SavingsAccounts table
            SavingsAccount savingsaccount = db.SavingsAccounts.Find(id);
            db.BankAccounts.Remove(savingsaccount);
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