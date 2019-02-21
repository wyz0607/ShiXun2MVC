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
    public class FinanceController : Controller
    {
        //静态化两个进货商品的成本价表
        public static List<GoodsViewModel> gList;
        public static List<GoodsViewModel> glist;
        /// <summary>
        /// 查询到所有商品的成本价
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowCost()
        {
            gList = JsonConvert.DeserializeObject<List<GoodsViewModel>>(HttpClientHelper.Seng("get", "api/FinanceApi/ShowCost", null));
            glist = gList;
            return View(gList);
        }
        /// <summary>
        /// 按照名称查询结果
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShowCost(string name)
        {
            gList = glist.Where(m => m.GoodsName.Contains(name)).ToList();
            return View(gList);
        }
        //静态化两个账单的list集合
        public static List<BillViewModel> bList;
        public static List<BillViewModel> blist;
        // GET: Finance
        /// <summary>
        /// 获取所有的账单信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowBill()
        {
            bList = JsonConvert.DeserializeObject<List<BillViewModel>>(HttpClientHelper.Seng("get", "api/FinanceApi/ShowBill", null));
            blist = bList.OrderByDescending(m => m.BillID).ToList();
            return View(blist);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="begin">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShowBill(string begin,string end,string name)
        {
            if (begin != "" && end != "")
            {
                bList = blist.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) >= Convert.ToDateTime(begin) && Convert.ToDateTime(m.PaymentTime) <= Convert.ToDateTime(end)).ToList();
            }else if(begin =="" && end != "")
            {
                bList = blist.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) <= Convert.ToDateTime(end)).ToList();
            }else if(begin !="" && end == "")
            {
                bList = blist.Where(m => m.UserName.Contains(name) && Convert.ToDateTime(m.PaymentTime) >= Convert.ToDateTime(begin)).ToList();
            }
            else
            {
                bList = blist.Where(m => m.UserName.Contains(name)).ToList();
            }
            return View(bList);
        }
        /// <summary>
        /// 收入消费查询视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowEarning()
        {
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
            for (int i = Convert.ToInt32(dt); i >0 ; i--)
            {
                iList.Add(i);
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
                bvList = blist.Where(m => m.PaymentTime.Substring(7,2) == item.ToString()).ToList();
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
        public ActionResult FinancialStatement()
        {
            glist = JsonConvert.DeserializeObject<List<GoodsViewModel>>(HttpClientHelper.Seng("get", "api/FinanceApi/ShowCost", null));
            return View(glist);
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
        public void ExcelToLead(HttpPostedFileBase file)
        {
            string filePath = @"C:\Users\hp\Desktop\";
            DataTable dt = ImportExcelHelper.GetExcelDataTable(filePath + file.FileName);
            //List<GoodsViewModel> list = JsonConvert.DeserializeObject<List<GoodsViewModel>>(JsonConvert.SerializeObject(dt));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HttpClientHelper.Seng("post", "api/FinanceApi/ExcelToLead", dt.Rows[i].ToString());
            }
        }
    }
    #region 导入帮助类
    static public class ImportExcelHelper
    {
        public static DataTable GetExcelDataTable(string filePath)
        {
            IWorkbook Workbook;
            DataTable table = new DataTable();
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                    string fileExt = Path.GetExtension(filePath).ToLower();
                    if (fileExt == ".xls")
                    {
                        Workbook = new HSSFWorkbook(fileStream);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        Workbook = new XSSFWorkbook(fileStream);
                    }
                    else
                    {
                        Workbook = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //定位在第一个sheet
            ISheet sheet = Workbook.GetSheetAt(0);
            //第一行为标题行
            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;
            int rowCount = sheet.LastRowNum;
            //循环添加标题列
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }
            //数据
            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();
                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            dataRow[j] = GetCellValue(row.GetCell(j));
                        }
                    }
                }
                table.Rows.Add(dataRow);
            }
            return table;
        }
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
            {
                return string.Empty;
            }
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                case CellType.Unknown:
                default:
                    return cell.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }
    }
    #endregion
}