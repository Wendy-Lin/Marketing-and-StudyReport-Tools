using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Drawing;

using OfficeOpenXml;
using OfficeOpenXml.Style;

using Discoverx.Models.CBIS_DB;
using Discoverx.Models;
using System.Reflection;

namespace Discoverx.Models.TargetPriceList
{
    public class ExcelTargetListModel : ExcelWorkSheetModel
    {
        internal readonly string[] TargetSheetNameList = new string[] { "KnownGPCR", "OrphanGPCR", "Dimerization", "NHR", "CellBasedKinase", "Pathway", "InCellHunter", "Pharmacotrafficking", "Synergy" };
        
        internal void DownloadExcelWholeTragetList()
        {
            using (ExcelPackage pkg = new ExcelPackage())
            {
                //Here setting some document properties
                pkg.Workbook.Properties.Author = "discoverx.com";
                pkg.Workbook.Properties.Title = "Target List";

                SetWorkSheetProperties();
                int headerRowNumber;
                foreach (WorkSheetProperty property in workSheetPropertyList)
                {
                    headerRowNumber=AddWorkSheet(pkg, property, "Verdana", 9);
                    FillWorkSheetData(ws, property, (headerRowNumber + 1));
                }

                string fileName = string.Format("DRx_Targets_Q{0}_{1}{2}", DateTime.Now.GetQuarter(), DateTime.Now.GetTwoDigitMonth(), DateTime.Now.GetTwoDigitYear());
                HttpContext.Current.Response.BinaryWrite(pkg.GetAsByteArray());
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename="+fileName+".xlsx");
            }


        }

        private void SetWorkSheetProperties()
        {
            workSheetPropertyList = new List<WorkSheetProperty>();

            for (int i = 0; i < TargetSheetNameList.Length; i++ )
            {
                SetWorkSheetProperty(TargetSheetNameList[i], i+1);
            }
           
        }

        private void SetWorkSheetProperty(string sheetName, int i)
        {
            Type t = this.GetType();
            MethodInfo method = this.GetType().GetMethod(string.Format("Set{0}Property", sheetName), BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(this, new object[] {i});
        }

        private void SetKnownGPCRProperty(int sheetIndex)
        {
            WorkSheetProperty property = new WorkSheetProperty();
            property.sheetIndex = sheetIndex;
            property.name = "Known GPCR";
            property.viewName = "DiscoveRxTotalGPCRProductOfferingList";
            property.headerBackColor = "#FF0000";
            property.headerFirstRow = new List<string>() { "Family", "Common\r\nName", "GPCR Gene", "Coupling", "PATHUNTER® Arrestin Cell Line\r\n(Ca^2+ mode indicated)", "", "PATHUNTER® Arrestin eXpress Kits*", "", "cAMP Hunter®\r\nCell Lines", "", "cAMP Hunter®\r\neXpress Kits*", "", "Calcium\r\nCell Lines", "Internalization\r\nCell Lines", "Internalization\r\neXpress Kits*", "PathHunter®\r\nBioassay Kits", "LeadHunter Service", "" };
            property.headerSecondRow = new List<string>() { "", "", "", "", "Human", "Ortholog", "Human", "Ortholog", "Human", "Ortholog", "Human", "Ortholog", "Human", "Human", "Human", "", "gpcrMAX", "Profiling**" };
            property.columnWidth = new List<int>() { 35, 35, 20, 20, 20, 20, 20, 25, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20 };
            property.sectionHeader = new List<string>() { "PATHUNTER® Known GPCR Assay" };
            property.footerNotes="* Small (S) and Large (L) Assay Kit formats are also available. For complete catalog listings, please visit www.discoverx.com/gpcrs/assay-ready\r\n** Profiling Service: C = Available in cAMP only; F = Available in Calcium Flux only";																			

            workSheetPropertyList.Add(property);
        }
        private void SetOrphanGPCRProperty(int sheetIndex)
        {
            WorkSheetProperty property = new WorkSheetProperty();
            property.sheetIndex = sheetIndex;
            property.name = "Orphan GPCRs";
            property.viewName = "PathHunterArrestinOrphanGPCRTotalOfferingList";
            property.headerBackColor = "#FF00FF";
            property.headerFirstRow = new List<string>() { "TARGET", "GENBANK ACCESION NUMBER", "CELL TYPE", "PATHUNTER® CELL LINE\r\n(ORPHAN)", "PATHUNTER® CELL\r\nLINE (LIGAND-BASED)", "PATHUNTER® ORPHAN\r\nEXPRESS KITS*", "LeadHunter SERVICE", "" };
            property.headerSecondRow = new List<string>() { "", "", "", "", "", "", "orphanMAX", "Profiling**" };
            property.columnWidth = new List<int>() { 20, 40, 20, 20, 30, 20, 15, 15 };
            property.sectionHeader = new List<string>() { "PATHUNTER® Orphan GPCR Assay" };
            property.footerNotes = "±   Liganded orphans that have a demonstrated Arrestin response are generally run as part of the gpcrMAX panel.\r\n*   Small (S) and Large (L) Assay Kit formats are also available. For complete catalog listings, please visit www.discoverx.com/gpcrs/assay-ready. \r\n** When two different cell backgrounds are available for the same target, the CHO-K1 background is used for profiling requests but the alternate background can be requested if necessary.  Alternate backgrounds cannot be requested for the panel.";							

            workSheetPropertyList.Add(property);
        }

        private void SetDimerizationProperty(int sheetIndex)
        {
            WorkSheetProperty property = new WorkSheetProperty();
            property.sheetIndex = sheetIndex;
            property.name = "Dimerization Assays";
            property.viewName = "PathHunterDimerizationAssaysList";
            property.headerBackColor = "#E26B05";
            property.headerFirstRow = new List<string>() { "TARGET", "PRODUCTION DESCRIPTION", "ASSAY MEASUREMENT", "PATHUNTER®\r\nCELL LINE", "PATHUNTER®\r\nEXPRESS KITS", "LeadHunter SERVICE" };
            property.headerSecondRow = new List<string>() { "", "", "", "", "", "Profiling" };
            property.columnWidth = new List<int>() { 30, 65, 30, 30, 20, 25 };
            property.sectionHeader = new List<string>() { "PATHUNTER® Dimerization Assay" };
            property.footerNotes = "* Small (S) and Large (L) Assay Kit formats are also available. For complete catalog listings, please visit www.discoverx.com.";	
										
            workSheetPropertyList.Add(property);
        }

        private void SetNHRProperty(int sheetIndex)
        {
            WorkSheetProperty property = new WorkSheetProperty();
            property.sheetIndex = sheetIndex;
            property.name = "NHR";
            property.viewName = "NuclearHormoneReceptorActivationAssaysList";
            property.headerBackColor = "#008000";
            property.headerFirstRow = new List<string>() { "TARGET", "PRODUCTION NAME", "CELL\r\nBACKGROUND", "PATHUNTER® NHR\r\nCELL LINE", "PATHUNTER® NHR\r\nEXPRESS KITS", "LeadHunter SERVICE", "" };
            property.headerSecondRow = new List<string>() { "", "", "", "", "", "nhrMax", "Profiling" };
            property.columnWidth = new List<int>() { 20, 60, 20, 20, 20, 15, 15 };
            property.sectionHeader = new List<string>() { "Nuclear translacation Assays", "Protein Interaction Assays" };
            property.footerNotes = "* Small (S) and Large (L) Assay Kit formats are also available. For complete catalog listings, please visit www.discoverx.com.";
											
            workSheetPropertyList.Add(property);
        }

        private void SetCellBasedKinaseProperty(int sheetIndex)
        {
            WorkSheetProperty property = new WorkSheetProperty();
            property.sheetIndex = sheetIndex;
            property.name = "Cell-Based Kinases";
            property.viewName = "PathHunterCellBasedKinaseAssaysList";
            property.headerBackColor = "#800080";
            property.headerFirstRow = new List<string>() { "TARGET", "PRODUCTION NAME", "PARTNER\r\nPROTEIN", "PATHUNTER®\r\nCell Line", "PATHUNTER®\r\neXpress Kits", "INTERNALIZATION\r\nCell Lines", "INTERNALIZATION\r\neXpress Kits", "PathHunter®\r\nBioAssay Kits", "LeadHunter Service", "" };
            property.headerSecondRow = new List<string>() { "", "", "", "", "", "", "", "", "gpcrMAX", "Profiling" };
            property.columnWidth = new List<int>() { 20, 50, 15, 20, 20, 20, 20, 20, 15, 15 };
            property.sectionHeader = new List<string>() { "Receptor Tyrosine Kinase Assays", "Cytosolic Tyrosine Kinase Assays" };
            property.footerNotes = "* Small (S) and Large (L) Assay Kit formats are also available. For complete catalog listings, please visit www.discoverx.com.";											

            workSheetPropertyList.Add(property);
        }


        private void SetPathwayProperty(int sheetIndex)
        {
            WorkSheetProperty property = new WorkSheetProperty();
            property.sheetIndex = sheetIndex;
            property.name = "Pathway Assays";
            property.viewName = "PathHunterPathwayAssaysList";
            property.headerBackColor = "#0060A8";
            property.headerFirstRow = new List<string>() { "TARGET", "PRODUCTION DESCRIPTION", "ASSAY MEASUREMENT", "PATHUNTER® \r\nCELL LINE", "PATHUNTER®\r\nXpress Kits", "LeadHunter SERVICE" };
            property.headerSecondRow = new List<string>() { "", "", "", "", "", "Profiling" };
            property.columnWidth = new List<int>() { 20, 65, 30, 30, 20, 25 };
            property.sectionHeader = new List<string>() { "PathHunter® Pathway Assays" };
            property.footerNotes = "* Small (S) and Large (L) Assay Kit formats are also available. For complete catalog listings, please visit www.discoverx.com.";	
										
            workSheetPropertyList.Add(property);
        }

        private void SetInCellHunterProperty(int sheetIndex)
        {
            WorkSheetProperty property = new WorkSheetProperty();
            property.sheetIndex = sheetIndex;
            property.name = "InCell Hunter Assays";
            property.viewName = "DiscoveRxInCELLHunterAssaysList";
            property.headerBackColor = "#93990F";
            property.headerFirstRow = new List<string>() { "TARGET", "PRODUCTION DESCRIPTION", "INCELL HUNTER™\r\nCELL POOL", "INCELL HUNTER™\r\nCell Line", "INCELL HUNTER™\r\nXpress Kits", "LeadHunter Service" };
            property.headerSecondRow = new List<string>() { "", "", "", "", "", "Profiling" };
            property.columnWidth = new List<int>() { 20, 65, 20, 20, 20, 25 };
            property.sectionHeader = new List<string>() { "InCell Hunter™ Compound Binding Assays - Bromodomain", "InCell Hunter™ Compound Binding - Kinase", "InCell Hunter™ Compound Binding Assays - Methyltransferase", "InCell Hunter™ Protein Binding Assays" };
            property.footerNotes = "* Small (S) and Large (L) Assay Kit formats are also available. For complete catalog listings, please visit www.discoverx.com.";
											
            workSheetPropertyList.Add(property);
        }

        private void SetPharmacotraffickingProperty(int sheetIndex)
        {
            WorkSheetProperty property = new WorkSheetProperty();
            property.sheetIndex = sheetIndex;
            property.name = "Pharmacotrafficking Assays";
            property.viewName = "PathHunterPharmacotraffickingAssayList";
            property.headerBackColor = "#FFC000";
            property.headerFirstRow = new List<string>() { "TARGET", "PRODUCTION DESCRIPTION", "PATHUNTER® \r\nCELL LINE", "LeadHunter SERVICE" };
            property.headerSecondRow = new List<string>() { "", "", "", "Profiling" };
            property.columnWidth = new List<int>() { 20, 65, 30, 30 };
            property.sectionHeader = new List<string>() { "PATHUNTER® Pharmacotrafficking Assay" };

            workSheetPropertyList.Add(property);
        }

        private void SetSynergyProperty(int sheetIndex)
        {
            WorkSheetProperty property = new WorkSheetProperty();
            property.sheetIndex = sheetIndex;
            property.name = "Synergy Table";
            property.viewName = "SynergyTable";
            property.headerBackColor = "#0070C0";
            property.headerFirstRow = new List<string>() { "Part#", "Core#", "Target", "Product Category", "Ligand Part#", "Ligand Description", "AssayComplete™\r\nCell Culture Kit"
, "AssayComplete™\r\nCell Plating Reagent", "Detection\r\nReagents", "AssayComplete™\r\nRevive Media", "AssayComplete™\r\nPreserve Freezing Reagent", "CytoTracker™\r\nCell Proliferation Kit",
"CytoTracker™\r\nLDH Quantification Kit", "CytoTracker™\r\nGlutathione\r\nQuantification Kit", "CytoTracker™\r\nDNA Damage \r\nQuantification Kit"};
            property.columnWidth = new List<int>() { 25, 15, 20, 70, 15, 25, 18, 18, 15, 18, 25, 20, 20, 20, 20};
            
            workSheetPropertyList.Add(property);
        }

        private void FillWorkSheetData(ExcelWorksheet ws, WorkSheetProperty property, int rowIndex)
        {
            int sectionHeaderIndex=0;
            
            
            TargetListViewModel model = new TargetListViewModel();

            model.GetAssayList(property.viewName);

            if (property.sectionHeader != null && property.sectionHeader.Count > 0)
            {
                CreateSectionHeader(ws, rowIndex, property, sectionHeaderIndex);
                sectionHeaderIndex++;
            }

            if (property.name == "Known GPCR")
            {
                rowIndex = PopulateDataFromAssayListTotalGPCR(rowIndex, model.TotalGPCRList, property.viewName, ws);
            }
            else if (property.name == "Cell-Based Kinases")
            {
                rowIndex = PopulateDataFromAssayListCellBasedKinases(rowIndex, model.RECEPTORList, ws);

                if (property.sectionHeader != null && property.sectionHeader.Count > 1)
                {
                    CreateSectionHeader(ws, rowIndex, property, sectionHeaderIndex);
                    sectionHeaderIndex++;
                }

                rowIndex = PopulateDataFromAssayListCellBasedKinases(rowIndex, model.CYTOSOLICList, ws);
            }
            else if (property.name == "Synergy Table")
            {
                rowIndex = PopulateDataFromSynergyList(rowIndex, model.SynergyList, property.viewName, ws);
            }
            else
            {
                rowIndex = PopulateDataFromAssayList(rowIndex, model.AssayList1, property.viewName, ws);
            

                if (property.sectionHeader != null && property.sectionHeader.Count > 1)
                {
                    CreateSectionHeader(ws, rowIndex, property, sectionHeaderIndex);
                    sectionHeaderIndex++;
                }

                rowIndex = PopulateDataFromAssayList(rowIndex, model.AssayList2, property.viewName, ws);

                if (property.sectionHeader != null && property.sectionHeader.Count > 2)
                {
                    CreateSectionHeader(ws, rowIndex, property, sectionHeaderIndex);
                    sectionHeaderIndex++;
                }

                rowIndex = PopulateDataFromAssayList(rowIndex, model.AssayList3, property.viewName, ws);

                if (property.sectionHeader != null && property.sectionHeader.Count > 3)
                {
                    CreateSectionHeader(ws, rowIndex, property, sectionHeaderIndex);
                    sectionHeaderIndex++;
                }

                rowIndex = PopulateDataFromAssayList(rowIndex, model.AssayList4, property.viewName, ws);
            }

            if (property.footerNotes != null)
            {
                CreateFooter(rowIndex, ws, property);
            }
        }

        private int PopulateDataFromAssayListCellBasedKinases(int rowIndex, List<vw_TPL_CellBasedKinase> List, ExcelWorksheet ws)
        {

            if (List != null && List.Count > 0)
            {
                rowIndex++;

                foreach (vw_TPL_CellBasedKinase item in List)
                {
                    ws.Row(rowIndex).Height = EXCEL_ROW_HEIGHT;

                    FillCellBasedKinaseDataRow(ws, rowIndex, item);
                    rowIndex++;
                }
            }

            

            return rowIndex;
        }

        private int PopulateDataFromAssayListTotalGPCR(int rowIndex, List<vw_TPL_TotalGPCR> list, string p, ExcelWorksheet ws)
        {
            if (list != null && list.Count > 0)
            {
                rowIndex++;

                foreach (vw_TPL_TotalGPCR item in list)
                {
                    ws.Row(rowIndex).Height = EXCEL_ROW_HEIGHT;

                    FillKnownGPCRDataRow(ws, rowIndex, item);
                    
                    rowIndex++;
                }
            }

            return rowIndex;
        }

        private int PopulateDataFromAssayList(int rowIndex, List<vw_TPL_Assays> list, string viewName, ExcelWorksheet ws)
        {
            if (list != null && list.Count > 0)
            {
                rowIndex++;

                foreach (vw_TPL_Assays item in list)
                {
                    SetNewProductStyle(item.DateAdded, ws, rowIndex);

                    ws.Row(rowIndex).Height = EXCEL_ROW_HEIGHT;

                    switch (viewName)
                    {
                        case "PathHunterPharmacotraffickingAssayList":
                            FillPharmacotraffickingDataRow(ws, rowIndex, item);
                            break;
                        case "DiscoveRxInCELLHunterAssaysList":
                            FillInCELLHunterDataRow(ws, rowIndex, item);
                            break;
                        case "PathHunterPathwayAssaysList":
                            FillPathwayDataRow(ws, rowIndex, item);
                            break;
                        case "NuclearHormoneReceptorActivationAssaysList":
                            FillNHRDataRow(ws, rowIndex, item);
                            break;
                        case "PathHunterDimerizationAssaysList":
                            FillDimerizationDataRow(ws, rowIndex, item);
                            break;
                        case "PathHunterArrestinOrphanGPCRTotalOfferingList":
                            FillOrphanGPCRDataRow(ws, rowIndex, item);
                            break;
                        default:
                            break;
                    }

                    //if (viewName == "PathHunterPharmacotraffickingAssayList")
                    //{
                    //    FillPharmacotraffickingDataRow(ws, rowIndex, item);
                    //}
                    //else if (viewName == "DiscoveRxInCELLHunterAssaysList")
                    //{
                    //    FillInCELLHunterDataRow(ws, rowIndex, item);
                    //}
                    //else if (viewName == "PathHunterPathwayAssaysList")
                    //{
                    //    FillPathwayDataRow(ws, rowIndex, item);
                    //}
                    
                    //else if (viewName == "NuclearHormoneReceptorActivationAssaysList")
                    //{
                    //    FillNHRDataRow(ws, rowIndex, item);
                    //}
                    //else if (viewName == "PathHunterDimerizationAssaysList")
                    //{
                    //    FillDimerizationDataRow(ws, rowIndex, item);
                    //}
                    //else if (viewName == "PathHunterArrestinOrphanGPCRTotalOfferingList")
                    //{
                    //    FillOrphanGPCRDataRow(ws, rowIndex, item);
                    //}
                    

                    rowIndex++;
                }
            }

            return rowIndex;
        }

        private int PopulateDataFromSynergyList(int rowIndex, List<vw_TPL_Synergy> list, string p, ExcelWorksheet ws)
        {
            
            if (list != null && list.Count > 0)
            {
                foreach (vw_TPL_Synergy item in list)
                {
                    ws.Row(rowIndex).Height = EXCEL_ROW_HEIGHT;

                    FillSynergyDataRow(ws, rowIndex, item);

                    rowIndex++;
                }
            }

            return rowIndex;
        }

        private void FillKnownGPCRDataRow(ExcelWorksheet ws, int rowIndex, vw_TPL_TotalGPCR item)
        {
            int colIndex = 1;
            SetCellValue(ws, rowIndex, colIndex, item.Family);
            SetCellValue(ws, rowIndex, ++colIndex, item.CommonName);
            SetCellValue(ws, rowIndex, ++colIndex, item.TARGET);
            SetCellValue(ws, rowIndex, ++colIndex, item.Coupling);
            SetCellValue(ws, rowIndex, ++colIndex, item.ArrestinCellLines);
            SetCellValue(ws, rowIndex, ++colIndex, GetOrthologPartNo(item.ArrestinCellLineMouseOrtholog, item.ArrestinCellLineRatOrtholog, item.ArrestinCellLineCanineOrtholog, item.ArrestinCellLineSimianOrtholog));
            SetCellValue(ws, rowIndex, ++colIndex, item.ArrestinKits);
            SetCellValue(ws, rowIndex, ++colIndex, GetOrthologPartNo(item.ArrestinKitsMouseOrtholog, item.ArrestinKitsRatOrtholog, item.ArrestinKitsCanineOrtholog, item.ArrestinKitsSimianOrtholog));
            SetCellValue(ws, rowIndex, ++colIndex, item.cAMPCellLines);
            SetCellValue(ws, rowIndex, ++colIndex, GetOrthologPartNo(item.cAMPCellLineMouseOrtholog, item.cAMPCellLineRatOrtholog, item.cAMPCellLineCanineOrtholog, item.cAMPCellLineSimianOrtholog));
            SetCellValue(ws, rowIndex, ++colIndex, item.cAMPKits);
            SetCellValue(ws, rowIndex, ++colIndex, GetOrthologPartNo(item.cAMPKitMouseOrtholog, item.cAMPKitRatOrtholog, item.cAMPKitCanineOrtholog, item.cAMPKitSimianOrtholog));
            SetCellValue(ws, rowIndex, ++colIndex, item.CalciumCellLine);
            SetCellValue(ws, rowIndex, ++colIndex, item.InternalizationCellLines);
            SetCellValue(ws, rowIndex, ++colIndex, item.InternalizationKits);
            SetCellValue(ws, rowIndex, ++colIndex, item.PathHunterKits);
            SetCellValue(ws, rowIndex, ++colIndex,  (item.LeadHunterServiceMax == "gpcrMAX") ? "X" : "");

            SetCellValue(ws, rowIndex, ++colIndex, GetProfilingServiceSymbol(item));
        }

        private string GetProfilingServiceSymbol(vw_TPL_TotalGPCR item)
        {
            string profilingService = string.Empty;
            if (item.LeadHunterServiceProfiling.ParseString() != string.Empty)
                profilingService = "X";
            else if (item.cAMPProfiling.ParseString() != string.Empty)
                profilingService = "C";
            else if (item.CalciumProfiling.ParseString() != string.Empty)
                profilingService = "F";

            return profilingService;
        }

        private void FillOrphanGPCRDataRow(ExcelWorksheet ws, int rowIndex, vw_TPL_Assays item)
        {
            int colIndex = 1;
            SetCellValue(ws, rowIndex, colIndex, item.TARGET, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.GenBankAccessionNumber);
            SetCellValue(ws, rowIndex, ++colIndex, item.CellBackGround);

            SetCellValue(ws, rowIndex, ++colIndex, item.CellLinePN);
            SetCellValue(ws, rowIndex, ++colIndex, item.Ligand);
            SetCellValue(ws, rowIndex, ++colIndex, item.ExpressKitPN);
            SetCellValue(ws, rowIndex, ++colIndex, (item.PROF_COMMERCIAL_NAME_PANEL != null && item.PROF_COMMERCIAL_NAME_PANEL == "orphanMAX") ? "X" : "");
            SetCellValue(ws, rowIndex, ++colIndex, (item.PROF_COMMERCIAL_NAME_PRIMARY_ELECT!=null && item.PROF_COMMERCIAL_NAME_PRIMARY_ELECT == "gpcrELECT") ? "X" : "");
        }

        private void FillDimerizationDataRow(ExcelWorksheet ws, int rowIndex, vw_TPL_Assays item)
        {
            int colIndex = 1;
            SetCellValue(ws, rowIndex, colIndex, item.TARGET, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.DESCRIPTION, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.Assay_Measures, ExcelHorizontalAlignment.Left);

            SetCellValue(ws, rowIndex, ++colIndex, item.CellLinePN);
            SetCellValue(ws, rowIndex, ++colIndex, item.ExpressKitPN);
            SetCellValue(ws, rowIndex, ++colIndex, (item.PROF_COMMERCIAL_NAME_PRIMARY_ELECT != null && item.PROF_COMMERCIAL_NAME_PRIMARY_ELECT == "tkELECT") ? "X" : "");
        }

        private void FillNHRDataRow(ExcelWorksheet ws, int rowIndex, vw_TPL_Assays item)
        {
            int colIndex = 1;
            SetCellValue(ws, rowIndex, colIndex, item.TARGET, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.DESCRIPTION, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.CellBackGround);
            SetCellValue(ws, rowIndex, ++colIndex, item.CellLinePN);
            SetCellValue(ws, rowIndex, ++colIndex, item.ExpressKitPN );
            SetCellValue(ws, rowIndex, ++colIndex, item.PROF_COMMERCIAL_NAME_PANEL == "nhrMAX" ? "X" : "");
            SetCellValue(ws, rowIndex, ++colIndex, (item.PROF_COMMERCIAL_NAME_PRIMARY_ELECT!=null && item.PROF_COMMERCIAL_NAME_PRIMARY_ELECT == "nhrELECT") ? "X" : "");
        }

        private void FillCellBasedKinaseDataRow(ExcelWorksheet ws, int rowIndex, vw_TPL_CellBasedKinase item)
        {
            int colIndex = 1;
            SetCellValue(ws, rowIndex, colIndex, item.TARGET, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.DESCRIPTION, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.PartnerProtein);
            SetCellValue(ws, rowIndex, ++colIndex, item.CellLines);
            SetCellValue(ws, rowIndex, ++colIndex, item.ExpressKits);
            SetCellValue(ws, rowIndex, ++colIndex, item.InternalizationCellLines);
            SetCellValue(ws, rowIndex, ++colIndex, item.InternalizationKits);
            SetCellValue(ws, rowIndex, ++colIndex, item.PathHunterKits);

            SetCellValue(ws, rowIndex, ++colIndex, (item.LeadHunterServiceMax != null && item.LeadHunterServiceMax == "tkMAX") ? "X" : "");
            SetCellValue(ws, rowIndex, ++colIndex, (item.LeadHunterServiceProfiling != null && item.LeadHunterServiceProfiling == "tkELECT") ? "X" : "");
        }

        private void FillPathwayDataRow(ExcelWorksheet ws, int rowIndex, vw_TPL_Assays item)
        {
            int colIndex = 1;
            SetCellValue(ws, rowIndex, colIndex, item.TARGET, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.DESCRIPTION, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.Assay_Measures, ExcelHorizontalAlignment.Left);

            SetCellValue(ws, rowIndex, ++colIndex, item.CellLinePN);
            SetCellValue(ws, rowIndex, ++colIndex, item.ExpressKitPN );
            SetCellValue(ws, rowIndex, ++colIndex, (item.PROF_COMMERCIAL_NAME_PRIMARY_ELECT!=null && item.PROF_COMMERCIAL_NAME_PRIMARY_ELECT == "pathELECT") ? "X" : "");
        }

        private void FillInCELLHunterDataRow(ExcelWorksheet ws, int rowIndex, vw_TPL_Assays item)
        {
            int colIndex = 1;
            SetCellValue(ws, rowIndex, colIndex, item.TARGET, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.DESCRIPTION, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.CellPoolPN );
            SetCellValue(ws, rowIndex, ++colIndex, item.CellLinePN);
            SetCellValue(ws, rowIndex, ++colIndex, item.ExpressKitPN );
            SetCellValue(ws, rowIndex, ++colIndex, (item.PROF_COMMERCIAL_NAME_PRIMARY_ELECT!=null && item.PROF_COMMERCIAL_NAME_PRIMARY_ELECT.Contains("ICH ELECT")) ? "X" : "");
        }

        private void FillPharmacotraffickingDataRow(ExcelWorksheet ws, int rowIndex, vw_TPL_Assays item)
        {
            int colIndex = 1;
            SetCellValue(ws, rowIndex, colIndex, item.TARGET, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.DESCRIPTION, ExcelHorizontalAlignment.Left);

            SetCellValue(ws, rowIndex, ++colIndex, item.CellLinePN);
            SetCellValue(ws, rowIndex, ++colIndex, (item.PROF_COMMERCIAL_NAME_PRIMARY_ELECT!=null && item.PROF_COMMERCIAL_NAME_PRIMARY_ELECT == "gpcrELECT") ? "X" : "");
        }

        private void FillSynergyDataRow(ExcelWorksheet ws, int rowIndex, vw_TPL_Synergy item)
        {
            int colIndex = 1;
            SetCellValue(ws, rowIndex, colIndex, item.PartNumber);
            SetCellValue(ws, rowIndex, ++colIndex, item.CoreNumber);
            SetCellValue(ws, rowIndex, ++colIndex, item.TARGET);
            SetCellValue(ws, rowIndex, ++colIndex, item.GroupID.Substring(3));
            SetCellValue(ws, rowIndex, ++colIndex, item.LigandPartNumber);
            SetCellValue(ws, rowIndex, ++colIndex, item.LigandDescription);
            SetCellValue(ws, rowIndex, ++colIndex, item.CellCultureKit);
            SetCellValue(ws, rowIndex, ++colIndex, item.CellPlatingReagent);
            SetCellValue(ws, rowIndex, ++colIndex, item.DetectionReagents);
            SetCellValue(ws, rowIndex, ++colIndex, item.ReviveMedia);
            SetCellValue(ws, rowIndex, ++colIndex, item.PreserveFreezingReagent);
            SetCellValue(ws, rowIndex, ++colIndex, item.CellProliferationKit);
            SetCellValue(ws, rowIndex, ++colIndex, item.LDHQuantificationKit);
            SetCellValue(ws, rowIndex, ++colIndex, item.GlutathionQuantificationKit);
            SetCellValue(ws, rowIndex, ++colIndex, item.DNADamageQuantificationKit);
        }

        private string GetOrthologPartNo(string mousePN, string ratPN, string caninePN, string simianPN)
        {
            string orthologPN=string.Empty;

            orthologPN = AppendOrtholog("(m) " + mousePN, orthologPN);
            orthologPN = AppendOrtholog("(r) " + ratPN, orthologPN);
            orthologPN = AppendOrtholog("(c) " + caninePN, orthologPN);
            orthologPN = AppendOrtholog("(s) " + simianPN, orthologPN);

            return orthologPN;
        }

        private string AppendOrtholog(string ortholog, string str)
        {
            if (!string.IsNullOrEmpty(ortholog) && ortholog.Length>7)
            {
                str += (str == string.Empty) ? ortholog : "\r\n" + ortholog;
            }
            return str;
        }
    }
}