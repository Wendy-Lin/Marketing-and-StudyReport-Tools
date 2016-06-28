using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Discoverx.Models
{
    public class MvcRazorToPdf
    {
        public byte[] GeneratePdfOutput(ControllerContext context, object model = null, string viewName = null,
            Action<PdfWriter, Document> configureSettings = null)
        {
            if (viewName == null)
            {
                viewName = context.RouteData.GetRequiredString("action");
            }

            context.Controller.ViewData.Model = model;

            byte[] output;
            using (var document = new Document())
            {
                using (var workStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, workStream);
                    writer.CloseStream = false;

                    if (configureSettings != null)
                    {
                        configureSettings(writer, document);
                    }
                    document.Open();


                    using (var reader = new StringReader(RenderRazorView(context, viewName)))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, reader);

                        document.Close();
                        output = workStream.ToArray();
                    }
                }
            }
            return output;
        }

        public string RenderRazorView(ControllerContext context, string viewName)
        {
            IView viewEngineResult = ViewEngines.Engines.FindView(context, viewName, null).View;
            var sb = new StringBuilder();


            using (TextWriter tr = new StringWriter(sb))
            {
                var viewContext = new ViewContext(context, viewEngineResult, context.Controller.ViewData,
                    context.Controller.TempData, tr);
                viewEngineResult.Render(viewContext, tr);
            }
            return sb.ToString();
        }
    }

    public static class MvcRazorToPdfExtensions
    {
        public static byte[] GeneratePdf(this ControllerContext context, object model = null, string viewName = null,
            Action<PdfWriter, Document> configureSettings = null)
        {
            return new MvcRazorToPdf().GeneratePdfOutput(context, model, viewName, configureSettings);
        }
    }

    public class PdfActionResult : ActionResult
    {
        public PdfActionResult(string viewName, object model)
        {
            ViewName = viewName;
            Model = model;
        }

        public PdfActionResult(object model)
        {
            Model = model;
        }

        public PdfActionResult(object model, Action<PdfWriter, Document> configureSettings)
        {
            if (configureSettings == null) throw new ArgumentNullException("configureSettings");
            Model = model;
            ConfigureSettings = configureSettings;
        }

        public PdfActionResult(string viewName, object model, Action<PdfWriter, Document> configureSettings)
        {
            if (configureSettings == null) throw new ArgumentNullException("configureSettings");
            ViewName = viewName;
            Model = model;
            ConfigureSettings = configureSettings;
        }

        public string ViewName { get; set; }
        public object Model { get; set; }
        public Action<PdfWriter, Document> ConfigureSettings { get; set; }

        public string FileDownloadName { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            IView viewEngineResult;
            ViewContext viewContext;

            if (ViewName == null)
            {
                ViewName = context.RouteData.GetRequiredString("action");
            }

            context.Controller.ViewData.Model = Model;


            if (context.HttpContext.Request.QueryString["html"] != null &&
                context.HttpContext.Request.QueryString["html"].ToLower().Equals("true"))
            {
                RenderHtmlOutput(context);
            }
            else
            {
                if (!String.IsNullOrEmpty(FileDownloadName))
                {
                    context.HttpContext.Response.AddHeader("content-disposition",
                        "attachment; filename=" + FileDownloadName);
                }

                new FileContentResult(context.GeneratePdf(Model, ViewName, ConfigureSettings), "application/pdf")
                    .ExecuteResult(context);
            }
        }

        private void RenderHtmlOutput(ControllerContext context)
        {
            IView viewEngineResult = ViewEngines.Engines.FindView(context, ViewName, null).View;
            var viewContext = new ViewContext(context, viewEngineResult, context.Controller.ViewData,
                context.Controller.TempData, context.HttpContext.Response.Output);
            viewEngineResult.Render(viewContext, context.HttpContext.Response.Output);
        }
    }
    /// <summary>
    /// This class is responsible for rendering a html text string to a PDF document using the html renderer of iTextSharp.
    /// </summary>
    public class PDFManager
    {
        private const int HorizontalMargin = 40;
        private const int VerticalMargin = 40;

        public byte[] Render(string htmlText, string pageTitle)
        {
            byte[] renderedBuffer;

            using (var outputMemoryStream = new MemoryStream())
            {
                using (var pdfDocument = new Document(PageSize.A4, HorizontalMargin, HorizontalMargin, VerticalMargin, VerticalMargin))
                {
                    
        
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, outputMemoryStream);
                    pdfWriter.CloseStream = false;
                    pdfWriter.PageEvent = new PrintHeaderFooter { Title = pageTitle };
                    pdfDocument.Open();
                    using (var htmlViewReader = new StringReader(htmlText))
                    {
                        using (var htmlWorker = new HTMLWorker(pdfDocument))
                        {
                            htmlWorker.Parse(htmlViewReader);
                        }
                    }
                }

                renderedBuffer = new byte[outputMemoryStream.Position];
                outputMemoryStream.Position = 0;
                outputMemoryStream.Read(renderedBuffer, 0, renderedBuffer.Length);
            }

            return renderedBuffer;
        }
    }

    public class PrintHeaderFooter : PdfPageEventHelper
    {
        private PdfContentByte pdfContent;
        private PdfTemplate pageNumberTemplate;
        private BaseFont baseFont;
        private DateTime printTime;

        public string Title { get; set; }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            printTime = DateTime.Now;
            baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            pdfContent = writer.DirectContent;
            pageNumberTemplate = pdfContent.CreateTemplate(50, 50);
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);

            Rectangle pageSize = document.PageSize;

            if (Title != string.Empty)
            {
                pdfContent.BeginText();
                pdfContent.SetFontAndSize(baseFont, 11);
                pdfContent.SetRGBColorFill(0, 0, 0);
                pdfContent.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetTop(40));
                pdfContent.ShowText(Title);
                pdfContent.EndText();
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            int pageN = writer.PageNumber;
            string text = pageN + " - ";
            float len = baseFont.GetWidthPoint(text, 8);

            Rectangle pageSize = document.PageSize;
            pdfContent = writer.DirectContent;
            pdfContent.SetRGBColorFill(100, 100, 100);

            pdfContent.BeginText();
            pdfContent.SetFontAndSize(baseFont, 8);
            pdfContent.SetTextMatrix(pageSize.Width / 2, pageSize.GetBottom(30));
            pdfContent.ShowText(text);
            pdfContent.EndText();

            pdfContent.AddTemplate(pageNumberTemplate, (pageSize.Width / 2) + len, pageSize.GetBottom(30));

            pdfContent.BeginText();
            pdfContent.SetFontAndSize(baseFont, 8);
            pdfContent.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, printTime.ToString(), pageSize.GetRight(40), pageSize.GetBottom(30), 0);
            pdfContent.EndText();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            pageNumberTemplate.BeginText();
            pageNumberTemplate.SetFontAndSize(baseFont, 8);
            pageNumberTemplate.SetTextMatrix(0, 0);
            pageNumberTemplate.ShowText(string.Empty + (writer.PageNumber - 1));
            pageNumberTemplate.EndText();
        }
    }

    public class HtmlViewRenderer
    {
        public string RenderViewToString(Controller controller, string viewName, object viewData)
        {
            var renderedView = new StringBuilder();
            using (var responseWriter = new StringWriter(renderedView))
            {
                var fakeResponse = new HttpResponse(responseWriter);
                var fakeContext = new HttpContext(HttpContext.Current.Request, fakeResponse);
                var fakeControllerContext = new ControllerContext(new HttpContextWrapper(fakeContext), controller.ControllerContext.RouteData, controller.ControllerContext.Controller);

                var oldContext = HttpContext.Current;
                HttpContext.Current = fakeContext;

                using (var viewPage = new ViewPage())
                {
                    var html = new HtmlHelper(CreateViewContext(responseWriter, fakeControllerContext), viewPage);
                    html.RenderPartial(viewName, viewData);
                    HttpContext.Current = oldContext;
                }
            }

            return renderedView.ToString();
        }

        private static ViewContext CreateViewContext(TextWriter responseWriter, ControllerContext fakeControllerContext)
        {
            return new ViewContext(fakeControllerContext, new FakeView(), new ViewDataDictionary(), new TempDataDictionary(), responseWriter);
        }
    }

    public class BinaryContentResult : ActionResult
    {
        private readonly string contentType;
        private readonly byte[] contentBytes;
        private string fileName = "doc.pdf";

        public BinaryContentResult(byte[] contentBytes, string contentType)
        {
            this.contentBytes = contentBytes;
            this.contentType = contentType;
        }

        public BinaryContentResult(byte[] contentBytes, string contentType, string file)
        {
            this.contentBytes = contentBytes;
            this.contentType = contentType;
            this.fileName = file;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.ContentType = this.contentType;
            response.AddHeader("content-disposition", "attachment; filename=" + fileName);

            using (var stream = new MemoryStream(this.contentBytes))
            {
                stream.WriteTo(response.OutputStream);
                stream.Flush();
            }
        }
    }

    public class FakeView : IView
    {
        #region IView Members

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}