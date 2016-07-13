namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Z_WebColor
    {
        [Key]
        public int ColorID { get; set; }

        [StringLength(6)]
        public string Code { get; set; }

        [StringLength(32)]
        public string Name { get; set; }
    }
}
