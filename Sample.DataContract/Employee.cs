using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.DataContract
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string EmployeeCode { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        public string EmailAddress { get; set; }
        public string Location { get; set; }
    }
}
