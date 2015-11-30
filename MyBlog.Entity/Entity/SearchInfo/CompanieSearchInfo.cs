using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entity
{
    /// <summary>
    /// 公司列表搜索条件
    /// </summary>
    public class CompanieSearchInfo:BasePager
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工作状态
        /// </summary>
        public int? UserToState { get; set; }
    }
}
