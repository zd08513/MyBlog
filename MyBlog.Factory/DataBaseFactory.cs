using MyBlog.IDAL;
using MyBlog.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Access.DAL;

namespace MyBlog.Factory
{
    /// <summary>
    /// 用户工厂
    /// </summary>
    public class DataBaseFactory:BaseFactory
    {
        public static IUserInfoDAL GetUserInfoFactory()
        {
            IUserInfoDAL dal = null;
            switch (DbConfig)
            {
                case "sql server":
                    dal = new UserInfoDAL();
                    break;
                case "access":
                    dal = new UserInfoAccessDAL();
                    break;
            }
            return dal;
        }

        public static ICompanieDAL GetCompanieFactory()
        {
            ICompanieDAL dal = null;
            switch (DbConfig)
            {
                case "sql server":
                    dal = new CompanieDAL();
                    break;
                case "access":
                    dal = new CompanieAccessDAL();
                    break;
            }
            return dal;
        }

        public static ISalaryInfoDAL GetSalaryInfoFactory()
        {
            ISalaryInfoDAL dal = null;
            switch (DbConfig)
            {
                case "sql server":
                    dal = new SalaryInfoDAL();
                    break;
                case "access":
                    dal = new SalaryInfoAccessDAL();
                    break;
            }
            return dal;
        }
    }
}
