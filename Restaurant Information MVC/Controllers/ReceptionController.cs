﻿using System;
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
                var listOrder = list.Where(c => c.OrderID == OrderId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (listOrder.Count()==0)
                {
                    ViewBag.pCount = 1;
                    return View(listOrder);
                }
                else
                {
                    return View(listOrder);
                }
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
            return Content(JsonConvert.SerializeObject(wastes.Skip((pageIndex - 1) * pageSize).Take(pageSize)));
        }
        /// <summary>
        /// 生成就餐码视图
        /// </summary>
        /// <returns></returns>
        public ActionResult EatingYards()
        {
            return View();
        }
        //生成随机数函数中从strchar 数组中随机抽取
        //字母区分大小写
        //参数n为生成随机数的位数,一般取四位
        public ActionResult RandomNum() //
        {
            Random rm = new Random();
            string str = rm.Next(100000, 1000000).ToString();
            Session["str"] = str;
            int n=Convert.ToInt32(HttpClientHelper.Seng("post", "api/ReceptionApi/AddValidationNum/?num="+str, str));
            return View("EatingYards");
        }
    }
}