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
        public ActionResult ShowOrder(int OrderId, int pageIndex=1,int pageSize=5)
        {
            string json = HttpClientHelper.Seng("get", "api/Reception/ShowOrder", null);
            return View(JsonConvert.DeserializeObject<List<OrderViewModel>>(json).Skip((pageIndex-1)*pageSize).Take(pageSize));
        }                    

        public ActionResult ShowFoodSelection(int OrderId,int pageIndex=1,int pageSize=5)
        {
            string json = HttpClientHelper.Seng("get", "api/Reception/ShowFoodSelection", null);
            return View(JsonConvert.DeserializeObject<List<FoodSelectionViewModel>>(json).Skip((pageIndex - 1) * pageSize).Take(pageSize));
        }

        public ActionResult ShowWaste(int WasteId,int pageIndex=1,int pageSize=5)
        {
            string json = HttpClientHelper.Seng("get", "api/Reception/ShowWaste", null);
            return View(JsonConvert.DeserializeObject<List<WasteViewModel>>(json).Skip((pageIndex - 1) * pageSize).Take(pageSize));
        }   

    }
}