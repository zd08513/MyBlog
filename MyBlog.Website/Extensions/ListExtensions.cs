using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI.WebControls;

namespace MyBlog.Website.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// 扩展 DropDownList
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="htmlName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MvcHtmlString ListBoxs(this HtmlHelper helper, string htmlName, int value,string className)
        {
            List<SelectListItem> lists = new List<SelectListItem>{
                new SelectListItem{Text="请选择",Value="0"},
                new SelectListItem{Text="正序",Value="1",Selected=value==1},
                new SelectListItem{Text="倒序",Value="2",Selected=value==2}
            };
            return helper.DropDownList(htmlName, lists, new { @class=className});
        }

        /// <summary>
        /// DropDownList扩展
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="htmlName">标签名</param>
        /// <param name="value">值</param>
        /// <param name="className">类样式</param>
        /// <returns></returns>
        public static MvcHtmlString DropDownListExtensions(this HtmlHelper helper,string htmlName,int? value, string className)
        {
            if (value == null)
                value = -1;
            List<SelectListItem> lists = new List<SelectListItem>{
                new SelectListItem{Text="--全部--",Value="-1",Selected=value==-1},
                new SelectListItem{Text="应聘过",Value="0",Selected=value==0},
                new SelectListItem{Text="工作过",Value="1",Selected=value==1},
                new SelectListItem{Text="工作中",Value="2",Selected=value==2},
                new SelectListItem{Text="离职中",Value="3",Selected=value==3},
                new SelectListItem{Text="未去应聘",Value="4",Selected=value==4}
            };
            return helper.DropDownList(htmlName, lists, new { @class = className });
        }
    }
}