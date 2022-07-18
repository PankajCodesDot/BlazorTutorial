using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EditEmployeeBase:ComponentBase
    {
        public string PageHeaderText { get; set; }
        private Employee Employee { get; set; } = new Employee();

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();

        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();

        public string DepartmentId { get; set; }


        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }
        protected async override Task OnInitializedAsync()
        {

            int.TryParse(Id, out int employeeId);

            if(employeeId !=0)
            {
                PageHeaderText = "Edit Employee";
            Employee = await EmployeeService.GetEmployee(int.Parse(Id));

            }
            else
            {
                PageHeaderText = "Create Employee";
                Employee = new Employee {
                    DepartmentID = 1,
                    DateOfBirth = DateTime.Now,
                PhotoPath="images/img.jpg"
                };
            }

            Departments = (await DepartmentService.GetDepartments()).ToList();
            DepartmentId = Employee.DepartmentID.ToString();
            Mapper.Map(Employee, EditEmployeeModel);
            //EditEmployeeModel.EmployeeID = Employee.EmployeeID;
            //EditEmployeeModel.FirstName = Employee.FirstName;
            //EditEmployeeModel.LastName = Employee.LastName;
            //EditEmployeeModel.Email = Employee.Email;
            //EditEmployeeModel.ConfirmEmail = Employee.Email;
            //EditEmployeeModel.DateOfBirth = Employee.DateOfBirth;
            //EditEmployeeModel.Gender = Employee.Gender;
            //EditEmployeeModel.PhotoPath = Employee.PhotoPath;
            //EditEmployeeModel.DepartmentID = Employee.DepartmentID;
            //EditEmployeeModel.Department = Employee.Department;
        }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditEmployeeModel, Employee);

            Employee result = null;
            if(Employee.EmployeeID!=0)
            {
            result = await EmployeeService.UpdateEmployee(Employee);
            }
            else
            {
            result = await EmployeeService.CreateEmployee(Employee);
            }

            if(result!=null)
            {
                NavigationManager.NavigateTo("/");
            }
        }

        protected async Task Delete_Click()
        {
           await EmployeeService.DeleteEmployee(Employee.EmployeeID);
           NavigationManager.NavigateTo("/");
        }

    }
}
