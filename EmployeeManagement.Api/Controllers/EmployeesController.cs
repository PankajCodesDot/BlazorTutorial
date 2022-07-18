using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class EmployeesController : ControllerBase
        {
            private readonly IEmployeeRepository employeeRepository;

            public EmployeesController(IEmployeeRepository employeeRepository)
            {
                this.employeeRepository = employeeRepository;
            }
            
            [HttpGet("{Search}")]
            public async Task<ActionResult<IEnumerable<Employee>>> Search(string name,Gender? gender)
            {
            try
            {
                var result = await employeeRepository.Search(name, gender);
                if(result.Any())
                {
                    return Ok(result);
                }


                return NotFound();


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       "Error Searched data from the database not found...");

            }

            }

            [HttpGet]
            public async Task<ActionResult> GetEmployees()
            {
                try
                {
                    return Ok(await employeeRepository.GetEmployees());
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error retrieving data from the database");
                }
            }

           [HttpGet("{id:int}")]
            public async Task<ActionResult<Employee>> GetEmployee(int id)
            {
             try
             {
                var result = await employeeRepository.GetEmployee(id);

                if (result == null) return NotFound();

                return result;
             }
             catch (Exception)
             {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
             }
            }
           [HttpPost]
         public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
         {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }

                var emp = await employeeRepository.GetEmployeeByEmail(employee.Email);
                if(emp != null)
                {
                    ModelState.AddModelError("email","This Email is Already in Used......!");
                    return BadRequest(ModelState);
                }

              var createdEmployee= await employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee),new {id=createdEmployee.EmployeeID },createdEmployee);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error retrieving data from the database");
            }
         }
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee)
        {
            try
            {
               
                var employeeToUpdate= await employeeRepository.GetEmployee(employee.EmployeeID);

                if(employeeToUpdate==null)
                {
                    return NotFound($"Employee ID {employee.EmployeeID} Not Found..");
                }

                return await employeeRepository.UpdateEmployee(employee);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Error Updating data from the database");

            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await employeeRepository.GetEmployee(id);
                if(employeeToDelete==null)
                {
                    return NotFound($"Employee ID {id} Not Found..");
                }

                return await employeeRepository.DeleteEmployee(id);


            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error Deleting data from the database");

            }
        }

        }
    
}
