namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            UserAuth = new HashSet<UserAuth>();
            UserNotification = new HashSet<UserNotification>();
            UserPreference = new HashSet<UserPreference>();
        }

        public int UserId { get; set; }

        public int UserType { get; set; }

        public Guid UserSid { get; set; }

        [Required]
        [StringLength(64)]
        public string LoginName { get; set; }

        [Required]
        [StringLength(64)]
        public string LoginPassword { get; set; }

        [Required]
        [StringLength(64)]
        public string Alias { get; set; }

        public int Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int ModifiedBy { get; set; }

        public bool Retired { get; set; }

        public DateTime? RetiredOn { get; set; }

        public int? RetiredBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserAuth> UserAuth { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserNotification> UserNotification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserPreference> UserPreference { get; set; }
    }
}
