using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class GoodsViewModel
    {
        public int GoodsId { get; set; }
        public string GoodsName { get; set; }
        public double SellingPrice { get; set; }
        public double CostPrice { get; set; }
        public double GoodsProfit { get; set; }
        public int GoodsNum { get; set; }
    }
}