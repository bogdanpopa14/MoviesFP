﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Movies.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MovieFPEntities : DbContext
    {
        public MovieFPEntities()
            : base("name=MovieFPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Ratting> Ratting { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
