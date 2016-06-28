using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Discoverx.Models.CBIS_DB;
using Discoverx.Models;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Drawing;


using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Discoverx.Models.StudyReport
{
    enum Redundancies { Singlicate = 1, Duplicate = 2, Triplicate = 3, Quadruplicate = 4 };

    public class PHStudyReportViewModel : BaseViewModel
    {
        [Required(ErrorMessage=" * Enter order/contract ID.")]
        public string orderID { get; set; }
        public string commonID { get; set; }
        public string serviceType { get; set; }
        public string Objective { get; set; }
        public string techPrinciple { get; set; }
        public int requestID { get; set; }
        public string quoteID { get; set; }
        public string targetList { get; set; }
        public string targetClassList { get; set; }
        public string assayTypeList { get; set; }
        public string assayModeList { get; set; }
        public string targetAssayList { get; set; }
        public string quoteIDList { get; set; }
        
        public string figure1Text { get; set; }
        public string figure2Text { get; set; }
        public string table1Text { get; set; }
        public string summaryText { get; set; }
        public int numberOfDatapoints { get; set; }
        public int numberOfCompounds { get; set; }
        public int numberOfAssays { get; set; }
        public int numberOfRedundancy { get; set; }
        
        public bool isExportPDF { get; set; }
        public Order order { get; set; }
        public Contract contract { get; set; }
        public List<Contract> contractList { get; set; }

        public bool isPrimaryScreen { get; set; }
        public List<mtp_Dx_RequestAssaysSelect_Result> assaySelectList { get; set; }
        public List<DX_GetAssayReportSummary_Result> AssayReportSummary { get; set; }
        public List<DX_GetPrimaryAssayData_Result> PrimaryAssayData { get; set; }
        public List<DX_GetPrimaryPlateControl_Result> PrimaryPlateControl { get; set; }
        public double minPrimaryAssayData { get; set; }
        public double maxPrimaryAssayData { get; set; }
        public double PosControlDeviation { get; set; }
        public double NegControlDeviation { get; set; }
        public string primaryPlateID { get; set; }
        

        public void GetOrderInfo()
        {
            order = CBIS_DiscoveRx_Orders_ctx.Orders.Where(o => o.AmbitOrderID.Contains(orderID)).FirstOrDefault();
            if (order != null)
            {
                orderID = order.AmbitOrderID;
                commonID = order.AmbitOrderID.Left(order.AmbitOrderID.NthIndexOf("-", 2));
                contractList = CBIS_HTSContracts_ctx.Contracts.Where(c => c.ContractID.Contains(commonID)).ToList();

                targetList = string.Empty;
                assayModeList = string.Empty;
                targetClassList = string.Empty;
                assayTypeList = string.Empty;
                targetAssayList = string.Empty;
                quoteIDList = string.Empty;
                primaryPlateID = string.Empty;
                numberOfDatapoints = 0;
                numberOfCompounds = order.NumberOfCompounds.ParseInt();
                numberOfAssays = 0;



                if (contractList.Count > 0)
                {
                    this.contract = contractList[0];
                    GetServiceType(this.contract);

                    foreach (Contract contract in contractList)
                    {
                        
                        quoteID = contract.QuoteID;
                        quoteIDList += (quoteIDList == string.Empty) ? quoteID : "/ " + quoteID;
                        Dx_Request request = CBIS_KinomeScan_RequestManager_ctx.Dx_Request.Where(d => d.QuoteNumber == quoteID).FirstOrDefault();
                        if (request != null)
                        {
                            requestID = request.Dx_RequestID;
                            assaySelectList = CBIS_KinomeScan_RequestManager_ctx.mtp_Dx_RequestAssaysSelect(requestID).ToList();
                            if (assaySelectList.Count > 0)
                            {

                                for (int i = 0; i < assaySelectList.Count; i++)
                                {
                                    if (!targetList.Contains(assaySelectList[i].Target))
                                    {
                                        targetList += assaySelectList[i].Target + ", ";
                                    }

                                    if (!assayModeList.Contains(assaySelectList[i].Mode))
                                    {
                                        assayModeList += assaySelectList[i].Mode + ", ";
                                    }

                                    if (!targetClassList.Contains(assaySelectList[i].Class))
                                    {
                                        targetClassList += assaySelectList[i].Class + ", ";
                                    }

                                    if (!assayTypeList.Contains(assaySelectList[i].Type))
                                    {
                                        assayTypeList += assaySelectList[i].Type.ToLower() + ", ";
                                    }

                                    targetAssayList += string.Format("{0} - {1} - {2}", assaySelectList[i].Target, assaySelectList[i].Type, assaySelectList[i].Mode) + "<br />";
                                    numberOfAssays += 1;
                                }
                            }
                        }

                        Redundancies redundancies=0;

                        if (Enum.TryParse(contract.Redundancy, out redundancies))
                        {
                            numberOfRedundancy = (int)redundancies;
                        }
                        
                    }

                    targetList = targetList.Left(targetList.Length - 2);
                    targetClassList = targetClassList.Left(targetClassList.Length - 2).ReplaceLastOccurrence(", ", " and ");
                    assayModeList = assayModeList.Left(assayModeList.Length - 2).ReplaceLastOccurrence(", ", " and ");

                    if (order.AmbitOrderID.Contains("-d-"))
                    {
                        isPrimaryScreen = false;
                        Objective = string.Format("{0} {1}", assayModeList, "Secondary Screen");
                    }
                    else if (order.AmbitOrderID.Contains("-c-"))
                    {
                        isPrimaryScreen = true;
                        Objective = string.Format("{0} {1}", assayModeList, "Primary Screen");
                        //PrimaryAssayData = CBIS_DiscoveRx_Orders_ctx.DX_GetPrimaryAssayData(commonID).OrderBy(m => m.CompoundName).ThenBy(m => m.AssayTarget).ThenBy(m => m.AssayType).ThenBy(m => m.AssayFormat).ThenByDescending(m=>m.Concentration).ToList();
                        PrimaryAssayData = CBIS_DiscoveRx_Orders_ctx.DX_GetPrimaryAssayData(commonID).OrderBy(m => m.CompoundName).ThenBy(m => m.AssayTarget).ThenBy(m => m.ContainerID).ToList();

                        foreach (DX_GetPrimaryAssayData_Result data in PrimaryAssayData)
                        {
                            if(data.AssayTarget=="Various")
                            {
                                //data.AssayTarget = PrimaryAssayData.Where(p => p.CompoundName == data.CompoundName && p.AssayTarget != "Various").Count() > 0 ? PrimaryAssayData.Where(p => p.CompoundName == data.CompoundName && p.AssayTarget != "Various").First().AssayTarget : data.AssayTarget;
                            }

                            DX_GetPrimaryAssayData_Result NegControlData = PrimaryAssayData.Where(p => p.ContainerID == data.ContainerID && p.SubstanceID=="NegControl").Count() > 0 ? PrimaryAssayData.Where(p => p.ContainerID == data.ContainerID && p.SubstanceID=="NegControl").First(): null;
                            DX_GetPrimaryAssayData_Result PosControlData = PrimaryAssayData.Where(p => p.ContainerID == data.ContainerID && p.SubstanceID=="PosControl").Count() > 0 ? PrimaryAssayData.Where(p => p.ContainerID == data.ContainerID && p.SubstanceID=="PosControl").First(): null;

                            if(NegControlData!=null)
                            {
                                data.NegControl = NegControlData.AvgValue;
                                data.NegControlDeviation = NegControlData.SD;
                            }

                            if(PosControlData!=null)
                            {
                                data.PosControl = PosControlData.AvgValue;
                                data.PosControlDeviation = PosControlData.SD;
                            }
                        }

                        PrimaryPlateControl = CBIS_DiscoveRx_Orders_ctx.DX_GetPrimaryPlateControl(commonID).ToList();

                        if (PrimaryPlateControl.Count > 0)
                        {
                            primaryPlateID = PrimaryPlateControl.First().ContainerID;
                        }

                        
                    }

                    if (contractList.Count > 1)
                    {
                        string noGreekSymbol = targetList.ReplaceGreekSymbol();
                        AssayReportSummary = CBIS_DiscoveRx_Orders_ctx.DX_GetAssayReportSummary(commonID).Where(m => (targetList.ToLower().Contains(m.AssayTarget.ToLower()) || (targetList.ReplaceGreekSymbol().ToLower().Contains(m.AssayTarget.ToLower())))).OrderBy(m => m.CompoundName).ThenBy(m => m.AssayTarget).ThenBy(m => m.AssayName).ThenBy(m => m.AssayFormat).ToList();
                         //AssayReportSummary = CBIS_DiscoveRx_Orders_ctx.DX_GetAssayReportSummary(commonID).OrderBy(m => m.CompoundName).ThenBy(m => m.AssayTarget).ThenBy(m => m.AssayName).ThenBy(m => m.AssayFormat).ToList();
                    }
                    else
                    {
                        AssayReportSummary = CBIS_DiscoveRx_Orders_ctx.DX_GetAssayReportSummary(orderID.Left(17)).Where(m => (targetList.ToLower().Contains(m.AssayTarget.ToLower()) || (targetList.ReplaceGreekSymbol().ToLower().Contains(m.AssayTarget.ToLower())))).OrderBy(m => m.CompoundName).ThenBy(m => m.AssayTarget).ThenBy(m => m.AssayName).ThenBy(m => m.AssayFormat).ToList();
                    }

                    if(isPrimaryScreen)
                    {
                        //numberOfCompounds += PrimaryAssayData.Where(a => a.CompoundName!=null).Select(a => a.CompoundName).Distinct().Count();
                        numberOfDatapoints += numberOfAssays * numberOfCompounds * numberOfRedundancy;
                    }
                    else 
                    { 
                        //numberOfCompounds += AssayReportSummary.Where(a => !a.AssayID.Contains("REF-")).Select(a=>a.CompoundName).Distinct().Count();
                        numberOfDatapoints += numberOfAssays * numberOfCompounds * 20;
                    }
                    

                    if (!isExportPDF)
                    {
                        buildFigureLegend();
                    }
                }
            }
        }

        private void buildFigureLegend()
        {
            string compoundTrailer = (numberOfCompounds > 1 ? "s were" : " was");
            string assayTrailer = (numberOfAssays > 1 ? "s" : "");

            figure1Text = string.Format("Control dose curve{0} performed for the requested {1} Biosensor Assay{2}. Data shown was normalized to the maximal and minimal response observed in the presence of control compound and vehicle respectively.", (numberOfAssays > 1 ? "s were" : " was"), targetClassList, assayTrailer);
            figure2Text = string.Format("Compound{0} tested in {1} mode with the requested {2} Biosensor Assay{3}. ", compoundTrailer, assayModeList.ToLower(), targetClassList, assayTrailer);
            if (assayModeList.ToLower().IndexOf("agonist") == 0 || assayModeList.ToLower().IndexOf(" agonist") > 8)
            {
                figure2Text += "For agonist assay" + assayTrailer + ", data was normalized to the maximal and minimal response observed in the presence of control ligand and vehicle. ";
            }
            if (assayModeList.ToLower().IndexOf("antagonist") >= 0)
            {
                figure2Text += "For antagonist assay" + assayTrailer + ", data was normalized to the maximal and minimal response observed in the presence of EC80 ligand and vehicle. ";
            }
            table1Text = string.Format("Compound{0} tested in {1} mode with the {2} Biosensor Assay{3}. For {4} assay{5}, data was normalized to the maximal and minimal response observed in the presence of control ligand and vehicle.", compoundTrailer, assayModeList.ToLower(), targetClassList, assayTrailer, assayModeList.ToLower(), assayTrailer);

        }

        private void GetServiceType(Contract contract)
        {
            if (contract.Product.Contains("GPCR"))
            {
                serviceType = ServiceType.GPCR;
            }
            else if (contract.Product.Contains("NHR"))
            {
                serviceType = ServiceType.NHR;
            }
            else if (contract.Product.Contains("Epigenetics"))
            {
                serviceType = ServiceType.Epigenetics;
            }
            else if (contract.Product.Contains("Pathway"))
            {
                serviceType = ServiceType.Pathway;
            }
            else if (contract.Product.Contains("Kinase"))
            {
                serviceType = ServiceType.Kinase;
            }
            else if (contract.Product.Contains("Cytokine"))
            {
                serviceType = ServiceType.Cytokine;
            }

        }

        private List<double> GetPosControlValues()
        {
            List<double> list = new List<double>();

            foreach (DX_GetPrimaryPlateControl_Result item in PrimaryPlateControl.Where(p=>p.SubstanceID =="PosControl"))
            {
                list.Add(item.Value.ParseDouble());
            }

            return list;
        }

        private List<double> GetNegControlValues()
        {
            List<double> list = new List<double>();

            foreach (DX_GetPrimaryPlateControl_Result item in PrimaryPlateControl.Where(p => p.SubstanceID == "NegControl"))
            {
                list.Add(item.Value.ParseDouble());
            }

            return list;
        }

        private int GetNumberOfReplicates()
        {
            string replicate=contract.Redundancy.ToLower();
            if(replicate=="singlicate")
                return 1;
            else if(replicate=="duplicate")
                return 2;
            else if(replicate=="triplicate")
                return 3;
            else if(replicate=="quadruplicate")
                return 4;
            else
                return 1;
        }

        internal void DownLoadPDF(Controller controller)
        {
            this.isExportPDF = true;
            GetOrderInfo();
            string htmlText = pdfModel.ConvertViewToHtmlText(controller, "Index", this);
            pdfModel.DownloadPDFFile(htmlText, GetPDFFileName(), "PHStudyReport");
        }

        private string GetPDFFileName()
        {
            return string.Format("{0}_{1} Profile Report", this.order.Company, this.quoteIDList);
        }

        

        internal void DownLoadExcelFile(Controllers.PHStudyReportController pHStudyReportController)
        {
            GetOrderInfo();

            ExcelPHStudyReportModel excelPHStudyReportModel = new ExcelPHStudyReportModel(this);

            excelPHStudyReportModel.DownloadExcelFile();
           
        }

        public List<DX_GetSecondaryAssayGraphData_Result> GetSecondaryAssayGraphData(string projectID, string assayID)
        {
            return CBIS_DiscoveRx_Orders_ctx.DX_GetSecondaryAssayGraphData(projectID, assayID, "").OrderBy(d => d.Concentration).ToList();
        }
        
    }

   
}