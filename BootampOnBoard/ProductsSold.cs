//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BootampOnBoard
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductsSold
    {
        public int Ps_Id { get; set; }
        public int Ps_CustomerId { get; set; }
        public int Ps_ProductId { get; set; }
        public int Ps_StoreId { get; set; }
        public System.DateTime DateSold { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
