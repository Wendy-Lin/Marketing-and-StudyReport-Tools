using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Discoverx.Models;
using Discoverx.Models.TargetPriceList;

namespace Discoverx.Controllers
{
    public class PriceListController : BaseController
    {
        PriceListViewModel priceListViewModel;

        public PriceListController()
        {
            this.priceListViewModel = new PriceListViewModel();
        }

        // GET: PriceList
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPDF()
        {
            return View();
        }

       

        public ActionResult SubPriceList(PriceListViewModel priceListViewModel)
        {
            return View(priceListViewModel);
        }

        public void ViewPdf(string pageTitle, string viewName, string fileName)
        {
            
            if (viewName == "SubPriceList")
            {
                switch (fileName)
                {
                    case "PathHunter<sup>®</sup> β-Arrestin Human GPCR Cell Lines":
                        priceListViewModel.GetPathHunterArrestinHumanGPCRCellLines();
                        break;
                    case "PathHunter<sup>®</sup> β-Arrestin Ortholog GPCR Cell Lines":
                        priceListViewModel.GetPathHunterArrestinOrthologGPCRCellLines();
                        break;
                    case "PathHunter<sup>®</sup> β-Arrestin Orphan GPCR Cell Lines":
                        priceListViewModel.GetPathHunterArrestinOrphanGPCRCellLines();
                        break;
                    case "PathHunter<sup>®</sup> Total GPCR Internalization Cell Lines":
                        priceListViewModel.GetPathHunterGPCRTotalInternalizationCellLines();
                        break;
                    case "PathHunter<sup>®</sup> Activated GPCR Internalization Cell Lines":
                        priceListViewModel.GetPathHunterGPCRActiveInternalizationCellLines();
                        break;
                    case "PathHunter<sup>®</sup> Dimerization Assays":
                        priceListViewModel.GetPathHunterDimerizationAssays();
                        break;
                    case "PathHunter<sup>®</sup> β-Arrestin Ortholog GPCR Cell Pool":
                        priceListViewModel.GetPathHunterArrestinOrthologGPCRCellPool();
                        break;
                    case "PathHunter<sup>®</sup> Total GPCR Internalization Cell Pool":
                        priceListViewModel.GetPathHunterGPCRTotalInternalizationCellPool();
                        break;
                    case "PathHunter<sup>®</sup> Activated GPCR Internalization Cell Pool":
                        priceListViewModel.GetPathHunterGPCRActiveInternalizationCellPool();
                        break;
                    case "PathHunter<sup>®</sup> Pharmacotrafficking Assays":
                        priceListViewModel.GetPHARMACOTRAFFICKINGASSAY();
                        break;
                    case "cAMP Hunter™ Human GPCR Cell Lines":
                        priceListViewModel.GetcAMPHunterHumanGPCRCellLines();
                        break;
                    case "cAMP Hunter™ Ortholog GPCR Cell Lines":
                        priceListViewModel.GetcAMPHunterOrthologGPCRCellLines();
                        break;
                    case "Native Calcium Cell Lines":
                        priceListViewModel.GetNativeCalciumCellLines();
                        break;
                    case "PathHunter<sup>®</sup> Nuclear Translocation Cell Lines":
                        priceListViewModel.GetNuclearTranslocationAssays();
                        break;
                    case "PathHunter<sup>®</sup> Protein Interaction Cell Lines":
                        priceListViewModel.GetProteinInteractionAssays();
                        break;
                    case "PathHunter<sup>®</sup> Protease Cell Lines":
                        priceListViewModel.GetPathHunterProteaseAssays();
                        break;
                    case "PathHunter<sup>®</sup> RTK Activity Assays":
                        priceListViewModel.GetPathHunterCellBasedRTKActivityAssays();
                        break;
                    case "PathHunter<sup>®</sup> RTK Functional Assays":
                        priceListViewModel.GetPathHunterCellBasedRTKFunctionalAssays();
                        break;
                    case "PathHunter<sup>®</sup> CTK Activity Assays":
                        priceListViewModel.GetPathHunterCellBasedCTKActivityAssays();
                        break;
                    case "PathHunter<sup>®</sup> CTK Functional Assays":
                        priceListViewModel.GetPathHunterCellBasedCTKFunctionalAssays();
                        break;
                    case "PathHunter<sup>®</sup> Pathway Assays":
                        priceListViewModel.GetPathHunterPathwayAssays();
                        break;
                    case "InCELL Hunter™ Bromodomain Cell Lines":
                        priceListViewModel.GetInCELLHunterBromodomainBindingAssays();
                        break;
                    case "InCELL Hunter™ Methyltransferase Cell Lines":
                        priceListViewModel.GetInCELLHunterMethyltransferaseBindingAssays();
                        break;
                    case "InCELL Hunter™ Kinase Cell Lines":
                        priceListViewModel.GetInCELLHunterKinaseBindingAssays();
                        break;
                    case "InCELL Hunter™ Pathway Cell Lines":
                        priceListViewModel.GetInCellPathwayAssays();
                        break;
                    case "InCELL Hunter™ Target Engagement Assay System Kit":
                        priceListViewModel.GetTargetEngagementAssaySystemKit();
                        break;
                    case "PathHunter<sup>®</sup> eXpress Human β-Arrestin GPCR Assays":
                        priceListViewModel.GetPathHuntereXpressArrestinHumanGPCRAssays();
                        break;
                    case "PathHunter<sup>®</sup> eXpress Ortholog β-Arrestin GPCR Assays":
                        priceListViewModel.GetPathHuntereXpressArrestinOrthologGPCRAssays();
                        break;
                    case "PathHunter<sup>®</sup> eXpress Orphan β-Arrestin GPCR Assays":
                        priceListViewModel.GetPathHuntereXpressArrestinOrphanGPCRAssays();
                        break;
                    case "PathHunter<sup>®</sup> eXpress Total GPCR Internalization Assays":
                        priceListViewModel.GetPathHunterGPCRTotalInternalizationKits();
                        break;
                    case "PathHunter<sup>®</sup> eXpress Activated GPCR Internalization Assays":
                        priceListViewModel.GetPathHunterGPCRActiveInternalizationKits();
                        break;
                    case "PathHunter<sup>®</sup> GPCR Explorer Kit":
                        priceListViewModel.GetPathHunterGPCRExplorerKits();
                        break;
                    case "PathHunter<sup>®</sup> Dimerization Assay-Ready Kits":
                        priceListViewModel.GetPathHunterDimerizationAssayReadyKits();
                        break;
                    case "cAMP Hunter™ eXpress GPCR Assays":
                        priceListViewModel.GetcAMPHuntereXpressGPCRAssays();
                        break;
                    case "PathHunter<sup>®</sup> Xpress GPCR Profiler Kit":
                        priceListViewModel.GetPathHuntereXpressGPCRProfilerAssays();
                        break;
                    case "PathHunter<sup>®</sup> eXpress RTK Activity Assays":
                        priceListViewModel.GetPathHunterCellBasedRTKActivityKits();
                        break;
                    case "PathHunter<sup>®</sup> eXpress RTK Functional Assays":
                        priceListViewModel.GetPathHunterCellBasedRTKFunctionalKits();
                        break;
                    case "PathHunter<sup>®</sup> eXpress CTK Activity Assays":
                        priceListViewModel.GetPathHunterCellBasedCTKActivityKits();
                        break;
                    case "PathHunter<sup>®</sup> eXpress CTK Functional Assays":
                        priceListViewModel.GetPathHunterCellBasedCTKFunctionalKits();
                        break;
                    case "PathHunter<sup>®</sup> eXpress NHR Protein Interaction Assays":
                        priceListViewModel.GetProteinInteractionKits();
                        break;
                    case "PathHunter<sup>®</sup> eXpress NHR NuclearTranslocation Assays":
                        priceListViewModel.GetNuclearTranslocationKits();
                        break;
                    case "PathHunter<sup>®</sup> eXpress Pathway Assays":
                        priceListViewModel.GetPathHunterPathwayAssayReadyKits();
                        break;
                    case "InCELL Hunter™ eXpress Bromodomain Assays":
                        priceListViewModel.GetInCELLHunterBromodomainBindingKits();
                        break;
                    case "InCELL Hunter™ eXpress Histone Methyltransferase Assays":
                        priceListViewModel.GetInCELLHunterMethyltransferaseBindingKits();
                        break;
                    case "InCELL Hunter™ eXpress Pathway Assays":
                        priceListViewModel.GetInCellPathwayKits();
                        break;
                    case "InCELL Hunter™ eXpress Kinase Assays":
                        priceListViewModel.GetInCELLHunterKinaseBindingAssayReadyKits();
                        break;
                    case "Ab Hunter™ GPCR Functional Kits":
                        priceListViewModel.GetAbHunterAntiReceptorGPCRFunctionalAssayKits();
                        break;
                    case "Ab Hunter™ RTK Functional Kits":
                        priceListViewModel.GetAbHunterAntiReceptorRTKFunctionalAssayKits();
                        break;
                    case "Ab Hunter™ GPCR Antibody Binding Kits":
                        priceListViewModel.GetAbHunterAntiReceptorGPCRAntibodyBindingKits();
                        break;
                    case "Ab Hunter™ RTK Antibody Binding Kits":
                        priceListViewModel.GetAbHunterAntiReceptorRTKAntibodyBindingKits();
                        break;
                    case "PathHunter<sup>®</sup> Detection Kits":
                        priceListViewModel.GetPathhunterDetectionKits();
                        break;
                    case "PathHunter<sup>®</sup> Flash Detection Kits":
                        priceListViewModel.GetPathhunterFlashDetectionKits();
                        break;
                    case "PathHunter<sup>®</sup> Bioassay Detection Kits":
                        priceListViewModel.GetPathhunterBioassayDetectionKits();
                        break;
                    case "InCELL Hunter™ Detection Kit II (Kinase)":
                        priceListViewModel.GetInCellhunterDetectionKitII();
                        break;
                    case "InCELL Hunter™ Detection Kit":
                        priceListViewModel.GetInCellhunterDetectionKit();
                        break;
                    case "HitHunter<sup>®</sup> cAMP for Small Molecules":
                        priceListViewModel.GetHitHuntercAMPSmallMolecules();
                        break;
                    case "HitHunter<sup>®</sup> cAMP for Biologics":
                        priceListViewModel.GetHitHuntercAMPBiologics();
                        break;
                    case "HitHunter<sup>®</sup> cAMP Mem Assays":
                        priceListViewModel.GetHitHuntercAMPMemAssays();
                        break;
                    case "HitHunter<sup>®</sup> cAMP HS+ Assays":
                        priceListViewModel.GetHitHuntercAMPHSPlusAssays();
                        break;
                    case "Calcium  No WashPLUS Assays":
                        priceListViewModel.GetCalciumNoWashPLUSAssays();
                        break;
                    case "Cell Plating Reagents":
                        priceListViewModel.GetAssayCompleteCellCultureKit();
                        break;
                    case "Revive™ Media":
                        priceListViewModel.GetAssayCompleteReviveMedia();
                        break;
                    case "Preserve™ Freezing Reagent":
                        priceListViewModel.GetAssayCompletePreserveFreezingReagent();
                        break;
                    case "Cell Culture Kit":
                        priceListViewModel.GetAssayCompleteCellCultureKit();
                        break;
                    case "Control Ligands":
                        priceListViewModel.GetControlLigands();
                        break;
                    case "CytoTracker™":
                        priceListViewModel.GetCytoTracker();
                        break;
                    case "InCELL Hunter™ Target Engagement Assay (TEA) Detection Kit":
                        priceListViewModel.GetTargetEngagementDetectionKit();
                        break;
                    case "New Product List":
                        priceListViewModel.GetNewProducts();
                        break;
                    default:
                        break;
                }
                
                
            }
            priceListViewModel.pdfModel.DownloadPDFSingleView(this, viewName, priceListViewModel, fileName, "PriceList");
      
        }

        public ActionResult NewProducts(bool displayPDFLink=false)
        {

            priceListViewModel.GetNewProducts(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        /*	Second Messenger Assays*/

        public ActionResult HitHuntercAMPAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetHitHuntercAMPAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult cAMPHunterHumanGPCRCellLines(bool displayPDFLink = false)
        {
            priceListViewModel.GetcAMPHunterHumanGPCRCellLines(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult cAMPHunterOrthologGPCRCellLines(bool displayPDFLink = false)
        {
            priceListViewModel.GetcAMPHunterOrthologGPCRCellLines(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult cAMPHuntereGPCRKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetcAMPHuntereXpressGPCRAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult NativeCalciumCellLines(bool displayPDFLink = false)
        {
            priceListViewModel.GetNativeCalciumCellLines(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult HitHunterIP3Assays(bool displayPDFLink = false)
        {
            priceListViewModel.GetHitHunterIP3Assays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult HitHuntercGMPAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetHitHuntercGMPAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }


        public ActionResult PathHunterArrestinHumanGPCRCellLines(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterArrestinHumanGPCRCellLines(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterArrestinOrthologGPCRCellLines(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterArrestinOrthologGPCRCellLines(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterArrestinOrphanGPCRCellLines(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterArrestinOrphanGPCRCellLines(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterGPCRInternalizationCellLines(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterGPCRInternalizationCellLines(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterGPCRTotalInternalizationCellLines(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterGPCRTotalInternalizationCellLines(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterGPCRActiveInternalizationCellLines(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterGPCRActiveInternalizationCellLines(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterDimerizationAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterDimerizationAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterArrestinOrthologGPCRCellPool(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterArrestinOrthologGPCRCellPool(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult PathHunterGPCRTotalInternalizationCellPool(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterGPCRTotalInternalizationCellPool(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterGPCRActiveInternalizationCellPool(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterGPCRActiveInternalizationCellPool(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PHARMACOTRAFFICKINGASSAY(bool displayPDFLink = false)
        {
            priceListViewModel.GetPHARMACOTRAFFICKINGASSAY(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterCellBasedRTKActivityAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterCellBasedRTKActivityAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult PathHunterCellBasedRTKFunctionalAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterCellBasedRTKFunctionalAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult PathHunterCellBasedCTKActivityAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterCellBasedCTKActivityAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult PathHunterCellBasedCTKFunctionalAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterCellBasedCTKFunctionalAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterCellBasedRTKActivityKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterCellBasedRTKActivityKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult PathHunterCellBasedRTKFunctionalKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterCellBasedRTKFunctionalKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult PathHunterCellBasedCTKActivityKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterCellBasedCTKActivityKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult PathHunterCellBasedCTKFunctionalKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterCellBasedCTKFunctionalKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult PathHunterProteaseAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterProteaseAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }


        public ActionResult InCELLHunterBromodomainBindingAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetInCELLHunterBromodomainBindingAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult InCELLHunterMethyltransferaseBindingAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetInCELLHunterMethyltransferaseBindingAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult InCELLHunterKinaseBindingAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetInCELLHunterKinaseBindingAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult InCellPathwayAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetInCellPathwayAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult InCELLHunterBromodomainBindingKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetInCELLHunterBromodomainBindingKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult InCELLHunterMethyltransferaseBindingKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetInCELLHunterMethyltransferaseBindingKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult InCELLHunterKinaseBindingAssayReadyKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetInCELLHunterKinaseBindingAssayReadyKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult InCellPathwayKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetInCellPathwayKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult TargetEngagementAssaySystemKit(bool displayPDFLink = false)
        {
            priceListViewModel.GetTargetEngagementAssaySystemKit(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult NuclearTranslocationAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetNuclearTranslocationAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult ProteinInteractionAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetProteinInteractionAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult NuclearTranslocationKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetNuclearTranslocationKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult ProteinInteractionKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetProteinInteractionKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHuntereXpressArrestinHumanGPCRAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHuntereXpressArrestinHumanGPCRAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHuntereXpressArrestinOrthologGPCRAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHuntereXpressArrestinOrthologGPCRAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHuntereXpressArrestinOrphanGPCRAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHuntereXpressArrestinOrphanGPCRAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterGPCRTotalInternalizationKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterGPCRTotalInternalizationKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterGPCRActiveInternalizationKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterGPCRActiveInternalizationKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterGPCRExplorerKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterGPCRExplorerKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult PathHunterDimerizationAssayReadyKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterDimerizationAssayReadyKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHuntereXpressGPCRProfilerAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHuntereXpressGPCRProfilerAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult PathHunterPathwayAssayReadyKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterPathwayAssayReadyKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathHunterPathwayAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathHunterPathwayAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }


        public ActionResult AbHunterAntiReceptorGPCRFunctionalAssayKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetAbHunterAntiReceptorGPCRFunctionalAssayKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult AbHunterAntiReceptorRTKFunctionalAssayKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetAbHunterAntiReceptorRTKFunctionalAssayKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult AbHunterAntiReceptorRTKAntibodyBindingKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetAbHunterAntiReceptorRTKAntibodyBindingKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult AbHunterAntiReceptorGPCRAntibodyBindingKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetAbHunterAntiReceptorGPCRAntibodyBindingKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }



        public ActionResult PathhunterDetectionKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathhunterDetectionKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathhunterBioassayDetectionKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathhunterBioassayDetectionKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult InCellhunterDetectionKitII(bool displayPDFLink = false)
        {
            priceListViewModel.GetInCellhunterDetectionKitII(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult InCellhunterDetectionKit(bool displayPDFLink = false)
        {
            priceListViewModel.GetInCellhunterDetectionKit(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult PathhunterFlashDetectionKits(bool displayPDFLink = false)
        {
            priceListViewModel.GetPathhunterFlashDetectionKits(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }


        public ActionResult DetectionReagents(bool displayPDFLink = false)
        {
            priceListViewModel.GetDetectionReagents(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult CalciumAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetCalciumAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult AssayCompleteCellPlatingReagents(bool displayPDFLink = false)
        {
            priceListViewModel.GetAssayCompleteCellPlatingReagents(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult AssayCompleteReviveMedia(bool displayPDFLink = false)
        {
            priceListViewModel.GetAssayCompleteReviveMedia(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult AssayCompletePreserveFreezingReagent(bool displayPDFLink = false)
        {
            priceListViewModel.GetAssayCompletePreserveFreezingReagent(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult AssayCompleteCellCultureKit(bool displayPDFLink = false)
        {
            priceListViewModel.GetAssayCompleteCellCultureKit(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult ControlLigands(bool displayPDFLink = false)
        {
            priceListViewModel.GetControlLigands(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult CytoTracker(bool displayPDFLink = false)
        {
            priceListViewModel.GetCytoTracker(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult HitHuntercAMPSmallMolecules(bool displayPDFLink = false)
        {
            priceListViewModel.GetHitHuntercAMPSmallMolecules(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public ActionResult HitHuntercAMPXBiologics(bool displayPDFLink = false)
        {
            priceListViewModel.GetHitHuntercAMPBiologics(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

       
        public ActionResult CalciumNoWashPLUSAssays(bool displayPDFLink = false)
        {
            priceListViewModel.GetCalciumNoWashPLUSAssays(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }
        public ActionResult TargetEngagementDetectionKit(bool displayPDFLink = false)
        {
            priceListViewModel.GetTargetEngagementDetectionKit(displayPDFLink);
            return View("SubPriceList", priceListViewModel);
        }

        public void DownloadWholePriceList(bool displayPDFLink = false)
        {
            Cursor.Current = Cursors.WaitCursor;
            priceListViewModel.DownloadPDFWholePriceList();
        }
        
    }
}