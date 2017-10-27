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
    public class MortgageAccountController : Controller
    {
        private BankOfBITContext db = new BankOfBITContext();

        //
        // GET: /MortgageAccount/

        public ActionResult Index()
        {
            // Updated to Mortgage Accounts table
            var bankaccounts = db.MortgageAccounts.Include(m => m.Client).Include(m => m.AccountState);
            return View(bankaccounts.ToList());
        }

        //
        // GET: /MortgageAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            // Updated to to MortgageAccounts table
            MortgageAccount mortgageaccount = db.MortgageAccounts.Find(id);
            if (mortgageaccount == null)
            {
                return HttpNotFound();
            }
            return View(mortgageaccount);
        }

        //
        // GET: /MortgageAccount/Create

        public ActionResult Create()
        {
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description");
            return View();
        }

        //
        // POST: /MortgageAccount/Create

        [HttpPost]
        public ActionResult Create(MortgageAccount mortgageaccount)
        {
            if (ModelState.IsValid)
            {
                // Updated to add the set next account number method
                mortgageaccount.SetNextAccountNumber();
                db.BankAccounts.Add(mortgageaccount);
                db.SaveChanges();
                // Testing of the change state method needs to call the method and then save the changes to the database
                mortgageaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", mortgageaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", mortgageaccount.AccountStateId);
            return View(mortgageaccount);
        }

        //
        // GET: /MortgageAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            // Updated to to MortgageAccounts table
            MortgageAccount mortgageaccount = db.MortgageAccounts.Find(id);
            if (mortgageaccount == null)
            {
                return HttpNotFound();
            }
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", mortgageaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", mortgageaccount.AccountStateId);
            return View(mortgageaccount);
        }

        //
        // POST: /MortgageAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(MortgageAccount mortgageaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mortgageaccount).State = EntityState.Modified;
                db.SaveChanges();
                // Testing of the change state method needs to call the method and then save the changes to the database
                mortgageaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", mortgageaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", mortgageaccount.AccountStateId);
            return View(mortgageaccount);
        }

        //
        // GET: /MortgageAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            // Updated to to MortgageAccounts table
            MortgageAccount mortgageaccount = db.MortgageAccounts.Find(id);
            if (mortgageaccount == null)
            {
                return HttpNotFound();
            }
            return View(mortgageaccount);
        }

        //
        // POST: /MortgageAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // Updated to to MortgageAccounts table
            MortgageAccount mortgageaccount = db.MortgageAccounts.Find(id);
            db.BankAccounts.Remove(mortgageaccount);
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