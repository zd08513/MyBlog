using MyBlog.IBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Entity;
using System.Data;
using Tools;
using MyBlog.Entity.SearchInfo;
using MyBlog.Factory;
using MyBlog.IDAL;

namespace MyBlog.Biz
{
    public class SalaryInfoBiz : ISalaryInfoBiz
    {
        private static ISalaryInfoDAL dal = DataBaseFactory.GetSalaryInfoFactory();
        public bool SalaryInfoSave(SalaryInfo info)
        {
            info.Id = Guid.NewGuid().ToString();
            return dal.SalaryInfoSave(info);
        }

        public bool SalaryInfoUpdate(SalaryInfo info)
        {
            return dal.SalaryInfoUpdate(info);
        }

        public IEnumerable<SalaryInfo> SalaryInfo(SalaryInfoSearchInfo searchInfo)
        {
            DataTable dt = dal.SalaryInfo(searchInfo);
            foreach (DataRow dr in dt.Rows)
            {
                SalaryInfo info = new SalaryInfo
                {
                    Id = Functions.ToConvert<string>(dr["id"]),
                    Money = Functions.ToConvert<float>(dr["money"]),
                    SendDate = Functions.ToConvert<string>(dr["send_date"]),
                    CreateTime = Functions.ToConvert<string>(dr["createtime"])
                };
                yield return info;
            }
        }


        public SalaryInfo Details(string id)
        {
            DataTable dt = dal.Details(id);

            SalaryInfo info = null;
            foreach (DataRow dr in dt.Rows)
            {
                info = new SalaryInfo
                {
                    Id = Functions.ToConvert<string>(dr["id"]),
                    Money = Functions.ToConvert<float>(dr["money"]),
                    SendDate = Functions.ToConvert<string>(dr["send_date"]),
                    CreateTime = Functions.ToConvert<string>(dr["createtime"])
                };
            }
            return info;
        }
    }
}
