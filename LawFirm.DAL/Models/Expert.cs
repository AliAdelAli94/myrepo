//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LawFirm.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Expert
    {
        public Expert()
        {
            this.Experiences = new HashSet<Experience>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string title { get; set; }
        public string phone { get; set; }
        public string content1 { get; set; }
        public string quote { get; set; }
        public string content2 { get; set; }
        public string image { get; set; }
    
        public virtual ICollection<Experience> Experiences { get; set; }
    }
}
