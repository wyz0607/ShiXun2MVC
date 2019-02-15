using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_Information_MVC.Models;



namespace Restaurant_Information_MVC.Controllers
{
    public class KitchenController : Controller
    {
        // GET: Kitchen
        
        [HttpGet]
        //显示
        public ActionResult ShowMenu()
        {
            return View();
        }

        [HttpGet]
        //添加
        public ActionResult AddMenu()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMenu(KitchenViewModel kit)
        {
            return View();
        }

        //删除
        public ActionResult DelMenu()
        {
            return View();
        }

        //修改
        [HttpGet]
        public ActionResult GetOneMenu(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult UptMenu(KitchenViewModel kit)
        {
            return View();
        }








    }
}