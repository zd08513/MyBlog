using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Tools;
using Ninject;
using MyBlog.IBiz;
using MyBlog.Entity;
using MyBlog.Website.App_Start;

namespace MyBlog.Website.Controllers
{
    public class CompanyController : BaseController
    {
        [Inject]
        public ICompanieBiz Biz { get; set; }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="Name">公司</param>
        /// <param name="createdateFlag">按日期排序</param>
        /// <returns></returns>
        public ActionResult Index(int PageIndex = 1, string Name = "",int UserToState=-1)
        {
            CompanieSearchInfo searchInfo = new CompanieSearchInfo
            {
                Name = Name,
                UserToState=UserToState,
                PageIndex = PageIndex,
                PageSize = MyBlogConfiguration.PagerSize
            };

            IList<Company> ltCompany = Biz.CompanieQry(searchInfo);

            if (ltCompany != null)
            {
                ViewBag.VBCompanieSearchInfo = searchInfo;
                return View(ltCompany);
            }
            return HttpNotFound();
        }
        
        // GET: /Company/Details/5

        public ActionResult Details(int id = 0)
        {
            Company company = Biz.CompanieDetail(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // GET: /Company/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Company/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                company.CreateDate = DateTime.Now;
                if(Biz.CompanieSave(company))
                    return RedirectToAction("Index");
            }
            return View(company);
        }

        //
        // GET: /Company/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Company company = Biz.CompanieDetail(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // POST: /Company/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                company.CreateDate = DateTime.Now;
                if(Biz.CompanieUpdate(company))
                    return RedirectToAction("Index");
            }
            return View(company);
        }

        //
        // GET: /Company/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Company company = Biz.CompanieDetail(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        //
        // POST: /Company/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(Biz.CompanieDelete(id))
                return RedirectToAction("Index");
            return RedirectToAction("Index");
        }
    }
}