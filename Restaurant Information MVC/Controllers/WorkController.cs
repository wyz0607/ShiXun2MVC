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
using System.Net;
using System.IO;
using System.Net.Mail;

namespace Restaurant_Information_MVC.Controllers
{
    [ShouQuanAttribute]
    [Authorize]
    public class WorkController : Controller
    {


        // GET: Work
        /// <summary>
        /// 显示所有的评论
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowComment(int pageindex = 1, int pagesize = 5)
        {
            var str = HttpClientHelper.Seng("get", "api/WorkApi/ShowComment", null);
            var str1 = HttpClientHelper.Seng("get","",null);
            List<CommentViewModel> list1 = JsonConvert.DeserializeObject<List<CommentViewModel>>(str);
            List<OrderViewModel> list2 = JsonConvert.DeserializeObject<List<OrderViewModel>>(str1);
            var list = from s in list1.AsEnumerable()
                       join b in list2 on s.OrderID equals b.OrderID
                       select new CommentViewModel
                       {
                           CommentId = s.CommentId,
                           MenuID = s.MenuID,
                           OrderID = s.OrderID,
                           CName = b.UserName,
                           Comments=s.Comments,
                           Ctime=s.Ctime,
                           ReviewState=s.ReviewState,
                           
                       };
            ViewBag.currentindex = pageindex;
            ViewBag.totaldata = list.Count();
            ViewBag.totalpage = (Math.Floor((list.Count() * 1.0) / 5)) + 1;

            return View(list.Skip((pageindex - 1) * 5).Take(5).ToList());
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
            if (jsonstr.Contains("成功"))
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
            int id =Convert.ToInt32(Session["UserID"]);
            var str = HttpClientHelper.Seng("get", "api/WorkApi/GetOneUser/?userid="+id, null);
            UserInfo user = JsonConvert.DeserializeObject<UserInfo>(str);

            return View(user);
        }
        [HttpPost]
        public ActionResult Uptuser(UserInfo userInfo)
        {
            var str = JsonConvert.SerializeObject(userInfo);
            string jsonstr = HttpClientHelper.Seng("put", "api/WorkApi/Uptuser", str);
            if (jsonstr.Contains("成功"))
            {
                return Content("修改成功");
            }
            else
            {
                return Content("修改失败");
            }

        }
       
        /// <summary>
        /// 获取单个用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOneUser(int id)
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
        /// <summary>
        /// 添加员工
        /// </summary>
        /// <returns></returns>
       public ActionResult AddRole(RoleViewModel roleView)
        {
            var dd = JsonConvert.SerializeObject(roleView);
            string str = HttpClientHelper.Seng("post", "api/WorkApi/AddEmp", dd);
            if(str.Contains("成功"))
            {
                return Content("添加成功");
            }
            else
            {
                return Content("添加失败");
            }
        }
        /// <summary>
        /// 显示所有的角色信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowRole()
        {
            var str = HttpClientHelper.Seng("get","api/WorkApi/GetRoles",null);
         
            List<RoleViewModel> list = JsonConvert.DeserializeObject<List<RoleViewModel>>(str);
            return View(list);

        }
        /// <summary>
        /// 显示所有员工的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowEmp()
        {
            var str = HttpClientHelper.Seng("get","api/WorkApi/ShowUserinfo",null);
            List<UserInfo> list = JsonConvert.DeserializeObject<List<UserInfo>>(str);
            return View(list);
        }
        /// <summary>
        /// 显示个人的资料
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOneuserinfo(int id)
        {
            var str = HttpClientHelper.Seng("get", "api/WorkApi/ShowUserinfo", null);
           UserInfo list = JsonConvert.DeserializeObject<List<UserInfo>>(str).Where(c=>c.UserID==id).FirstOrDefault();
            return View(list);
        }

        /// <summary>
        /// 根据id显示审核界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UptProposer(int id)
        {
            var str = HttpClientHelper.Seng("get", "api/WorkApi/GetProposer/?id=" + id,null);
            ProposerViewModel proposerView = JsonConvert.DeserializeObject<List<ProposerViewModel>>(str).FirstOrDefault();
            return View(proposerView);

        }
        public ActionResult UptProposer(ProposerViewModel proposerView)
        {
            var str1 = JsonConvert.SerializeObject(proposerView);
            var str = HttpClientHelper.Seng("put", "api/WorkApi/UptProposer", str1);
            if(str.Contains("成功"))
            {
                return Content("操作成功");
            }
            else
            {
                return Content("操作失败");
            }
        }
        /// <summary>
        /// 显示所有的待审核信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ShowProposer()
        {
            var str = HttpClientHelper.Seng("get", "api/WorkApi/GetProposers", null);
            ProposerViewModel proposerView = JsonConvert.DeserializeObject<List<ProposerViewModel>>(str).FirstOrDefault();
            return View(proposerView);
        }

        public  string Yanzheng;
        [HttpGet]
        public string SendEmail(string email)
        {
            Random rd = new Random();
            string validateCode = rd.Next(100000, 1000000).ToString();
            Email e = new Email()
            {
                host = "smtp.qq.com",
                mailBody = "验证码：" + validateCode,
                mailFrom = "2630106690@qq.com",
                mailPwd = "ysuhjbrxsnrtebhc",
                mailSubject = "[一份来自测试的邮件]",
                mailToArray = new string[] { email }
            };
            e.Send();
            return validateCode;
        }
       
























    }
    
    public enum Stateinfo
    {
        待审核 = 0,
        审核通过 = 1,
        驳回 = 2

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











    public class Email
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string mailFrom { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string[] mailToArray { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public string[] mailCcArray { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string mailSubject { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string mailBody { get; set; }

        /// <summary>
        /// 发件人密码
        /// </summary>
        public string mailPwd { get; set; }

        /// <summary>
        /// SMTP邮件服务器
        /// </summary>
        public string host { get; set; }

        /// <summary>
        /// 正文是否是html格式
        /// </summary>
        public bool isbodyHtml { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string[] attachmentsPath { get; set; }

        public bool Send()
        {
            //使用指定的邮件地址初始化MailAddress实例
            MailAddress maddr = new MailAddress(mailFrom);
            //初始化MailMessage实例
            MailMessage myMail = new MailMessage();


            //向收件人地址集合添加邮件地址
            if (mailToArray != null)
            {
                for (int i = 0; i < mailToArray.Length; i++)
                {
                    myMail.To.Add(mailToArray[i].ToString());
                }
            }

            //向抄送收件人地址集合添加邮件地址
            if (mailCcArray != null)
            {
                for (int i = 0; i < mailCcArray.Length; i++)
                {
                    myMail.CC.Add(mailCcArray[i].ToString());
                }
            }
            //发件人地址
            myMail.From = maddr;

            //电子邮件的标题
            myMail.Subject = mailSubject;

            //电子邮件的主题内容使用的编码
            myMail.SubjectEncoding = Encoding.UTF8;

            //电子邮件正文
            myMail.Body = mailBody;

            //电子邮件正文的编码
            myMail.BodyEncoding = Encoding.Default;

            myMail.Priority = MailPriority.High;

            myMail.IsBodyHtml = isbodyHtml;

            //在有附件的情况下添加附件
            try
            {
                if (attachmentsPath != null && attachmentsPath.Length > 0)
                {
                    Attachment attachFile = null;
                    foreach (string path in attachmentsPath)
                    {
                        attachFile = new Attachment(path);
                        myMail.Attachments.Add(attachFile);
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("在添加附件时有错误:" + err);
            }

            SmtpClient smtp = new SmtpClient();
            //指定发件人的邮件地址和密码以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(mailFrom, mailPwd);


            //设置SMTP邮件服务器
            smtp.Host = host;

            try
            {
                //将邮件发送到SMTP邮件服务器
                smtp.Send(myMail);
                return true;

            }
            catch (System.Net.Mail.SmtpException)
            {
                return false;
            }

        }
    }







}