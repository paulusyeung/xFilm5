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
    
    public partial class vwUserNotificationList
    {
        public int NotifyId { get; set; }
        public string DeviceId { get; set; }
        public int NotifyType { get; set; }
        public int Platform { get; set; }
        public string NotifyXml { get; set; }
        public int AuthType { get; set; }
        public string AuthXml { get; set; }
        public int UserId { get; set; }
        public string UserAlias { get; set; }
        public Nullable<short> SecurityLevel { get; set; }
        public bool PrimaryUser { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
    }
}
