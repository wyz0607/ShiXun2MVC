using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class WasteViewModel
    {
        public int WasteID { get; set; }
        public int OrderID { get; set; }
        public string WasteTime { get; set; }
        public string UserName { get; set; }
        public double WasteMoney { get; set; }
        public string WasteCause { get; set; }
    }
}