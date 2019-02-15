using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class MenuViewModel
    {
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public string MenuPhoto { get; set; }
        public string MenuIntroduce { get; set; }
        public string MenuIngredients { get; set; }
        public string MenuCuisine { get; set; }
        public string MenuKind { get; set; }
        public double UnitPrice { get; set; }
        public int SalesVolume { get; set; }
    }
}