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
        private 客戶資料Entities db = new 客戶資料Entities();
        // GET: CustomerContact
        public ActionResult Index()
        {
            var datas = db.客戶聯絡人.ToList().Where(p => p.IsDelete != true);
            return View(datas);
        }

        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶聯絡人 customerContact)
        {
            if (ModelState.IsValid)
            {
                db.客戶聯絡人.Add(customerContact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View(customerContact);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var data = db.客戶聯絡人.Find(id);

            return View(data);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var data = db.客戶聯絡人.Find(id);

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶聯絡人 customerContact)
        {
            var data = db.客戶聯絡人.Find(id);

            if (ModelState.IsValid)
            {

                data.InjectFrom(customerContact);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");

            return View(data);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var data = db.客戶聯絡人.Find(id);

            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            var data = db.客戶聯絡人.Find(id);

            db.Configuration.ValidateOnSaveEnabled = false;

            data.IsDelete = true;

            db.SaveChanges();

            return RedirectToAction("index");
        }
    }
}