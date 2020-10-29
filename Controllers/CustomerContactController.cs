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
        public ActionResult Index(string sortOrder, string searchString, string 職稱)
        {
            ViewData["sortOrder"] = sortOrder;
            ViewBag.職稱 = new SelectList(repo.All().Select(p => new { p.職稱 }).Distinct(), "職稱", "職稱");
            var datas = repo.All();

            if (!String.IsNullOrEmpty(searchString))
            {
                datas = datas.Where(p => p.姓名.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(職稱))
            {
                datas = datas.Where(p => p.職稱.Contains(職稱));
            }

            switch (sortOrder)
            {
                case "職稱_desc":
                    datas = datas.OrderByDescending(p => p.職稱);
                    break;
                case "姓名_desc":
                    datas = datas.OrderByDescending(p => p.姓名);
                    break;
                case "姓名":
                    datas = datas.OrderBy(p => p.姓名);
                    break;
                case "Email_desc":
                    datas = datas.OrderByDescending(p => p.Email);
                    break;
                case "Email":
                    datas = datas.OrderBy(p => p.Email);
                    break;
                case "手機_desc":
                    datas = datas.OrderByDescending(p => p.手機);
                    break;
                case "手機":
                    datas = datas.OrderBy(p => p.手機);
                    break;
                case "電話_desc":
                    datas = datas.OrderByDescending(p => p.電話);
                    break;
                case "電話":
                    datas = datas.OrderBy(p => p.電話);
                    break;
                case "客戶名稱_desc":
                    datas = datas.OrderByDescending(p => p.客戶資料.客戶名稱);
                    break;
                case "客戶名稱":
                    datas = datas.OrderBy(p => p.客戶資料.客戶名稱);
                    break;
                default:
                    datas = datas.OrderBy(s => s.職稱);
                    break;
            }
            return View(datas);
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

        [HttpGet]
        public JsonResult IsEmailAvailable(客戶聯絡人 model)
        {
            // 同一客戶
            var sameCustomer = repo.Where(p => p.客戶Id == model.客戶Id);
            // 聯絡人信箱必須不同
            var result = sameCustomer.Where(p => p.Email == model.Email && p.Id != model.Id).Any();

            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetReport()
        {
            // 參考 https://dotblogs.com.tw/rexhuang/2017/05/18/230611
            using (XLWorkbook wb = new XLWorkbook())
            {
                var data = repo.All().Select(p => new { p.職稱, p.姓名, p.Email, p.手機, p.電話, p.客戶資料.客戶名稱 });
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