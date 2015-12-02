using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Website.Controllers
{
    public class ProjectFlowController : BaseController
    {
        public ActionResult Index()
        {
            return View(db.ProjectFlows.ToList());
        }

        //
        // GET: /ProjectFlow/Details/5

        public ActionResult Details(int id = 0)
        {
            ProjectFlow projectflow = db.ProjectFlows.Find(id);
            if (projectflow == null)
            {
                return HttpNotFound();
            }
            return View(projectflow);
        }

        //
        // GET: /ProjectFlow/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ProjectFlow/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectFlow projectflow)
        {
            if (ModelState.IsValid)
            {
                db.ProjectFlows.Add(projectflow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projectflow);
        }

        //
        // GET: /ProjectFlow/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProjectFlow projectflow = db.ProjectFlows.Find(id);
            if (projectflow == null)
            {
                return HttpNotFound();
            }
            return View(projectflow);
        }

        //
        // POST: /ProjectFlow/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectFlow projectflow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectflow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectflow);
        }

        //
        // GET: /ProjectFlow/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProjectFlow projectflow = db.ProjectFlows.Find(id);
            if (projectflow == null)
            {
                return HttpNotFound();
            }
            return View(projectflow);
        }

        //
        // POST: /ProjectFlow/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectFlow projectflow = db.ProjectFlows.Find(id);
            db.ProjectFlows.Remove(projectflow);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}