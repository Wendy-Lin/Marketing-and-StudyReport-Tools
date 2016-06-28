using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Discoverx.Models
{
    public class ExcelWorkSheetModel 
    {
        public ExcelWorksheet ws;
        public readonly int EXCEL_ROW_HEIGHT = 25;
        public const string EXCEL_HEADER_COLOR = "#23608A";
        public Color EXCEL_BORDER_COLOR = ColorTranslator.FromHtml("#969696");
        public List<WorkSheetProperty> workSheetPropertyList;

        protected int AddWorkSheet(ExcelPackage p, WorkSheetProperty property, string fontName, int fontSize)
        {
            //Create a sheet
            p.Workbook.Worksheets.Add(property.name);
            ws = p.Workbook.Worksheets[property.sheetIndex];
            ws.Name = property.name; //Setting Sheet's name
            ws.TabColor = (ColorTranslator.FromHtml(property.headerBackColor));
            ws.DefaultRowHeight = 45;
            ws.Cells.Style.Font.Size = fontSize; //Default font size for whole sheet
            ws.Cells.Style.Font.Name = fontName; //Default Font name for whole sheet
            ws.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            int headerRowNumber = AddWorkSheetHeader(ws, property);

            return headerRowNumber;
        }

        private int AddWorkSheetHeader(ExcelWorksheet ws, WorkSheetProperty property)
        {
            int columnCount = property.headerFirstRow.Count;
            int headerRowNumber = 0;

            for (int i = 0; i < columnCount; i++)
            {
                ws.Column(i + 1).Width = property.columnWidth[i];
            }

            if (property.headerFirstRow != null)
            {
                headerRowNumber++;
                ws.Row(headerRowNumber).Height = EXCEL_ROW_HEIGHT;

                for (int i = 0; i < columnCount; i++)
                {
                    if (property.headerFirstRow[i] != "")
                    {
                        ws.Cells[1, i + 1].Value = property.headerFirstRow[i];
                        if (property.headerFirstRow[i].Contains("\r\n"))
                        {
                            ws.Cells[1, i + 1].Style.WrapText = true;
                            //rowWrap = true;
                            ws.Row(1).Height = (property.headerFirstRow[i].getNumberOfOccurencies("\r\n") + 1) * 18 + 10;
                        }

                    }
                    else if (i != 0)
                    {
                        if (property.headerFirstRow[i - 1] != "")
                        {
                            if (property.headerFirstRow.Count > i + 1 && property.headerFirstRow[i + 1] == "")
                                ws.Cells[1, i, 1, i + 2].Merge = true;
                            else
                                ws.Cells[1, i, 1, i + 1].Merge = true;
                        }
                    }
                }
            }

            if (property.headerSecondRow != null)
            {
                headerRowNumber++;
                ws.Row(headerRowNumber).Height = EXCEL_ROW_HEIGHT;

                for (int i = 0; i < columnCount; i++)
                {
                    if (property.headerSecondRow[i] != "")
                    {
                        ws.Cells[2, (i + 1)].Value = property.headerSecondRow[i];
                        if (property.headerSecondRow[i].Contains("\r\n"))
                        {
                            ws.Cells[2, (i + 1)].Style.WrapText = true;
                            ws.Row(2).Height = (property.headerSecondRow[i].getNumberOfOccurencies("\r\n") + 1) * 18 + 10;
                        }
                    }
                    else
                        ws.Cells[1, (i + 1), 2, (i + 1)].Merge = true;
                }

            }

            ExcelFill fill;

            for (int i = 1; i <= headerRowNumber; i++)
            {

                for (int j = 1; j <= columnCount; j++)
                {
                    var cell = ws.Cells[i, j];
                    fill = cell.Style.Fill;
                    fill.PatternType = ExcelFillStyle.Solid;

                    fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(property.headerBackColor));

                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.Font.Bold = true;
                    cell.Style.Font.Color.SetColor(Color.White);

                    SetBorderStyle(cell.Style.Border, EXCEL_BORDER_COLOR);
                }
            }

            return headerRowNumber;
        }

        protected void SetNewProductStyle(string DateAdded, ExcelWorksheet ws, int rowIndex)
        {
            if (DateAdded.ParseDate() > DateTime.Now.AddDays(-45))
            {
                ws.Row(rowIndex).Style.Font.Color.SetColor(Color.Red);
                ws.Row(rowIndex).Style.Font.Bold = true;
            }
        }

        protected void SetBorderStyle(Border border, Color color, ExcelBorderStyle borderStyle = ExcelBorderStyle.Thin)
        {
            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = borderStyle;
            border.Bottom.Color.SetColor(color);
            border.Top.Color.SetColor(color);
            border.Left.Color.SetColor(color);
            border.Right.Color.SetColor(color);
        }

        protected void SetCellValue(ExcelWorksheet ws, int rowIndex, int colIndex, string cellValue, ExcelHorizontalAlignment alignment = ExcelHorizontalAlignment.Center, int fontSize = 0, int extraHeight=0)
        {
            var cell = ws.Cells[rowIndex, colIndex];
            cell.Style.HorizontalAlignment = alignment;

            if (fontSize != 0)
            {
                cell.Style.Font.Size = 9;
            }

            cell.Value = cellValue;
            if (cellValue.ParseString().Contains("\r\n"))
            {
                cell.Style.WrapText = true;
                ws.Row(rowIndex).Height = (cellValue.getNumberOfOccurencies("\r\n") + 1) * 18 + extraHeight;
            }

            SetBorderStyle(cell.Style.Border, EXCEL_BORDER_COLOR);
        }

        protected void SetCellValueNumber(ExcelWorksheet ws, int rowIndex, int colIndex, double? cellValue, ExcelHorizontalAlignment alignment = ExcelHorizontalAlignment.Right, int fontSize = 0)
        {
            var cell = ws.Cells[rowIndex, colIndex];
            cell.Style.HorizontalAlignment = alignment;
            if (fontSize != 0)
            {
                cell.Style.Font.Size = fontSize;
            }
            cell.Value = cellValue;

            var border = cell.Style.Border;
            SetBorderStyle(cell.Style.Border, EXCEL_BORDER_COLOR);
        }

        

        protected void AddImage(ExcelWorksheet oSheet, int rowIndex, int colIndex, string imagePath, int width, int height)
        {
            Bitmap image = Utility.LoadPicture(imagePath);
            OfficeOpenXml.Drawing.ExcelPicture excelImage = null;
            if (image != null)
            {
                excelImage = oSheet.Drawings.AddPicture("Graph_" + rowIndex, image);
                excelImage.From.Column = colIndex;
                excelImage.From.Row = rowIndex;
                excelImage.SetSize(width, height);
                
                // 2x2 px space for better alignment
                excelImage.From.ColumnOff = Utility.Pixel2MTU(2);
                excelImage.From.RowOff = Utility.Pixel2MTU(2);
            }
        }
        

        protected void SetWorkSheetProperty(int sheetIndex, string workSheetName, string headerBackColor, List<string> headerFirstRow, List<int> columnWidth)
        {
            WorkSheetProperty property = new WorkSheetProperty();
            property.sheetIndex = sheetIndex;
            property.name = workSheetName;
            property.headerBackColor = headerBackColor;
            property.headerFirstRow = headerFirstRow;
            property.columnWidth = columnWidth;

            workSheetPropertyList.Add(property);
        }

        protected void CreateSectionHeader(ExcelWorksheet ws, int rowIndex, WorkSheetProperty property, int sectionHeaderIndex)
        {
            int columnCount = property.headerFirstRow.Count;
            string headerBackColor = property.headerBackColor;
            string headerStr = property.sectionHeader[sectionHeaderIndex];
            ws.Row(rowIndex).Height = EXCEL_ROW_HEIGHT;
            ws.Cells[rowIndex, 1].Value = headerStr;
            ws.Cells[rowIndex, 1, rowIndex, columnCount].Merge = true;
            ws.Cells[rowIndex, 1, rowIndex, columnCount].Style.Font.Bold = true;
            ws.Cells[rowIndex, 1, rowIndex, columnCount].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            ExcelFill fill;
            var cell = ws.Cells[rowIndex, 1];
            fill = cell.Style.Fill;
            fill.PatternType = ExcelFillStyle.Solid;

            fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(headerBackColor));

            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell.Style.Font.Bold = true;
            cell.Style.Font.Color.SetColor(Color.White);

            SetBorderStyle(cell.Style.Border, EXCEL_BORDER_COLOR);
            
        }

        protected void CreateFooter(int rowIndex, ExcelWorksheet ws, WorkSheetProperty property)
        {
            int columnCount = property.headerFirstRow.Count;

            ws.Row(rowIndex).Height = EXCEL_ROW_HEIGHT;
            SetCellValue(ws, rowIndex, 1, property.footerNotes, ExcelHorizontalAlignment.Left, 0, 15);
            
            for (int i = 1; i <= columnCount; i++)
            {
                SetBorderStyle(ws.Cells[rowIndex, i].Style.Border, EXCEL_BORDER_COLOR);

            }

            ws.Cells[rowIndex, 1, rowIndex, columnCount].Merge = true;
        }
    }

    public class WorkSheetProperty
    {
        public int sheetIndex { get; set; }
        public int columnCount { get; set; }
        public string name { get; set; }
        public string viewName { get; set; }
        public string headerBackColor { get; set; }
        public string footerNotes { get; set; }
        public string fontName { get; set; }
        public int fontSize { get; set; }
        public List<string> headerFirstRow { get; set; }
        public List<string> headerSecondRow { get; set; }
        public List<string> sectionHeader { get; set; }
        public List<int> columnWidth { get; set; }
    }
}