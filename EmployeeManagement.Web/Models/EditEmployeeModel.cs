using EmployeeManagement.Api.CustomValidators;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Models
{
    public class EditEmployeeModel
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Your First Name Is look like invalid ,Not Empty , min len 2 char)")]
        [MinLength(2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Hello Dear Please Insert Valid Second Name ,not empty)")]
        public string LastName { get; set; }
        [EmailAddress]
        [EmailDomainValidator(AllowedDomain = "gmail.com")]
        public string Email { get; set; }
        [CompareProperty("Email",
        ErrorMessage = "Email and Confirm Email must match")]
        public string ConfirmEmail { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int DepartmentID { get; set; }
        public string PhotoPath { get; set; }
        public Department Department { get; set; }
    }
}
