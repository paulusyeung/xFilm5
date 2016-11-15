namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserPreference")]
    public partial class UserPreference
    {
        [Key]
        public int PreferenceId { get; set; }

        public int UserId { get; set; }

        public Guid ObjectId { get; set; }

        public int ObjectType { get; set; }

        [Column(TypeName = "xml")]
        public string MetadataXml { get; set; }

        public virtual User User { get; set; }
    }
}
