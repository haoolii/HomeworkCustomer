using HomeworkCustomer.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeworkCustomer.Controllers
{
    public class CustomerContactController : Controller
    {
        客戶聯絡人Repository repo;
        客戶資料Repository repoCustomer;
        // GET: CustomerContact
        public CustomerContactController()
        {
            repo = RepositoryHelper.Get客戶聯絡人Repository();
            repoCustomer = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
        }
        public ActionResult Index()
        {
            return View(repo.All());
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶聯絡人 customerContact)
        {
            if (ModelState.IsValid)
            {
                repo.Add(customerContact);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱");
            return View(customerContact);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var data = repo.Where(p => p.Id == id).FirstOrDefault();

            return View(data);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var data = repo.Where(p => p.Id == id).FirstOrDefault();

            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱");

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶聯絡人 customerContact)
        {
            var data = repo.Where(p => p.Id == id).FirstOrDefault();

            if (ModelState.IsValid)
            {

                data.InjectFrom(customerContact);

                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱");

            return View(data);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var data = repo.Where(p => p.Id == id).FirstOrDefault();

            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            var data = repo.Where(p => p.Id == id).FirstOrDefault();
            repo.Delete(data);
            repo.UnitOfWork.Commit();
            return RedirectToAction("index");
        }

        // 修改時會有BUG
        [HttpGet]
        public JsonResult IsEmailAvailable(客戶聯絡人 model)
        {
            // 同一客戶
            var sameCustomer = repo.Where(p => p.Id != model.Id && p.客戶Id == model.客戶Id);
            // 聯絡人信箱必須不同
            var result = sameCustomer.Where(p => p.Email != model.Email).Any();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}