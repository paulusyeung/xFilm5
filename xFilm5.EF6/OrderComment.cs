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
    
    public partial class OrderComment
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public Nullable<int> OrderID { get; set; }
    
        public virtual OrderHeader OrderHeader { get; set; }
    }
}
