namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReceiptDetail")]
    public partial class ReceiptDetail
    {
        public int ReceiptDetailId { get; set; }

        public int ReceiptHeaderId { get; set; }

        public int? OrderPkPrintQId { get; set; }

        [StringLength(16)]
        public string BillingCode { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public short? Qty { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitAmount { get; set; }

        public decimal? Discount { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        public virtual OrderPkPrintQ OrderPkPrintQ { get; set; }

        public virtual ReceiptHeader ReceiptHeader { get; set; }
    }
}
