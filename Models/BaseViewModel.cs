using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Discoverx.Models;
using Discoverx.Models.CBIS_DB;
using System.Windows.Forms;


namespace Discoverx.Models
{
    public class BaseViewModel
    {
        public Staging_DxFremontProductInfoEntities CBIS_DxFremontProductInfo_ctx;
        public HTSContractsEntities CBIS_HTSContracts_ctx;
        public DiscoveRx_OrdersEntities CBIS_DiscoveRx_Orders_ctx;
        public KinomeScan_RequestManagerEntities CBIS_KinomeScan_RequestManager_ctx;
        
        public List<ViewItem> viewList;
        public PDFModel pdfModel = new PDFModel();

        
        internal readonly int EXCEL_ROW_HEIGHT = 25;

        public BaseViewModel()
        {
            CBIS_DxFremontProductInfo_ctx = new Staging_DxFremontProductInfoEntities();
            CBIS_HTSContracts_ctx = new HTSContractsEntities();
            CBIS_DiscoveRx_Orders_ctx = new DiscoveRx_OrdersEntities();
            CBIS_KinomeScan_RequestManager_ctx = new KinomeScan_RequestManagerEntities();

        }
    }

       
    public class ViewItem
    {
        public bool display { get; set; }
        public string viewName { get; set; }  
    }
}