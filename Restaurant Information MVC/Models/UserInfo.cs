using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_Information_MVC.Models
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public bool UserSex { get; set; }
        public string UserPwd { get; set; }
        public string RoleName { get; set; }
        public string Rname { get; set; }
        public string Privilege { get; set; }
    }
}