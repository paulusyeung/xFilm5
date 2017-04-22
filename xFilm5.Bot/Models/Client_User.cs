//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace xFilm5.Bot.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Client_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client_User()
        {
            this.OrderHeader = new HashSet<OrderHeader>();
            this.ReceiptHeader = new HashSet<ReceiptHeader>();
        }
    
        public int ID { get; set; }
        public int ClientID { get; set; }
        public bool PrimaryUser { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public Nullable<short> SecurityLevel { get; set; }
        public string Email { get; set; }
        public string LastIP { get; set; }
        public Nullable<System.DateTime> LastVisit { get; set; }
        public Nullable<int> Branch { get; set; }
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderHeader> OrderHeader { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceiptHeader> ReceiptHeader { get; set; }
    }
}
