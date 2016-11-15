namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_BillingCode_Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_BillingCode_Item()
        {
            ClientPricing = new HashSet<ClientPricing>();
        }

        public int ID { get; set; }

        public int? DeptID { get; set; }

        [StringLength(16)]
        public string ItemCode { get; set; }

        [StringLength(16)]
        public string GroupCode { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(32)]
        public string UoM { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public bool Retired { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientPricing> ClientPricing { get; set; }

        public virtual T_BillingCode_Dept T_BillingCode_Dept { get; set; }
    }
}
