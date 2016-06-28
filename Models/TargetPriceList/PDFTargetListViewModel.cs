using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using System.IO;

namespace Discoverx.Models.TargetPriceList
{
    public class PDFTargetListViewModel : BaseViewModel
    {
        TargetListViewModel model; 
        internal void DownloadPDFSingleView(Controller controller, string viewName, string fileName, bool displayPDFLink = false)
        {
            model = new TargetListViewModel();
            model.displayPDFLink = displayPDFLink;

            model.GetAssayList(viewName);

            pdfModel.DownloadPDFSingleView(controller, viewName, model, fileName, "TargetList");

        }



        internal void DownloadPDFWholeTragetList()
        {

            viewList = new List<ViewItem>() { 
                new ViewItem(){viewName="CoverPage", display=true }, 
                new ViewItem(){viewName="ContentPage", display=true }, 
                new ViewItem(){viewName="DiscoveRxTotalGPCRProductOfferingList", display=true }, 
                new ViewItem(){viewName="PathHunterArrestinOrphanGPCRTotalOfferingList", display=true },
                new ViewItem(){viewName="PathHunterDimerizationAssaysList", display=true },
                new ViewItem(){viewName="NuclearHormoneReceptorActivationAssaysList", display=true },
                new ViewItem(){viewName="PathHunterCellBasedKinaseAssaysList", display=true },
                new ViewItem(){viewName="PathHunterPathwayAssaysList", display=true },
                new ViewItem(){viewName="DiscoveRxInCELLHunterAssaysList", display=true },
                new ViewItem(){viewName="PathHunterPharmacotraffickingAssayList", display=true },
                new ViewItem(){viewName="OrderingInformation", display=true }
            };

            pdfModel.DownloadViewsToSinglePDFFile(viewList, "DRx_Target_List_");
        }
    }
}