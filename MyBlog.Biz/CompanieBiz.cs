using MyBlog.Entity;
using MyBlog.IBiz;
using Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Factory;
using MyBlog.IDAL;

namespace MyBlog.Biz
{
    /// <summary>
    /// 实现应聘公司接口
    /// </summary>
    public class CompanieBiz : ICompanieBiz
    {
        private static ICompanieDAL dal = DataBaseFactory.GetCompanieFactory();
        /// <summary>
        /// 查询应聘公司列表
        /// </summary>
        /// <param name="searchInfo"></param>
        /// <returns></returns>
        public IList<Company> CompanieQry(CompanieSearchInfo searchInfo)
        {
            DataTable dtCompanieQry = dal.CompanieQry(searchInfo);

            IList<Company> list = new List<Company>();
            foreach (DataRow dr in dtCompanieQry.Rows)
            {
                list.Add(new Company
                {
                    Id = Functions.ToConvert<int>(dr["Id"]),
                    Name = Functions.ToConvert<string>(dr["Name"]),
                    Address = Functions.ToConvert<string>(dr["Address"]),
                    UserToState = (UserToState)Enum.Parse(typeof(UserToState), Functions.ToConvert<string>(dr["UserToState"]), true),
                    WorkToDate = Functions.ToConvert<string>(dr["WorkToDate"]),
                    CreateDate = Functions.ToConvert<DateTime>(dr["CreateDate"])
                });
            }
            return list;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Company CompanieDetail(int Id)
        {
            return dal.CompanieDetail(Id);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="comany"></param>
        /// <returns></returns>
        public bool CompanieSave(Company comany)
        {
            return dal.CompanieSave(comany);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool CompanieDelete(int Id)
        {
            return dal.CompanieDelete(Id);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public bool CompanieUpdate(Company company)
        {
            return dal.CompanieUpdate(company);
        }
    }
}
