using ClosedXML.Excel;
using HomeworkCustomer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeworkCustomer.Controllers
{
    public class HomeController : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Report()
        {
            var datas = db.Customer_Test_View.ToList();

            return View(datas);
        }

        [HttpPost]
        public ActionResult GetReport()
        {
            // 暫時參考 https://dotblogs.com.tw/rexhuang/2017/05/18/230611
            using (XLWorkbook wb = new XLWorkbook())
            {
                var data = db.Customer_Test_View.Select(p => new { p.客戶名稱, p.客戶聯絡人數, p.客戶銀行數 });

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