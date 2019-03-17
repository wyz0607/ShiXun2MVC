using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public int MenuID { get; set; }
        public int OrderID { get; set; }
        public string Comments { get; set; }
        public int ReviewState { get; set; }
        public DateTime Ctime { get; set; }
        public string CName { get; set; }
        
    }
}