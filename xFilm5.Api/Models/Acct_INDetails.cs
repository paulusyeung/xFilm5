namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Acct_INDetails
    {
        public int ID { get; set; }

        public int INMasterID { get; set; }

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

        public virtual Acct_INMaster Acct_INMaster { get; set; }
    }
}
