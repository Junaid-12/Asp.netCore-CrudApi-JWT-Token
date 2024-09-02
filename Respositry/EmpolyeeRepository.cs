using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationApi.DataAccess;

namespace WebApplicationApi.Respositry
{
    public class EmpolyeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _Context;
        public EmpolyeeRepository(ApplicationDbContext Context)
        {
            _Context = Context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _Context.employees.AddAsync(employee);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            var result = await _Context.employees.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (result != null)
            {
                _Context.employees.Remove(result);

                await _Context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<Employee> GetEmployee(int id)
        {
           return await _Context.employees.Where(a=>a.Id == id).FirstOrDefaultAsync();
        }

        public  async Task<IEnumerable<Employee>> GetEmployees()
        {
           return await _Context.employees.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var Result = await _Context.employees.Where(a => a.Id == employee.Id).FirstOrDefaultAsync();
            if(Result != null)
            {
                Result.Id = employee.Id;
                Result.Name = employee.Name;
                Result.City = employee.City;
                await _Context.SaveChangesAsync();
                return Result;
            }
            return null;
        }

        Task<ActionResult<Employee>> IEmployeeRepository.DeleteEmployee(Employee employeeDelete)
        {
            throw new NotImplementedException();
        }
    }
}
