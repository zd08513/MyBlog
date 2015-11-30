using MyBlog.Entity;
using MyBlog.Entity.SearchInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IBiz
{
    /// <summary>
    /// 工资接口业务
    /// </summary>
    public interface ISalaryInfoBiz
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool SalaryInfoSave(SalaryInfo info);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool SalaryInfoUpdate(SalaryInfo info);

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        IEnumerable<SalaryInfo> SalaryInfo(SalaryInfoSearchInfo searchInfo);

        /// <summary>
        /// 读取工资实体类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SalaryInfo Details(string id);
    }
}
