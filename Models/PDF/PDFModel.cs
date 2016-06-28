using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Configuration;
using System.Web.Mvc;

using HiQPdf;

namespace Discoverx.Models
{
    public class PDFModel
    {
        private readonly HtmlViewRenderer htmlViewRenderer;
        private readonly PDFManager standardPdfRenderer;

        private readonly string PRICE_LIST_FOOTER = @"FOR INTERNAL USE ONLY";
        private readonly string TARGET_LIST_FOOTER = @"";
        private readonly string PHSTUDY_REPORT_FOOTER = @"Confidential";

        public PDFModel()
        {
            this.htmlViewRenderer = new HtmlViewRenderer();
            this.standardPdfRenderer = new PDFManager();
        }

         private byte[] ConvertHTMLtoPDF(string html, string fileName, string fileType, string Selector = "")
        {
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            htmlToPdfConverter.SerialNumber = Utility.GetAPPSettingKey("HiQPDFSerialNumber");

            if (fileType == "PHStudyReport")
            {
                htmlToPdfConverter.Document.Margins.Left = 50;
                htmlToPdfConverter.Document.Margins.Right = 30;

                // set a handler for PageCreatingEvent where to configure the PDF document pages
                htmlToPdfConverter.PageCreatingEvent += 
                        new PdfPageCreatingDelegate(htmlToPdfConverter_PageCreatingEvent);
            }
            else
            {
                htmlToPdfConverter.Document.Margins.Left = 20;
                htmlToPdfConverter.Document.Margins.Right = 20;
            }

            SetHeader(htmlToPdfConverter.Document, fileType);
            SetFooter(htmlToPdfConverter.Document, fileType);

           
            if (Selector!="")
            { 
                htmlToPdfConverter.ConvertedHtmlElementSelector = Selector;
            }
            
            byte[] buffer = htmlToPdfConverter.ConvertHtmlToMemory(html, "www.discoverx.com");
            //htmlToPdfConverter.ConvertHtmlToFile(html, GetFullRootUrl(), "c:\\result.pdf");
            return buffer;
        }

        private void htmlToPdfConverter_PageCreatingEvent(PdfPageCreatingParams eventParams)
        {
            PdfPage pdfPage = eventParams.PdfPage;
            int pdfPageNumber = eventParams.PdfPageNumber;

            if (pdfPageNumber == 1)
            {
                // set the header and footer visibility in first page

                pdfPage.DisplayFooter = false;
            }
            else
            {
                //int pageCount = eventParams.PdfDocument.Pages.Count;
                //if (pageCount == pdfPageNumber)
                //{
                //    AddSignatureFooter(pdfPage);
                    
                //}
                //// set the header and footer visibility in second page
                //pdfPage.DisplayHeader = checkBoxDisplayHeaderInSecondPage.Checked;
                //pdfPage.DisplayFooter = checkBoxDisplayFooterInSecondPage.Checked;

                //if (pdfPage.DisplayHeader && checkBoxCustomizedHeaderInSecondPage.Checked)
                //{
                //    // override the default document header in this page
                //    // with a customized header of 200 points in height
                //    pdfPage.CreateHeaderCanvas(200);

                //    // layout a HTML document in header
                //    PdfHtml htmlInHeader = new PdfHtml("http://www.google.com");
                //    htmlInHeader.FitDestHeight = true;
                //    pdfPage.Header.Layout(htmlInHeader);

                //    // create a border for the customized header
                //    PdfRectangle borderRectangle = new PdfRectangle(0, 0,
                //                        pdfPage.Header.Width - 1, pdfPage.Header.Height - 1);
                //    borderRectangle.LineStyle.LineWidth = 0.5f;
                //    borderRectangle.ForeColor = System.Drawing.Color.Navy;
                //    pdfPage.Header.Layout(borderRectangle);
                //}
            }
        }

        private void AddSignatureFooter(PdfPage pdfPage)
        {
            pdfPage.CreateFooterCanvas(300);

            pdfPage.Footer.Layout(new PdfHtml(20, 10, "<span style=\"font-family:Verdana; font-size:14px; color:black; text-decoration:none\">This is to certify that the data contained within this report was conducted as described above.</span>", null));

            string signImageFile = HttpContext.Current.Server.MapPath(Utility.GetAPPSettingKey("ImageFolder") + "PHStudyReport/signiture.png");
            PdfImage signImage = new PdfImage(20, 50, 218, Image.FromFile(signImageFile));
            pdfPage.Footer.Layout(signImage);

            pdfPage.Footer.Layout(new PdfHtml(20, 260, "<p style=\"font-family:Verdana; font-size:14px; color:black; text-decoration:none\">Dr. N. W. Charter<br><br>Director, Profiling Services</p>", null));

            Font pageFooterFont =
                new Font(new System.Drawing.FontFamily("Verdana"),
                            5, System.Drawing.GraphicsUnit.Point);

            pdfPage.Footer.Layout(new PdfHtml(0, 300, "<a href=\"http://www.discoverx.com\" style=\"font-family:Verdana; font-size:9px; color:black; text-decoration:none\">http://www.discoverx.com</a>", null));

            string footerStr = string.Empty;
           

            // layout HTML in footer
            //footerHtml.FitDestHeight = true;
            PdfText centerText = new PdfText(10, 10,
                    footerStr, pageFooterFont);
            centerText.HorizontalAlign = PdfTextHAlign.Center;
            centerText.EmbedSystemFont = true;
            centerText.ForeColor = System.Drawing.Color.Black;
            pdfPage.Footer.Layout(centerText);
            //htmlToPdfDocument.Footer.Layout(new PdfHtml(200, 5, footerStr, null));

            PdfText rightText = new PdfText(10, 15,
                   string.Format("Q{0} - {1}", DateTime.Now.GetQuarter(), DateTime.Now.Year), pageFooterFont);
            rightText.HorizontalAlign = PdfTextHAlign.Right;
            rightText.EmbedSystemFont = true;
            rightText.ForeColor = System.Drawing.Color.Black;
            pdfPage.Footer.Layout(rightText);
            // layout HTML in footer
            PdfHtml footerHtml = new PdfHtml(5, 5, footerStr, null);
            footerHtml.FitDestHeight = true;
            footerHtml.FontEmbedding = true;
            pdfPage.Footer.Layout(footerHtml);


            float footerHeight = pdfPage.Footer.Height;
            float footerWidth = pdfPage.Footer.Width;

            // add page numbering
            Font pageNumberFont = new Font(new FontFamily("Verdana"), 6, GraphicsUnit.Point);
            //pageNumberingFont.Mea

            PdfText pageNumberingText = new PdfText(5, footerHeight - 25, "{CrtPage}", pageNumberFont);
            pageNumberingText.HorizontalAlign = PdfTextHAlign.Center;
            pageNumberingText.EmbedSystemFont = true;
            pageNumberingText.ForeColor = Color.Black;
            pdfPage.Footer.Layout(pageNumberingText);

        }


        private void SetHeader(PdfDocumentControl htmlToPdfDocument, string fileType)
        {
            // enable header display
            htmlToPdfDocument.Header.Enabled = true;

            // set header height
            htmlToPdfDocument.Header.Height =95;
            if (fileType == "PHStudyReport")
            {

                float pdfPageWidth =
                    htmlToPdfDocument.PageOrientation == PdfPageOrientation.Portrait ?
                            htmlToPdfDocument.PageSize.Width : htmlToPdfDocument.PageSize.Height;

                float headerWidth = pdfPageWidth - htmlToPdfDocument.Margins.Left
                            - htmlToPdfDocument.Margins.Right;
                float headerHeight = htmlToPdfDocument.Header.Height;

                // set header background color
                //htmlToPdfDocument.Header.BackgroundColor = System.Drawing.Color.WhiteSmoke;


                string headerImageFile1 = HttpContext.Current.Server.MapPath(Utility.GetAPPSettingKey("ImageFolder") + "PHStudyReport/DrxLogo.jpg");
                PdfImage logoHeaderImage1 = new PdfImage(0, 45, 150, System.Drawing.Image.FromFile(headerImageFile1));
                htmlToPdfDocument.Header.Layout(logoHeaderImage1);

                string headerImageFile2 = HttpContext.Current.Server.MapPath(Utility.GetAPPSettingKey("ImageFolder") + "PHStudyReport/LeadHunterLogo.jpg");
                PdfImage logoHeaderImage2 = new PdfImage(340, 45, 150, System.Drawing.Image.FromFile(headerImageFile2));
                htmlToPdfDocument.Header.Layout(logoHeaderImage2);

                // layout HTML in header
                //            PdfHtml headerHtml = new PdfHtml(5, 5,
                //                    @"<span style=""color:Navy; font-family:Times New Roman; font-style:italic"">
                //                    Powered by </span><a href=""http://www.DiscoverX.com"">DiscoverX</a>",
                //                    null);
                //            headerHtml.FitDestHeight = true;
                //            htmlToPdfDocument.Header.Layout(headerHtml);

                // create a border for header

               
                //PdfRectangle borderRectangle = new PdfRectangle(1, 1, headerWidth - 2, headerHeight - 2);
                //borderRectangle.LineStyle.LineWidth = 0.5f;
                //borderRectangle.ForeColor = System.Drawing.Color.Navy;
                //htmlToPdfDocument.Header.Layout(borderRectangle);
                
            }
        }

        private void SetFooter(PdfDocumentControl htmlToPdfDocument, string fileType = "PriceList")
        {
            // enable footer display
            htmlToPdfDocument.Footer.Enabled = true;

            // set footer height
            htmlToPdfDocument.Footer.Height = 45;

            // set footer background color
            //htmlToPdfDocument.Footer.BackgroundColor = System.Drawing.Color.WhiteSmoke;

            float pdfPageWidth =
                    htmlToPdfDocument.PageOrientation == PdfPageOrientation.Portrait ?
                    htmlToPdfDocument.PageSize.Width : htmlToPdfDocument.PageSize.Height;

            float footerWidth = pdfPageWidth - htmlToPdfDocument.Margins.Left - htmlToPdfDocument.Margins.Right;
            float footerHeight = htmlToPdfDocument.Footer.Height;


            

            Font pageNumberingFont =
                new Font(new System.Drawing.FontFamily("Verdana"),
                            8, System.Drawing.GraphicsUnit.Point);

            if (fileType == "PHStudyReport")
            {
                htmlToPdfDocument.Footer.Layout(new PdfHtml(0, 10, "<a href=\"http://www.discoverx.com\" style=\"font-family:Arial; font-size:16px; color:black; text-decoration:none\">http://www.discoverx.com</a>", null));
            }
            else
            {
                string logoImageFile = HttpContext.Current.Server.MapPath(Utility.GetAPPSettingKey("ImageFolder") + "PDFCover/DRx_logo_2013.png");
                PdfImage logoImage = new PdfImage(5, 5, 40, Image.FromFile(logoImageFile));
                htmlToPdfDocument.Footer.Layout(logoImage);
                htmlToPdfDocument.Footer.Layout(new PdfHtml(0, 10, "<a href=\"http://www.discoverx.com\" style=\"font-family:Verdana; font-size:9px; color:black; text-decoration:none\">http://www.discoverx.com</a>", null));
            }
            string footerStr = string.Empty;
            if (fileType == "PriceList")
                footerStr = PRICE_LIST_FOOTER;
            else if (fileType == "TargetList")
                footerStr = TARGET_LIST_FOOTER;
            else if (fileType == "PHStudyReport")
            {
                footerStr = PHSTUDY_REPORT_FOOTER;
            }

            // layout HTML in footer
            //footerHtml.FitDestHeight = true;
            PdfText centerText = new PdfText(5, 10,
                    footerStr, pageNumberingFont);
            centerText.HorizontalAlign = PdfTextHAlign.Center;
            centerText.EmbedSystemFont = true;
            centerText.ForeColor = System.Drawing.Color.Black;
            htmlToPdfDocument.Footer.Layout(centerText);
            //htmlToPdfDocument.Footer.Layout(new PdfHtml(200, 5, footerStr, null));

            PdfText rightText;
            if (fileType == "PHStudyReport")
            {
                rightText = new PdfText(5, 10,
                   string.Format("{0}", DateTime.Now.ToShortDateString()), pageNumberingFont);
            }
            else { 
            rightText = new PdfText(5, 10,
                   string.Format("Q{0} - {1}", DateTime.Now.GetQuarter(), DateTime.Now.Year), pageNumberingFont);
            }
            rightText.HorizontalAlign = PdfTextHAlign.Right;
            rightText.EmbedSystemFont = true;
            rightText.ForeColor = System.Drawing.Color.Black;
            htmlToPdfDocument.Footer.Layout(rightText);
            //htmlToPdfDocument.Footer.Layout(new PdfHtml(500, 5, 80, 6, footerStr, null));

            // add page numbering in a text element
            
            PdfText pageNumberingText = new PdfText(5, footerHeight - 25,
                    "{CrtPage}", pageNumberingFont);
            pageNumberingText.HorizontalAlign = PdfTextHAlign.Center;
            pageNumberingText.EmbedSystemFont = true;
            pageNumberingText.ForeColor = System.Drawing.Color.Black;
            htmlToPdfDocument.Footer.Layout(pageNumberingText);

            /*
            string footerImageFile = Server.MapPath("~") + @"\DemoFiles\Images\HiQPdfLogo.png";
            PdfImage logoFooterImage = new PdfImage(footerWidth - 40 - 5, 5, 40, 
                        System.Drawing.Image.FromFile(footerImageFile));
            htmlToPdfDocument.Footer.Layout(logoFooterImage);
            */

            /*
            // create a border for footer
            PdfRectangle borderRectangle = new PdfRectangle(1, 1, footerWidth - 2, footerHeight - 2);
            borderRectangle.LineStyle.LineWidth = 0.5f;
            borderRectangle.ForeColor = System.Drawing.Color.DarkGreen;
            htmlToPdfDocument.Footer.Layout(borderRectangle);
            */
        }

        private void SetHeader(PdfDocument document, string fileName = "")
        {
//            if (!checkBoxAddHeader.Checked)
//                return;

//            // create the document header
//            document.CreateHeaderCanvas(50);

//            // add PDF objects to the header canvas
//            string headerImageFile = Application.StartupPath + @"\DemoFiles\Images\HiQPdfLogo.png";
//            PdfImage logoHeaderImage = new PdfImage(5, 5, 40, Image.FromFile(headerImageFile));
//            document.Header.Layout(logoHeaderImage);

//            // layout HTML in header
//            PdfHtml headerHtml = new PdfHtml(50, 5, @"<span style=""color:Navy; font-family:Times New Roman; font-style:italic"">
//                    Quickly Create High Quality PDFs with </span><a href=""http://www.hiqpdf.com"">HiQPdf</a>", null);
//            headerHtml.FitDestHeight = true;
//            headerHtml.FontEmbedding = true;
//            document.Header.Layout(headerHtml);

//            // create a border for header
//            float headerWidth = document.Header.Width;
//            float headerHeight = document.Header.Height;
//            PdfRectangle borderRectangle = new PdfRectangle(1, 1, headerWidth - 2, headerHeight - 2);
//            borderRectangle.LineStyle.LineWidth = 0.5f;
//            borderRectangle.ForeColor = Color.Navy;
//            document.Header.Layout(borderRectangle);
        }

        private void SetFooter(PdfDocument document, string fileName = "")
        {

            //create the document footer
            document.CreateFooterCanvas(45);

            string logoImageFile = HttpContext.Current.Server.MapPath(Utility.GetAPPSettingKey("ImageFolder") + "PDFCover/DRx_logo_2013.png");
            PdfImage logoImage = new PdfImage(5, 5, 40, Image.FromFile(logoImageFile));
            document.Footer.Layout(logoImage);

            Font pageFooterFont =
                new Font(new System.Drawing.FontFamily("Verdana"),
                            5, System.Drawing.GraphicsUnit.Point);

            document.Footer.Layout(new PdfHtml(0, 10, "<a href=\"http://www.discoverx.com\" style=\"font-family:Verdana; font-size:9px; color:black; text-decoration:none\">http://www.discoverx.com</a>", null));

            string footerStr=string.Empty;
            if (fileName == "DRx_Price_List_")
                footerStr = PRICE_LIST_FOOTER;
            else if (fileName == "DRx_Target_List_")
                footerStr = TARGET_LIST_FOOTER;
           

            // layout HTML in footer
            //footerHtml.FitDestHeight = true;
            PdfText centerText = new PdfText(10, 10,
                    footerStr, pageFooterFont);
            centerText.HorizontalAlign = PdfTextHAlign.Center;
            centerText.EmbedSystemFont = true;
            centerText.ForeColor = System.Drawing.Color.Black;
            document.Footer.Layout(centerText);
            //htmlToPdfDocument.Footer.Layout(new PdfHtml(200, 5, footerStr, null));

            PdfText rightText = new PdfText(10, 15,
                   string.Format("Q{0} - {1}", DateTime.Now.GetQuarter(), DateTime.Now.Year), pageFooterFont);
            rightText.HorizontalAlign = PdfTextHAlign.Right;
            rightText.EmbedSystemFont = true;
            rightText.ForeColor = System.Drawing.Color.Black;
            document.Footer.Layout(rightText);
            // layout HTML in footer
            PdfHtml footerHtml = new PdfHtml(5, 5, footerStr, null);
            footerHtml.FitDestHeight = true;
            footerHtml.FontEmbedding = true;
            document.Footer.Layout(footerHtml);


            float footerHeight = document.Footer.Height;
            float footerWidth = document.Footer.Width;

            // add page numbering
            Font pageNumberFont = new Font(new FontFamily("Verdana"), 6, GraphicsUnit.Point);
            //pageNumberingFont.Mea

            PdfText pageNumberingText = new PdfText(5, footerHeight - 25, "Page {CrtPage}", pageNumberFont);
            pageNumberingText.HorizontalAlign = PdfTextHAlign.Center;
            pageNumberingText.EmbedSystemFont = true;
            pageNumberingText.ForeColor = Color.Black;
            document.Footer.Layout(pageNumberingText);

            //string footerImageFile = Application.StartupPath + @"\DemoFiles\Images\HiQPdfLogo.png";
            //PdfImage logoFooterImage = new PdfImage(footerWidth - 40 - 5, 5, 40, Image.FromFile(footerImageFile));
            //document.Footer.Layout(logoFooterImage);

            //// create a border for footer
            //PdfRectangle borderRectangle = new PdfRectangle(1, 1, footerWidth - 2, footerHeight - 2);
            //borderRectangle.LineStyle.LineWidth = 0.5f;
            //borderRectangle.ForeColor = Color.DarkGreen;
            //document.Footer.Layout(borderRectangle);
        }

        public void DownloadPDFSingleView(Controller controller, string viewName, object model, string  fileName, string fileType)
        {
            string htmlText = ConvertViewToHtmlText(controller, viewName, model);
            DownloadPDFFile(htmlText, fileName, fileType);
        }

        public string ConvertViewToHtmlText(Controller controller, string viewName, object model)
        {
             // Render the view html to a string.
            return this.htmlViewRenderer.RenderViewToString(controller, viewName, model);
        }

        public void DownloadPDFFile(string htmlText, string fileName, string fileType)
        {
            // Return the PDF as a binary stream to the client.
            byte[] pdfBuffer = ConvertHTMLtoPDF(htmlText, fileName, fileType, "#Content");

            DownloadPDFFile(pdfBuffer, fileName);
        }

        public void DownloadPDFFile(byte[] pdfBuffer, string fileName)
        {
            
            // inform the browser about the binary data format
            HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

            // let the browser know how to open the PDF document, attachment or inline, and the file name
            HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("attachment; filename=" + fileName + ".pdf; size={0}",
                        pdfBuffer.Length.ToString()));

            // write the PDF buffer to HTTP response
            HttpContext.Current.Response.BinaryWrite(pdfBuffer);

            // call End() method of HTTP response to stop ASP.NET page processing
            HttpContext.Current.Response.End();
        }

        internal void DownloadViewsToSinglePDFFile(List<ViewItem> viewList, string fileName)
        {
            String baseUrl = HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf("/") + 1);

            // create an empty PDF document
            PdfDocument document = new PdfDocument();
            document.SerialNumber = Utility.GetAPPSettingKey("HiQPDFSerialNumber");

            PdfDocument documentCover = new PdfDocument();
            documentCover.SerialNumber = Utility.GetAPPSettingKey("HiQPDFSerialNumber");

            PdfDocument documentList = new PdfDocument();
            documentList.SerialNumber = Utility.GetAPPSettingKey("HiQPDFSerialNumber");

            foreach (ViewItem viewItem in viewList)
            {
                if(viewItem.display)
                {
                    PdfPage page;
                    // add a page to document
                    if (viewItem.viewName == "CoverPage" || viewItem.viewName == "ContentPage")
                    {
                        
                        page = documentCover.AddPage(PdfPageSize.A4, new PdfDocumentMargins(0), PdfPageOrientation.Portrait);
                        //page.DisplayHeader = false;
                        //page.DisplayFooter = false;
                    }
                    
                    else
                    {
                        page = documentList.AddPage(PdfPageSize.A4, new PdfDocumentMargins(20), PdfPageOrientation.Portrait);
                        if (viewItem.viewName != "OrderingInformation" )
                        {
                            page.DisplayHeader = true;
                            page.DisplayFooter = true;

                            SetHeader(documentList, fileName);
                            SetFooter(documentList, fileName);
                        }
                        else
                        {
                            page.DisplayHeader = false;
                            page.DisplayFooter = false;
                        }
                    }

                    // layout the HTML from URL 
                    PdfHtml html = new PdfHtml(baseUrl + viewItem.viewName);
                    page.Layout(html);
                }
            }

            if (documentCover.Pages.Count > 0)
            {
                document.AddDocument(documentCover);
            }

            document.AddDocument(documentList);

            DownloadPDFFile(document.WriteToMemory(), fileName + DateTime.Now.ToShortDateString().Replace("/", "_"));
        }
        
    }

    
}