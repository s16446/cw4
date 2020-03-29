using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1.DAL
{
    public class MockDbService : IDbService
    {
        private static List<Student> _students;

        static MockDbService() 
        {
            _students = new List<Student>();
            //{
            //    new Student{ IdStudent = 1, FirstName = "Anna", LastName = "Nowak"},
            //    new Student{ IdStudent = 2, FirstName = "Robert", LastName = "Kowalski"},
            //    new Student{ IdStudent = 3, FirstName = "Jerzy", LastName = "Szczepaniak"}
            //};
            string conn_str = "Data Source=db-mssql;Initial Catalog=s16446;Integrated Security=True";
            using (var client = new SqlConnection(conn_str))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select [IndexNumber], [FirstName], [LastName] from dbo.Student;";
                client.Open();

                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    //st.IdStudent = Int32.Parse(dr["IdStudent"].ToString());
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    _students.Add(st);
                }

            }
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

        public IEnumerable<Student> GetStudent(int id) {
            List<Student> n = new List<Student>();
            n.Add(_students.Find(x => x.IdStudent == id));
            return n;
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            _students.Remove(student);
        }
    }
}
