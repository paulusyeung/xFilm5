namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Acct_INMaster = new HashSet<Acct_INMaster>();
            Client_AddressBook = new HashSet<Client_AddressBook>();
            Client_User = new HashSet<Client_User>();
            OrderHeader = new HashSet<OrderHeader>();
            PrintQueue = new HashSet<PrintQueue>();
        }

        public int ID { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        public DateTime? CreatedOn { get; set; }

        public short? Status { get; set; }

        public short? CreditLimit { get; set; }

        public short? PaymentTerms { get; set; }

        public short? PaymentType { get; set; }

        [StringLength(16)]
        public string PIN { get; set; }

        public int? Branch { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Acct_INMaster> Acct_INMaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client_AddressBook> Client_AddressBook { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client_User> Client_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderHeader> OrderHeader { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrintQueue> PrintQueue { get; set; }
    }
}
