using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace Restaurant_Information_MVC.Models
{
    public class KitchenViewModel
    {
        //菜单号
        [Display(Name = "菜单号")]
        public int MenuID { get; set; }
        //菜式名
        [Display(Name = "菜式名")]
        public string MenuName { get; set; }
        //菜式图片
        [Display(Name = "菜式图片")]
        public string MenuPhoto { get; set; }
        //菜式名
        [Display(Name = "菜式简介")]
        public string MenuIntroduce { get; set; }
        //主要食材
        [Display(Name = "主要食材")]
        public string MenuIngredients { get; set; }
        //所属菜系
        [Display(Name = "所属菜系")]
        public string MenuCuisine { get; set; }
        //种类
        [Display(Name = "种类")]
        public string MenuKind { get; set; }
        //单价
        [Display(Name = "单价")]
        public double UnitPrice { get; set; }
        //销售量
        [Display(Name = "销售量")]
        public int SalesVolume { get; set; }
        public int MenuState { set; get; }
    }
}