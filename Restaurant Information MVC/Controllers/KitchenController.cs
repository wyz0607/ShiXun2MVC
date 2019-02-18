using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_Information_MVC.Models;
using Newtonsoft.Json;



namespace Restaurant_Information_MVC.Controllers
{
    public class KitchenController : Controller
    {
        // GET: Kitchen
      
        
        [HttpGet]
        //显示
        public ActionResult ShowMenu()
        {
            return View();
        }

        [HttpGet]
        //添加
        public ActionResult AddMenu()
        {
            //分类下拉列表
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
                kit.MenuPhoto = "http://localhost:53169/Images/" + fileName;
            }
            string strJson = JsonConvert.SerializeObject(kit);
            string result = HttpClientHelper.Seng("post", "api/Kitchen/AddMenu",strJson);
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
        public ActionResult DelMenu()
        {
            return View();
        }

        //修改
        [HttpGet]
        public ActionResult GetOneMenu(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult UptMenu(KitchenViewModel kit)
        {
            return View();
        }








    }
}