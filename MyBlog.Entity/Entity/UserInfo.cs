using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entity.Entity
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    public class UserInfo
    {
        [DisplayName("Id")]
        public string UserId { get; set; }

        [DisplayName("用户名")]
        [Required(ErrorMessage = "用户名不能为空！")]
        public string UserName { get; set; }

        [DisplayName("密码")]
        [Required(ErrorMessage = "密码不能为空！")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "{0} 必须至少包含 {2} 个字符", MinimumLength = 6)]
        public string UserPassword { get; set; }

        [DisplayName("确认密码")]
        [Required(ErrorMessage = "新密码和确认密码不匹配！")]
        [StringLength(50, ErrorMessage = "{0} 必须至少包含 {2} 个字符", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [DisplayName("创建时间")]
        public DateTime CreateTime { get; set; }

        [DisplayName("最近登录时间")]
        public DateTime LoginTime { get; set; }
    }
}
