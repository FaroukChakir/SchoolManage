//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Absence
    {
        public int id { get; set; }
        public string id_etudiant { get; set; }
        public int id_seance { get; set; }
        public Nullable<System.DateTime> DateAbsence { get; set; }
    
        public virtual Etudiant Etudiant { get; set; }
        public virtual seance seance { get; set; }
    }
}
