//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aries.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class product_img
    {
        public long img_id { get; set; }
        public string img_url { get; set; }
        public string img_title { get; set; }
        public string img_alt { get; set; }
        public Nullable<long> product_id { get; set; }
    
        public virtual product product { get; set; }
    }
}
