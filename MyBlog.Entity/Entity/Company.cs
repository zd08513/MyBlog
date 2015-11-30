using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entity
{
    [DisplayName("公司信息")]
    public class Company
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("公司名称")]
        [Required(ErrorMessage = "{0}不能为空！")]
        public string Name { get; set; }

        [DisplayName("公司地址")]
        [Required(ErrorMessage = "{0}不能为空！")]
        public string Address { get; set; }

        [DisplayName("状态")]
        public UserToState UserToState { get; set; }

        [DisplayName("工作时间范围")]
        [Required(ErrorMessage = "{0}不能为空！")]
        public string WorkToDateString { get; set; }

        [DisplayName("工作时长")]
        [Required(ErrorMessage = "{0}不能为空！")]
        public string WorkToDate { get; set; }

        [DisplayName("创建时间")]
        public DateTime CreateDate { get; set; }
    }

    #region 枚举变量
    /// <summary>
    /// 应聘状态
    /// </summary>
    public enum UserToState
    {
        应聘过 = 0,
        工作过 = 1,
        工作中 = 2,
        离职中 = 3,
        未去应聘 = 4
    }
    #endregion
}
