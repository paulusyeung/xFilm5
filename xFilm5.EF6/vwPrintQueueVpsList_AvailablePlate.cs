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
    
    public partial class vwPrintQueueVpsList_AvailablePlate
    {
        public int ClientID { get; set; }
        public int OrderID { get; set; }
        public string CupsJobID { get; set; }
        public string CupsJobTitle { get; set; }
        public string PlateSize { get; set; }
        public Nullable<bool> BlueprintOrdered { get; set; }
        public int PrintQueueID { get; set; }
        public int Status { get; set; }
        public System.DateTime PrintedOn { get; set; }
        public bool Retired { get; set; }
        public int VpsPrintQueueID { get; set; }
        public string VpsFileName { get; set; }
        public System.DateTime VpsCreatedOn { get; set; }
        public bool VpsPlateOrdered { get; set; }
        public Nullable<bool> VpsRetired { get; set; }
        public Nullable<int> VpsAge { get; set; }
        public string ClientName { get; set; }
        public Nullable<int> OrderHeaderId { get; set; }
        public int WorkshopId { get; set; }
        public string WorkshopName { get; set; }
    }
}
