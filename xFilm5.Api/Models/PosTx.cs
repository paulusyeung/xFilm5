namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PosTx")]
    public partial class PosTx
    {
        [Key]
        public int TxID { get; set; }

        public DateTime TxDate { get; set; }

        public int? Branch { get; set; }

        public int TxPosID { get; set; }

        public int TxType { get; set; }

        [StringLength(32)]
        public string TxRef { get; set; }

        [Column(TypeName = "money")]
        public decimal? TxAmount { get; set; }

        public int? TxAmountType { get; set; }

        public int? CreatedBy { get; set; }

        [StringLength(256)]
        public string Remarks { get; set; }
    }
}
