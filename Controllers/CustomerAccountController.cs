using ClosedXML.Excel;
using HomeworkCustomer.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Index(string searchString)
        {
            var datas = repo.All();

            if (!String.IsNullOrEmpty(searchString))
            {
                datas = datas.Where(p => p.銀行名稱.Contains(searchString));
            }
            return View(datas);
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

        [HttpPost]
        public ActionResult GetReport()
        {
            // 參考 https://dotblogs.com.tw/rexhuang/2017/05/18/230611
            using (XLWorkbook wb = new XLWorkbook())
            {
                var data = repo.All().Select(p => new { p.銀行名稱, p.銀行代碼, p.分行代碼, p.帳戶名稱, p.帳戶號碼, p.客戶資料.客戶名稱 });
                var ws = wb.Worksheets.Add("cusdata", 1);
                ws.Cell(1, 1).InsertData(data);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return this.File(memoryStream.ToArray(), "application/vnd.ms-excel", "Report.xlsx");
                }
            }
        }
    }
}