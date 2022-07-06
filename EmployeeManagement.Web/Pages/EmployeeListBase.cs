using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeListBase:ComponentBase
    {
        public IEnumerable<Employee> Employees { get; set; }

        protected override async Task OnInitializedAsync()
        {
           await Task.Run( LoadEmployees);
           
        }

        private void LoadEmployees()
        {
            System.Threading.Thread.Sleep(1000);
            Employee e1 = new Employee
            {
                EmployeeID = 1,
                FirstName = "Codes",
                LastName = "Dot",
                Email = "Codesdot@gmail.com",
                DateOfBirth = new DateTime(10 / 05 / 2015),
                Gender = Gender.Male,
                DepartmentID =  1,
                PhotoPath = "images/2page.jpg"
            };
            Employee e2 = new Employee 
            {
             EmployeeID=2,
             FirstName="DotNet",
             LastName="Developer",
             Email="dotnet.codesdot@gmail.com",
             DateOfBirth=new DateTime(15/06/2022),
             Gender=Gender.Male,
             DepartmentID=2,
             PhotoPath="images/3.jpg"
            };
            Employee e3 = new Employee
            {
                EmployeeID = 3,
                FirstName = "Programmer",
                LastName = "Girl",
                Email = "pro.codesdot@gmail.com",
                DateOfBirth = new DateTime(19 / 03 / 2002),
                Gender = Gender.Female,
                DepartmentID = 3,
                PhotoPath = "images/img.jpg"
            };
            Employee e4 = new Employee
            {
                EmployeeID = 4,
                FirstName = "Abc",
                LastName = "Xyz",
                Email = "abcxyz@gmail.com",
                DateOfBirth = new DateTime(10 / 08 / 2021),
                Gender = Gender.Other,
                DepartmentID = 4,
                PhotoPath = "images/page1.png"
            };
            Employees = new List<Employee> { e1, e2, e3, e4, };

        }
    }
}
