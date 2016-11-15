namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PrintQueue")]
    public partial class PrintQueue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PrintQueue()
        {
            OrderPkPrintQ = new HashSet<OrderPkPrintQ>();
            PrintQueue_VPS = new HashSet<PrintQueue_VPS>();
            PrintQueue_LifeCycle = new HashSet<PrintQueue_LifeCycle>();
        }

        public int ID { get; set; }

        public int ClientID { get; set; }

        public int? OrderID { get; set; }

        [Required]
        [StringLength(16)]
        public string CupsJobID { get; set; }

        [Required]
        [StringLength(255)]
        public string CupsJobTitle { get; set; }

        [StringLength(16)]
        public string PlateSize { get; set; }

        public bool? BlueprintOrdered { get; set; }

        public int Status { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public bool Retired { get; set; }

        public DateTime? RetiredOn { get; set; }

        public int? RetiredBy { get; set; }

        public virtual Client Client { get; set; }

        public virtual OrderHeader OrderHeader { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPkPrintQ> OrderPkPrintQ { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrintQueue_VPS> PrintQueue_VPS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PrintQueue_LifeCycle> PrintQueue_LifeCycle { get; set; }
    }
}
