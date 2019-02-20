using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Restaurant_Information_MVC.Models;
using Newtonsoft.Json;
namespace Restaurant_Information_MVC.Controllers
{
    public class FinanceController : Controller
    {
        //静态化两个进货商品的成本价表
        public static List<GoodsViewModel> gList;
        public static List<GoodsViewModel> glist;
        /// <summary>
        /// 查询到所有商品的成本价
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowCost()
        {
            gList = JsonConvert.DeserializeObject<List<GoodsViewModel>>(HttpClientHelper.Seng("get", "api/FinanceApi/ShowCost", null));
            glist = gList;
            return View(gList);
        }
        /// <summary>
        /// 按照名称查询结果
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShowCost(string name)
        {
            gList = glist.Where(m => m.GoodsName.Contains(name)).ToList();
            return View(gList);
        }
        //静态化两个账单的list集合
        public static List<BillViewModel> bList;
        public static List<BillViewModel> blist;
        // GET: Finance
        /// <summary>
        /// 获取所有的账单信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowBill()
        {
            bList = JsonConvert.DeserializeObject<List<BillViewModel>>(HttpClientHelper.Seng("get", "api/FinanceApi/ShowBill", null));
            blist = bList.OrderByDescending(m => m.BillID).ToList();
            return View(blist);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShowBill(string begin,string end,string name)
        {
            if (begin != "" && end != "")
            {
                bList = blist.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) >= Convert.ToDateTime(begin) && Convert.ToDateTime(m.PaymentTime) <= Convert.ToDateTime(end)).ToList();
            }else if(begin =="" && end != "")
            {
                bList = blist.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) <= Convert.ToDateTime(end)).ToList();
            }else if(begin !="" && end == "")
            {
                bList = blist.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) >= Convert.ToDateTime(begin)).ToList();
            }
            else
            {
                bList = blist.Where(m => m.UserName.Contains(name)).ToList();
            }
            return View(bList);
        }
        /// <summary>
        /// 收入消费查询视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowEarning()
        {
            return View();
        }
        //实例化list集合
        public static List<int> iList = new List<int>();
        /// <summary>
        /// 获取到今天的日期
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDate()
        {
            
            string dt = DateTime.Now.ToString("dd");
            for (int i = Convert.ToInt32(dt); i >0 ; i--)
            {
                iList.Add(i);
            }
            return Content(JsonConvert.SerializeObject(iList));
        }
        /// <summary>
        /// 获取到每日的收入情况
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDayIncome()
        {
            List<double> dList = new List<double>();
            List<BillViewModel> bvList = new List<BillViewModel>();
            blist = JsonConvert.DeserializeObject<List<BillViewModel>>(HttpClientHelper.Seng("get", "api/FinanceApi/ShowBill", null));
            foreach (var item in iList)
            {
                bvList = blist.Where(m => m.PaymentTime.Substring(7,2) == item.ToString()).ToList();
                dList.Add(bvList.Sum(m => m.BillMoney));
            }
            return Content(JsonConvert.SerializeObject(dList));
        }
        /// <summary>
        /// 审核账单
        /// </summary>
        /// <returns></returns>
        public ActionResult UptBill()
        {
            return View();
        }
        /// <summary>
        /// 获取单个的账单信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOneBill()
        {
            return View();
        }
    }
}