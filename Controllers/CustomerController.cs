using HomeworkCustomer.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeworkCustomer.Controllers
{
    public class CustomerController : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        // GET: Customer
        public ActionResult Index()
        {
            var datas = db.客戶資料.ToList();
            return View(datas);
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
                db.客戶資料.Add(customer);
                db.SaveChanges();

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

            var data = db.客戶資料.Find(id);

            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(int id, 客戶資料 customer)
        {
            var data = db.客戶資料.Find(id);

            if (ModelState.IsValid)
            {
                data.InjectFrom(customer);

                db.SaveChanges();

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

            var data = db.客戶資料.Find(id);

            return View(data);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            var data = db.客戶資料.Find(id);

            db.客戶資料.Remove(data);

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var data = db.客戶資料.Find(id);

            return View(data);
        }
    }
}