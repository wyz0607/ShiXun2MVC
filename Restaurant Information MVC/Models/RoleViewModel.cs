using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class RoleViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int Permission { get; set; }
        public double InvestPrice { get; set; }
        public double DayPrice { get; set; }
        public double AccrPrice { get; set; }
    }
}