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
    public class CustomerController : Controller
    {
        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        // GET: Customer
        public ActionResult Index(string queryString)
        {
            if (!String.IsNullOrEmpty(queryString))
            {
                var queryDatas = repo.All().Where(p => p.客戶名稱.Contains(queryString));
                return View(queryDatas);
            }
            return View(repo.All());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶資料 customer)
        {
            if (ModelState.IsValid)
            {
                repo.Add(customer);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            
            return View(customer);
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
        public ActionResult Edit(int id, 客戶資料 customer)
        {
            var data = repo.All().Where(p => p.Id == id).FirstOrDefault();

            if (ModelState.IsValid)
            {
                data.InjectFrom(customer);

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

        [HttpPost]
        public ActionResult GetReport()
        {
            // 參考 https://dotblogs.com.tw/rexhuang/2017/05/18/230611
            using (XLWorkbook wb = new XLWorkbook())
            {
                var data = repo.All().Select(p => new { p.客戶名稱, p.統一編號, p.電話, p.傳真, p.地址, p.Email });
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