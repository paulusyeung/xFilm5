namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PrintQueue_VPS
    {
        public int ID { get; set; }

        public int PrintQueueID { get; set; }

        [Required]
        [StringLength(256)]
        public string VpsFileName { get; set; }

        public bool PlateOrdered { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ModifiedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? Retired { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime RetiredOn { get; set; }

        public int? RetiredBy { get; set; }

        public virtual PrintQueue PrintQueue { get; set; }
    }
}
