using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public int UserInfo_UserID { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string ScheduledTime { get; set; }
        public string RepastTime { get; set; }
        public int TableID { get; set; }
        public double TotalPrice { get; set; }
        public int OrderState { get; set; }
    }
}