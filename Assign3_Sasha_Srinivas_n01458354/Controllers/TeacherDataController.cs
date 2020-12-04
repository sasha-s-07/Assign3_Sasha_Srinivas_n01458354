using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Assign3_Sasha_Srinivas_n01458354.Models;
using MySql.Data.MySqlClient;

namespace Assign3_Sasha_Srinivas_n01458354.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext school = new SchoolDbContext();

        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key","%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;

                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return Teachers;
        }

        /// <summary>
        /// Finds Teachers in the system by Id
        /// </summary>
        /// <example>GET api/TeacherData/FindTeacher</example>
        /// <returns>
        /// returns a Teacher object based on TeacherId
        /// </returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
            }


            return NewTeacher;
        }

        /// <summary>
        /// Deletes an Teacher from the connected MySQL Database if the ID of that Teacher exists. Does NOT maintain relational integrity. Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }
        /// <summary>
        /// Adds an Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the Teacher's table. Non-Deterministic.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Christine",
        ///	"TeacherLname":"Bittle",
        ///	"EmployeeNumber":"T409854",
        /// }
        /// </example>
        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            bool isValid = ValidateTeacherInput(NewTeacher);

            if (isValid)
            {

                //Create an instance of a connection
                MySqlConnection Conn = school.AccessDatabase();

                //Open the connection between the web server and database
                Conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = Conn.CreateCommand();

                //SQL QUERY
                cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber) " +
                    "values (@TeacherFname,@TeacherLname,@EmployeeNumber)";
                cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
                cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
                cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                Conn.Close();
            }
        }

        private bool ValidateTeacherInput(Teacher newTeacher)
        {
            // Check that there is no missing information when a teacher is added
            if (String.IsNullOrEmpty(newTeacher.TeacherLname)
                ||String.IsNullOrEmpty(newTeacher.TeacherFname)
                ||String.IsNullOrEmpty(newTeacher.EmployeeNumber)
                )
            {
                return false;
            }

            return true;
        }
    }
}