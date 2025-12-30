using Employee_Mgmt_Back.Data;
using Employee_Mgmt_Back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Mgmt_Back.Controllers
{
    [ApiController]
    [Route("api/Employee")]
    public class EmployeesController : ControllerBase
    {
        private readonly EmpDbContext _db;
        public EmployeesController(EmpDbContext db) {
        _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _db.Employees.ToListAsync());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpById(int id)
        {
            var emp = await _db.Employees.FindAsync(id);
            return emp == null ? NotFound() : Ok(emp);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddEmp(Employee emp)
        {
            _db.Employees.Add(emp);
            await _db.SaveChangesAsync();
            return Ok();
        }


        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmp(int id, Employee emp)
        {
            var existing = await _db.Employees.FindAsync(id);
            if (existing == null) { return NotFound(); }

            existing.Fullname = emp.Fullname;
            existing.Email = emp.Email;
            existing.PhoneNo = emp.PhoneNo;
            existing.Department = emp.Department;
            existing.Designation = emp.Designation;
            existing.Salary = emp.Salary;
            existing.DateOfJoining = emp.DateOfJoining;

            await _db.SaveChangesAsync();
            return Ok(existing);
        }


        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmp(int id)
        {
            var emp = await _db.Employees.FindAsync(id);
            if (emp == null) { return NotFound(); };

            _db.Employees.Remove(emp);
            await _db.SaveChangesAsync();
            return NoContent();

        }

    }
}
