using HomeworkCustomer.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeworkCustomer.Controllers
{
    public class CustomerCategoryController : Controller
    {
        客戶分類Repository repo = RepositoryHelper.Get客戶分類Repository();
        public ActionResult Index(string searchString)
        {
            var datas = repo.All();

            if (!String.IsNullOrEmpty(searchString))
            {
                datas = datas.Where(p => p.客戶分類名稱.Contains(searchString));
            }

            return View(datas);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶分類 customerCategory)
        {
            if (ModelState.IsValid)
            {
                repo.Add(customerCategory);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(customerCategory);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            };

            var data = repo.All().Where(p => p.Id == id).FirstOrDefault();

            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(int id, 客戶分類 customerCategory)
        {
            var data = repo.All().Where(p => p.Id == id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                data.InjectFrom(customerCategory);

                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(data);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            };

            var data = repo.All().Where(p => p.Id == id).FirstOrDefault();

            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {

            var data = repo.All().Where(p => p.Id == id).FirstOrDefault();
            repo.Delete(data);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var data = repo.All().Where(p => p.Id == id).FirstOrDefault();

            return View(data);
        }

    }
}