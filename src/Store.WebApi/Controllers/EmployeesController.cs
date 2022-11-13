using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.ApplicationCore.Entities;
using Store.Infrastructure.Persistence.Contexts;
using System.Linq;

namespace Store.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly StoreContext _EmpDBContext;

        public EmployeeController(StoreContext EmpDBContext)
        {
            _EmpDBContext = EmpDBContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_EmpDBContext.Employees.ToList());
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_EmpDBContext.Employees.FirstOrDefault(c => c.EmpId == id));
        }

        [HttpGet("GetTest")]
        public IActionResult GetTest(string p)
        {
            // return Ok(_EmpDBContext.Employees.FirstOrDefault(c => c.EmpId == id));
            return Ok(p.ToString());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Employee employee)
        {
            _EmpDBContext.Employees.Add(employee);
            _EmpDBContext.SaveChanges();

            return Ok("Employee created");
        }


        [HttpPut]
        public IActionResult Put([FromBody] Employee employee)
        {
            var emp = _EmpDBContext.Employees.FirstOrDefault(c => c.EmpId == employee.EmpId);

            if (emp == null)
                return BadRequest();

            emp.FirstName = employee.FirstName;
            emp.LastName = employee.LastName;
            emp.Email = employee.Email;
            emp.PhoneNumber = employee.PhoneNumber;

            _EmpDBContext.Employees.Update(emp);
            _EmpDBContext.SaveChanges();

            return Ok("Employee updated");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var emp = _EmpDBContext.Employees.FirstOrDefault(c => c.EmpId == id);

            if (emp == null)
                return BadRequest();

            _EmpDBContext.Employees.Remove(emp);
            _EmpDBContext.SaveChanges();

            return Ok("Employee deleted");
        }
    }
}
