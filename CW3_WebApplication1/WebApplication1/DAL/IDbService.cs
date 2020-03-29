using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public interface IDbService
    {
        IEnumerable<Student> GetStudents();
        
        void AddStudent(Student student);

        void DeleteStudent(Student student);

        IEnumerable<Student> GetStudent(string id);

        IEnumerable<Enrollment> GetEnrollments(string id, int semester);
    }
}
