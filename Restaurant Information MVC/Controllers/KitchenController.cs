using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_Information_MVC.Models;
using Newtonsoft.Json;
using System.Data;

namespace Restaurant_Information_MVC.Controllers
{
    public class KitchenController : Controller
    {
        // GET: Kitchen
      
        
        [HttpGet]
        //显示
        public ActionResult ShowMenu(int pageindex = 1)
        {
            //显示菜品信息
            string result = HttpClientHelper.Seng("get", "api/KitchensApi/ShowMenu", null);
            List<KitchenViewModel> kit = JsonConvert.DeserializeObject<List<KitchenViewModel>>(result);
            ViewBag.currentindex = pageindex;
            ViewBag.totaldata = kit.Count;
            ViewBag.totalpage = Math.Round((kit.Count() * 1.0) / 6);
            return View(kit.Skip((pageindex - 1) * 6).Take(6).ToList());
        }

        [HttpGet]
        //添加
        public ActionResult AddMenu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddMenu(KitchenViewModel kit,HttpPostedFileBase img)
        {
            //判断是否有图片上传
            if (img!=null)
            {
                //获取images绝对路径
                string path = Server.MapPath("/Images/");
                string fileName = DateTime.Now.ToString("yyyyMMddhhmmss")+img.FileName;
                img.SaveAs(path+fileName);
                kit.MenuPhoto = "http://localhost:58557/Images/" + fileName;
            }
            string strJson = JsonConvert.SerializeObject(kit);
            string result = HttpClientHelper.Seng("post", "api/KitchensApi/AddMenu",strJson);
            if (result.Contains("成功"))
            {
                return Redirect("/Kitchen/ShowMenu");
            }
            else
             {
                return Content("<script>alert('"+ result +"')</script>");
            }
        }

        //删除
        public ActionResult DelMenu(int id)
        {
            string str = HttpClientHelper.Seng("delete", "api/KitchensApi/DelMenu?MenuID="+id,"null");
            if (str.Contains("成功"))
            {
                return Content("删除成功");
            }
            else
            {
                return Content("删除失败");
            }
        }

        //修改
        [HttpGet]
        public ActionResult UptMenu(int id)
        {
            string str2 = HttpClientHelper.Seng("get","api/KitchensApi/ShowMenu",null);
            List<KitchenViewModel> list = JsonConvert.DeserializeObject<List<KitchenViewModel>>(str2);
            KitchenViewModel list1 = list.Where(c => c.MenuID == id).FirstOrDefault();
            return View(list1);
        }
        [HttpPost]
        public ActionResult UptMenu(KitchenViewModel kit, HttpPostedFileBase img)
        {
            if (img != null)//需要更换图片时
            {
                string path = Server.MapPath("/Images/");
                string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + img.FileName;
                img.SaveAs(path + filename);
                kit.MenuPhoto = "http://localhost:58557/Images/" + filename;
            }
      
            string jsonstr = JsonConvert.SerializeObject(kit);
            string str = HttpClientHelper.Seng("put", "api/KitchensApi/UptMenu",jsonstr);
            if (str.Contains("成功"))
            {
                return Content("修改成功");
            }
            else
            {
                return Content("修改失败");
            }
           
        }
        /// <summary>
        /// 获取一个菜式
        /// </summary>
        /// <returns>类名</returns>
        public ActionResult GetOneMenu(int id)
        {

            string str = HttpClientHelper.Seng("get", "api/Kitchen/GetOneMenu?MenuID=" + id, "null");
            KitchenViewModel kit = JsonConvert.DeserializeObject<KitchenViewModel>(str);
            return View(kit);

        }

    }
}