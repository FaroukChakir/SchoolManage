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
    
    public partial class Prof
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prof()
        {
            this.Exam = new HashSet<Exam>();
        }
    
        public string CNE { get; set; }
        public int id_matiere { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string adresse { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string Tel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exam> Exam { get; set; }
        public virtual matiere matiere { get; set; }
    }
}
