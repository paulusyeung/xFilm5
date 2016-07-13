namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderHeader")]
    public partial class OrderHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderHeader()
        {
            Acct_INMaster = new HashSet<Acct_INMaster>();
            Order_Details = new HashSet<Order_Details>();
            Order_Internal = new HashSet<Order_Internal>();
            Order_Journal = new HashSet<Order_Journal>();
            OrderComment = new HashSet<OrderComment>();
            PrintQueue = new HashSet<PrintQueue>();
        }

        public int ID { get; set; }

        public int ClientID { get; set; }

        public int? UserID { get; set; }

        public int? ServiceType { get; set; }

        public int? PrePressOp { get; set; }

        public int? ProofingOp { get; set; }

        public bool Attachment { get; set; }

        [StringLength(255)]
        public string AttachmentURL { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public DateTime DateReceived { get; set; }

        public DateTime? DateCompleted { get; set; }

        public int? Status { get; set; }

        public int? Priority { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public bool Paid { get; set; }

        public DateTime? PaidOn { get; set; }

        [StringLength(32)]
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
        public virtual ICollection<OrderComment> OrderComment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrintQueue> PrintQueue { get; set; }

        public virtual T_Priority T_Priority { get; set; }

        public virtual T_Service T_Service { get; set; }

        public virtual T_Status T_Status { get; set; }
    }
}
