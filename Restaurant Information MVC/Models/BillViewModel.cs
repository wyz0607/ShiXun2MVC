using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class BillViewModel
    {
        public int BillID { get; set; }
        public int OrderID { get; set; }
        public string PaymentTime { get; set; }
        public string UserName { get; set; }
        public string PaymentWay { get; set; }
        public double BillMoney { get; set; }
        public string BillAccount { get; set; }
    }
}