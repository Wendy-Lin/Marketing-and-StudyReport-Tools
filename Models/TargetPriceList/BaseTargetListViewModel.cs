using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Discoverx.Models.CBIS_DB;
using Discoverx.Models;


namespace Discoverx.Models.TargetPriceList
{
    public class BaseTargetListViewModel : BaseViewModel
    {
        
        public List<vw_TPL_Assays> AssayList1, AssayList2, AssayList3, AssayList4;
        public List<vw_TPL_TotalGPCR> TotalGPCRList;
        public List<vw_TPL_CellBasedKinase> RECEPTORList, CYTOSOLICList;
        public List<vw_TPL_Synergy> SynergyList;

        internal void GetPathHunterPharmacotraffickingAssayList()
        {
            AssayList1 = CBIS_DxFremontProductInfo_ctx.vw_TPL_Assays.Where(c => c.DESCRIPTION.Contains("PathHunter") && c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
        }

        internal void GetDiscoveRxInCELLHunterAssaysList()
        {
            AssayList1 = CBIS_DxFremontProductInfo_ctx.vw_TPL_Assays.Where(c => c.DESCRIPTION.Contains("bromodomain") && c.Brands == "InCell").DistinctBy(x => x.TARGET).ToList();
            AssayList2 = CBIS_DxFremontProductInfo_ctx.vw_TPL_Assays.Where(c => c.Brands == "InCell" && c.Web_Target_Class == "kinase").DistinctBy(x => x.TARGET).ToList();
            AssayList3 = CBIS_DxFremontProductInfo_ctx.vw_TPL_Assays.Where(c => c.Brands == "InCell" && c.DESCRIPTION.Contains("methyltransferase")).ToList();
            AssayList4 = CBIS_DxFremontProductInfo_ctx.vw_TPL_Assays.Where(c => c.Web_Target_Class == "Signaling Pathway" && c.Brands == "InCELL").ToList();

        }

        internal void GetPathHunterPathwayAssaysList()
        {
            AssayList1 = CBIS_DxFremontProductInfo_ctx.vw_TPL_Assays.Where(c => c.Web_Target_Class == "Signaling Pathway" && c.Brands == "PathHunter" && c.Assay_Type != "Dimerization").ToList();
        }

        internal void GetPathHunterDimerizationAssaysList()
        {
            AssayList1 = CBIS_DxFremontProductInfo_ctx.vw_TPL_Assays.Where(c => c.Assay_Type == "Dimerization").ToList();
        }

        internal void GetNuclearHormoneReceptorActivationAssaysList()
        {
            AssayList1 = CBIS_DxFremontProductInfo_ctx.vw_TPL_Assays.Where(c => c.Web_Target_Class == "Nuclear Receptor" && c.Assay_Measures == "translocation" && c.Product_Type.Contains("translocation")).ToList(); 
            AssayList2 = CBIS_DxFremontProductInfo_ctx.vw_TPL_Assays.Where(c => c.Web_Target_Class == "Nuclear Receptor" && c.Assay_Measures == "protein interaction" && c.Product_Type.Contains("protein interaction")).ToList(); 
        }

        internal void GetPathHunterArrestinOrphanGPCRTotalOfferingList()
        {
            AssayList1 = CBIS_DxFremontProductInfo_ctx.vw_TPL_Assays.Where(c => c.Web_Target_Class == "GPCR" && (c.DESCRIPTION.Contains("Arrestin Orphan") || c.Family.Contains("Orphan")) && (c.Product_Type == "Orphan" || c.Product_Type == "Arrestin") && (c.Assay_Readout.Contains("Arrestin"))).ToList();
        }

        internal void GetDiscoveRxTotalGPCRProductOfferingList()
        {
            TotalGPCRList = CBIS_DxFremontProductInfo_ctx.vw_TPL_TotalGPCR.Where(c => c.Web_Target_Class == "GPCR").DistinctBy(x => x.TARGET).ToList();
        }

        internal void GetPathHunterCellBasedKinaseAssaysList()
        {
            RECEPTORList = CBIS_DxFremontProductInfo_ctx.vw_TPL_CellBasedKinase.Where(c => c.Family.Contains("Receptor Tyrosine")).ToList();
            CYTOSOLICList = CBIS_DxFremontProductInfo_ctx.vw_TPL_CellBasedKinase.Where(c => c.Family.Contains("Cytosolic") || c.Family.Contains("Cytokine")).ToList();
        }

        internal void GetSynergyAssaysList()
        {
            SynergyList = CBIS_DxFremontProductInfo_ctx.vw_TPL_Synergy.Where(c => c.GroupID != null).OrderBy(c => c.GroupID).ThenBy(c=>c.TARGET).ToList();
        }

        internal void GetAssayList(string viewName)
        {
            if (viewName == "PathHunterPharmacotraffickingAssayList")
            {
                this.GetPathHunterPharmacotraffickingAssayList();
            }
            else if (viewName == "DiscoveRxInCELLHunterAssaysList")
            {
                this.GetDiscoveRxInCELLHunterAssaysList();
            }
            else if (viewName == "PathHunterPathwayAssaysList")
            {
                this.GetPathHunterPathwayAssaysList();
            }
            else if (viewName == "PathHunterDimerizationAssaysList")
            {
                this.GetPathHunterDimerizationAssaysList();
            }
            else if (viewName == "NuclearHormoneReceptorActivationAssaysList")
            {
                this.GetNuclearHormoneReceptorActivationAssaysList();
            }
            else if (viewName == "PathHunterArrestinOrphanGPCRTotalOfferingList")
            {
                this.GetPathHunterArrestinOrphanGPCRTotalOfferingList();
            }
            else if (viewName == "DiscoveRxTotalGPCRProductOfferingList")
            {
                this.GetDiscoveRxTotalGPCRProductOfferingList();
            }
            else if (viewName == "PathHunterCellBasedKinaseAssaysList")
            {
                this.GetPathHunterCellBasedKinaseAssaysList();
            }
            else if (viewName == "SynergyTable")
            {
                this.GetSynergyAssaysList();
            }

        }
    }
}