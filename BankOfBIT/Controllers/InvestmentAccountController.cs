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
    public class InvestmentAccountController : Controller
    {
        private BankOfBITContext db = new BankOfBITContext();

        //
        // GET: /InvestmentAccount/

        public ActionResult Index()
        {
            // Updated to Investment Accounts table
            var bankaccounts = db.InvestmentAccounts.Include(i => i.Client).Include(i => i.AccountState);
            return View(bankaccounts.ToList());
        }

        //
        // GET: /InvestmentAccount/Details/5

        public ActionResult Details(int id = 0)
        {
            // Updated to to InvestmentAccounts table
            InvestmentAccount investmentaccount = db.InvestmentAccounts.Find(id);
            if (investmentaccount == null)
            {
                return HttpNotFound();
            }
            return View(investmentaccount);
        }

        //
        // GET: /InvestmentAccount/Create

        public ActionResult Create()
        {
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description");
            return View();
        }

        //
        // POST: /InvestmentAccount/Create

        [HttpPost]
        public ActionResult Create(InvestmentAccount investmentaccount)
        {
            if (ModelState.IsValid)
            {
                // Updated to add the set next account number method
                investmentaccount.SetNextAccountNumber();
                db.BankAccounts.Add(investmentaccount);
                db.SaveChanges();
                // Testing of the change state method needs to call the method and then save the changes to the database
                investmentaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", investmentaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", investmentaccount.AccountStateId);
            return View(investmentaccount);
        }

        //
        // GET: /InvestmentAccount/Edit/5

        public ActionResult Edit(int id = 0)
        {
            // Updated to to InvestmentAccounts table
            InvestmentAccount investmentaccount = db.InvestmentAccounts.Find(id);
            if (investmentaccount == null)
            {
                return HttpNotFound();
            }
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", investmentaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", investmentaccount.AccountStateId);
            return View(investmentaccount);
        }

        //
        // POST: /InvestmentAccount/Edit/5

        [HttpPost]
        public ActionResult Edit(InvestmentAccount investmentaccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(investmentaccount).State = EntityState.Modified;
                db.SaveChanges();
                // Testing of the change state method needs to call the method and then save the changes to the database
                investmentaccount.ChangeState();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Updated to show The Full Name and Description
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName", investmentaccount.ClientId);
            ViewBag.AccountStateId = new SelectList(db.AccountStates, "AccountStateId", "Description", investmentaccount.AccountStateId);
            return View(investmentaccount);
        }

        //
        // GET: /InvestmentAccount/Delete/5

        public ActionResult Delete(int id = 0)
        {
            // Updated to to InvestmentAccounts table
            InvestmentAccount investmentaccount = db.InvestmentAccounts.Find(id);
            if (investmentaccount == null)
            {
                return HttpNotFound();
            }
            return View(investmentaccount);
        }

        //
        // POST: /InvestmentAccount/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            // Updated to to InvestmentAccounts table
            InvestmentAccount investmentaccount = db.InvestmentAccounts.Find(id);
            db.BankAccounts.Remove(investmentaccount);
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