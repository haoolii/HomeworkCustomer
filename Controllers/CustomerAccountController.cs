using HomeworkCustomer.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeworkCustomer.Controllers
{
    public class CustomerAccountController : Controller
    {
        客戶銀行資訊Repository repo;
        客戶資料Repository repoCustomer;
        public CustomerAccountController()
        {
            repo = RepositoryHelper.Get客戶銀行資訊Repository();
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
        public ActionResult Create(客戶銀行資訊 customerAccount)
        {
            if (ModelState.IsValid)
            {
                repo.Add(customerAccount);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(customerAccount);
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
        public ActionResult Edit(int id, 客戶銀行資訊 account)
        {
            var data = repo.Where(p => p.Id == id).FirstOrDefault();

            if (ModelState.IsValid)
            {

                data.InjectFrom(account);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱");

            return View(data);
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

            return RedirectToAction("Index");
        }
    }
}