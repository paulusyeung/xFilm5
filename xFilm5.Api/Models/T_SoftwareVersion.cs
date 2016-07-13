namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_SoftwareVersion
    {
        public int ID { get; set; }

        public int SoftwareID { get; set; }

        [StringLength(10)]
        public string VersionNumber { get; set; }

        public virtual T_Software T_Software { get; set; }
    }
}
