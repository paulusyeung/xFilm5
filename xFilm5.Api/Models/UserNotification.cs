namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserNotification")]
    public partial class UserNotification
    {
        [Key]
        public int NotifyId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(64)]
        public string DeviceId { get; set; }

        public int DeviceType { get; set; }

        public int Plateform { get; set; }

        [Column(TypeName = "xml")]
        public string MetadataXml { get; set; }

        public virtual User User { get; set; }
    }
}
