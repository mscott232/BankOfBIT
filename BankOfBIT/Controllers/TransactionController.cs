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
    public class TransactionController : Controller
    {
        private BankOfBITContext db = new BankOfBITContext();

        //
        // GET: /Transaction/

        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.TransactionType).Include(t => t.BankAccount);
            return View(transactions.ToList());
        }

        //
        // GET: /Transaction/Details/5

        public ActionResult Details(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // GET: /Transaction/Create

        public ActionResult Create()
        {
            // Modified so that the bank account number is shown instead of notes
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "TransactionTypeId", "Description");
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "BankAccountId", "AccountNumber");
            return View();
        }

        //
        // POST: /Transaction/Create

        [HttpPost]
        public ActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                // Updated to add the set next transaction number method
                transaction.SetNextTransactionNumber();
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Modified so that the bank account number is shown instead of notes
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "TransactionTypeId", "Description", transaction.TransactionTypeId);
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "BankAccountId", "AccountNumber", transaction.BankAccountId);
            return View(transaction);
        }

        //
        // GET: /Transaction/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            // Modified so that the bank account number is shown instead of notes
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "TransactionTypeId", "Description", transaction.TransactionTypeId);
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "BankAccountId", "AccountNumber", transaction.BankAccountId);
            return View(transaction);
        }

        //
        // POST: /Transaction/Edit/5

        [HttpPost]
        public ActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // Modified so that the bank account number is shown instead of notes
            ViewBag.TransactionTypeId = new SelectList(db.TransactionTypes, "TransactionTypeId", "Description", transaction.TransactionTypeId);
            ViewBag.BankAccountId = new SelectList(db.BankAccounts, "BankAccountId", "AccountNumber", transaction.BankAccountId);
            return View(transaction);
        }

        //
        // GET: /Transaction/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        //
        // POST: /Transaction/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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