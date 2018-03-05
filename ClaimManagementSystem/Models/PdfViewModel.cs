using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClaimManagementSystem.Models
{
    public class PdfViewModel
    {
        public string ImagePath { get; set; }
        public string BgImagePath { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceDueDate { get; set; }
        public string Insured { get; set; }
        public string InsuredPolicy { get; set; }
        public string FileNumber { get; set; }
        public string Adjuster { get; set; }
        public string LostDate { get; set; }
        public string LostUnit { get; set; }
        public List<PdfBillingItems> BillingItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string CountryName { get; set; }
        public string ProvinceName { get; set; }
        public string PostalCode { get; set; }
        public string ContactName { get; set; }

    }

    public class PdfBillingItems
    {
        public string Item { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
    }
}