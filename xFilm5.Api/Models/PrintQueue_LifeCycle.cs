namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PrintQueue_LifeCycle
    {
        [Key]
        public int LifeCycleId { get; set; }

        public int PrintQueueId { get; set; }

        public int PrintQSubitemType { get; set; }

        public int Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public virtual PrintQueue PrintQueue { get; set; }
    }
}
