﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class etronEntities : DbContext
    {
        public etronEntities()
            : base("name=etronEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cat> cats { get; set; }
        public virtual DbSet<news> news { get; set; }
        public virtual DbSet<product_img> product_img { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<video> videos { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
