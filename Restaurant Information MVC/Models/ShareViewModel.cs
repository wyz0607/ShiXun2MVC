using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class ShareViewModel
    {
        public int ShareHolderId { get; set; }
        public string HolderName { get; set; }
        public string HeadPortrait { get; set; }
        public string HolderSex { get; set; }
        public int RoleId { get; set; }
        public virtual RoleViewModel Roles { get; set; }
    }
}