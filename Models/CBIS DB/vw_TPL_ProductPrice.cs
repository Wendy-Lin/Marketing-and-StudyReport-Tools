//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Discoverx.Models.CBIS_DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class vw_TPL_ProductPrice
    {
        public long ID { get; set; }
        public string PartNumber { get; set; }
        public string TARGET { get; set; }
        public string Brands { get; set; }
        public string DESCRIPTION { get; set; }
        public string Web_Target_Class { get; set; }
        public string Assay_Measures { get; set; }
        public string Assay_Readout { get; set; }
        public string Assay_Type { get; set; }
        public string Technology { get; set; }
        public string Web_Product_Category_Primary { get; set; }
        public string Web_Product_Category_Tertiary { get; set; }
        public string Web_Cellular_or_Biochemical { get; set; }
        public string Assay_Format { get; set; }
        public string Web_Product_Category_Secondary { get; set; }
        public string web_Target_Name { get; set; }
        public string WebCoTarget { get; set; }
        public string CONFIGURATION { get; set; }
        public string Date_Added { get; set; }
        public decimal PRICE_USD { get; set; }
        public decimal PRICE_GBP { get; set; }
        public decimal PRICE_EUR { get; set; }
        public decimal PRICE_CHF { get; set; }
        public decimal PRICE_DKK { get; set; }
        public decimal PRICE_SEK { get; set; }
        public Nullable<decimal> NFP_DISCOUNT { get; set; }
        public string Family { get; set; }
    }
}
