using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;

using Discoverx.Models;
using Discoverx.Models.TargetPriceList;

namespace Discoverx.Controllers
{
    public class TargetListController : BaseController
    {
        TargetListViewModel targetListViewModel;
        PDFTargetListViewModel pdfTargetListViewModel;
        ExcelTargetListModel excelTargetListModel;

        public TargetListController()
        {
            targetListViewModel = new TargetListViewModel();
            pdfTargetListViewModel = new PDFTargetListViewModel();
            excelTargetListModel = new ExcelTargetListModel();
        }

        // GET: TargetList
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CoverPage()
        {
            return View();
        }

        public ActionResult ContentPage()
        {
            return View();
        }

        public ActionResult OrderingInformation()
        {
            return View();
        }

        public ActionResult DiscoveRxTotalGPCRProductOfferingList(bool displayPDFLink = false)
        {
            
            targetListViewModel.GetDiscoveRxTotalGPCRProductOfferingList();
            targetListViewModel.displayPDFLink = displayPDFLink;
            return View(targetListViewModel);
        }

        public ActionResult PathHunterArrestinOrphanGPCRTotalOfferingList(bool displayPDFLink = false)
        {
            
            targetListViewModel.GetPathHunterArrestinOrphanGPCRTotalOfferingList();
            targetListViewModel.displayPDFLink = displayPDFLink;
            return View(targetListViewModel);
        }

        public ActionResult PathHunterDimerizationAssaysList(bool displayPDFLink = false)
        {
            
            targetListViewModel.GetPathHunterDimerizationAssaysList();
            targetListViewModel.displayPDFLink = displayPDFLink;
            return View(targetListViewModel);
        }

        public ActionResult PathHunterPathwayAssaysList(bool displayPDFLink = false)
        {
            targetListViewModel.GetPathHunterPathwayAssaysList();
            targetListViewModel.displayPDFLink = displayPDFLink;
            return View(targetListViewModel);
        }

        public ActionResult NuclearHormoneReceptorActivationAssaysList(bool displayPDFLink = false)
        {

            targetListViewModel.GetNuclearHormoneReceptorActivationAssaysList();
            targetListViewModel.displayPDFLink = displayPDFLink;
            return View(targetListViewModel);
        }


        public ActionResult PathHunterCellBasedKinaseAssaysList(bool displayPDFLink = false)
        {
            targetListViewModel.GetPathHunterCellBasedKinaseAssaysList();
            targetListViewModel.displayPDFLink = displayPDFLink;
            return View(targetListViewModel);
        }

        public ActionResult DiscoveRxInCELLHunterAssaysList(bool displayPDFLink = false)
        {

            targetListViewModel.GetDiscoveRxInCELLHunterAssaysList();
            targetListViewModel.displayPDFLink = displayPDFLink;
            return View(targetListViewModel);
        }

        public ActionResult PathHunterPharmacotraffickingAssayList(bool displayPDFLink = false)
        {

            targetListViewModel.GetPathHunterPharmacotraffickingAssayList();
            targetListViewModel.displayPDFLink = displayPDFLink;
            return View(targetListViewModel);
        }

        
        public void ViewPdf(string pageTitle, string viewName, string fileName)
        {
            pdfTargetListViewModel.DownloadPDFSingleView(this, viewName, fileName);
        }

        public void DownloadWholeTargetListPDF()
        {
            Cursor.Current = Cursors.WaitCursor;
            pdfTargetListViewModel.DownloadPDFWholeTragetList();
        }

        public void DownloadWholeTargetListExcel()
        {
            Cursor.Current = Cursors.WaitCursor;
            excelTargetListModel.DownloadExcelWholeTragetList();
        }

       
    }
}
