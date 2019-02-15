using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Information_MVC.Controllers
{
    public class KitchenController : Controller
    {
        // GET: Kitchen
        /// <summary>
        /// 显示菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowMenu()
        {
            return View();
        }
        /// <summary>
        /// 添加菜色
        /// </summary>
        /// <returns></returns>
        public ActionResult AddMenu()
        {
            return View();
        }
        /// <summary>
        /// 修改菜色
        /// </summary>
        /// <returns></returns>
        public ActionResult UptMenu()
        {
            return View();
        }
        /// <summary>
        /// 删除菜色
        /// </summary>
        /// <returns></returns>
        public ActionResult DelMenu()
        {
            return View();
        }
        /// <summary>
        /// 获取单个菜色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOneMenu()
        {
            return View();
        }
    }
}