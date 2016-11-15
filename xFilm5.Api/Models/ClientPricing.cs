namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientPricing")]
    public partial class ClientPricing
    {
        [Key]
        public int PricingId { get; set; }

        public int ClientId { get; set; }

        public int ItemId { get; set; }

        [StringLength(255)]
        public string Alias { get; set; }

        [StringLength(64)]
        public string Tag { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public int Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int ModifiedBy { get; set; }

        public bool Retired { get; set; }

        public DateTime? RetiredOn { get; set; }

        public int? RetiredBy { get; set; }

        public virtual Client Client { get; set; }

        public virtual T_BillingCode_Item T_BillingCode_Item { get; set; }
    }
}
