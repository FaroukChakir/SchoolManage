﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManagement
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ecoleEntities : DbContext
    {
        public ecoleEntities()
            : base("name=ecoleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Absence> Absence { get; set; }
        public virtual DbSet<admin> admin { get; set; }
        public virtual DbSet<Etudiant> Etudiant { get; set; }
        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<Filiere> Filiere { get; set; }
        public virtual DbSet<Groupe> Groupe { get; set; }
        public virtual DbSet<matiere> matiere { get; set; }
        public virtual DbSet<note> note { get; set; }
        public virtual DbSet<Prof> Prof { get; set; }
        public virtual DbSet<seance> seance { get; set; }
    }
}
