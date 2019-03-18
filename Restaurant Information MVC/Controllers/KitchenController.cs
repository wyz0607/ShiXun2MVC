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
        public ActionResult ShowMenu(int Permission,int pageindex = 1,string name="")
        {
            if (Permission != (Permission & Convert.ToInt32(Session["Privilege"])))
            {
                Session["msg"] = "1";
                return Content("<script>location.href='/Login/Show'</script>");
            }
            //显示菜品信息
            string result = HttpClientHelper.Seng("get", "api/KitchensApi/ShowMenu", null);
            List<KitchenViewModel> kit = JsonConvert.DeserializeObject<List<KitchenViewModel>>(result);
            if (name != "")
            {
                List<KitchenViewModel> k = kit.Where(m => m.MenuName.Contains(name)).ToList();
                ViewBag.currentindex = pageindex;
                ViewBag.totaldata = k.Count;
                ViewBag.totalpage = Math.Round((k.Count() * 1.0) / 6);
                return View(k.Skip((pageindex - 1) * 6).Take(6).ToList());
            }
            else
            {
                ViewBag.currentindex = pageindex;
                ViewBag.totaldata = kit.Count;
                ViewBag.totalpage = Math.Round((kit.Count() * 1.0) / 6);
                return View(kit.Skip((pageindex - 1) * 6).Take(6).ToList());
            }
        }

        public ActionResult Menu(int pageIndex, int pageSize=6)
        {
            ViewBag.pIndex = pageIndex;
            string json = HttpClientHelper.Seng("get", "api/KitchensApi/ShowMenu", null);
            List<MenuViewModel> menu = JsonConvert.DeserializeObject<List<MenuViewModel>>(json);
            return Content(JsonConvert.SerializeObject(menu.Skip((pageIndex - 1) * pageSize).Take(pageSize)));
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
        public ActionResult DelMenu(string id)
        {
            string str = "";
            String[] ids = id.Split(',');
            foreach (var item in ids)
            {
                str = HttpClientHelper.Seng("delete", "api/KitchensApi/DelMenu?MenuID=" + item, "null");
            }
            
            if (str.Contains("成功"))
            {
                return Redirect("/Kitchen/ShowMenu");

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
                return Redirect("/Kitchen/ShowMenu");
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

      
        /// <summary>
        /// 管理员界面餐桌显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Gettabless()
        {
            string str = HttpClientHelper.Seng("get", "api/KitchensApi/GetCtables", null);
            List<CtableViewModel> ctable = JsonConvert.DeserializeObject<List<CtableViewModel>>(str);
            return View(ctable);
        }

        public string GetTableId(string id)
        {
            var result = HttpClientHelper.Seng("get", "api/Login/GetTableId?id="+id+"&userName="+Session["UserNameFirst"],null);
            if (Convert.ToInt32(result)>0)
            {
                return "1";
            }
            return "0";
        }
        [HttpGet]
        public ActionResult Addtable()
        {
            return View();
        }
        [HttpPost]
         public ActionResult Addtable(CtableViewModel ctable)
        {
            ctable.TableState = 0;
            string table = JsonConvert.SerializeObject(ctable);
            string str = HttpClientHelper.Seng("post", "api/KitchensApi/AddCtable", table);
            if(str.Contains("成功"))
            {
                return Content("添加成功");
            }
            else
            {
                return Content("添加失败");
            }
        }

        public string Updatetable(int id)
        {
            string str1 = HttpClientHelper.Seng("get", "api/KitchensApi/GetCtables", null);
            CtableViewModel ctable = JsonConvert.DeserializeObject<List<CtableViewModel>>(str1).Where(c=>c.TableID==id).FirstOrDefault();
            ctable.TableState = 0;
            string table = JsonConvert.SerializeObject(ctable);
            string str = HttpClientHelper.Seng("post", "api/KitchensApi/Update", table);
            if (str.Contains("成功"))
            {
                return ("操作成功");
            }
            else
            {
                return ("操作失败");
            }
        }

    }
   
}