//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EagleDigital.DbFirst.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class RequestProject
    {
        public int Id { get; set; }
        public Nullable<int> DomainId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Contact { get; set; }
    
        public virtual Domain Domain { get; set; }
    }
}
