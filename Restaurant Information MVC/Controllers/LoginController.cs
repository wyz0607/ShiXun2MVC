using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_Information_MVC.Models;
using Newtonsoft.Json;
using System.Web.Security;
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
                FormsAuthentication.SetAuthCookie(Name, true);
                Session["UserName"] = Name;
                Session["UserID"] = user.UserID;
                Response.Cookies["UserID"].Value = $"{user.UserID}";
                return Content("<script>location.href='/Login/Show'</script>");
            }
            else
            {
                return Content("账户或密码错误");
            }
            
        }
        [Authorize]
        [ShouQuanAttribute]
        public ActionResult Show()
        {
            var time = DateTime.Now.ToString("yyyy年MM月dd日");
        
            var num= HttpClientHelper.Seng("get", "api/Login/GetOrderNum?time=" + time,null);
            ViewBag.OrderNum = num.ToString();
            var oneMoney = HttpClientHelper.Seng("get", "api/Login/GetOneMoney?time=" + time, null);
            ViewBag.oneMoney = oneMoney.ToString();
            var menu = HttpClientHelper.Seng("get", "api/Login/GetMenuNum", null);
            ViewBag.menu = menu;
            var money = HttpClientHelper.Seng("get", "api/Login/GetAllMoney", null);
            ViewBag.money = money;
            

            return View();
        }

        public ActionResult GetResult()
        {
            List<int> getDay = new List<int>();
            var yea = DateTime.Now.Year.ToString();
            var mon = DateTime.Now.Month.ToString();
            var day = DateTime.Now.Day;
            var tiao = day - 7;

            for (int i = day; tiao< i; i--)
            {
                string shi = "";
                if (Convert.ToInt32( mon)<10 && day<10)
                {
                     shi = yea + "年" + "0" + mon + "月" + "0" + day + "日";
                    
                }
                else if (Convert.ToInt32(mon)<10)
                {
                    shi = yea + "年" + "0" + mon + "月" + day + "日";
                }
                else if (day<10)
                {
                    shi = yea + "年"+ mon + "月" + "0" + day + "日";
                }
                else
                {
                    shi = yea + "年"+ mon + "月" + day + "日";
                }

                var numLine = HttpClientHelper.Seng("get", "api/Login/GetOrderNum?time=" + shi, null);
                getDay.Add(Convert.ToInt32(numLine));
                day = day - 1;

            }
            return Content(JsonConvert.SerializeObject(getDay));
        }

        public ActionResult GetArr()
        {
            List<int> getDay = new List<int>();
            var yea = DateTime.Now.Year.ToString();
            var mon = DateTime.Now.Month.ToString();
            var day = DateTime.Now.Day;
            var tiao = day - 7;

            for (int i = day; tiao < i; i--)
            {
                getDay.Add(day);
                day = day - 1;
            }
            return Content(JsonConvert.SerializeObject(getDay));
        }
    }
}