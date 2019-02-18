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
        public static List<BillViewModel> bList;
        // GET: Finance
        /// <summary>
        /// 获取所有的账单信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowBill()
        {
            bList = JsonConvert.DeserializeObject<List<BillViewModel>>(HttpClientHelper.Seng("get", "api/FinanceApi/ShowBill", null));
            return View(bList);
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
            if (begin != null && end != null)
            {
                bList = bList.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) >= Convert.ToDateTime(begin) && Convert.ToDateTime(m.PaymentTime) <= Convert.ToDateTime(end)).ToList();
            }else if(begin ==null && end != null)
            {
                bList = bList.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) <= Convert.ToDateTime(end)).ToList();
            }else if(begin !=null && end == null)
            {
                bList = bList.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) >= Convert.ToDateTime(begin)).ToList();
            }
            else
            {
                bList = bList.Where(m => m.UserName.Contains(name)).ToList();
            }
            return View(bList);
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