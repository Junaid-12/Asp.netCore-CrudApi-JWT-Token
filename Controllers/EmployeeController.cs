using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplicationApi.Respositry;

namespace WebApplicationApi.Controllers
{
    [Route ("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
          _employeeRepository= employeeRepository;
        }
       [HttpGet]
        [Route("GetEmpolyees")]
        public async Task<ActionResult> GetEmpolyees()
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error in Data Base from Resiving data");
            }
        }
        [HttpGet]
        [Route("GetEmpolyee")]
        public async Task<ActionResult<Employee>> GetEmpolyee(int id)
        {
            try
            {

                var result = await _employeeRepository.GetEmployee(id);
                if ( result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Id not Found ");
            }
        }
        [HttpPost]
        [Route("PostEmpolyee")]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }

                var CreatedEmployee = await _employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmpolyee), new { id = CreatedEmployee.Id }, CreatedEmployee);
                
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Id not Found ");
            }
       

        }
        [HttpPut/*("{id:int}")*/]
        [Route("UpdateEmployee/{id?}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id,Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("Id miss match");
                }

                var employeeupdate = await _employeeRepository.GetEmployee(id);
                if(employeeupdate == null)
                {
                    return NotFound($"EMployee id {id} is not found");
                   

                }
                return await _employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Id not Found ");
            }


        }
        [HttpDelete]
        [Route("DeletEmpolyee/{id?}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
               

                var employeeDelete = await _employeeRepository.GetEmployee(id);
                if (employeeDelete == null)
                {
                    return NotFound($"EMployee id {id} is not found");


                }
                return await _employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Id not Found ");
            }


        }


    }
}
