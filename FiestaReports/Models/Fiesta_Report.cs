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
    
    public partial class Fiesta_Report
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fiesta_Report()
        {
            this.Fiesta_EmpStoreReport = new HashSet<Fiesta_EmpStoreReport>();
        }
    
        public int ReportId { get; set; }
        public string ReportName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fiesta_EmpStoreReport> Fiesta_EmpStoreReport { get; set; }
    }
}
