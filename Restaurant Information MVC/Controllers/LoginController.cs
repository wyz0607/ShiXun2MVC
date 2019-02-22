using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_Information_MVC.Models;
using Newtonsoft.Json;
namespace Restaurant_Information_MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Name, string Password)
        {
            var result= HttpClientHelper.Seng("get","api/Login/Login?UserName="+Name+"&Pwd="+Password+"",null);
            UserInfo user = JsonConvert.DeserializeObject<UserInfo>(result);

            if (result!=null)
            {
                Session["UserName"] = Name;
                Session["UserID"] = user.UserID;
                Response.Cookies["UserID"].Value = $"{user.UserID}";

              
                return View("Show");
            }
            else
            {
                return Content("账户或密码错误");
            }
            
        }

        public ActionResult Show()
        {
            var time = DateTime.Now.ToString("yyyy年MM月dd日");

            var num= HttpClientHelper.Seng("get", "api/Login/GetOrderNum?time=" + time,null);
            ViewBag.OrderNum = num;
            var oneMoney = HttpClientHelper.Seng("get", "api/Login/GetOneMoney?time=" + time, null);
            ViewBag.oneMoney = oneMoney;
            var menu = HttpClientHelper.Seng("get", "api/Login/GetMenuNum", null);
            ViewBag.menu = menu;
            var money = HttpClientHelper.Seng("get", "api/Login/GetAllMoney", null);
            ViewBag.money = money;
            return View();
        }
    }
}