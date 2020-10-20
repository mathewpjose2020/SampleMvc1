using SampleMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace SampleMvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void FormsAuthentication_OnAuthenticate(object sender, FormsAuthenticationEventArgs e)
        {
            if (FormsAuthentication.CookiesSupported)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName]!=null)
                {
                    try
                    {
                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string role = string.Empty;
                        using (ContactBookDb db = new ContactBookDb())
                        {
                            var account = db.Accounts.FirstOrDefault(x => x.AccountName == username && x.AccountActive == true);
                            role = account.AccountRole;
                        }
                        e.User = new System.Security.Principal.GenericPrincipal(
                            new System.Security.Principal.GenericIdentity(username, "Forms"), role.Split(';'));
                    }
                    catch (Exception)
                    {                        
                        throw;
                    }
                }
            }
        }
    }
}