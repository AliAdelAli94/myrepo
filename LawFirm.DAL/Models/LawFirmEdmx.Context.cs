﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LawFirmEntities : DbContext
    {
        public LawFirmEntities()
            : base("name=LawFirmEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
    }
}
