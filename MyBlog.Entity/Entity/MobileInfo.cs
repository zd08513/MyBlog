using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entity
{
    /// <summary>
    /// 手机实体类
    /// </summary>
    public class MobileInfo
    {
        [DisplayName("手机名")]
        public string Name { get; set; }

        [DisplayName("文件名")]
        public string FileName { get; set; }

        [DisplayName("文件路径")]
        [Required(ErrorMessage="文件路径不能为空！")]
        public string FilePath { get; set; }

        [DisplayName("文件保存路径")]
        public string SavePath { get {
            return @"F:\KuGou";
        } }
    }
}
