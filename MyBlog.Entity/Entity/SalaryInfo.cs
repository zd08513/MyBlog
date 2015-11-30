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
    /// 工资实体类
    /// </summary>
    public class SalaryInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 工资
        /// </summary>
        [DisplayName("工资")]
        [Required(ErrorMessage="请输入工资！")]
        [DataType(DataType.Currency,ErrorMessage="请输入正确的值！")]
        public float Money { get; set; }

        /// <summary>
        /// 发放工资日期
        /// </summary>
        [DisplayName("工资日期")]
        [Required(ErrorMessage="请输入发放工资日期！")]
        public string SendDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建日期")]
        public string CreateTime { get; set; }
    }
}
