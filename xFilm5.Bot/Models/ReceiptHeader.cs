//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace xFilm5.Bot.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReceiptHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReceiptHeader()
        {
            this.ReceiptDetails = new HashSet<ReceiptDetail>();
        }
    
        public int ReceiptHeaderId { get; set; }
        public string ReceiptNumber { get; set; }
        public Nullable<System.DateTime> ReceiptDate { get; set; }
        public Nullable<decimal> ReceiptAmount { get; set; }
        public int ClientId { get; set; }
        public Nullable<int> PaymentType { get; set; }
        public Nullable<int> INMasterId { get; set; }
        public Nullable<int> ClientUserId { get; set; }
        public string Remarks { get; set; }
        public bool Paid { get; set; }
        public Nullable<System.DateTime> PaidOn { get; set; }
        public decimal PaidAmount { get; set; }
        public string PaidRef { get; set; }
        public int Status { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
    
        public virtual Acct_INMaster Acct_INMaster { get; set; }
        public virtual Client Client { get; set; }
        public virtual Client_User Client_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
        public virtual T_PaymentType T_PaymentType { get; set; }
    }
}