using SampleMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SampleMvc.Controllers
{
    public class AccountsController : Controller
    {
        //
        // GET: /Accounts/
        [Authorize(Roles="Normal")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(RegisterModel model)
        {
            try
            {
                using (ContactBookDb db = new ContactBookDb())
                {
                    Accounts account = new Accounts() 
                    {
                        AccountName=model.UserName,
                        AccountPassword = model.UserPassword,
                        AccountActive =true,
                        AccountRole = "Normal"
                    };
                    db.Accounts.Add(account);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                using (ContactBookDb db = new ContactBookDb())
                {
                    var account = db.Accounts.FirstOrDefault(x => x.AccountName == model.UserName
                        && x.AccountPassword == model.UserPassword
                        && x.AccountActive == true);
                    if (account != null)
                    {
                        FormsAuthentication.SetAuthCookie(account.AccountName,true);
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
            return View();
        }
    }
}
