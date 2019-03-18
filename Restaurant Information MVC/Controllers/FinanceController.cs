using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Restaurant_Information_MVC.Models;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Remoting.Contexts;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data;
using NPOI.XSSF.UserModel;

namespace Restaurant_Information_MVC.Controllers
{
    [ShouQuanAttribute]
    [Authorize]
    public class FinanceController : Controller
    {
        //静态化两个进货商品的成本价表
        public static List<GoodsViewModel> gList;
        public static List<GoodsViewModel> glist;
        /// <summary>
        /// 查询到所有商品的成本价
        /// </summary>
        /// <returns></returns>
     
        public ActionResult ShowCost(int Permission, int pageindex=1,int pagesize=5,string name="")
        {
            if (Permission != (Permission & Convert.ToInt32(Session["Privilege"])))
            {
                return Content("<script>alert('您没有权限');location.href='/Login/Show'</script>");
            }
            gList = JsonConvert.DeserializeObject<List<GoodsViewModel>>(HttpClientHelper.Seng("get", "api/FinanceApi/ShowCost", null));
            glist = gList;
            if (name == null)
            {
                ViewBag.currentindex = pageindex;
                ViewBag.totaldata = gList.Count;
                ViewBag.totalpage = Math.Round((glist.Count() * 1.0) / 3);
                ViewBag.pCount = glist.Count();
                ViewBag.pSize = pagesize;
                return View(glist.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList());
            }
            else
            {
                ViewBag.currentindex = pageindex;
                ViewBag.totaldata = gList.Count;
                ViewBag.totalpage = Math.Round((gList.Count() * 1.0) / 3);
                var list = glist.Where(c => c.GoodsName.Contains(name));
                ViewBag.pCount = list.Count();
                ViewBag.pSize = pagesize;
                return View(list.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList());
            }
        }
        /// <summary>
        /// 按照名称查询结果
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
      
        //[HttpPost]
        //public ActionResult ShowCost(int pageindex=1,int pagesize=3,string name="")
        //{

        //    gList = glist.Where(m => m.GoodsName.Contains(name)).ToList();
            
        //        ViewBag.currentindex = pageindex;
        //        ViewBag.totaldata = gList.Count;
        //        ViewBag.totalpage = Math.Round((gList.Count() * 1.0) / 3);
        //    ViewBag.pCount = gList.Count();
        //        return View(gList.Skip((pageindex-1)*pagesize).Take(pagesize).ToList());
            
           
        //}
        public ActionResult Cost(int pageIndex = 1, int pagesize = 5,string name="")
        {
            //gList = glist.Where(m => m.GoodsName.Contains(name)).ToList();
            var list = gList.Skip((pageIndex - 1) * pagesize).Take(pagesize).ToList();
            return Content(JsonConvert.SerializeObject(list));
        }

        //静态化两个账单的list集合
        public static List<BillViewModel> bList;
        public static List<BillViewModel> blist;
        // GET: Finance
        /// <summary>
        /// 获取所有的账单信息
        /// </summary>
        /// <returns></returns>
       
        public ActionResult ShowBill(int Permission,int pageIndex = 1, int pagesize = 5)
        {
               if (Permission != (Permission & Convert.ToInt32(Session["Privilege"])))
            {
                return Content("<script>alert('您没有权限');location.href='/Login/Show'</script>");
            }
            bList = JsonConvert.DeserializeObject<List<BillViewModel>>(HttpClientHelper.Seng("get", "api/FinanceApi/ShowBill", null));
            blist = bList.OrderByDescending(m => m.BillID).ToList();
            ViewBag.pIndex = pageIndex;
            ViewBag.pSize = pagesize;
            ViewBag.pCount = bList.Count();
            return View(bList.Skip((pageIndex-1)*pagesize).Take(pagesize));
        }

        public ActionResult Bill(int pageIndex = 1, int pagesize = 5)
        {
            ViewBag.pIndex = pageIndex;
            ViewBag.pSize = pagesize;
            ViewBag.pCount = bList.Count();
            return Content(JsonConvert.SerializeObject(bList.Skip((pageIndex - 1) * pagesize).Take(pagesize)));
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShowBill(string begin, string end, string name, int pageIndex = 1, int pagesize = 5)
        {
            if (begin != "" && end != "")
            {
                bList = blist.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) >= Convert.ToDateTime(begin) && Convert.ToDateTime(m.PaymentTime) <= Convert.ToDateTime(end)).ToList();
                ViewBag.pCount = blist.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) >= Convert.ToDateTime(begin) && Convert.ToDateTime(m.PaymentTime) <= Convert.ToDateTime(end)).ToList().Count();
            }
            else if (begin == "" && end != "")
            {
                bList = blist.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) <= Convert.ToDateTime(end)).ToList();
                ViewBag.pCount = blist.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) <= Convert.ToDateTime(end)).ToList().Count();
            }
            else if (begin != "" && end == "")
            {
                bList = blist.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) >= Convert.ToDateTime(begin)).ToList();
                ViewBag.pCount = blist.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) >= Convert.ToDateTime(begin)).ToList().Count();
            }
            else
            {
                bList = blist.Where(m => m.UserName.Contains(name)).ToList();
                ViewBag.pCount = blist.Where(m => m.UserName.Contains(name)).ToList().Count();
            }
            return View(bList.Skip((pageIndex - 1) * pagesize).Take(pagesize));
        }
        /// <summary>
        /// 收入消费查询视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowEarning(int Permission)
        {
            if (Permission != (Permission & Convert.ToInt32(Session["Privilege"])))
            {
                return Content("<script>alert('您没有权限');location.href='/Login/Show'</script>");
            }
            return View();
        }
        //实例化list集合
        public static List<int> iList = new List<int>();
        /// <summary>
        /// 获取到今天的日期
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDate()
        {
            string dt = DateTime.Now.ToString("dd");
            if (iList.Count==0)
            {
                for (int i = Convert.ToInt32(dt.TrimStart('0')); i > 0; i--)
                {
                    iList.Add(i);
                }
            }
            return Content(JsonConvert.SerializeObject(iList));
        }
        /// <summary>
        /// 获取到每日的收入情况
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDayIncome()
        {
            List<double> dList = new List<double>();
            List<BillViewModel> bvList = new List<BillViewModel>();
            blist = JsonConvert.DeserializeObject<List<BillViewModel>>(HttpClientHelper.Seng("get", "api/FinanceApi/ShowBill", null));
            foreach (var item in iList)
            {
                bvList = blist.Where(m => m.PaymentTime.Substring(0, 11) == DateTime.Now.ToString("yyyy年MM月") + item.ToString().PadLeft(2, '0') + "日").ToList();
                dList.Add(bvList.Sum(m => m.BillMoney));
            }
            return Content(JsonConvert.SerializeObject(dList));
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
        /// <summary>
        /// 财务报表视图
        /// </summary>
        /// <returns></returns>
        public ActionResult FinancialStatement(int Permission, int pageIndex=1,int pageSize=5)
        {
            if (Permission != (Permission & Convert.ToInt32(Session["Privilege"])))
            {
                return Content("<script>alert('您没有权限');location.href='/Login/Show'</script>");
            }
            glist = JsonConvert.DeserializeObject<List<GoodsViewModel>>(HttpClientHelper.Seng("get", "api/FinanceApi/ShowCost", null));
            ViewBag.pIndex = pageIndex;
            ViewBag.pSize = pageSize;
            ViewBag.pCount = glist.Count();
            return View(glist.Skip((pageIndex - 1) * pageSize).Take(pageSize));
        }
        public ActionResult Statement(int pageIndex = 1, int pagesize = 5)
        {
            ViewBag.pIndex = pageIndex;
            ViewBag.pSize = pagesize;
            return Content(JsonConvert.SerializeObject(glist.Skip((pageIndex - 1) * pagesize).Take(pagesize)));
        }
        /// <summary>
        /// Excel导出
        /// </summary>
        /// <returns></returns>
        public FileResult ExcelDerive()
        {
            //创建Excel对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个Sheet
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("Sheet");
            //给sheet添加第一行的头部标题
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("商品编号");
            row.CreateCell(1).SetCellValue("商品名称");
            row.CreateCell(2).SetCellValue("商品价格");
            row.CreateCell(3).SetCellValue("成本价");
            row.CreateCell(4).SetCellValue("纯利润");
            row.CreateCell(5).SetCellValue("出售数量");
            //逐个填入到sheet里边各行
            for (int i = 0; i < glist.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(glist[i].GoodsId);
                rowtemp.CreateCell(1).SetCellValue(glist[i].GoodsName);
                rowtemp.CreateCell(2).SetCellValue(glist[i].SellingPrice);
                rowtemp.CreateCell(3).SetCellValue(glist[i].CostPrice);
                rowtemp.CreateCell(4).SetCellValue(glist[i].GoodsProfit);
                rowtemp.CreateCell(5).SetCellValue(glist[i].GoodsNum);
            }
            System.IO.MemoryStream ms = new MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "菜品出售数量，以及成本价格.xls");
        }
        /// <summary>
        /// Excel导入
        /// </summary>
        /// <param name="file"></param>
        public void ExcelToLead(HttpPostedFileBase file)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var item = Request.Files[i];
                var FileName = item.FileName;
                var SubName = FileName.Substring(FileName.LastIndexOf("."));

                IWorkbook wb = null;
                if (SubName.Equals(".xlsx"))
                {
                    wb = new XSSFWorkbook(item.InputStream);
                }
                else if (SubName.Equals(".xls"))
                {
                    wb = new HSSFWorkbook(item.InputStream);
                }
                ISheet sh = wb.GetSheetAt(0);  //获取表
                IRow rows = sh.GetRow(1);   //获取行
                for (int k = 1; k <= sh.LastRowNum; k++)   //循环行
                {
                    rows = sh.GetRow(k);

                    string sql = string.Format("insert into " + file.FileName.Substring(0,5) + " values ( ");
                    //string sql1 = string.Format("insert into Preson values ('{0}','{1}','{2}')");
                    for (int j = 0; j < rows.LastCellNum; j++)  //循环列
                    {
                        try
                        {
                            string value = rows.GetCell(j).ToString();

                            sql += string.Format("'" + value.ToString() + "',");
                        }
                        catch
                        {

                        }
                    }
                    sql = sql.Substring(0, sql.Length - 1) + ")";
                    HttpClientHelper.Seng("post", "api/FinanceApi/ExcelToLead/?sql=" + sql, sql);
                }
            }
            Response.Write("<script>alert('导入成功!');location.href='/Finance/FinancialStatement'</script>");
        }
        }
    }