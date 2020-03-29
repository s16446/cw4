using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/students")]

    public class StudentsController : ControllerBase
    {
        private IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_dbService.GetStudents());
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            return Ok(_dbService.GetStudent(id));
        }

        [HttpGet("{id}/{semester}")] // student's ID for whom we want to get enrollments
        public IActionResult GetEnrollments(string id, int semester)
        {
            return Ok(_dbService.GetEnrollments(id, semester));
        }

        [HttpPost] // add
        public IActionResult CreateStudent(Student student) 
        {
            _dbService.AddStudent(student);
             return Ok(student);
        }

        [HttpPut("{id}")] // update
        public IActionResult UpdateStudent(string id)
        {
            return Ok("Aktualizacja zakonczona: " + id);
        }

        [HttpDelete("{id}")] // delete
        public IActionResult DeleteStudent(string id)
        {
            _dbService.DeleteStudent(_dbService.GetStudent(id).ElementAt(0));
            return Ok("Usuwanie zakonczone" + id);
        }
    }
}