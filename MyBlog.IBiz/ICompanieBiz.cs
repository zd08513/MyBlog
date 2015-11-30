using MyBlog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IBiz
{
    /// <summary>
    /// 应聘公司接口
    /// </summary>
    public interface ICompanieBiz
    {
        /// <summary>
        /// 查询应聘公司列表
        /// </summary>
        /// <param name="searchInfo"></param>
        /// <returns></returns>
        IList<Company> CompanieQry(CompanieSearchInfo searchInfo);

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Company CompanieDetail(int Id);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="comany"></param>
        /// <returns></returns>
        bool CompanieSave(Company comany);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        bool CompanieUpdate(Company company);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool CompanieDelete(int Id);
    }
}
