namespace xFilm5.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client_AddressBook
    {
        public int ID { get; set; }

        public int ClientID { get; set; }

        public bool PrimaryAddr { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(32)]
        public string Tel { get; set; }

        [StringLength(32)]
        public string Fax { get; set; }

        [StringLength(64)]
        public string ContactPerson { get; set; }

        [StringLength(32)]
        public string Mobile { get; set; }

        [StringLength(32)]
        public string Pager { get; set; }

        [StringLength(32)]
        public string SMS { get; set; }

        public int? SMS_Lang { get; set; }

        public DateTime? CreatedOn { get; set; }

        public virtual Client Client { get; set; }
    }
}
