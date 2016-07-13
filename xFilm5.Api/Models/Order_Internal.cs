namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Internal
    {
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int? OutputBy { get; set; }

        public bool A4 { get; set; }

        public short? A4Qty { get; set; }

        public bool A3 { get; set; }

        public short? A3Qty { get; set; }

        public bool A4SetWProof { get; set; }

        public short? A4SetWProofQty { get; set; }

        public bool A3SetWProof { get; set; }

        public short? A3SetWProofQty { get; set; }

        public bool OtherSize { get; set; }

        public short? OtherSizeW { get; set; }

        public short? OtherSizeH { get; set; }

        public short? OtherSizeQty { get; set; }

        public bool Proofing4C { get; set; }

        public short? Proofing4CQty { get; set; }

        public bool ProofingSpotColor { get; set; }

        public short? ProofingSpotColorQty { get; set; }

        public bool ProofingAdditional { get; set; }

        public short? ProofingAdditionalQty { get; set; }

        public bool ArtworkFee { get; set; }

        [Column(TypeName = "money")]
        public decimal? ArtworkFeeAmount { get; set; }

        public bool UrgentCharge { get; set; }

        public bool Reprint { get; set; }

        [StringLength(32)]
        public string ReprintText { get; set; }

        public DateTime? DateUpdated { get; set; }

        public short? UpdateCounter { get; set; }

        public virtual OrderHeader OrderHeader { get; set; }
    }
}
