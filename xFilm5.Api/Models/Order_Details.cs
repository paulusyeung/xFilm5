namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Details
    {
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int? Media { get; set; }

        public int? Platform { get; set; }

        [StringLength(32)]
        public string Software { get; set; }

        public int? DeliveryMethod { get; set; }

        public int? DeliveryAddr { get; set; }

        public bool Proofing { get; set; }

        public int? ProofingWith { get; set; }

        public int? ProofingQty { get; set; }

        public int? LineScreensResolution { get; set; }

        public bool StandardSize { get; set; }

        public int? SizeID { get; set; }

        public bool A4Size { get; set; }

        public bool A3Size { get; set; }

        public bool OtherSize { get; set; }

        [StringLength(32)]
        public string OtherSizeText { get; set; }

        public bool Positive { get; set; }

        public bool Negative { get; set; }

        public bool EmulsionUp { get; set; }

        public bool EmulsionDown { get; set; }

        public bool PrintComposite { get; set; }

        public bool BlackPlateOnly { get; set; }

        public short? Pages { get; set; }

        public bool Separation { get; set; }

        public bool SeparationC { get; set; }

        public bool SeparationM { get; set; }

        public bool SeparationY { get; set; }

        public bool SeparationK { get; set; }

        public bool DieCut { get; set; }

        public bool DieCutB { get; set; }

        public bool DieCutM { get; set; }

        public bool DieCutC { get; set; }

        public bool SpotColor { get; set; }

        [StringLength(32)]
        public string SpotColorText { get; set; }

        public bool CropMarks { get; set; }

        public bool BlackOverprint { get; set; }

        public bool RegistrationMarks { get; set; }

        public short? TotalFilms { get; set; }

        public bool ColorPrint { get; set; }

        public short? ColorPrintType { get; set; }

        public short? ColorPrintQty { get; set; }

        public bool ColorScan { get; set; }

        public short? ColorScanType { get; set; }

        public short? ColorScanQty { get; set; }

        public bool Barcode { get; set; }

        public short? BarcodeQty { get; set; }

        public bool Artwork { get; set; }

        [Column(TypeName = "money")]
        public decimal? ArtworkAmount { get; set; }

        public virtual OrderHeader OrderHeader { get; set; }

        public virtual T_DeliveryMethod T_DeliveryMethod { get; set; }

        public virtual T_Media T_Media { get; set; }

        public virtual T_PaperSize T_PaperSize { get; set; }

        public virtual T_Platform T_Platform { get; set; }
    }
}
