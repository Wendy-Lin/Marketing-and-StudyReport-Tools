using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Discoverx.Models.StudyReport;

namespace Discoverx.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class PHStudyReportController : BaseController
    {
        // GET: PHStudyReport
        public ActionResult Index()
        {
            PHStudyReportViewModel model = new PHStudyReportViewModel();
            model.orderID = "";
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(PHStudyReportViewModel model, string submitButton)
        {
            ModelState.Clear();

            if (submitButton == "Submit")
            {
                model.GetOrderInfo();
                
            }
            else if (submitButton == "Export to PDF")
            {
                ViewPdf(model);
            }

            return View(model);
        }

        public void ViewPdf(PHStudyReportViewModel model)
        {
           
            model.DownLoadPDF(this);
            
        }

        public void DownLoadExcelFile(string orderID)
        {
            PHStudyReportViewModel model = new PHStudyReportViewModel();
            model.orderID = orderID;
            model.DownLoadExcelFile(this);
            
        }

        
        public ActionResult CoverPage()
        {
            return View();
        }

        public ActionResult SummaryPage(string orderID)
        {
            return View();
        }

        public ActionResult TechPriciplePage(string targetClass)
        {
            return View();
        }

        public ActionResult AssayDesignPage(string targetClass, string assayType)
        {
            return View();
        }

        public ActionResult ResultPage(string orderID)
        {
            return View();
        }

    }
}