using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiQPdf;
using Discoverx.Models.CBIS_DB;
using Discoverx.Models;

namespace Discoverx.Models.TargetPriceList
{

    public class PriceListViewModel : BaseViewModel
    {
        public string caption;
        public List<vw_TPL_ProductPrice> CommonPriceList;
        public List<vw_TPL_ProductPrice> HitHuntercAMPAssays, cAMPHunterHumanGPCRCellLines, cAMPHunterOrthologGPCRCellLines, CalciumAssays, NativeCalciumCellLines, HitHunterIP3Assays, HitHuntercGMPAssays;
        public List<vw_TPL_ProductPrice> PathHunterArrestinHumanGPCRCellLines, PathHunterArrestinOrthologGPCRCellLines, PathHunterArrestinOrphanGPCRCellLines, PathHunterGPCRInternalizationCellLines, PathHunterGPCRTotalInternalizationCellLines, PathHunterGPCRActiveInternalizationCellLines;
        public List<vw_TPL_ProductPrice> PHARMACOTRAFFICKINGASSAY;
        public List<vw_TPL_ProductPrice> cAMPHuntereXpressHumanGPCRAssays, cAMPHuntereXpressOrthologGPCRAssays, PathHuntereXpressArrestinHumanGPCRAssays, PathHuntereXpressArrestinOrthologGPCRAssays, PathHuntereXpressGPCRInternalizationAssays, PathHunterGPCRExplorerKits, PathHuntereXpressGPCRProfilerAssays;
        public List<vw_TPL_ProductPrice> PathHunterDimerizationAssays, PathHunterDimerizationAssayReadyKits;
        public List<vw_TPL_ProductPrice> PathHunterCellBasedKinaseAssays, PathHunterCytosolicTyrosineKinaseAssays, PathHunterCellBasedKinaseAssayReadyKits, PathHunterCytosolicTyrosineKinaseAssayReadyKits, ADPAccumulationAssays;
        public List<vw_TPL_ProductPrice> PathHunterNuclearHormoneReceptorActivationAssays, PathHunterNuclearHormoneReceptorAssayReadyKits, PathHunterProteaseAssays, BiochemicalAssays;
        public List<vw_TPL_ProductPrice> PathHunterPathwayAssays, PathHunterPathwayAssayReadyKits;
        public List<vw_TPL_ProductPrice> InCELLHunterCompoundBindingAssays, InCELLHunterProteinBindingAssays, InCELLHunterProteinBindingAssayReadyKits, InCELLHunterKinaseBindingAssays, InCELLHunterKinaseBindingAssayReadyKits;
        public List<vw_TPL_ProductPrice> AbHunterAntiReceptorGPCRFunctionalAssayKits, AbHunterAntiReceptorRTKFunctionalAssayKits, AbHunterAntiReceptorGPCRAntibodyBindingKits, AbHunterAntiReceptorRTKAntibodyBindingKits;
        public List<vw_TPL_ProductPrice> DetectionReagents, CytoTracker, ControlLigands, AssayCompleteCellPlatingReagents, AssayCompleteReviveMedia, AssayCompletePreserveFreezingReagent,AssayCompleteCellCultureKit, AdditionalSynergyProducts;

        public List<vw_TPL_ProductPrice> PathhunterDetectionKits, PathhunterFlashDetectionKits, PathhunterBioassayDetectionKits, InCellhunterDetectionKitII, InCellhunterDetectionKit, TargetEngagementAssaySystemKit;
        public List<vw_TPL_ProductPrice> PathHunterCellBasedRTKActivityAssays, PathHunterCellBasedRTKFunctionalAssays, PathHunterCellBasedCTKActivityAssays, PathHunterCellBasedCTKFunctionalAssays, InCELLHunterBromodomainBindingAssays, InCELLHunterMethyltransferaseBindingAssays;
        public List<vw_TPL_ProductPrice> NuclearTranslocationAssays, ProteinInteractionAssays, NuclearTranslocationKits, ProteinInteractionKits, PathHunterCellBasedRTKActivityKits, PathHunterCellBasedRTKFunctionalKits, PathHunterCellBasedCTKActivityKits, PathHunterCellBasedCTKFunctionalKits;
        public List<vw_TPL_ProductPrice> InCELLHunterBromodomainBindingKits, InCELLHunterMethyltransferaseBindingKits, PathHunterPathwayKits, PathHuntereXpressArrestinOrphanGPCRAssays, HitHuntercAMPSmallMoleculeAssays, HitHuntercAMPMemAssays, HitHuntercAMPBiologicAssays, HitHuntercAMPHSPlusAssays;

        public List<ViewItem> viewList;

        public bool displayPDFLink;

        public PriceListViewModel()
        {
            this.displayPDFLink = false;
        }

        public void GetNewProducts(bool displayPDFLink = false)
        {
            CommonPriceList = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.ToList().Where(c => c.Date_Added.ParseDate() > DateTime.Now.AddDays(-90)).ToList();
            caption = "New Product List";
            this.displayPDFLink = displayPDFLink;
            
        }
        /*	cAMPHunter Second Messenger Assays*/

        public void GetHitHuntercAMPAssays(bool displayPDFLink = false)
        {
            CommonPriceList=HitHuntercAMPAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.TARGET == "cAMP").ToList();
            caption = "HitHunter<sup>®</sup> cAMP Assays";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetcAMPHunterHumanGPCRCellLines(bool displayPDFLink = false)
        {
            CommonPriceList = cAMPHunterHumanGPCRCellLines = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "cAMP Hunter" && c.CONFIGURATION == "Clone").ToList().Where(c => c.TARGET != null && c.TARGET.IndexOf("m") != 0 && c.TARGET.IndexOf("r") != 0 && c.TARGET.IndexOf("c") != 0 && c.TARGET.IndexOf("s") != 0).ToList();
            caption = "cAMP Hunter™ Human GPCR Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetcAMPHunterOrthologGPCRCellLines(bool displayPDFLink = false)
        {
            CommonPriceList = cAMPHunterOrthologGPCRCellLines = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "cAMP Hunter" && c.CONFIGURATION == "Clone").ToList().Where(c => c.TARGET != null && (c.TARGET.IndexOf("m") == 0 || c.TARGET.IndexOf("r") == 0 || c.TARGET.IndexOf("c") == 0 || c.TARGET.IndexOf("s") == 0)).ToList();
            caption = "cAMP Hunter™ Ortholog GPCR Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetCalciumAssays(bool displayPDFLink = false)
        {
            CommonPriceList = CalciumAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.TARGET == "Calcium").ToList();
            caption = "Calcium Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetCalciumNoWashPLUSAssays(bool displayPDFLink = false)
        {
            CommonPriceList = CalciumAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.TARGET == "Calcium" && c.DESCRIPTION.Contains("Calcium No WashPLUS")).ToList();
            caption = "Calcium  No WashPLUS Assays";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetNativeCalciumCellLines(bool displayPDFLink = false)
        {
            CommonPriceList = NativeCalciumCellLines = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.TARGET != "Calcium" && c.Assay_Readout == "Calcium" && c.CONFIGURATION == "Clone").ToList();
            caption = "Native Calcium Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetHitHunterIP3Assays(bool displayPDFLink = false)
        {
            CommonPriceList = HitHunterIP3Assays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.TARGET == "IP3" && c.Brands == "HitHunter").ToList();
            caption = "HitHunter<sup>®</sup> IP3 Assays";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetHitHuntercGMPAssays(bool displayPDFLink = false)
        {
            CommonPriceList = HitHuntercGMPAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.TARGET == "cGMP" && c.Brands == "HitHunter").ToList();
            caption = "HitHunter<sup>®</sup> cGMP Assays";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetcAMPHuntereXpressGPCRAssays(bool displayPDFLink = false)
        {
            CommonPriceList = cAMPHuntereXpressHumanGPCRAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Brands == "cAMP Hunter" && c.Web_Product_Category_Primary == "Assay Ready Kits" &&  !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "cAMP Hunter™ eXpress GPCR Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetHitHuntercAMPSmallMolecules(bool displayPDFLink = false)
        {
            CommonPriceList = HitHuntercAMPSmallMoleculeAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.DESCRIPTION.Contains("Small Molecules") && c.TARGET == "cAMP" && c.Brands == "HitHunter").ToList();
            caption = "HitHunter<sup>®</sup> cAMP for Small Molecules";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetHitHuntercAMPBiologics(bool displayPDFLink = false)
        {
            CommonPriceList = HitHuntercAMPBiologicAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.DESCRIPTION.Contains("Biologics") && c.TARGET == "cAMP" && c.Brands == "HitHunter").ToList();
            caption = "HitHunter<sup>®</sup> cAMP for Biologics";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetHitHuntercAMPMemAssays(bool displayPDFLink = false)
        {
            CommonPriceList = HitHuntercAMPMemAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.DESCRIPTION.Contains("Mem") && c.TARGET == "cAMP" && c.Brands == "HitHunter").ToList();
            caption = "HitHunter<sup>®</sup> cAMP Mem Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetHitHuntercAMPHSPlusAssays(bool displayPDFLink = false)
        {
            CommonPriceList = HitHuntercAMPHSPlusAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.DESCRIPTION.Contains("HS+") && c.TARGET == "cAMP" && c.Brands == "HitHunter").ToList();
            caption = "HitHunter<sup>®</sup> cAMP HS+ Assays";
            this.displayPDFLink = displayPDFLink;
        }
        /*b)	PathHunter GPCR Cell-Base Assays*/

        public void GetPathHunterArrestinHumanGPCRCellLines(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterArrestinHumanGPCRCellLines = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Readout == "B-Arrestin" && c.Brands == "Pathhunter" && c.CONFIGURATION == "Clone" && !c.DESCRIPTION.Contains("orphan") && c.TARGET.IndexOf("s") != 0 && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList().Where(c => c.TARGET != null && c.TARGET.IndexOf("m") != 0 && c.TARGET.IndexOf("r") != 0 && c.TARGET.IndexOf("c") != 0).ToList();
            caption = "PathHunter<sup>®</sup> β-Arrestin Human GPCR Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetPathHunterArrestinOrthologGPCRCellLines(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterArrestinOrthologGPCRCellLines = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Readout == "B-Arrestin" && c.Brands == "Pathhunter" && c.CONFIGURATION == "Clone" && !c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList().Where(c => c.TARGET != null && (c.TARGET.IndexOf("m") == 0 || c.TARGET.IndexOf("r") == 0 || c.TARGET.IndexOf("c") == 0 || c.TARGET.IndexOf("s") == 0)).ToList();
            caption = "PathHunter<sup>®</sup> β-Arrestin Ortholog GPCR Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetPathHunterArrestinOrphanGPCRCellLines(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterArrestinOrphanGPCRCellLines = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Readout == "B-Arrestin" && c.Brands == "Pathhunter" && c.CONFIGURATION == "Clone" && c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> β-Arrestin Orphan GPCR Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetPathHunterGPCRInternalizationCellLines(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterGPCRInternalizationCellLines = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Readout == "Internalization" && c.Brands == "Pathhunter" && c.CONFIGURATION == "Clone" && !c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> Total GPCR Internalization Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterGPCRTotalInternalizationCellLines(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterGPCRTotalInternalizationCellLines = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Format == "Total Internalization" && c.Brands == "Pathhunter" && c.CONFIGURATION == "Clone" && !c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> Total GPCR Internalization Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterGPCRActiveInternalizationCellLines(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterGPCRActiveInternalizationCellLines = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Format == "Active Internalization" && c.Brands == "Pathhunter" && c.CONFIGURATION == "Clone" && !c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> Activated GPCR Internalization Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }

        /*b)	PathHunter GPCR Cell Pool*/
        public void GetPathHunterArrestinOrthologGPCRCellPool(bool displayPDFLink = false)
        {
            CommonPriceList = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Readout == "B-Arrestin" && c.Brands == "Pathhunter" && c.CONFIGURATION == "Cell Pool" && !c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList().Where(c => c.TARGET != null && (c.TARGET.IndexOf("m") == 0 || c.TARGET.IndexOf("r") == 0 || c.TARGET.IndexOf("c") == 0 || c.TARGET.IndexOf("s") == 0)).ToList();
            caption = "PathHunter<sup>®</sup> β-Arrestin Ortholog GPCR Cell Pool";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterGPCRTotalInternalizationCellPool(bool displayPDFLink = false)
        {
            CommonPriceList = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Format == "Total Internalization" && c.Brands == "Pathhunter" && c.CONFIGURATION == "Cell Pool" && !c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> Total GPCR Internalization Cell Pool";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterGPCRActiveInternalizationCellPool(bool displayPDFLink = false)
        {
            CommonPriceList = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Format == "Active Internalization" && c.Brands == "Pathhunter" && c.CONFIGURATION == "Cell Pool" && !c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> Activated GPCR Internalization Cell Pool";
            this.displayPDFLink = displayPDFLink;
        }

        /*c)	PHARMACOTRAFFICKING ASSAY*/
        public void GetPHARMACOTRAFFICKINGASSAY(bool displayPDFLink = false)
        {
            CommonPriceList = PHARMACOTRAFFICKINGASSAY = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> Pharmacotrafficking Assays";
            this.displayPDFLink = displayPDFLink;
        }
        
        /*d)	PathHunter GPCR Cell-Base Assay-Ready Kits*/
        public void GetcAMPHuntereXpressHumanGPCRAssays(bool displayPDFLink = false)
        {
            CommonPriceList = cAMPHuntereXpressHumanGPCRAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Brands == "cAMP Hunter" && c.Web_Product_Category_Primary == "Assay Ready Kits" && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList().Where(c => c.TARGET != null && c.TARGET.IndexOf("m") != 0 && c.TARGET.IndexOf("r") != 0 && c.TARGET.IndexOf("c") != 0 && c.TARGET.IndexOf("s") != 0).ToList();

            caption = "cAMP Huntere Xpress Human GPCR Assays";
            this.displayPDFLink = displayPDFLink;
            
        }
        public void GetcAMPHuntereXpressOrthologGPCRAssays(bool displayPDFLink = false)
        {
            CommonPriceList = cAMPHuntereXpressOrthologGPCRAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Brands == "cAMP Hunter" && c.Web_Product_Category_Primary == "Assay Ready Kits" && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList().Where(c => c.TARGET != null && (c.TARGET.IndexOf("m") == 0 || c.TARGET.IndexOf("r") == 0 || c.TARGET.IndexOf("c") == 0 || c.TARGET.IndexOf("s") == 0)).ToList();
            caption = "cAMP Hunter eXpress Ortholog GPCR Assays";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetPathHuntereXpressArrestinHumanGPCRAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHuntereXpressArrestinOrthologGPCRAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Readout == "B-Arrestin" && c.Brands == "Pathhunter" && c.Web_Product_Category_Primary == "Assay Ready Kits" && !c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList().Where(c => c.TARGET!=null && c.TARGET.IndexOf("m") != 0 && c.TARGET.IndexOf("r") != 0 && c.TARGET.IndexOf("c") != 0 && c.TARGET.IndexOf("s") != 0).ToList(); ;
            caption = "PathHunter<sup>®</sup> eXpress Human β-Arrestin GPCR Assays";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetPathHuntereXpressArrestinOrthologGPCRAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHuntereXpressArrestinOrthologGPCRAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Readout == "B-Arrestin" && c.Brands == "Pathhunter" && c.Web_Product_Category_Primary == "Assay Ready Kits" && !c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList().Where(c => c.TARGET != null && (c.TARGET.IndexOf("m") == 0 || c.TARGET.IndexOf("r") == 0 || c.TARGET.IndexOf("c") == 0 || c.TARGET.IndexOf("s") == 0)).ToList();
            caption = "PathHunter<sup>®</sup> eXpress Ortholog β-Arrestin GPCR Assays";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetPathHuntereXpressArrestinOrphanGPCRAssays(bool displayPDFLink = false)
        {
            CommonPriceList =PathHuntereXpressArrestinOrphanGPCRAssays= CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Readout == "B-Arrestin" && c.Brands == "Pathhunter" && c.Web_Product_Category_Primary=="Assay Ready Kits" && c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> eXpress Orphan β-Arrestin GPCR Assays";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetPathHuntereXpressGPCRInternalizationAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHuntereXpressGPCRInternalizationAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Readout == "Internalization" && c.Brands == "Pathhunter" && c.Web_Product_Category_Primary=="Assay Ready Kits" && c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> eXpress GPCR Internalization Assays";
            this.displayPDFLink = displayPDFLink;
            
        }

        public void GetPathHunterGPCRTotalInternalizationKits(bool displayPDFLink = false)
        {
            CommonPriceList = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Format == "Total Internalization" && c.Brands == "Pathhunter" && c.Web_Product_Category_Primary=="Assay Ready Kits" && !c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> eXpress Total GPCR Internalization Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterGPCRActiveInternalizationKits(bool displayPDFLink = false)
        {
            CommonPriceList = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Assay_Format == "Active Internalization" && c.Brands == "Pathhunter" && c.Web_Product_Category_Primary=="Assay Ready Kits" && !c.DESCRIPTION.Contains("orphan") && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> eXpress Activated GPCR Internalization Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterGPCRExplorerKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterGPCRExplorerKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Brands == "Pathhunter" && c.Web_Product_Category_Secondary == "Explorer Kit" && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> GPCR Explorer Kit";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetPathHuntereXpressGPCRProfilerAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHuntereXpressGPCRProfilerAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Brands == "Pathhunter" && c.Web_Product_Category_Secondary == "Profiler" && !c.DESCRIPTION.Contains("Pharmacotrafficking")).ToList();
            caption = "PathHunter<sup>®</sup> Xpress GPCR Profiler Kit";
            this.displayPDFLink = displayPDFLink;
        }
        
        /*e)	DIMERIZATION ASSAYS*/
        public void GetPathHunterDimerizationAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterDimerizationAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "Pathhunter" && c.Assay_Type == "Dimerization" && c.Web_Product_Category_Primary == "Stable Cell Lines").ToList();
            caption = "PathHunter<sup>®</sup> Dimerization Assays";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetPathHunterDimerizationAssayReadyKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterDimerizationAssayReadyKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "Pathhunter" && c.Assay_Type == "Dimerization" && c.Web_Product_Category_Primary == "Assay Ready Kits").ToList();
            caption = "PathHunter<sup>®</sup> Dimerization Assay-Ready Kits";
            this.displayPDFLink = displayPDFLink;
        }
        
        /*f)	Kinase Assays*/
        public void GetPathHunterCellBasedKinaseAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterCellBasedKinaseAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "Pathhunter" && c.Assay_Measures.Contains("Receptor") && c.Family.Contains("Receptor Tyrosine") && c.CONFIGURATION == "Clone").ToList();
        }
        public void GetPathHunterCytosolicTyrosineKinaseAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterCytosolicTyrosineKinaseAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "Pathhunter" && c.Assay_Measures.Contains("Receptor") && c.Family.Contains("Cytosolic") && c.CONFIGURATION == "Clone").ToList();
        }
        public void GetPathHunterCellBasedKinaseAssayReadyKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterCellBasedKinaseAssayReadyKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "Pathhunter" && c.Assay_Measures.Contains("Receptor") && c.Family.Contains("Receptor Tyrosine") && c.Web_Product_Category_Primary=="Assay Ready Kits").ToList();
        }
        public void GetPathHunterCytosolicTyrosineKinaseAssayReadyKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterCytosolicTyrosineKinaseAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "Pathhunter" && c.Assay_Measures.Contains("Receptor") && c.Family.Contains("Cytosolic") && c.Web_Product_Category_Primary=="Assay Ready Kits").ToList();
        }
        public void GetADPAccumulationAssays(bool displayPDFLink = false)
        {
            CommonPriceList = ADPAccumulationAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Product_Category_Tertiary == "ADP Accumulation Assays").ToList();
        }
        
        /*g)	PathHunter Nuclear Hormone Receptor & Protease Cell Lines*/
        public void GetPathHunterNuclearHormoneReceptorActivationAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterNuclearHormoneReceptorActivationAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Nuclear Receptor" && c.Brands == "PathHunter" && c.CONFIGURATION == "Clone").ToList();
        }
        public void GetPathHunterNuclearHormoneReceptorAssayReadyKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterNuclearHormoneReceptorAssayReadyKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Nuclear Receptor" && c.Brands == "PathHunter" && c.Web_Product_Category_Primary=="Assay Ready Kits").ToList();
        }
        public void GetPathHunterProteaseAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterProteaseAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Protease" && c.Brands == "PathHunter" && c.CONFIGURATION == "Clone").ToList();
            caption = "PathHunter<sup>®</sup> Protease Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetBiochemicalAssays(bool displayPDFLink = false)
        {
            CommonPriceList = BiochemicalAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Nuclear Receptor" && c.Brands == "HitHunter" && c.CONFIGURATION == "Clone").ToList();
        }

        public void GetNuclearTranslocationAssays(bool displayPDFLink = false)
        {
            CommonPriceList = NuclearTranslocationAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.DESCRIPTION.Contains("Nuclear Translocation") && c.Brands == "PathHunter" && c.CONFIGURATION == "Clone" && c.Web_Target_Class=="Nuclear Receptor").ToList();
            caption = "PathHunter<sup>®</sup> Nuclear Translocation Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetProteinInteractionAssays(bool displayPDFLink = false)
        {
            CommonPriceList = ProteinInteractionAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Assay_Measures.Contains("Protein Interaction") && c.Brands == "PathHunter" && c.CONFIGURATION == "Clone").ToList();
            caption = "PathHunter<sup>®</sup> Protein Interaction Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetNuclearTranslocationKits(bool displayPDFLink = false)
        {
            CommonPriceList = NuclearTranslocationKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.DESCRIPTION.Contains("Nuclear Translocation") && c.Brands == "PathHunter" && c.Web_Product_Category_Primary == "Assay Ready Kits" && c.Web_Target_Class == "Nuclear Receptor").ToList();
            caption = "PathHunter<sup>®</sup> eXpress NHR NuclearTranslocation Assays";
        }
        public void GetProteinInteractionKits(bool displayPDFLink = false)
        {
            CommonPriceList = ProteinInteractionKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Assay_Measures.Contains("Protein Interaction") && c.Brands == "PathHunter" && c.Web_Product_Category_Primary == "Assay Ready Kits").ToList();
            caption = "PathHunter<sup>®</sup> eXpress NHR Protein Interaction Assays";
        }
        
        /*h)	PATHWAY ASSAYS*/
        public void GetPathHunterPathwayAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterPathwayAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Signaling Pathway" && c.Brands == "PathHunter" && c.CONFIGURATION == "Clone").ToList();
            caption = "PathHunter<sup>®</sup> Pathway Assays";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetPathHunterPathwayAssayReadyKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterPathwayAssayReadyKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Signaling Pathway" && c.Brands == "PathHunter" && c.Web_Product_Category_Primary=="Assay Ready Kits").ToList();
            caption = "PathHunter<sup>®</sup> eXpress Pathway Assays";
        }
        
        /*i)	INCELL HUNTER™ ASSAYS */
        public void GetInCELLHunterCompoundBindingAssays(bool displayPDFLink = false)
        {
            CommonPriceList = InCELLHunterCompoundBindingAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Epigenetic" && c.Brands == "InCELL" && c.CONFIGURATION == "Clone").ToList();
        }
        public void GetInCELLHunterProteinBindingAssays(bool displayPDFLink = false)
        {
            CommonPriceList = InCELLHunterProteinBindingAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Assay_Measures == "Intracellular Binding" && c.Brands == "InCELL" && c.CONFIGURATION == "Clone").ToList();
        }
        public void GetInCELLHunterProteinBindingAssayReadyKits(bool displayPDFLink = false)
        {
            CommonPriceList = InCELLHunterProteinBindingAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Assay_Measures == "Intracellular Binding" && c.Brands == "InCELL" && c.Web_Product_Category_Primary=="Assay Ready Kits").ToList();
        }
        public void GetInCELLHunterKinaseBindingAssays(bool displayPDFLink = false)
        {
            CommonPriceList = InCELLHunterKinaseBindingAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "InCELL" && c.CONFIGURATION == "Clone").ToList();
            caption = "InCELL Hunter™ Kinase Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetInCELLHunterKinaseBindingAssayReadyKits(bool displayPDFLink = false)
        {
            CommonPriceList = InCELLHunterKinaseBindingAssayReadyKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "InCELL" && c.Web_Product_Category_Primary=="Assay Ready Kits").ToList();
            caption = "InCELL Hunter™ eXpress Kinase Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterCellBasedRTKActivityAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterCellBasedRTKActivityAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "Pathhunter" && c.Assay_Type == "Activity" && c.Technology == "RTK" && c.CONFIGURATION == "Clone").ToList();
            caption = "PathHunter<sup>®</sup> RTK Activity Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterCellBasedRTKFunctionalAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterCellBasedRTKFunctionalAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "Pathhunter" && c.Assay_Type == "Functional" && c.Technology == "RTK" && c.CONFIGURATION == "Clone").ToList();
            caption = "PathHunter<sup>®</sup> RTK Functional Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterCellBasedCTKActivityAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterCellBasedCTKActivityAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "Pathhunter" && c.Assay_Type == "Activity" && c.Technology == "CTK" && c.CONFIGURATION == "Clone").ToList();
            caption = "PathHunter<sup>®</sup> CTK Activity Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterCellBasedCTKFunctionalAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterCellBasedCTKFunctionalAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "Pathhunter" && c.Assay_Type == "Functional" && c.Technology == "CTK" && c.CONFIGURATION == "Clone").ToList();
            caption = "PathHunter<sup>®</sup> CTK Functional Assays";
            this.displayPDFLink = displayPDFLink;
        }


        public void GetPathHunterCellBasedRTKActivityKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterCellBasedRTKActivityKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "Pathhunter" && c.Assay_Type == "Activity" && c.Technology == "RTK" && c.Web_Product_Category_Primary == "Assay Ready Kits").ToList();
            caption = "PathHunter<sup>®</sup> eXpress RTK Activity Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterCellBasedRTKFunctionalKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterCellBasedRTKFunctionalKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "Pathhunter" && c.Assay_Type == "Functional" && c.Technology == "RTK" && c.Web_Product_Category_Primary == "Assay Ready Kits").ToList();
            caption = "PathHunter<sup>®</sup> eXpress RTK Functional Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterCellBasedCTKActivityKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterCellBasedCTKActivityKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "Pathhunter" && c.Assay_Type == "Activity" && c.Technology == "CTK" && c.Web_Product_Category_Primary == "Assay Ready Kits").ToList();
            caption = "PathHunter<sup>®</sup> eXpress CTK Activity Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathHunterCellBasedCTKFunctionalKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterCellBasedCTKFunctionalKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Kinase" && c.Brands == "Pathhunter" && c.Assay_Type == "Functional" && c.Technology == "CTK" && c.Web_Product_Category_Primary == "Assay Ready Kits").ToList();
            caption = "PathHunter<sup>®</sup> eXpress CTK Functional Assays";
            this.displayPDFLink = displayPDFLink;
        }



        public void GetInCELLHunterBromodomainBindingAssays(bool displayPDFLink = false)
        {
            CommonPriceList = InCELLHunterBromodomainBindingAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Epigenetic" && c.Brands == "InCELL" && c.CONFIGURATION == "Clone" && !c.DESCRIPTION.Contains("Methyltransferase")).ToList();
            caption = "InCELL Hunter™ Bromodomain Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetInCELLHunterMethyltransferaseBindingAssays(bool displayPDFLink = false)
        {
            CommonPriceList = InCELLHunterMethyltransferaseBindingAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Epigenetic" && c.Brands == "InCELL" && c.CONFIGURATION == "Clone" && c.DESCRIPTION.Contains("Methyltransferase")).ToList();
            caption = "InCELL Hunter™ Methyltransferase Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetInCellPathwayAssays(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterPathwayAssays = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Signaling Pathway" && c.Brands == "InCELL" && c.CONFIGURATION == "Clone").ToList();
            caption = "InCELL Hunter™ Pathway Cell Lines";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetTargetEngagementAssaySystemKit(bool displayPDFLink = false)
        {
            CommonPriceList = TargetEngagementAssaySystemKit = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "InCELL" && c.DESCRIPTION.Contains("Target Engagement Assay System") && c.CONFIGURATION == "kit").ToList();
            caption = "InCELL Hunter™ Target Engagement Assay System Kit";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetTargetEngagementDetectionKit(bool displayPDFLink = false)
        {
            CommonPriceList = TargetEngagementAssaySystemKit = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "InCELL" && c.DESCRIPTION.Contains("Target Engagement Detection") && c.CONFIGURATION == "kit").ToList();
            caption = "InCELL Hunter™ Target Engagement Assay (TEA) Detection Kit";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetInCELLHunterBromodomainBindingKits(bool displayPDFLink = false)
        {
            CommonPriceList = InCELLHunterBromodomainBindingKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Epigenetic" && c.Brands == "InCELL" && c.Web_Product_Category_Primary == "Assay Ready Kits" && !c.DESCRIPTION.Contains("Methyltransferase")).ToList();
            caption = "InCELL Hunter™ eXpress Bromodomain Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetInCELLHunterMethyltransferaseBindingKits(bool displayPDFLink = false)
        {
            CommonPriceList = InCELLHunterMethyltransferaseBindingKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Epigenetic" && c.Brands == "InCELL" && c.Web_Product_Category_Primary == "Assay Ready Kits" && c.DESCRIPTION.Contains("Methyltransferase")).ToList();
            caption = "InCELL Hunter™ eXpress Histone Methyltransferase Assays";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetInCellPathwayKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathHunterPathwayKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "Signaling Pathway" && c.Brands == "InCELL" && c.Web_Product_Category_Primary == "Assay Ready Kits").ToList();
            caption = "InCELL Hunter™ eXpress Pathway Assays";
            this.displayPDFLink = displayPDFLink;
        }
        
        
        
        /*j)	AB HUNTER™ ASSAYS */
        public void GetAbHunterAntiReceptorGPCRFunctionalAssayKits(bool displayPDFLink = false)
        {
            CommonPriceList = AbHunterAntiReceptorGPCRFunctionalAssayKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Brands == "Ab Hunter" && c.Assay_Type == "Functional" && c.Web_Product_Category_Primary=="Assay Ready Kits").ToList();
            caption = "Ab Hunter™ GPCR Functional Kits";
            this.displayPDFLink = displayPDFLink;

        }
        public void GetAbHunterAntiReceptorRTKFunctionalAssayKits(bool displayPDFLink = false)
        {
            CommonPriceList = AbHunterAntiReceptorRTKFunctionalAssayKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Technology == "RTK" && c.Brands == "Ab Hunter" && c.Assay_Type == "Functional" && c.Web_Product_Category_Primary=="Assay Ready Kits").ToList();
            caption = "Ab Hunter™ RTK Functional Kits";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetAbHunterAntiReceptorGPCRAntibodyBindingKits(bool displayPDFLink = false)
        {
            CommonPriceList = AbHunterAntiReceptorGPCRAntibodyBindingKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Web_Target_Class == "GPCR" && c.Brands == "Ab Hunter" && c.Web_Product_Category_Tertiary == "Antibody Binding Kits" && c.Web_Product_Category_Primary=="Assay Ready Kits").ToList();
            caption = "Ab Hunter™ GPCR Antibody Binding Kits";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetAbHunterAntiReceptorRTKAntibodyBindingKits(bool displayPDFLink = false)
        {
            CommonPriceList = AbHunterAntiReceptorRTKAntibodyBindingKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Technology == "RTK" && c.Brands == "Ab Hunter" && c.Web_Product_Category_Tertiary == "Antibody Binding Kits" && c.Web_Product_Category_Primary=="Assay Ready Kits").ToList();
            caption = "Ab Hunter™ RTK Antibody Binding Kits";
            this.displayPDFLink = displayPDFLink;
        }
        
        /*k)	CERTIFIED REAGENTS */
        public void GetDetectionReagents(bool displayPDFLink = false)
        {
            CommonPriceList = DetectionReagents = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "PathHunter" && c.Web_Product_Category_Primary=="Detection Reagents").ToList();
        }

        public void GetPathhunterDetectionKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathhunterDetectionKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "PathHunter" && c.Web_Product_Category_Primary == "Detection Reagents" && c.DESCRIPTION == "PathHunter<sup>®</sup> Detection Kit").ToList();
            caption = "PathHunter<sup>®</sup> Detection Kits";
            this.displayPDFLink = displayPDFLink;
        }

        public void GetPathhunterFlashDetectionKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathhunterFlashDetectionKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "PathHunter" && c.Web_Product_Category_Primary == "Detection Reagents" && c.DESCRIPTION.Contains("Flash Detection Kit")).ToList();
            caption = "PathHunter<sup>®</sup> Flash Detection Kits";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetPathhunterBioassayDetectionKits(bool displayPDFLink = false)
        {
            CommonPriceList = PathhunterBioassayDetectionKits = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "PathHunter" && c.Web_Product_Category_Primary == "Detection Reagents" && c.DESCRIPTION.Contains("Bioassay Detection Kit")).ToList();
            caption = "PathHunter<sup>®</sup> Bioassay Detection Kits";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetInCellhunterDetectionKitII(bool displayPDFLink = false)
        {
            CommonPriceList = InCellhunterDetectionKitII = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "InCELL" && c.Web_Product_Category_Primary == "Detection Reagents" && c.DESCRIPTION.Contains("InCELL Hunter™ Detection Kit II")).ToList();
            caption = "InCELL Hunter™ Detection Kit II (Kinase)";
            this.displayPDFLink = displayPDFLink;
        }


        public void GetInCellhunterDetectionKit(bool displayPDFLink = false)
        {
            CommonPriceList = InCellhunterDetectionKit = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "InCELL" && c.Web_Product_Category_Primary == "Detection Reagents" && c.DESCRIPTION.Contains("InCELL Hunter™ Detection Kit") && !c.DESCRIPTION.Contains("InCELL Hunter™ Detection Kit II")).ToList();
            caption = "InCELL Hunter™ Detection Kit";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetCytoTracker(bool displayPDFLink = false)
        {
            CommonPriceList = CytoTracker = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "CytoTracker").ToList();
            caption = "CytoTracker™";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetControlLigands(bool displayPDFLink = false)
        {
            CommonPriceList = ControlLigands = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.PartNumber.Contains("92-") && c.Web_Product_Category_Primary == "Ligands & Inhibitors").ToList();
            caption = "Control Ligands";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetAssayCompleteCellPlatingReagents(bool displayPDFLink = false)
        {
            CommonPriceList = AssayCompleteCellPlatingReagents = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "AssayComplete" && c.Web_Product_Category_Secondary == "Cell Plating Reagents" && c.Web_Product_Category_Tertiary == "AssayComplete Cell Plating Reagent").ToList();
            caption = "Cell Plating Reagents";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetAssayCompleteReviveMedia(bool displayPDFLink = false)
        {
            CommonPriceList = AssayCompleteReviveMedia = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "AssayComplete" && c.Web_Product_Category_Secondary == "Freezing & Preservation Reagents" && c.Web_Product_Category_Tertiary == "Revive").ToList();
            caption = "Revive™ Media";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetAssayCompletePreserveFreezingReagent(bool displayPDFLink = false)
        {
            CommonPriceList = AssayCompletePreserveFreezingReagent = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "AssayComplete" && c.Web_Product_Category_Secondary == "Freezing & Preservation Reagents" && c.Web_Product_Category_Tertiary=="Preserve").ToList();
            caption = "Preserve™ Freezing Reagent";
            this.displayPDFLink = displayPDFLink;     
        }
        public void GetAssayCompleteCellCultureKit(bool displayPDFLink = false)
        {
            CommonPriceList = AssayCompleteCellCultureKit = CBIS_DxFremontProductInfo_ctx.vw_TPL_ProductPrice.Where(c => c.Brands == "AssayComplete" && c.Web_Product_Category_Secondary=="Cell Culture Kits").ToList();
            caption = "Cell Culture Kit";
            this.displayPDFLink = displayPDFLink;
        }
        public void GetAdditionalSynergyProducts(bool displayPDFLink = false)
        {
        }

         internal void DownloadPDFWholePriceList()
        {
            viewList = new List<ViewItem>() { new ViewItem(){viewName="PathHunterArrestinHumanGPCRCellLines", display=true }, 
                                              new ViewItem(){viewName="PathHunterArrestinOrthologGPCRCellLines", display=true },
                                              new ViewItem(){viewName="PathHunterArrestinOrphanGPCRCellLines", display=true },
                                              new ViewItem(){viewName="PathHunterGPCRTotalInternalizationCellLines", display=true },
                                              new ViewItem(){viewName="PathHunterGPCRActiveInternalizationCellLines", display=true },
                                              new ViewItem(){viewName="PathHunterDimerizationAssays", display=true },
                                              new ViewItem(){viewName="PathHunterArrestinOrthologGPCRCellPool", display=true },
                                              new ViewItem(){viewName="PathHunterGPCRTotalInternalizationCellPool",display=true },
                                              new ViewItem(){viewName="PathHunterGPCRActiveInternalizationCellPool", display=true },
                                              new ViewItem(){viewName="PHARMACOTRAFFICKINGASSAY", display=true },
                                              new ViewItem(){viewName="cAMPHunterHumanGPCRCellLines", display=true },
                                              new ViewItem(){viewName="cAMPHunterOrthologGPCRCellLines",display=true },
                                              new ViewItem(){viewName="NuclearTranslocationAssays", display=true },
                                              new ViewItem(){viewName="ProteinInteractionAssays", display=true },
                                              new ViewItem(){viewName="PathHunterProteaseAssays",display=true },
                                              new ViewItem(){viewName="PathHunterCellBasedRTKActivityAssays",display=true },
            
                                              new ViewItem(){viewName="PathHunterCellBasedRTKFunctionalAssays", display=true }, 
                                              new ViewItem(){viewName="PathHunterCellBasedCTKActivityAssays", display=true },
                                              new ViewItem(){viewName="PathHunterCellBasedCTKFunctionalAssays", display=true },
                                              new ViewItem(){viewName="PathHunterPathwayAssays", display=true },
                                              new ViewItem(){viewName="InCELLHunterBromodomainBindingAssays", display=true },
                                              new ViewItem(){viewName="InCELLHunterMethyltransferaseBindingAssays", display=true },
                                              new ViewItem(){viewName="InCELLHunterKinaseBindingAssays", display=true },
                                              new ViewItem(){viewName="InCellPathwayAssays",display=true },
                                              new ViewItem(){viewName="TargetEngagementAssaySystemKit", display=true },
                                              new ViewItem(){viewName="PathHuntereXpressArrestinHumanGPCRAssays", display=true },
                                              new ViewItem(){viewName="PathHuntereXpressArrestinOrthologGPCRAssays", display=true },
                                              new ViewItem(){viewName="PathHuntereXpressArrestinOrphanGPCRAssays",display=true },
                                              new ViewItem(){viewName="PathHunterGPCRTotalInternalizationKits", display=true },
                                              new ViewItem(){viewName="PathHunterGPCRActiveInternalizationKits", display=true },
                                              new ViewItem(){viewName="PathHunterGPCRExplorerKits",display=true },
                                              new ViewItem(){viewName="PathHunterDimerizationAssayReadyKits",display=true },

                                              new ViewItem(){viewName="cAMPHuntereXpressGPCRAssays", display=true }, 
                                              new ViewItem(){viewName="PathHuntereXpressGPCRProfilerAssays", display=true },
                                              new ViewItem(){viewName="PathHunterCellBasedRTKActivityKits", display=true },
                                              new ViewItem(){viewName="PathHunterCellBasedRTKFunctionalKits", display=true },
                                              new ViewItem(){viewName="PathHunterCellBasedCTKActivityKits", display=true },
                                              new ViewItem(){viewName="PathHunterCellBasedCTKFunctionalKits", display=true },
                                              new ViewItem(){viewName="ProteinInteractionKits", display=true },
                                              new ViewItem(){viewName="NuclearTranslocationKits",display=true },
                                              new ViewItem(){viewName="PathHunterPathwayAssayReadyKits", display=true },
                                              new ViewItem(){viewName="InCELLHunterBromodomainBindingKits", display=true },
                                              new ViewItem(){viewName="InCELLHunterMethyltransferaseBindingKits", display=true },
                                              new ViewItem(){viewName="InCellPathwayKits",display=true },
                                              new ViewItem(){viewName="InCELLHunterKinaseBindingAssayReadyKits", display=true },
                                              new ViewItem(){viewName="AbHunterAntiReceptorGPCRFunctionalAssayKits", display=true },
                                              new ViewItem(){viewName="AbHunterAntiReceptorRTKFunctionalAssayKits",display=true },
                                              new ViewItem(){viewName="AbHunterAntiReceptorGPCRAntibodyBindingKits",display=true },

                                              new ViewItem(){viewName="AbHunterAntiReceptorRTKAntibodyBindingKits", display=true }, 
                                              new ViewItem(){viewName="PathhunterDetectionKits", display=true },
                                              new ViewItem(){viewName="PathhunterFlashDetectionKits", display=true },
                                              new ViewItem(){viewName="PathhunterBioassayDetectionKits", display=true },
                                              new ViewItem(){viewName="InCellhunterDetectionKitII", display=true },
                                              new ViewItem(){viewName="InCellhunterDetectionKit", display=true },

                                              new ViewItem(){viewName="HitHuntercAMPSmallMolecules", display=true },
                                              new ViewItem(){viewName="HitHuntercAMPXBiologics",display=true },
                                             
                                              new ViewItem(){viewName="CalciumNoWashPLUSAssays", display=true },
                                              new ViewItem(){viewName="AssayCompleteCellCultureKit",display=true },
                                              new ViewItem(){viewName="AssayCompleteReviveMedia", display=true },
                                              new ViewItem(){viewName="AssayCompletePreserveFreezingReagent", display=true },
                                              new ViewItem(){viewName="AssayCompleteCellCultureKit",display=true },
                                              new ViewItem(){viewName="ControlLigands",display=true },
                                              new ViewItem(){viewName="TargetEngagementDetectionKit",display=true }
            
            };

            pdfModel.DownloadViewsToSinglePDFFile(viewList, "DRx_Price_List_");
        }

       
    }
}