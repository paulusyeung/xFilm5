namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class X_Counter
    {
        [Key]
        public int CounterID { get; set; }

        public int? ClientID { get; set; }

        public int? UserID { get; set; }

        public int? StaffID { get; set; }

        public int? OrderID { get; set; }

        public int? InvoiceNo { get; set; }
    }
}
