//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FiestaReports.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fiesta_State
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fiesta_State()
        {
            this.Fiesta_EmpStates = new HashSet<Fiesta_EmpStates>();
        }
    
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fiesta_EmpStates> Fiesta_EmpStates { get; set; }
    }
}
