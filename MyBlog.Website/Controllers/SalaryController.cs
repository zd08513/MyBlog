using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tools;
using Ninject;
using MyBlog.Entity.SearchInfo;
using MyBlog.Website.MyFilter;
using MyBlog.IBiz;
using MyBlog.Website.App_Start;
using MyBlog.Entity;

namespace MyBlogs.Website.Controllers
{
    [UserFilter]
    public class SalaryController : Controller
    {
        [Inject]
        public ISalaryInfoBiz biz { get; set; }

        public PartialViewResult Index(int pageindex=1)
        {
            SalaryInfoSearchInfo searchInfo = new SalaryInfoSearchInfo
            {
                PageIndex = pageindex,
                PageSize = MyBlogConfiguration.PagerSize
            };
            ViewBag.SalaryInfoSearchInfo = searchInfo;
            IEnumerable<SalaryInfo> ltSalaryInfo = biz.SalaryInfo(searchInfo);
            if (ltSalaryInfo!=null)
            {
                ViewData.Model = ltSalaryInfo;
                return PartialView();
            }
            return PartialView();
        }

        //
        // GET: /Salary/Details/5

        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("index");
            SalaryInfo salaryInfo = biz.Details(id);
            return View(salaryInfo);
        }

        //
        // GET: /Salary/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Salary/Create

        [HttpPost]
        public ActionResult Create(SalaryInfo salaryInfo)
        {
            try
            {
                salaryInfo.CreateTime = DateTime.Now.ToString("yyyy/MM/dd");
                if (biz.SalaryInfoSave(salaryInfo))
                    return RedirectToAction("Index");
                else
                    return View(salaryInfo);
            }
            catch
            {
                return View(salaryInfo);
            }
        }

        //
        // GET: /Salary/Edit/5

        public ActionResult Edit(string id)
        {
            return View(biz.Details(id));
        }

        //
        // POST: /Salary/Edit/5

        [HttpPost]
        public ActionResult Edit(SalaryInfo salaryInfo)
        {
            if (biz.SalaryInfoUpdate(salaryInfo))
                return RedirectToAction("Index");
            else
                return View(salaryInfo);
        }

        //
        // GET: /Salary/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Salary/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
