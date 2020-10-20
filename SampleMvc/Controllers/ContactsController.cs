using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleMvc.Models;

namespace SampleMvc.Controllers
{
    public class ContactsController : Controller
    {
        private ContactBookDb db = new ContactBookDb();

        //
        // GET: /Contacts/

        public ActionResult Index()
        {
            var contacts = db.Contacts.Include(c => c.Accounts);
            return View(contacts.Where(x => x.Accounts.AccountName == HttpContext.User.Identity.Name).ToList());
        }

        //
        // GET: /Contacts/Details/5

        public ActionResult Details(int id = 0)
        {
            Contacts contacts = db.Contacts.Find(id);
            if (contacts == null)
            {
                return HttpNotFound();
            }
            return View(contacts);
        }

        //
        // GET: /Contacts/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Contacts/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contacts contacts)
        {
            var user = db.Accounts.FirstOrDefault(x=>x.AccountName==HttpContext.User.Identity.Name);
            contacts.ContactUserId = user.AccountId; 
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contacts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

             return View(contacts);
        }

        //
        // GET: /Contacts/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Contacts contacts = db.Contacts.Find(id);
            if (contacts == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactUserId = new SelectList(db.Accounts, "AccountId", "AccountName", contacts.ContactUserId);
            return View(contacts);
        }

        //
        // POST: /Contacts/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contacts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactUserId = new SelectList(db.Accounts, "AccountId", "AccountName", contacts.ContactUserId);
            return View(contacts);
        }

        //
        // GET: /Contacts/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Contacts contacts = db.Contacts.Find(id);
            if (contacts == null)
            {
                return HttpNotFound();
            }
            return View(contacts);
        }

        //
        // POST: /Contacts/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contacts contacts = db.Contacts.Find(id);
            db.Contacts.Remove(contacts);
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