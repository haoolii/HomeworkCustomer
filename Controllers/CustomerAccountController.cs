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
        private 客戶資料Entities db = new 客戶資料Entities();
        // GET: CustomerAccount
        public ActionResult Index()
        {
            var datas = db.客戶銀行資訊.ToList().Where(p => p.IsDelete != true);
            return View(datas);
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶銀行資訊 customerAccount)
        {
            if (ModelState.IsValid)
            {
                db.客戶銀行資訊.Add(customerAccount);
                db.SaveChanges();
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
            var data = db.客戶銀行資訊.Find(id);

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶銀行資訊 account)
        {
            var data = db.客戶銀行資訊.Find(id);

            if (ModelState.IsValid)
            {

                data.InjectFrom(account);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");

            return View(data);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var data = db.客戶銀行資訊.Find(id);

            return View(data);
        }

        public ActionResult Delete(int? id)
        {
            if(!id.HasValue)
            {
                return HttpNotFound();
            }

            var data = db.客戶銀行資訊.Find(id);

            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            var data = db.客戶銀行資訊.Find(id);

            data.IsDelete = true;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}