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
    public class ReceptionController : Controller
    {
        // GET: Reception
        public ActionResult ShowOrder(int OrderId=0, int pageIndex=1,int pageSize=3)
        {
            ViewBag.pIndex = pageIndex;
            ViewBag.pSize = pageSize;
            string json = HttpClientHelper.Seng("get", "api/ReceptionApi/ShowOrder", null);
            List<OrderViewModel> order = JsonConvert.DeserializeObject<List<OrderViewModel>>(json);
            //List<OrderViewModel> order = new List<OrderViewModel>() {
            //    new OrderViewModel{ OrderID=1,UserID=1,TableID=1, OrderState=1, RepastTime="12:00", ScheduledTime="12:00", TotalPrice=2170.00, UserName="张三", UserPhone="17301622446"},
            //    new OrderViewModel{ OrderID=2,UserID=1,TableID=1, OrderState=1, RepastTime="12:00", ScheduledTime="12:00", TotalPrice=2170.00, UserName="张三", UserPhone="17301622446"},
            //    new OrderViewModel{ OrderID=3,UserID=1,TableID=1, OrderState=1, RepastTime="12:00", ScheduledTime="12:00", TotalPrice=2170.00, UserName="张三", UserPhone="17301622446"},
            //    new OrderViewModel{ OrderID=4,UserID=1,TableID=1, OrderState=1, RepastTime="12:00", ScheduledTime="12:00", TotalPrice=2170.00, UserName="张三", UserPhone="17301622446"},
            //    new OrderViewModel{ OrderID=5,UserID=1,TableID=1, OrderState=1, RepastTime="12:00", ScheduledTime="12:00", TotalPrice=2170.00, UserName="张三", UserPhone="17301622446"},
            //};
            ViewBag.pCount = order.Count();
            if (OrderId==0)
            {
                return View(order.Skip((pageIndex - 1) * pageSize).Take(pageSize));
            }
            else
            {
                ViewBag.pCount = 1;
                return View(order.Where(c=>c.OrderID==OrderId).ToList());
            }
            
        }

        public ActionResult Order(int OrderId = 0, int pageIndex = 1, int pageSize = 3)
        {
            ViewBag.pIndex = pageIndex;
            string json = HttpClientHelper.Seng("get", "api/ReceptionApi/ShowOrder", null);
            List<OrderViewModel> order = JsonConvert.DeserializeObject<List<OrderViewModel>>(json);
            //List<OrderViewModel> order = new List<OrderViewModel>() {
            //    new OrderViewModel{ OrderID=1,UserID=1,TableID=1, OrderState=1, RepastTime="12:00", ScheduledTime="12:00", TotalPrice=2170.00, UserName="张三", UserPhone="17301622446"},
            //    new OrderViewModel{ OrderID=2,UserID=1,TableID=1, OrderState=1, RepastTime="12:00", ScheduledTime="12:00", TotalPrice=2170.00, UserName="张三", UserPhone="17301622446"},
            //    new OrderViewModel{ OrderID=3,UserID=1,TableID=1, OrderState=1, RepastTime="12:00", ScheduledTime="12:00", TotalPrice=2170.00, UserName="张三", UserPhone="17301622446"},
            //    new OrderViewModel{ OrderID=4,UserID=1,TableID=1, OrderState=1, RepastTime="12:00", ScheduledTime="12:00", TotalPrice=2170.00, UserName="张三", UserPhone="17301622446"},
            //    new OrderViewModel{ OrderID=5,UserID=1,TableID=1, OrderState=1, RepastTime="12:00", ScheduledTime="12:00", TotalPrice=2170.00, UserName="张三", UserPhone="17301622446"},
            //};
            return Content(JsonConvert.SerializeObject(order.Skip((pageIndex-1)*pageSize).Take(pageSize)));
        }

        public ActionResult ShowFoodSelection(int OrderId=0,int pageIndex=1,int pageSize=3)
        {
            ViewBag.pIndex = pageIndex;
            ViewBag.pSize = pageSize;
            string json = HttpClientHelper.Seng("get", "api/ReceptionApi/ShowFoodSelection", null);
            List<FoodSelectionViewModel> list = JsonConvert.DeserializeObject<List<FoodSelectionViewModel>>(json);
            ViewBag.pCount = list.Count();
            if (OrderId == 0)
            {
                return View(list.Skip((pageIndex - 1) * pageSize).Take(pageSize));
            }
            else
            {
                ViewBag.pCount = 1;
                return View(list.Where(c => c.OrderID == OrderId).ToList());
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
            string json = HttpClientHelper.Seng("get", "api/ReceptionApi/ShowWaste", null);
            List<WasteViewModel> wastes = JsonConvert.DeserializeObject<List<WasteViewModel>>(json);
            //List<WasteViewModel> wastes = new List<WasteViewModel>() {
            //    new WasteViewModel(){  WasteID=1,UserName="张三", WasteCause="太晚了，不去了", WasteMoney=1560.00, WasteTime="2019-2-18 16:34:00"},
            //    new WasteViewModel(){  WasteID=2,UserName="张三", WasteCause="太晚了，不去了", WasteMoney=1560.00, WasteTime="2019-2-18 16:34:00"},
            //    new WasteViewModel(){  WasteID=3,UserName="张三", WasteCause="太晚了，不去了", WasteMoney=1560.00, WasteTime="2019-2-18 16:34:00"},
            //    new WasteViewModel(){  WasteID=4,UserName="张三", WasteCause="太晚了，不去了", WasteMoney=1560.00, WasteTime="2019-2-18 16:34:00"},
            //    new WasteViewModel(){  WasteID=5,UserName="张三", WasteCause="太晚了，不去了", WasteMoney=1560.00, WasteTime="2019-2-18 16:34:00"}
            //};
            ViewBag.pCount = wastes.Count();
            if (WasteId == 0)
            {
                return View(wastes.Skip((pageIndex - 1) * pageSize).Take(pageSize));
            }
            else
            {
                ViewBag.pCount = 1;
                return View(wastes.Where(c => c.WasteID == WasteId).ToList());
            }
        }

        public ActionResult Waste(int WasteId = 0, int pageIndex = 1, int pageSize = 3)
        {
            ViewBag.pIndex = pageIndex;
            string json = HttpClientHelper.Seng("get", "api/ReceptionApi/ShowWastes", null);
            List<WasteViewModel> wastes = JsonConvert.DeserializeObject<List<WasteViewModel>>(json);
            //List<WasteViewModel> wastes = new List<WasteViewModel>() {
            //    new WasteViewModel(){  WasteID=1,UserName="张三", WasteCause="太晚了，不去了", WasteMoney=1560.00, WasteTime="2019-2-18 16:34:00"},
            //    new WasteViewModel(){  WasteID=2,UserName="张三", WasteCause="太晚了，不去了", WasteMoney=1560.00, WasteTime="2019-2-18 16:34:00"},
            //    new WasteViewModel(){  WasteID=3,UserName="张三", WasteCause="太晚了，不去了", WasteMoney=1560.00, WasteTime="2019-2-18 16:34:00"},
            //    new WasteViewModel(){  WasteID=4,UserName="张三", WasteCause="太晚了，不去了", WasteMoney=1560.00, WasteTime="2019-2-18 16:34:00"},
            //    new WasteViewModel(){  WasteID=5,UserName="张三", WasteCause="太晚了，不去了", WasteMoney=1560.00, WasteTime="2019-2-18 16:34:00"}
            //};
            return Content(JsonConvert.SerializeObject(wastes.Skip((pageIndex - 1) * pageSize).Take(pageSize)));
        }

    }
}