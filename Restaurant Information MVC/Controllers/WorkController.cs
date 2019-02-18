using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_Information_MVC;
using Restaurant_Information_MVC.Models;
using Restaurant_Information_MVC.Controllers;
using Newtonsoft.Json;
namespace Restaurant_Information_MVC.Controllers
{
    public class WorkController : Controller
    {
        // GET: Work
        /// <summary>
        /// 显示所有的评论
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowComment(int pageindex=1)
        {
            var str = HttpClientHelper.Seng("get", "api/WorkApi/Comment", null);
            List<CommentViewModel> list = JsonConvert.DeserializeObject<List<CommentViewModel>>(str);
            var list1 = from s in list
                        select new CommentViewModel
                        {
                            OrderID = s.OrderID,
                            CommentId = s.CommentId,
                            Comments = s.Comments,
                            ReviewState = s.ReviewState=0
                        };
            ViewBag.currentindex = pageindex;
            ViewBag.totaldata = list.Count;
            ViewBag.totalpage = (Math.Floor((list1.Count() * 1.0) / 5)) + 1;
            return View(list1.Skip((pageindex - 1) * 5).Take(5).ToList());
           
        }
        /// <summary>
        /// 登录信息
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
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUser()
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
        /// 获取单个评价信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOneComment()
        {
            return View();
        }
        /// <summary>
        /// 添加成本
        /// </summary>
        /// <returns></returns>
        public ActionResult AddGood()
        {
            return View();
        }
    }
    public enum Stateinfo
    {
        待审核=0,
        审核通过=1,
        驳回=2

    }
}