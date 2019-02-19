using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class FoodSelectionViewModel
    {
        public int FoodSelectionId { get; set; }
        public int OrderID { get; set; }
        public int MenuID { get; set; }
        public int Amount { get; set; }

        public MenuViewModel Menu { get; set; }
        public OrderViewModel Order { get; set; }
    }
}