using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entity
{
    /// <summary>
    /// 分页基类
    /// </summary>
    public class BasePager
    {
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总数据
        /// </summary>
        public int RowsCount { get; set; }
    }
}
