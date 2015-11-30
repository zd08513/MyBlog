using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Entity
{
    /// <summary>
    /// 电影实体类
    /// </summary>
    public class VideoInfo
    {
        [DisplayName("电影名称")]
        public string Name { get; set; }

        [DisplayName("电影路径")]
        public string FilePath { get; set; }

        [DisplayName("电影类型格式")]
        public string VideoType { get; set; }
    }
}
