using MyBlog.Entity;
using MyBlog.Entity.SearchInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IDAL
{
    /// <summary>
    /// 工资数据类接口
    /// </summary>
    public interface ISalaryInfoDAL
    {
        /// <summary>
        /// 工资保存
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool SalaryInfoSave(SalaryInfo info);

        /// <summary>
        /// 工资更新
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        bool SalaryInfoUpdate(SalaryInfo info);

        /// <summary>
        /// 工资查询
        /// </summary>
        /// <param name="searchInfo"></param>
        /// <returns></returns>
        DataTable SalaryInfo(SalaryInfoSearchInfo searchInfo);

        /// <summary>
        /// 工资详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DataTable Details(string id);
    }
}
