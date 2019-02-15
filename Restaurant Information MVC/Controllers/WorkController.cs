using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_Information_MVC;
namespace Restaurant_Information_MVC.Controllers
{
    public class WorkController : Controller
    {
        // GET: Work
        /// <summary>
        /// 显示所有的评论
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowComment()
        {
        
            return View();
        }
        /// <summary>
        /// 显示单个用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLoginUser()
        {
            return View();
        }
        /// <summary>
        /// 审核评论
        /// </summary>
        /// <returns></returns>
        public ActionResult UptComment()
        {
            return View();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult Uptuser()
        {
           
            return View();
        }
      
    }
}