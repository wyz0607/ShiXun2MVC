using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_Information_MVC;
using Newtonsoft.Json;
using Restaurant_Information_MVC.Models;

namespace Restaurant_Information_MVC.Controllers
{
    [ShouQuanAttribute]
    [Authorize]
    public class ReceptionController : Controller
    {
        // GET: Reception
        /// <summary>
        /// 订单显示
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult ShowOrder(int OrderId=0, int pageIndex=1,int pageSize=3)
        {
            ViewBag.pIndex = pageIndex;
            ViewBag.pSize = pageSize;
            string json = HttpClientHelper.Seng("get", "api/ReceptionApi/ShowOrder", null);
            List<OrderViewModel> order = JsonConvert.DeserializeObject<List<OrderViewModel>>(json);
           
            if (OrderId==0)
            {
                ViewBag.pIndex = pageIndex;
                ViewBag.pSize = pageSize;
                ViewBag.pCount = order.Count();
                return View(order.Skip((pageIndex - 1) * pageSize).Take(pageSize));
            }
            else
            {
                ViewBag.pIndex = pageIndex;
                ViewBag.pSize = pageSize;
               
                 List<OrderViewModel> list = order.Where(c => c.OrderID == OrderId).ToList();
                ViewBag.pCount = list.Count();
                return View(order.Skip((pageIndex - 1) * pageSize).Take(pageSize).Where(c=>c.OrderID==OrderId).ToList());
            }
            
        }

        public ActionResult Order(int OrderId=0, int pageIndex = 1, int pageSize = 3)
        {
            ViewBag.pIndex = pageIndex;
            string json = HttpClientHelper.Seng("get", "api/ReceptionApi/ShowOrder", null);
            List<OrderViewModel> order = JsonConvert.DeserializeObject<List<OrderViewModel>>(json);
            return Content(JsonConvert.SerializeObject(order.Skip((pageIndex-1)*pageSize).Take(pageSize)));
        }

        public ActionResult ShowFoodSelection(int OrderId=0,int pageIndex=1,int pageSize=3)
        {
           
            string json = HttpClientHelper.Seng("get", "api/ReceptionApi/ShowFoodSelection", null);
            List<FoodSelectionViewModel> list = JsonConvert.DeserializeObject<List<FoodSelectionViewModel>>(json);
            ViewBag.pCount = list.Count();
            if (OrderId == 0)
            {

                ViewBag.pIndex = pageIndex;
                ViewBag.pSize = pageSize;
                ViewBag.pCount = list.Count();
                return View(list.Skip((pageIndex - 1) * pageSize).Take(pageSize));
            }
            else
            {
                List<FoodSelectionViewModel> list1 = list.Where(c => c.OrderID == OrderId).ToList();
                ViewBag.pIndex = pageIndex;
                ViewBag.pSize = pageSize;
                // ViewBag.pCount = 1;
                ViewBag.pCount = list1.Count();
                return View(list1.Skip((pageIndex - 1) * pageSize).Take(pageSize).Where(c => c.OrderID == OrderId).ToList());
            }
        }

        public ActionResult FoodSelection(int OrderId = 0, int pageIndex = 1, int pageSize = 3)
        {
            ViewBag.pIndex = pageIndex;
            string json = HttpClientHelper.Seng("get", "api/ReceptionApi/ShowFoodSelection", null);
            List<FoodSelectionViewModel> list = JsonConvert.DeserializeObject<List<FoodSelectionViewModel>>(json);
            return Content(JsonConvert.SerializeObject(list.Skip((pageIndex - 1) * pageSize).Take(pageSize)));
        }

        public ActionResult ShowWaste(int WasteId=0,int pageIndex=1,int pageSize=3)
        {
            ViewBag.pIndex = pageIndex;
            ViewBag.pSize = pageSize;
            string json = HttpClientHelper.Seng("get", "api/ReceptionApi/ShowWastes", null);
            List<WasteViewModel> wastes = JsonConvert.DeserializeObject<List<WasteViewModel>>(json);
            
            if (WasteId == 0)
            {
                ViewBag.pIndex = pageIndex;
                ViewBag.pSize = pageSize;
                ViewBag.pCount = wastes.Count();
                return View(wastes.Skip((pageIndex - 1) * pageSize).Take(pageSize));
            }
            else
            {
                ViewBag.pIndex = pageIndex;
                ViewBag.pSize = pageSize;
                List<WasteViewModel> list1 = wastes.Where(c => c.WasteID == WasteId).ToList();
                //ViewBag.pCount = 1;
                ViewBag.pCount = list1.Count();
                return View(list1.Skip((pageIndex - 1) * pageSize).Take(pageSize).Where(c => c.WasteID == WasteId).ToList());
            }
        }

        public ActionResult Waste(int WasteId = 0, int pageIndex = 1, int pageSize = 3)
        {
            ViewBag.pIndex = pageIndex;
            string json = HttpClientHelper.Seng("get", "api/ReceptionApi/ShowWastes", null);
            List<WasteViewModel> wastes = JsonConvert.DeserializeObject<List<WasteViewModel>>(json);
            return Content(JsonConvert.SerializeObject(wastes.Skip((pageIndex - 1) * pageSize).Take(pageSize)));
        }

    }
}