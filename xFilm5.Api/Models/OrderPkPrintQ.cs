namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderPkPrintQ")]
    public partial class OrderPkPrintQ
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderPkPrintQ()
        {
            Acct_INDetails = new HashSet<Acct_INDetails>();
            ReceiptDetail = new HashSet<ReceiptDetail>();
        }

        public int OrderPkPrintQId { get; set; }

        public int OrderHeaderId { get; set; }

        public int PrintQueueId { get; set; }

        public int PrintQSubitemType { get; set; }

        public decimal Qty { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Acct_INDetails> Acct_INDetails { get; set; }

        public virtual OrderHeader OrderHeader { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceiptDetail> ReceiptDetail { get; set; }

        public virtual PrintQueue PrintQueue { get; set; }
    }
}
