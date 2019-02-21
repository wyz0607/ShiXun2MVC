using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_Information_MVC;
using Restaurant_Information_MVC.Models;
using Restaurant_Information_MVC.Controllers;
using Newtonsoft.Json;
using System.Text;

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
        [HttpGet]
        public ActionResult Uptuser()
        {
            int id =Convert.ToInt32(Response.Cookies["UserID"].Value);
            var str = HttpClientHelper.Seng("get", "api/WorkApi/GetOneUser/?userid=1", null);
            UserInfo user = JsonConvert.DeserializeObject<UserInfo>(str);

            return View(user);
        }
        [HttpPost]
        public ActionResult Uptuser(UserInfo userInfo)
        {
            var str = JsonConvert.SerializeObject(userInfo);
            string jsonstr = HttpClientHelper.Seng("put", "api/WorkApi/Uptuser", str);
            if(jsonstr.Contains("成功"))
            {
                return Content("修改成功");
            }
            else
            {
                return Content("修改失败");
            }
   
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
        public ActionResult SendSms()
        {
            string apiurl = "http://api.feige.ee";
            string data = "0123456789";
            var chararr = data.ToCharArray();
            Random rd = new Random();
            string result = "";
            for (int i = 0; i < 4; i++)
            {
                int charindex = rd.Next(chararr.Length);
                result += chararr[charindex];
            }
            int radom = Convert.ToInt32(result);
            CommonSmsRequest request = new CommonSmsRequest
            {
               
                Account = "17321446954",
                Pwd = "e5a47ed56946ff3b78f32712d",//登录web平台 http://sms.feige.ee  在管理中心--基本资料--接口密码 或者首页 接口秘钥 如登录密码修改，接口密码会发生改变，请及时修改程序
                Content = $"亲，你的验证码是{radom}",
                Mobile = "17321446954",
                SignId = 95171, //登录web平台 http://sms.feige.ee  在签名管理中--新增签名--获取id
                SendTime = Convert.ToInt64(common.ToUnixStamp(DateTime.Now))//定时短信 把时间转换成时间戳的格式
            };
            Session["Yan"] = radom;

            StringBuilder arge = new StringBuilder();
            arge.AppendFormat("Account={0}", request.Account);
            arge.AppendFormat("&Pwd={0}", request.Pwd);
            arge.AppendFormat("&Content={0}", request.Content);
            arge.AppendFormat("&Mobile={0}", request.Mobile);
            arge.AppendFormat("&SignId={0}", request.SignId);
            arge.AppendFormat("&SendTime={0}", request.SendTime);
            string weburl = apiurl + "/SmsService/Send";
            string resp = common.PushToWeb(weburl, arge.ToString(), Encoding.UTF8);

            try
            {
                SendSmsResponse response = JsonConvert.DeserializeObject<SendSmsResponse>(resp);
                if (response.Code == 0)
                {
                    //成功
                    return Content("发送成功");
                }
                else
                {
                    //失败
                    return Content("发送失败");
                }
            }
            catch (Exception ex)
            {
                //记录日志
                return Content(ex.Message);
            }


        }

    }
    public enum Stateinfo
    {
        待审核=0,
        审核通过=1,
        驳回=2

    }
 
    public class CommonSmsRequest
    {
        public string Account { get; set; }
        public string Pwd { get; set; }
        public string Content { get; set; }
        public string Mobile { get; set; }
        public int SignId { get; set; }
        public long SendTime { get; set; }
    }
    public class BaseCode
    {
        //状态码
        public int Code { get; set; }
        /// <summary>
        /// 返回错误信息
        /// </summary>
        public string Message { get; set; }
    }

    public class SendSmsResponse : BaseCode
    {
        /// <summary>
        /// 发送Id
        /// </summary>
        public string SendId { get; set; }
        /// <summary>
        /// 无效号码数量
        /// </summary>
        public int InvalidCount { get; set; }
        /// <summary>
        /// 成功数量
        /// </summary>
        public int SuccessCount { get; set; }
        /// <summary>
        /// 黑名单数量
        /// </summary>
        public int BlackCount { get; set; }
    }
}