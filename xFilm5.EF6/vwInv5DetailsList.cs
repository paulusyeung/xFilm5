//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace xFilm5.EF6
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwInv5DetailsList
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientTel { get; set; }
        public string ClientFax { get; set; }
        public int InvoiceHeaderId { get; set; }
        public Nullable<int> InvoiceNumber { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public Nullable<int> PaymentType { get; set; }
        public bool Paid { get; set; }
        public Nullable<System.DateTime> PaidOn { get; set; }
        public decimal PaidAmount { get; set; }
        public string PaidRef { get; set; }
        public string Remarks { get; set; }
        public Nullable<short> Status { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<short> ItemQty { get; set; }
        public string ItemUoM { get; set; }
        public Nullable<decimal> ItemUnitAmt { get; set; }
        public Nullable<decimal> ItemDiscount { get; set; }
        public Nullable<decimal> ItemAmount { get; set; }
        public Nullable<int> OrderPkPrintQueueVpsId { get; set; }
        public Nullable<int> OrderHeaderId { get; set; }
    }
}