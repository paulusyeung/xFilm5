namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Acct_INMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Acct_INMaster()
        {
            Acct_INDetails = new HashSet<Acct_INDetails>();
        }

        public int ID { get; set; }

        public int? InvoiceNumber { get; set; }

        public DateTime? InvoiceDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? InvoiceAmount { get; set; }

        public int ClientID { get; set; }

        public int? OrderID { get; set; }

        public int? PaymentType { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public bool Paid { get; set; }

        public DateTime? PaidOn { get; set; }

        [Column(TypeName = "money")]
        public decimal PaidAmount { get; set; }

        [StringLength(128)]
        public string PaidRef { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public short? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Acct_INDetails> Acct_INDetails { get; set; }

        public virtual Client Client { get; set; }

        public virtual OrderHeader OrderHeader { get; set; }

        public virtual T_PaymentType T_PaymentType { get; set; }
    }
}
