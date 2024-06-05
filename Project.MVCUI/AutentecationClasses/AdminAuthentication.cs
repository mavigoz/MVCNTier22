using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.AutentecationClasses
{
    public class AdminAuthentication:AuthorizeAttribute

    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["Admin"]!=null)

            {

                return true;

            }
            httpContext.Response.Redirect("/Home/Login");
            return false;

                    }

    }
}