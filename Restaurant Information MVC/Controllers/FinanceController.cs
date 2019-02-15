using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Restaurant_Information_MVC.Models;
namespace Restaurant_Information_MVC.Controllers
{
    public class FinanceController : Controller
    {
        // GET: Finance
        /// <summary>
        /// 成本信息视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ConstInfo()
        {
            return View();
        }
        /// <summary>
        /// 账目管理视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Account()
        {
            return View();
        }
        /// <summary>
        /// 收入消费查询视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Income()
        {
            return View();
        }
        /// <summary>
        /// 财务报表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Fiance()
        {
            return View();
        }
        /// <summary>
        /// 显示所有账单
        /// </summary>
        /// <returns></returns>
        public List<BillViewModel> ShowBill()
        {
            return JsonConvert.DeserializeObject<List<BillViewModel>>(GetApiResult("get", "ShowBill"));
        }
        /// <summary>
        /// 显示所有饭菜成本
        /// </summary>
        /// <returns></returns>
        public List<GoodsViewModel> ShowCost()
        {
            return JsonConvert.DeserializeObject<List<GoodsViewModel>>(GetApiResult("get", "ShowCost"));
        }
        /// <summary>
        /// 查询单个账单
        /// </summary>
        /// <param name="biliid"></param>
        /// <returns></returns>
        public BillViewModel GetOneBill(int biliid)
        {
            return JsonConvert.DeserializeObject<BillViewModel>(GetApiResult("get", "GetOneBill/?biliid=" + biliid));
        }
        #region 封装WebApi
        /// <summary>
        /// 封装
        /// </summary>
        /// <param name="ovebs"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public string GetApiResult(string verbs, string actionName, Object obj = null)
        {
            Task<HttpResponseMessage> task = null;
            string json = "";


            //创建一个http客户端对象
            HttpClient client = new HttpClient();
            //访问api方法路径
            client.BaseAddress = new Uri("http://localhost:/api//");
            switch (verbs)
            {
                case "get":
                    task = client.GetAsync(actionName);
                    break;
                default:
                    break;
            }
            //等待请求过程
            task.Wait();
            //接收响应结果
            var Response = task.Result;
            //判断相应的判断码是成功的时候
            if (Response.IsSuccessStatusCode)
            {
                //从响应对象的内容中读取字符串
                var read = Response.Content.ReadAsStringAsync();
                //等待读取的过程
                read.Wait();
                //接受读取的结果
                json = read.Result;
            }
            return json;
        }
        #endregion

    }
}