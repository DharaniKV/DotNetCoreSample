using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetCoreSample.Models
{
    public partial class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int DeptId { get; set; }
    }
}
