using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class ValidationViewModel
    {
        public int Id { get; set; }
        public string ValidationNum { get; set; }
        public int OrderId { get; set; }
        public int Static { get; set; }
    }
}