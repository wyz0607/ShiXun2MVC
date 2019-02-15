using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_Information_MVC;
using Newtonsoft.Json;

namespace Restaurant_Information_MVC.Controllers
{
    public class ReceptionController : Controller
    {
        // GET: Reception
        public ActionResult ShowOrder()
        {
            string json=HttpClientHelper.Seng("get", "api/Reception/ShowOrder",null);
            return View();
        }
    }
}