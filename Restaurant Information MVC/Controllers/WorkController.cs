using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Information_MVC.Controllers
{
    public class WorkController : Controller
    {
        // GET: Work
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUser()
        {
            return View();
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUser()
        {
            return View();
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UptUser()
        {
            return View();
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult DelUser()
        {
            return View();
        }

        /// <summary>
        /// 获取单个用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOneUser()
        {
            return View();
        }

        /// <summary>
        /// 登录账号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLoginUser()
        {
            return View();
        }

        /// <summary>
        /// 获取所有评论信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowComment()
        {
            return View();
        }

        /// <summary>
        /// 审核评论信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UptComment()
        {
            return View();
        }

        /// <summary>
        /// 获取单个评论信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOneComment()
        {
            return View();
        }

        /// <summary>
        /// 添加支出
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPayment()
        {
            return View();
        }

       
    }
}