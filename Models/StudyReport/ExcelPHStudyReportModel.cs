using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using Discoverx.Models;
using Discoverx.Models.CBIS_DB;

using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Discoverx.Models.StudyReport
{

    public class ExcelPHStudyReportModel : ExcelWorkSheetModel
    {
        private PHStudyReportViewModel viewModel;

        public ExcelPHStudyReportModel(PHStudyReportViewModel pHStudyReportViewModel)
        {
            // TODO: Complete member initialization
            this.viewModel = pHStudyReportViewModel;
        }

        internal void DownloadExcelFile()
        {
            int headerRowNumber = 0;
            using (ExcelPackage pkg = new ExcelPackage())
            {
                //Here setting some document properties
                pkg.Workbook.Properties.Author = "discoverx.com";
                pkg.Workbook.Properties.Title = GetExcelFileName();

                SetWorkSheetProperties();
                foreach (WorkSheetProperty property in workSheetPropertyList)
                {
                    headerRowNumber = AddWorkSheet(pkg, property, "Arial", 11);
                    FillWorkSheetData(ws, property, headerRowNumber);
                }

                string fileName = string.Format(GetExcelFileName());
                HttpContext.Current.Response.BinaryWrite(pkg.GetAsByteArray());
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
            }
        }

        private string GetExcelFileName()
        {
            return string.Format("{0}_{1} Data Sheet", viewModel.order.Company, viewModel.quoteIDList);
        }

       
        private void SetPrimaryDataProperty(int sheetIndex)
        {
            List<string> headerFirstRow = new List<string>() { "Compound Name", "Project ID", "Assay Name", "Assay Format", "Assay Target", "Conc(uM)", "Value 1", "Value 2", "Average Value", "Std Deviation", "% Efficacy" };
            List<int> columnWidth = new List<int>() { 30, 35, 20, 20, 20, 20, 20, 20, 20, 20, 20 };

            SetWorkSheetProperty(sheetIndex, "Primary Data", EXCEL_HEADER_COLOR, headerFirstRow, columnWidth);
        }

        private void SetPlateControlsProperty(int sheetIndex)
        {
            List<string> headerFirstRow = new List<string>() { "Project ID", "Assay Name", "Assay Format", "Assay Target", "Plate ID", "Pos Control\r\nMean", "Pos Control\r\nSD", "Neg Control\r\nMean", "Neg Control\r\nSD" };
            List<int> columnWidth = new List<int>() { 35, 20, 20, 20, 20, 20, 20, 20, 20 };

            SetWorkSheetProperty(sheetIndex, "Plate Controls", EXCEL_HEADER_COLOR, headerFirstRow, columnWidth);
        }

        private void SetSummaryProperty(int sheetIndex)
        {
            List<string> headerFirstRow = new List<string>() { "Compound Name", "Project ID", "Assay Name", "Assay Format", "Assay Target", "Result Type", "RC50 (uM)", "Hill", "Curve Bottom", "Curve Top", "Max Response" };
            List<int> columnWidth = new List<int>() { 35, 35, 20, 20, 20, 20, 20, 20, 20, 20, 20 };

            SetWorkSheetProperty(sheetIndex, "Summary", EXCEL_HEADER_COLOR, headerFirstRow, columnWidth);
        }

        private void SetGraphProperty(int sheetIndex)
        {
            List<string> headerFirstRow = new List<string>() { "Compound Name", "Project ID", "Assay Name", "Assay Format", "Assay Target", "Result Type", "RC50 (uM)", "Hill", "Curve Bottom", "Curve Top", "Max Response", "Graph Result", "Well ID", "Conc", "Raw Value", "Percent Efficacy" };
            List<int> columnWidth = new List<int>() { 35, 35, 20, 20, 20, 20, 20, 20, 20, 20, 20, 40, 20, 20, 20, 20 };

            SetWorkSheetProperty(sheetIndex, "Graph", EXCEL_HEADER_COLOR, headerFirstRow, columnWidth);
        }

        private void FillWorkSheetData(ExcelWorksheet ws, WorkSheetProperty property, int rowIndex)
        {
            if (viewModel.isPrimaryScreen)
            {
                if (property.name == "Primary Data")
                {
                    rowIndex = PopulateDataFromPrimaryAssayData(rowIndex, viewModel.PrimaryAssayData.Where(p => p.CompoundName != null).ToList(), property.viewName, ws);
                }
                else if (property.name == "Plate Controls")
                {
                    rowIndex = PopulateDataFromAssayReportSummaryPlateControl(rowIndex, viewModel.PrimaryAssayData.Where(p=>p.CompoundName==null).GroupBy(p=>p.ContainerID).Select(grp => grp.First()).ToList(), property.viewName, ws);
                }
            }

            if (property.name == "Summary")
            {
                rowIndex = PopulateDataFromAssayReportSummary(rowIndex, viewModel.AssayReportSummary, property.viewName, ws);
            }
            else if (property.name == "Graph")
            {
                rowIndex = PopulateDataFromAssayReportSummaryGraph(rowIndex, viewModel.AssayReportSummary, property.viewName, ws);
            }
        }

        private int PopulateDataFromPrimaryAssayData(int rowIndex, List<DX_GetPrimaryAssayData_Result> list, string p, ExcelWorksheet ws)
        {
            if (list != null && list.Count > 0)
            {
                rowIndex++;

                foreach (DX_GetPrimaryAssayData_Result item in list)
                {
                    FillPrimaryAssayDataRow(ws, rowIndex, item);
                    rowIndex++;
                }
            }

            return rowIndex;
        }

        private void FillPrimaryAssayDataRow(ExcelWorksheet ws, int rowIndex, DX_GetPrimaryAssayData_Result item)
        {
            int colIndex = 1;
            SetCellValue(ws, rowIndex, colIndex, item.CompoundName, ExcelHorizontalAlignment.Left);

            SetCellValue(ws, rowIndex, ++colIndex, item.ProjectID, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.AssayType, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.AssayFormat, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.AssayTarget, ExcelHorizontalAlignment.Left);

            SetCellValueNumber(ws, rowIndex, ++colIndex, item.Concentration);
            SetCellValueNumber(ws, rowIndex, ++colIndex, item.Value1);
            SetCellValueNumber(ws, rowIndex, ++colIndex, item.Value2);
            SetCellValueNumber(ws, rowIndex, ++colIndex, item.AvgValue);
            SetCellValueNumber(ws, rowIndex, ++colIndex, Math.Round(Utility.StandardDeviation(new List<double>() { item.Value1.ParseDouble(), item.Value2.ParseDouble() }), 1));
            SetCellValueNumber(ws, rowIndex, ++colIndex, Utility.GetPercentageEfficacy(item.AvgValue.ParseDouble(), item.NegControl.ParseDouble(), item.PosControl.ParseDouble()));
        }

        private int PopulateDataFromAssayReportSummaryGraph(int rowIndex, List<DX_GetAssayReportSummary_Result> list, string p, ExcelWorksheet ws)
        {
            if (list != null && list.Count > 0)
            {
                rowIndex++;

                List<DX_GetSecondaryAssayGraphData_Result> graphData;
                //foreach (DX_GetAssayReportSummary_Result item in list.OrderBy(a => (a.AssayID.Contains("REF-") ? 0 : 1)).ThenBy(a => a.AssayTarget).ThenBy(a => a.AssayFormat)) 
                foreach (DX_GetAssayReportSummary_Result item in list.Where(a => a.AssayID.Contains("REF-")).OrderBy(a => a.AssayTarget).ThenBy(a => a.AssayFormat).Union(list.Where(a => !a.AssayID.Contains("REF-")).OrderBy(a => a.CompoundName)))
                {

                    //ws.Row(rowIndex).Height = EXCEL_ROW_HEIGHT;

                    FillSummaryGraphDataRow(ws, rowIndex, item);

                    graphData = viewModel.GetSecondaryAssayGraphData(item.ProjectID, item.AssayID);

                    FillGraphData(ws, graphData, rowIndex, 13);

                    rowIndex += 20;
                }
            }
            return rowIndex;
        }

        private void FillGraphData(ExcelWorksheet ws, List<DX_GetSecondaryAssayGraphData_Result> graphData, int rowIndex, int columIndex)
        {
            foreach (DX_GetSecondaryAssayGraphData_Result dataRow in graphData)
            {
                SetCellValue(ws, rowIndex, columIndex, dataRow.WellID, ExcelHorizontalAlignment.Center, 9);
                SetCellValueNumber(ws, rowIndex, columIndex + 1, dataRow.Concentration, ExcelHorizontalAlignment.Right, 9);
                SetCellValueNumber(ws, rowIndex, columIndex + 2, dataRow.Value, ExcelHorizontalAlignment.Right, 9);
                SetCellValueNumber(ws, rowIndex, columIndex + 3, dataRow.PercentEfficacy, ExcelHorizontalAlignment.Right, 9);

                rowIndex++;
            }
        }

        private int PopulateDataFromAssayReportSummaryPlateControl(int rowIndex, List<DX_GetPrimaryAssayData_Result> list, string p, ExcelWorksheet ws)
        {
            if (list != null && list.Count > 0)
            {
                rowIndex++;

                foreach (DX_GetPrimaryAssayData_Result item in list)
                {
                    ws.Row(rowIndex).Height = EXCEL_ROW_HEIGHT;

                    FillSummaryPlateControlDataRow(ws, rowIndex, item);

                    rowIndex++;
                }
            }

            return rowIndex;
        }

        private void FillSummaryPlateControlDataRow(ExcelWorksheet ws, int rowIndex, DX_GetPrimaryAssayData_Result item)
        {
            int colIndex = 1;
            SetCellValue(ws, rowIndex, colIndex, item.ProjectID, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.AssayType, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.AssayFormat, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.AssayTarget, ExcelHorizontalAlignment.Left);

            SetCellValue(ws, rowIndex, ++colIndex, item.ContainerID, ExcelHorizontalAlignment.Left);
            SetCellValueNumber(ws, rowIndex, ++colIndex, item.PosControl);
            SetCellValueNumber(ws, rowIndex, ++colIndex, item.PosControlDeviation);
            SetCellValueNumber(ws, rowIndex, ++colIndex, item.NegControl);
            SetCellValueNumber(ws, rowIndex, ++colIndex, item.NegControlDeviation);
        }

        private int PopulateDataFromAssayReportSummary(int rowIndex, List<DX_GetAssayReportSummary_Result> list, string p, ExcelWorksheet ws)
        {
            if (list != null && list.Count > 0)
            {
                rowIndex++;

                //foreach (DX_GetAssayReportSummary_Result item in list.OrderBy(a => (a.AssayID.Contains("REF-") ? 0 : 1)).ThenBy(a => a.AssayTarget).ThenBy(a=>a.AssayFormat))
                foreach (DX_GetAssayReportSummary_Result item in list.Where(a => a.AssayID.Contains("REF-")).OrderBy(a => a.AssayTarget).ThenBy(a => a.AssayFormat).Union(list.Where(a => !a.AssayID.Contains("REF-")).OrderBy(a => a.CompoundName)))
                {
                    ws.Row(rowIndex).Height = EXCEL_ROW_HEIGHT;

                    FillSummaryDataRow(ws, rowIndex, item);

                    rowIndex++;
                }
            }
            return rowIndex;
        }

        private void FillSummaryGraphDataRow(ExcelWorksheet ws, int rowIndex, DX_GetAssayReportSummary_Result item)
        {
            FillSummaryDataRow(ws, rowIndex, item);

            for (int i = 1; i <= 12; i++)
            {
                for (int j = rowIndex; j <= rowIndex + 19; j++)
                {
                    var cell = ws.Cells[j, i];
                    SetBorderStyle(cell.Style.Border, EXCEL_BORDER_COLOR);
                }
                ws.Cells[rowIndex, i, rowIndex + 19, i].Merge = true;
            }

            AddImage(ws, rowIndex, 11, string.Format(Utility.GetAPPSettingKey("GraphImageURL"), item.AssayID), 250, 750);
            //AddImage(ws, rowIndex, 11, string.Format(Utility.GetAPPSettingKey("GraphImageURL"), item.AssayID), 252, 252);
        }

       

        private void FillSummaryDataRow(ExcelWorksheet ws, int rowIndex, DX_GetAssayReportSummary_Result item)
        {
            int colIndex = 1;
            SetCellValue(ws, rowIndex, colIndex, item.CompoundName, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.ProjectID, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.AssayName, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.AssayFormat, ExcelHorizontalAlignment.Left);
            SetCellValue(ws, rowIndex, ++colIndex, item.AssayTarget, ExcelHorizontalAlignment.Left);

            SetCellValue(ws, rowIndex, ++colIndex, item.ResultType, ExcelHorizontalAlignment.Left);
            if (item.ValuePrefix.ParseString() != string.Empty)
            {
                SetCellValue(ws, rowIndex, ++colIndex, item.ValuePrefix.ParseString() + item.ResultValue.ParseString(), ExcelHorizontalAlignment.Right);
            }
            else
            {
                SetCellValueNumber(ws, rowIndex, ++colIndex, item.ResultValue);
            }
            SetCellValueNumber(ws, rowIndex, ++colIndex, item.Hill);
            SetCellValueNumber(ws, rowIndex, ++colIndex, item.CurveBottom);
            SetCellValueNumber(ws, rowIndex, ++colIndex, item.CurveTop);
            SetCellValueNumber(ws, rowIndex, ++colIndex, item.MaxResponse);

        }

        private void SetWorkSheetProperties()
        {
            int sheetIndex = 1;

            workSheetPropertyList = new List<WorkSheetProperty>();
            if (viewModel.isPrimaryScreen)
            {
                SetPrimaryDataProperty(sheetIndex++);
                SetPlateControlsProperty(sheetIndex++);
            }

            SetSummaryProperty(sheetIndex++);
            SetGraphProperty(sheetIndex++);
        }
    }  
}