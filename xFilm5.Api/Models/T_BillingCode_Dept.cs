namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_BillingCode_Dept
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_BillingCode_Dept()
        {
            T_BillingCode_Item = new HashSet<T_BillingCode_Item>();
        }

        public int ID { get; set; }

        [StringLength(16)]
        public string Code { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_BillingCode_Item> T_BillingCode_Item { get; set; }
    }
}
