using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiestaReports.Models
{
    public class EmployeeReports
    {
        public Fiesta_Store Store { get; set; }
        public Fiesta_Report Report { get; set; }
        public bool IsAssigned { get; set; }
    }
}