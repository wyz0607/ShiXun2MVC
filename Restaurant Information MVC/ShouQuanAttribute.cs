using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Information_MVC
{
    public class ShouQuanAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // base.OnAuthorization(filterContext);
            //如果未登陆 就跳转到登录页
            if (filterContext.HttpContext.Session["Name"]==null)
            {
                filterContext.HttpContext.Response.Redirect("/Login/GetoneLogin");
            }
        }
    }
}