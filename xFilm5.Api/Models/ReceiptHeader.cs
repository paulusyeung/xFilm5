namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReceiptHeader")]
    public partial class ReceiptHeader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReceiptHeader()
        {
            ReceiptDetail = new HashSet<ReceiptDetail>();
        }

        public int ReceiptHeaderId { get; set; }

        [StringLength(16)]
        public string ReceiptNumber { get; set; }

        public DateTime? ReceiptDate { get; set; }

        [Column(TypeName = "money")]
        public decimal? ReceiptAmount { get; set; }

        public int ClientId { get; set; }

        public int? PaymentType { get; set; }

        public int? INMasterId { get; set; }

        public int? ClientUserId { get; set; }

        [StringLength(255)]
        public string Remarks { get; set; }

        public bool Paid { get; set; }

        public DateTime? PaidOn { get; set; }

        [Column(TypeName = "money")]
        public decimal PaidAmount { get; set; }

        [StringLength(128)]
        public string PaidRef { get; set; }

        public int Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int ModifiedBy { get; set; }

        public virtual Acct_INMaster Acct_INMaster { get; set; }

        public virtual Client Client { get; set; }

        public virtual Client_User Client_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceiptDetail> ReceiptDetail { get; set; }

        public virtual T_PaymentType T_PaymentType { get; set; }
    }
}
