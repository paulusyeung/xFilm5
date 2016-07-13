namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderComment")]
    public partial class OrderComment
    {
        public int ID { get; set; }

        [Column(TypeName = "ntext")]
        public string Comment { get; set; }

        public int? OrderID { get; set; }

        public virtual OrderHeader OrderHeader { get; set; }
    }
}
