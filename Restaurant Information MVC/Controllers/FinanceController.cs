using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_Information_MVC.Controllers
{
    public class FinanceController : Controller
    {
        // GET: Finance
        /// <summary>
        /// 获取所有的账单信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowBill()
        {
            return View();
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