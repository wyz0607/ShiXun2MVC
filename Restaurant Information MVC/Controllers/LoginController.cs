﻿using System;
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
           
            if (result.Equals("1"))
            {
                var str = HttpClientHelper.Seng("get", "api/Login/GetUser?Username=" + Name + "&Pwd=" + Password + "", null);

                UserInfo user = JsonConvert.DeserializeObject<UserInfo>(str);
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
            return View();
        }
    }
}