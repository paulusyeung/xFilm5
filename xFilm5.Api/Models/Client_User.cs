namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client_User()
        {
            OrderHeader = new HashSet<OrderHeader>();
            ReceiptHeader = new HashSet<ReceiptHeader>();
        }

        public int ID { get; set; }

        public int ClientID { get; set; }

        public bool PrimaryUser { get; set; }

        [StringLength(64)]
        public string FullName { get; set; }

        [StringLength(32)]
        public string Password { get; set; }

        public short? SecurityLevel { get; set; }

        [StringLength(64)]
        public string Email { get; set; }

        [StringLength(32)]
        public string LastIP { get; set; }

        public DateTime? LastVisit { get; set; }

        public int? Branch { get; set; }

        public virtual Client Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderHeader> OrderHeader { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceiptHeader> ReceiptHeader { get; set; }
    }
}
