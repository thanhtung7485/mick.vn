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
    
    public partial class Domain
    {
        public Domain()
        {
            this.RequestProjects = new HashSet<RequestProject>();
        }
    
        public int Id { get; set; }
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<RequestProject> RequestProjects { get; set; }
    }
}
