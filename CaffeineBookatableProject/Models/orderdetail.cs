//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaffeineBookatableProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class orderdetail
    {
        public int orderdetail_id { get; set; }
        public int order_fid { get; set; }
        public int product_fid { get; set; }
        public decimal psale_price { get; set; }
        public decimal pp_price { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual order order { get; set; }
        public virtual Product Product { get; set; }
    }
}