//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace xFilm5.EF6
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwOrderPkPrintQueueVpsList_Blueprint
    {
        public int OrderHeaderId { get; set; }
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public int PrintQueueVpsId { get; set; }
        public string VpsFileName { get; set; }
        public string VpsPlateSize { get; set; }
        public System.DateTime VpsCreatedOn { get; set; }
        public bool CheckedPlate { get; set; }
        public bool CheckedCip3 { get; set; }
        public bool CheckedBlueprint { get; set; }
        public bool IsReady { get; set; }
        public bool IsReceived { get; set; }
        public bool IsBilled { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public bool Retired { get; set; }
        public Nullable<System.DateTime> RetiredOn { get; set; }
        public string RetiredBy { get; set; }
    }
}
