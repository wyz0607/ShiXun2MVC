using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class RechargeViewModel
    {
        public int Id { get; set; }
        public int ShareHolderId { get; set; }
        public string RechargeMoney { get; set; }
        public string RechargeTime { get; set; }

        public virtual ShareViewModel Menu { get; set; }
    }
}