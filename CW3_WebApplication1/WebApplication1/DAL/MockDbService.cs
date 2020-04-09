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
        private const string CONN_STR = "Data Source=db-mssql;Initial Catalog=s16446;Integrated Security=True";
        static MockDbService() 
        {
            _students = new List<Student>();
           

            using (var client = new SqlConnection(CONN_STR))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select [IndexNumber], [FirstName], [LastName], [BirthDate] from dbo.Student;";

                client.Open();

                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToShortDateString();
                    _students.Add(st);
                }
            }
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

        public IEnumerable<Student> GetStudent(string id) 
        {
            List <Student> n = new List<Student>();
            string index_no = id;

            using (var client = new SqlConnection(CONN_STR))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "select [IndexNumber], [FirstName], [LastName], [BirthDate] from dbo.Student WHERE IndexNumber = @index_no";
                com.Parameters.AddWithValue("index_no", index_no);
                client.Open();
                
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToShortDateString();
                    n.Add(st);
                }
            }
            
            //n.Add(_students.Find(x => x.IndexNumber.Equals(id)));
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

        public IEnumerable<Enrollment> GetEnrollments(string id, int semester) {
            List<Enrollment> wpisy = null;
            
            using (var client = new SqlConnection(CONN_STR))
            using (var com = new SqlCommand())
            {
                com.Connection = client;
                com.CommandText = "" +
                "SELECT " +
                  "  st.IndexNumber " +
	              ", st.FirstName " +
	              ", st.LastName " +
                  ", e.Semester " +
                  ", e.StartDate " +
	              ", s.[Name] " +
                "FROM dbo.Student st " +
                "LEFT JOIN dbo.Enrollment e ON st.IdEnrollment = e.IdEnrollment " +
                "LEFT JOIN dbo.Studies s ON e.IdStudy = s.IdStudy " +
                "WHERE IndexNumber = '" + id + "' AND e.Semester = '" + semester + "'";
                Console.WriteLine(com.CommandText);
                client.Open();
                wpisy = new List<Enrollment>();

                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var e = new Enrollment();
                    e.Semester = dr["Semester"].ToString();
                    e.StartDate = DateTime.Parse(dr["StartDate"].ToString()).ToShortDateString();
                    e.StudiesName = dr["Name"].ToString();
                    wpisy.Add(e);
                    Console.WriteLine(e.StudiesName);
                }
            }
            return wpisy;
        }
    }
}
