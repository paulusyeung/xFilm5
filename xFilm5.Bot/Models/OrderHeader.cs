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
    
    public partial class OrderHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderHeader()
        {
            this.Acct_INMaster = new HashSet<Acct_INMaster>();
            this.Order_Details = new HashSet<Order_Details>();
            this.Order_Internal = new HashSet<Order_Internal>();
            this.Order_Journal = new HashSet<Order_Journal>();
            this.OrderComments = new HashSet<OrderComment>();
            this.OrderPkPrintQs = new HashSet<OrderPkPrintQ>();
            this.PrintQueues = new HashSet<PrintQueue>();
        }
    
        public int ID { get; set; }
        public int ClientID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> ServiceType { get; set; }
        public Nullable<int> PrePressOp { get; set; }
        public Nullable<int> ProofingOp { get; set; }
        public bool Attachment { get; set; }
        public string AttachmentURL { get; set; }
        public string Remarks { get; set; }
        public System.DateTime DateReceived { get; set; }
        public Nullable<System.DateTime> DateCompleted { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public bool Paid { get; set; }
        public Nullable<System.DateTime> PaidOn { get; set; }
        public string PaidRef { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Acct_INMaster> Acct_INMaster { get; set; }
        public virtual Client Client { get; set; }
        public virtual Client_User Client_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Internal> Order_Internal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Journal> Order_Journal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderComment> OrderComments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPkPrintQ> OrderPkPrintQs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrintQueue> PrintQueues { get; set; }
        public virtual T_Priority T_Priority { get; set; }
        public virtual T_Service T_Service { get; set; }
        public virtual T_Status T_Status { get; set; }
    }
}
