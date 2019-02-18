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
        public ActionResult ShowComment(int pageindex = 1, int pagesize = 2)
        {
            var str = HttpClientHelper.Seng("get", "api/WorkApi/ShowComment", null);
            List<CommentViewModel> list = JsonConvert.DeserializeObject<List<CommentViewModel>>(str);
            //var list1 = from s in list
            //            select new CommentViewModel
            //            {
            //                OrderID = s.OrderID,
            //                MenuID=s.MenuID,
            //                CommentId = s.CommentId,
            //                Comments = s.Comments,
            //                ReviewState = s.ReviewState
            //            };

            //List<CommentViewModel> ce = new List<CommentViewModel>(){ new CommentViewModel{ CommentId=1,Comments="好样的",MenuID=1,OrderID=1,ReviewState=0},
            //     new CommentViewModel{ CommentId=2,Comments="好的",MenuID=2,OrderID=2,ReviewState=1},
            //     new CommentViewModel{ CommentId=3,Comments="bu好样的",MenuID=3,OrderID=3,ReviewState=0} };
            ViewBag.currentindex = pageindex;
            ViewBag.totaldata = list.Count;
            ViewBag.totalpage = (Math.Floor((list.Count() * 1.0) / 2)) + 1;

            return View(list.Skip((pageindex - 1) * 2).Take(2).ToList());
            
           
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
        [HttpGet]
        public ActionResult UptComment(int id)
        {
            var str = HttpClientHelper.Seng("get", "api/WorkApi/ShowComment", null);
            CommentViewModel list = JsonConvert.DeserializeObject<List<CommentViewModel>>(str).Where(m => m.CommentId == id).FirstOrDefault();
            return View(list);
        }
        [HttpPost]
        public ActionResult UptComment(CommentViewModel comment)
        {
            var str = JsonConvert.SerializeObject(comment);
            string jsonstr = HttpClientHelper.Seng("put", "api/WorkApi/UptComment", str);
            if(jsonstr.Contains("成功"))
            {
                return Content("操作成功");
            }
            else
            {
                return Content("操作失败");
            }
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
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowUser()
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
        /// 添加支出
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPayment()
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