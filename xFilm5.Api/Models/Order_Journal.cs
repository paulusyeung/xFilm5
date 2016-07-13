namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Journal
    {
        public int ID { get; set; }

        public int OrderID { get; set; }

        public int? Status { get; set; }

        public int? UserID { get; set; }

        public DateTime? DateUpdated { get; set; }

        public virtual OrderHeader OrderHeader { get; set; }

        public virtual T_Workflow T_Workflow { get; set; }
    }
}
