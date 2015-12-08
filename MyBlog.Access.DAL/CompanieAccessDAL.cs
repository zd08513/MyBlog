using MyBlog.IDAL;
using MyBlog.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MyBlog.Access.DAL
{
    public class CompanieAccessDAL : ICompanieDAL
    {
        public DataTable CompanieQry(CompanieSearchInfo searchInfo)
        {
            throw new NotImplementedException();
        }

        public Company CompanieDetail(int Id)
        {
            throw new NotImplementedException();
        }

        public bool CompanieSave(Company company)
        {
            throw new NotImplementedException();
        }

        public bool CompanieUpdate(Company company)
        {
            throw new NotImplementedException();
        }

        public bool CompanieDelete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
